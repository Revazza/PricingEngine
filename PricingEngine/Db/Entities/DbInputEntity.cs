using System.ComponentModel.DataAnnotations;

namespace PricingEngine.Db.Entities
{
    public enum CreditRisk
    {
        Capital,
        None
    }
    public class DbInputEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public double MaintenanceRate { get; set; } = 0.02;
        public double PrepaymentRate { get; set; } = 0.07;
        public CreditRisk CreditRiskAllocation { get; set; } = CreditRisk.Capital;
        public double CapitalRiskRateWeight { get; set; } = 0.015;



    }
}
