using PricingEngine.Models.Requests;

namespace PricingEngine.Models.Calculators
{
    public class BallonPaymentAtMaturityCalculator
    {
        public decimal Calculate(
           CalculateLoanRequest input,
           decimal beeginingBalance,
           int month)
        {
            return month >= input.OriginalTermInMonths ? -beeginingBalance : 0;
        }
    }
}
