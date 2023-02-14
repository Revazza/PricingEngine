using PricingEngine.Models.Requests;

namespace PricingEngine.Models.Calculators
{
    public class ContractualInterestCalculator
    {

        public decimal Calculate(
            CalculateLoanRequest input,
            CalculatedInputs calcInputs,
            decimal paymentAmount)
        {

            if (input.PaymentType == PaymentType.InterestOnly ||
                input.PaymentType == PaymentType.PrincipalInterest)
            {
                var a = (paymentAmount * input.InterestSpread) +
                    (paymentAmount * input.InterestSpread * calcInputs.InterestRate);
                return (paymentAmount * input.InterestSpread) +
                    (paymentAmount * input.InterestSpread * calcInputs.InterestRate);
            }

            return input.CommitmentAmount + input.MonthlyFeeIncome + (paymentAmount * input.InterestSpread);

        }

    }
}
