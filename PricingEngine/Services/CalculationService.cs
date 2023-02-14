using PricingEngine.Models.Requests;
using PricingEngine.Models.Dto;
using PricingEngine.Models;
using PricingEngine.Controllers;
using PricingEngine.Models.Calculators;

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
        private const int MONTHS = 13;

        public List<Loan> PerformCalculations(CalculateLoanRequest input)
        {
            var calculatedInputs = CalculateInputs(input);

            var loans = CalculateLoans(input, calculatedInputs);

            return loans;
        }

        private decimal CalculateTotalContractualCashFlow(
           decimal contractualPrincipal,
           decimal ballonPaymentAtMaturity)
        {
            return contractualPrincipal + ballonPaymentAtMaturity;
        }

        private decimal CalculateAnnualizedInterestOnCashFlow(
            CalculatedInputs calcInputs,
            Loan loan)
        {
            return loan.TotalPrincipalPaid * calcInputs.InterestRate;
        }

        private CalculatedInputs CalculateInputs(CalculateLoanRequest input)
        {
            var terms = new Terms()
            {
                MaintenanceRate = MAINTENANCE_RATE,
                PrepaymentRate = PREPAYMENT_RATE,
                CreditRiskAllocation = CREDIT_RISK_ALLOCATION,
                CapitalRiskRateWeight = CAPITAL_RISK_RATE_WEIGHT
            };

            var calculatedInputs = new InputsCalculator(terms)
                .Calculate(input);

            return calculatedInputs;
        }

        private List<Loan> CalculateLoans(
            CalculateLoanRequest input,
            CalculatedInputs calculatedInputs)
        {
            var balance = input.Balance;

            var loans = new List<Loan>();

            for (int i = 2; i <= MONTHS; i++)
            {
                var loan = new Loan();
                loan.BeginningBalance = balance;
                loan.PaymentAmount = new PaymentAmountCalculator()
                    .Calculate(input, calculatedInputs, MAINTENANCE_RATE, month: i);

                loan.ContractualInterest = new ContractualInterestCalculator()
                    .Calculate(input, calculatedInputs, loan.PaymentAmount);

                loan.ContractualPrincipal = new ContractualPrincipalCalculator()
                    .Calculate(loan);

                loan.BalloonPaymentAtMaturity = new BallonPaymentAtMaturityCalculator()
                    .Calculate(input, loan.BeginningBalance, month: i);

                loan.TotalContractualCashflow = CalculateTotalContractualCashFlow(
                    loan.ContractualPrincipal,
                    loan.BalloonPaymentAtMaturity);

                loan.PrepaymentCashflow = new PrePaymentCashFlowCalculator()
                    .Calculate(loan, PREPAYMENT_RATE);

                loan.TotalPrincipalPaid = new TotalPrinciplePaidCalculator()
                    .Calculate(calculatedInputs, loan);

                loan.AnnualizedInterestOnCashFlow = CalculateAnnualizedInterestOnCashFlow(calculatedInputs, loan);
                loan.EndingBalance = loan.BeginningBalance + loan.TotalPrincipalPaid;

                balance = loan.EndingBalance;

                loans.Add(loan);

            }

            return loans;
        }



    }
}
