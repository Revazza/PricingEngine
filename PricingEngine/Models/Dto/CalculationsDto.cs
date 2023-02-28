using PricingEngine.Models.Requests;

namespace PricingEngine.Models.Dto
{
    public class CalculationsDto
    {
        public CalculateLoanRequest? Input { get; set; }
        public CalculatedInputs? CalculatedInputs { get; set; }
        public Loan? Loan { get; set; }
        public int Month { get; set; }
        public decimal MaintenanceRate { get; set; }
        public decimal PrepaymentRate { get; set; }


    }
}
