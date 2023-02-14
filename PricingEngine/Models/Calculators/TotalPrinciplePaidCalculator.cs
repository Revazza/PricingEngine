using PricingEngine.Models.Dto;

namespace PricingEngine.Models.Calculators
{
    public class TotalPrinciplePaidCalculator
    {

        public decimal Calculate(
            CalculatedInputs calcInputs,
            Loan loan)
        {
            return
                loan.TotalContractualCashflow +
                loan.PrepaymentCashflow +
                (loan.PrepaymentCashflow * calcInputs.CapitalAllocationRate);
        }


    }
}
