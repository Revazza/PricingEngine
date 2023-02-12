using System.ComponentModel.DataAnnotations;

namespace PricingEngine.Db.Entities
{
    public class CalculatedInputEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public double InterestRate { get; set; }
        public double TransactionCostRate { get; set; }
        public double CapitalAllocationRate { get; set; }
        public double UsedPayment { get; set; }


    }
}
