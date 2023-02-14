using PricingEngine.Models.Requests;
using PricingEngine.Models.Dto;
using PricingEngine.Models;
using PricingEngine.Controllers;

namespace PricingEngine.Services
{

    public interface ICalculationService
    {
        void PerformCalculations(CalculateLoanRequest input);


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

        public void PerformCalculations(CalculateLoanRequest input)
        {
            var calculatedInputs = CalculateInputs(input);

            for (int i = 1; i < 13; i++)
            {
                var loan = new Loan();
                loan.BeginningBalance = input.Balance;
                loan.PaymentAmount = CalculatePaymentAmount(input, calculatedInputs);
                loan.ContractualInterest = CalculateContractualInterest(input, calculatedInputs, loan.PaymentAmount);
                loan.ContractualPrincipal = CalculateContractualPrincipal();

            }



        }

        private decimal CalculateContractualPrincipal(Loan loan)
        {
            if ((-1) * (loan.PaymentAmount - loan.ContractualInterest) > S3)
            {
                result = (-1) * S3;
            }
            else
            {
                result = Math.Min(0, K3 - L3);
            }
            return 0;
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
            CalculatedInputs calcInputs)
        {
            var result = calcInputs.UsedPayment * 1 + calcInputs.UsedPayment * _terms.MaintenanceRate;
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
