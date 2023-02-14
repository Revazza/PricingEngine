using PricingEngine.Models.Dto;

namespace PricingEngine.Models.Calculators
{
    public class ContractualPrincipalCalculator
    {

        public decimal Calculate(Loan loan)
        {
            if (-(loan.PaymentAmount - loan.ContractualInterest) > loan.BeginningBalance)
            {
                return -loan.BeginningBalance;
            }

            return Math.Min(0, loan.PaymentAmount - loan.ContractualInterest);
        }

    }
}
