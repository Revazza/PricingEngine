using PricingEngine.Models;
using PricingEngine.Models.Dto;
using PricingEngine.Models.Requests;

namespace PricingEngine.Calculators
{
    public class PaymentAmountCalculator
    {
        public decimal Calculate(CalculationsDto c)
        {
            var result = c.CalculatedInputs!
                .UsedPayment * c.Month + c.CalculatedInputs.UsedPayment * c.MaintenanceRate;
            if (c.Input!.InterestType == InterestType.Variable)
            {
                result += c.Input.CommitmentAmount + c.Input.MonthlyFeeIncome;
            }
            return result;
        }

    }
}
