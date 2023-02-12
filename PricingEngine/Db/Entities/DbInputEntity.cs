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
        public double MaintenanceRate { get; set; } = 2.0;
        public double PrepaymentRate { get; set; } = 7.0;
        public CreditRisk CreditRiskAllocation { get; set; } = CreditRisk.Capital;
        public double CapitalRiskRateWeight { get; set; } = 1.5;



    }
}
