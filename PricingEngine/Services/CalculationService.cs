using PricingEngine.Models.Requests;
using PricingEngine.Models.Dto;
using PricingEngine.Models;
using PricingEngine.Calculators;
using System.Runtime;

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

        public Loan CalculateLoan(
            CalculationsDto calculations,
            CalculatorsStorage calculators)
        {
            var loan = calculations.Loan!;

            loan.PaymentAmount = calculators.PaymentAmountCalculator!
                .Calculate(calculations);

            loan.ContractualInterest = calculators.ContractualInterestCalculator!
                .Calculate(calculations);

            loan.ContractualPrincipal = calculators.ContractualPrincipalCalculator!
                .Calculate(calculations);

            loan.BalloonPaymentAtMaturity = calculators.BallonPaymentAtMaturityCalculator!
                .Calculate(calculations);

            loan.TotalContractualCashflow = CalculateTotalContractualCashFlow(
                loan.ContractualPrincipal,
                loan.BalloonPaymentAtMaturity);

            loan.PrepaymentCashflow = calculators.PrePaymentCashFlowCalculator!
                .Calculate(calculations);

            loan.TotalPrincipalPaid = calculators.TotalPrinciplePaidCalculator!
                .Calculate(calculations);

            loan
                .AnnualizedInterestOnCashFlow = 
                    CalculateAnnualizedInterestOnCashFlow(calculations.CalculatedInputs!, loan);
            loan.EndingBalance = loan.BeginningBalance + loan.TotalPrincipalPaid;
            return loan;
        }


        private List<Loan> CalculateLoans(
            CalculateLoanRequest input,
            CalculatedInputs calculatedInputs)
        {
            var balance = input.Balance;

            var loans = new List<Loan>();

            var calculators = new CalculatorsStorage();

            var calculations = new CalculationsDto()
            {
                CalculatedInputs = calculatedInputs,
                Input = input,
                MaintenanceRate = MAINTENANCE_RATE,
                PrepaymentRate = PREPAYMENT_RATE
            };

            for (int i = 1; i <= MONTHS; i++)
            {
                calculations.Loan = new Loan();
                if (i == 1)
                {
                    calculations.Loan.BeginningBalance = balance;
                    loans.Add(calculations.Loan);
                    continue;
                }

                calculations.Month = i;
                calculations.Loan.BeginningBalance = balance;

                var loan = CalculateLoan(calculations, calculators);

                balance = loan.EndingBalance;

                loans.Add(loan);

            }

            return loans;
        }



    }
}
