namespace PricingEngine.Models
{
    public enum CreditRisk
    {
        Capital,
        None
    }

    public class Terms
    {
        public decimal MaintenanceRate { get; set; }
        public decimal PrepaymentRate { get; set; }
        public CreditRisk CreditRiskAllocation { get; set; }
        public decimal CapitalRiskRateWeight { get; set; }
    }
}
