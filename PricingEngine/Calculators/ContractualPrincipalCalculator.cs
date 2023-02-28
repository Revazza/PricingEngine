using PricingEngine.Models.Dto;

namespace PricingEngine.Calculators
{
    public class ContractualPrincipalCalculator
    {

        public decimal Calculate(CalculationsDto c)
        {
            var loan = c.Loan;
            if (-(loan!.PaymentAmount - loan.ContractualInterest) > loan.BeginningBalance)
            {
                return -loan.BeginningBalance;
            }

            return Math.Min(0, loan.PaymentAmount - loan.ContractualInterest);
        }

    }
}
