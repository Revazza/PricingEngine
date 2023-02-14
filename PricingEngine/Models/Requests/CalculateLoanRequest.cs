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
        public decimal Balance { get; set; } = 1000.00m;
        public InterestType InterestType { get; set; } = InterestType.Variable;
        public ProductType ProductType { get; set; } = ProductType.Loan;
        public PaymentType PaymentType { get; set; } = PaymentType.PrincipalOnly;
        public int OriginalTermInMonths { get; set; } = 9;
        public decimal CommitmentAmount { get; set; } = 50.00m;
        public decimal MonthlyFeeIncome { get; set; } = 2.00m;
        public decimal InterestSpread { get; set; } = 0.03m;
        public int TeaserPeriod { get; set; } = 3;
        public decimal InterestRate { get; set; } = 0.08m;
        public decimal TeaserSpread { get; set; } = 0.04m;
        public decimal AvgMonthlyFeeIncome { get; set; } = 5.00m;
        public decimal DiscountFromStandardFee { get; set; } = 0.03m;


    }
}
