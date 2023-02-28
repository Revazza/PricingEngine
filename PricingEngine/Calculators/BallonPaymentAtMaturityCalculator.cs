using PricingEngine.Models.Dto;
using PricingEngine.Models.Requests;

namespace PricingEngine.Calculators
{
    public class BallonPaymentAtMaturityCalculator
    {

        public decimal Calculate(CalculationsDto c)
        {
            return c.Month >= c.Input!.OriginalTermInMonths ? -c.Loan!.BeginningBalance : 0;
        }

    }
}
