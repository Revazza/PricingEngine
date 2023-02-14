namespace PricingEngine.Models.Requests
{
    public enum InterestType
    {
        Variable,
        Fixed,
        None
    }

    public enum ProductType
    {
        Loan,
        CD,
        None
    }

    public enum PaymentType
    {
        PrincipalOnly,
        InterestOnly,
        PrincipalInterest,
        None
    }

    public class CalculateLoanRequest
    {
        public decimal Balance { get; set; }
        public InterestType InterestType { get; set; }
        public ProductType ProductType { get; set; }
        public PaymentType PaymentType { get; set; }
        public int OriginalTermInMonths { get; set; }
        public decimal CommitmentAmount { get; set; }
        public decimal MonthlyFeeIncome { get; set; }
        public decimal InterestSpread { get; set; }
        public int TeaserPeriod { get; set; }
        public decimal InterestRate { get; set; }
        public decimal TeaserSpread { get; set; }
        public decimal AvgMonthlyFeeIncome { get; set; }
        public decimal DiscountFromStandardFee { get; set; }


    }
}
