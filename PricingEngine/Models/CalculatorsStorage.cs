using PricingEngine.Calculators;

namespace PricingEngine.Models
{
    public class CalculatorsStorage
    {
        public PaymentAmountCalculator? PaymentAmountCalculator { get; set; }
        public ContractualInterestCalculator? ContractualInterestCalculator { get; set; }
        public ContractualPrincipalCalculator? ContractualPrincipalCalculator { get; set; }
        public BallonPaymentAtMaturityCalculator? BallonPaymentAtMaturityCalculator { get; set; }
        public PrePaymentCashFlowCalculator? PrePaymentCashFlowCalculator { get; set; }
        public TotalPrinciplePaidCalculator? TotalPrinciplePaidCalculator { get; set; }

        public CalculatorsStorage()
        {
            PaymentAmountCalculator = new PaymentAmountCalculator();
            ContractualInterestCalculator = new ContractualInterestCalculator();
            ContractualPrincipalCalculator = new ContractualPrincipalCalculator();
            BallonPaymentAtMaturityCalculator = new BallonPaymentAtMaturityCalculator();
            PrePaymentCashFlowCalculator = new PrePaymentCashFlowCalculator();
            TotalPrinciplePaidCalculator = new TotalPrinciplePaidCalculator();
        }


    }
}
