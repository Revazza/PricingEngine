using PricingEngine.Models.Dto;

namespace PricingEngine.Models.Calculators
{
    public class PrePaymentCashFlowCalculator
    {



        public decimal Calculate(Loan loan, decimal prePaymentCashRate)
        {
            if (-loan.TotalContractualCashflow >= loan.BeginningBalance)
            {
                return 0;
            }

            return Math.Max(
                -(loan.BeginningBalance + loan.TotalContractualCashflow),
                -prePaymentCashRate * loan.BeginningBalance
                );
        }

    }
}
