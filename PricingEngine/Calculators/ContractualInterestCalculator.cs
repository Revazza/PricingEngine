using PricingEngine.Models;
using PricingEngine.Models.Dto;
using PricingEngine.Models.Requests;

namespace PricingEngine.Calculators
{
    public class ContractualInterestCalculator
    {
        public decimal Calculate(CalculationsDto c)
        {

            if (c.Input!.PaymentType == PaymentType.InterestOnly ||
                c.Input.PaymentType == PaymentType.PrincipalInterest)
            {
                return (c.Loan!.PaymentAmount * c.Input.InterestSpread) +
                    (c.Loan!.PaymentAmount * c.Input.InterestSpread * c.CalculatedInputs!.InterestRate);
            }

            return c.Input!.CommitmentAmount + c.Input!.MonthlyFeeIncome
                + (c.Loan!.PaymentAmount * c.Input!.InterestSpread);

        }

    }
}
