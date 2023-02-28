using PricingEngine.Models.Dto;

namespace PricingEngine.Calculators
{
    public class PrePaymentCashFlowCalculator
    {

        public decimal Calculate(CalculationsDto c)
        {
            var loan = c.Loan;
            if (-loan!.TotalContractualCashflow >= loan.BeginningBalance)
            {
                return 0;
            }

            return Math.Max(
                -(loan.BeginningBalance + loan.TotalContractualCashflow),
                -c.PrepaymentRate * loan.BeginningBalance
                );
        }

    }
}
