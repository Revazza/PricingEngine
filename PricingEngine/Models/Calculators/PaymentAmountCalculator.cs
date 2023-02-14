using PricingEngine.Models.Requests;

namespace PricingEngine.Models.Calculators
{
    public class PaymentAmountCalculator
    {

        public decimal Calculate(
            CalculateLoanRequest input,
            CalculatedInputs calcInputs,
            decimal maintenanceRate,
            int month)
        {
            var result = calcInputs.UsedPayment * month + calcInputs.UsedPayment * maintenanceRate;
            if (input.InterestType == InterestType.Variable)
            {
                result += input.CommitmentAmount + input.MonthlyFeeIncome;
            }
            return result;
        }

    }
}
