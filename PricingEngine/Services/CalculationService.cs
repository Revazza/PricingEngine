using PricingEngine.Models.Requests;
using PricingEngine.Models.Dto;
using PricingEngine.Models;
using PricingEngine.Controllers;

namespace PricingEngine.Services
{

    public interface ICalculationService
    {
        List<Loan> PerformCalculations(CalculateLoanRequest input);


    }


    public class CalculationService : ICalculationService
    {

        private const decimal MAINTENANCE_RATE = 0.02m;
        private const decimal PREPAYMENT_RATE = 0.07m;
        private const CreditRisk CREDIT_RISK_ALLOCATION = CreditRisk.Capital;
        private const decimal CAPITAL_RISK_RATE_WEIGHT = 0.015m;
        private readonly Terms _terms;



        public CalculationService()
        {
            _terms = new Terms()
            {
                MaintenanceRate = MAINTENANCE_RATE,
                CapitalRiskRateWeight = CAPITAL_RISK_RATE_WEIGHT,
                CreditRiskAllocation = CREDIT_RISK_ALLOCATION,
                PrepaymentRate = PREPAYMENT_RATE,
            };
        }

        public List<Loan> PerformCalculations(CalculateLoanRequest input)
        {
            var calculatedInputs = CalculateInputs(input);

            var balance = input.Balance;

            var loans = new List<Loan>();

            for (int i = 2; i <= 13; i++)
            {
                var loan = new Loan();
                loan.BeginningBalance = balance;
                loan.PaymentAmount = CalculatePaymentAmount(input, calculatedInputs, month: i);
                loan.ContractualInterest = CalculateContractualInterest(input, calculatedInputs, loan.PaymentAmount);
                loan.ContractualPrincipal = CalculateContractualPrincipal(loan);
                loan.BalloonPaymentAtMaturity = CalculateBallonPaymentAtMaturity(input, loan, month: i);
                loan.TotalContractualCashflow = CalculateTotalContractualCashFlow(loan.ContractualPrincipal, loan.BalloonPaymentAtMaturity);
                loan.PrepaymentCashflow = CalculatePrePaymentCashFlow(loan);
                loan.TotalPrincipalPaid = CalculateTotalPrinciplePaid(calculatedInputs, loan);
                loan.AnnualizedInterestOnCashFlow = CalculateAnnualizedInterestOnCashFlow(calculatedInputs, loan);
                loan.EndingBalance = loan.BeginningBalance + loan.TotalPrincipalPaid;

                balance = loan.EndingBalance;

                loans.Add(loan);

            }

            return loans;
        }

        private decimal CalculateAnnualizedInterestOnCashFlow(
            CalculatedInputs calcInputs,
            Loan loan)
        {

            return loan.TotalPrincipalPaid * calcInputs.InterestRate;
        }

        private decimal CalculateTotalPrinciplePaid(
            CalculatedInputs calcInputs,
            Loan loan)
        {
            return
                loan.TotalContractualCashflow +
                loan.PrepaymentCashflow +
                (loan.PrepaymentCashflow * calcInputs.CapitalAllocationRate);
        }

        private decimal CalculatePrePaymentCashFlow(Loan loan)
        {
            if ((-1) * loan.TotalContractualCashflow >= loan.BeginningBalance)
            {
                return 0;
            }

            return Math.Max(
                (-1) * (loan.BeginningBalance + loan.TotalContractualCashflow),
                (-1) * _terms.PrepaymentRate * loan.BeginningBalance
                );
        }

        private decimal CalculateTotalContractualCashFlow(
            decimal contractualPrincipal,
            decimal ballonPaymentAtMaturity)
        {
            return contractualPrincipal + ballonPaymentAtMaturity;
        }

        private decimal CalculateBallonPaymentAtMaturity(
            CalculateLoanRequest input,
            Loan loan,
            int month)
        {
            return month >= input.OriginalTermInMonths ? (-1) * loan.ContractualInterest : 0;
        }

        private decimal CalculateContractualPrincipal(Loan loan)
        {
            if ((-1) * (loan.PaymentAmount - loan.ContractualInterest) > loan.BeginningBalance)
            {
                return (-1) * loan.BeginningBalance;
            }

            return Math.Min(0, loan.PaymentAmount - loan.ContractualInterest);
        }

        private decimal CalculateContractualInterest(
            CalculateLoanRequest input,
            CalculatedInputs calcInputs,
            decimal paymentAmount)
        {
            //=IF(OR(B$5="Interest only",B$5="Principal interest"),(K3*B$9)+(K3*B$9*H$2),B$7+B$8+(K3*B$9))
            if (input.PaymentType == PaymentType.PrincipalOnly ||
                input.PaymentType == PaymentType.PrincipalInterest)
            {
                return (paymentAmount * input.InterestSpread) +
                    (paymentAmount * input.InterestSpread * calcInputs.InterestRate);
            }

            return input.CommitmentAmount + input.MonthlyFeeIncome + (paymentAmount * input.InterestSpread);

        }

        private decimal CalculatePaymentAmount(
            CalculateLoanRequest input,
            CalculatedInputs calcInputs,
            int month)
        {
            var result = calcInputs.UsedPayment * month + calcInputs.UsedPayment * _terms.MaintenanceRate;
            if (input.InterestType == InterestType.Variable)
            {
                result += input.CommitmentAmount + input.MonthlyFeeIncome;
            }
            return result;
        }

        private CalculatedInputs CalculateInputs(CalculateLoanRequest input)
        {
            var calculatedInputs = new CalculatedInputs();

            calculatedInputs.InterestRate = CalculateInterestRate(input);
            calculatedInputs.TransactionCostRate = CalculateTransactionCostRate(input);
            calculatedInputs.CapitalAllocationRate = CalculateAllocationRate();
            calculatedInputs.UsedPayment = CalculateUsedPayment(input, calculatedInputs.TransactionCostRate);

            return calculatedInputs;
        }

        private decimal CalculateInterestRate(CalculateLoanRequest input)
        {
            if (
                (input.ProductType == ProductType.Loan
                    || input.ProductType == ProductType.CD)
                && input.InterestType == InterestType.Fixed)
            {
                return input.InterestRate;
            }


            return input.TeaserPeriod == 0 ?
                input.TeaserSpread :
                input.InterestSpread + input.TeaserSpread;
        }

        private decimal CalculateTransactionCostRate(CalculateLoanRequest input)
        {
            return input.AvgMonthlyFeeIncome / (1 - input.DiscountFromStandardFee);
        }

        private decimal CalculateAllocationRate()
        {
            return _terms.CreditRiskAllocation == CreditRisk.Capital ?
                _terms.CapitalRiskRateWeight + _terms.MaintenanceRate :
                _terms.MaintenanceRate;
        }


        private decimal CalculateUsedPayment(
            CalculateLoanRequest input,
            decimal transactionCostRate)
        {
            decimal result;
            if (input.InterestType == InterestType.Fixed)
            {
                result = input.Balance * input.InterestSpread;
            }
            else
            {
                result = input.Balance * input.TeaserSpread;
            }

            result += transactionCostRate;
            return result;
        }

    }
}
