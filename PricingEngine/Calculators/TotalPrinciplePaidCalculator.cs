using PricingEngine.Models;
using PricingEngine.Models.Dto;

namespace PricingEngine.Calculators
{
    public class TotalPrinciplePaidCalculator
    {

        public decimal Calculate(CalculationsDto c)
        {
            var loan = c.Loan!;
            return
                loan.TotalContractualCashflow +
                loan.PrepaymentCashflow +
                loan.PrepaymentCashflow * c.CalculatedInputs!.CapitalAllocationRate;
        }

    }
}
