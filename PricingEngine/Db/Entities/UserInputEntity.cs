using PricingEngine.Models.Requests;
using System.ComponentModel.DataAnnotations;

namespace PricingEngine.Db.Entities
{
    public class UserInputEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();


        public double Balance { get; set; } = 1000;
        public InterestType InterestType { get; set; } = InterestType.Variable;
        public ProductType ProductType { get; set; } = ProductType.Loan;
        public PaymentType PaymentType { get; set; } = PaymentType.PrincipalOnly;
        public int OriginalTermInMonths { get; set; } = 9;
        public double CommitmentAmount { get; set; } = 50.00;
        public double MonthlyFeeIncome { get; set; } = 2.00;
        public double InterestSpread { get; set; } = 0.03;
        public int TeaserPeriod { get; set; } = 3;
        public double InterestRate { get; set; } = 0.08;
        public double TeaserSpread { get; set; } = 0.04;
        public double AvgMonthlyFeeIncome { get; set; } = 5;
        public double DiscountFromStandardFee { get; set; } = 0.03;

    }
}
