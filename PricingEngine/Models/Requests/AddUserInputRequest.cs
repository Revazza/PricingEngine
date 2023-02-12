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

    public class AddUserInputRequest
    {
        public decimal Balance { get; set; }
        public InterestType InterestType { get; set; }
        public ProductType ProductType { get; set; }
        public PaymentType PaymentType { get; set; }
        public int OriginalTermInMonths { get; set; }
        public double CommitmentAmount { get; set; }
        public double MonthlyFeeIncome { get; set; }
        public double InterestSpread { get; set; }
        public int TeaserPeriod { get; set; }
        public double InterestRate { get; set; }
        public double TeaserSpread { get; set; }
        public double AvgMonthlyFeeIncome { get; set; }
        public double DiscountFromStandardFee { get; set; }


    }
}
