using PricingEngine.Models;
using PricingEngine.Models.Requests;

namespace PricingEngine.Calculators
{
    public class InputsCalculator
    {
        private readonly Terms _terms;

        public InputsCalculator(Terms terms)
        {
            _terms = terms;
        }

        public CalculatedInputs Calculate(
            CalculateLoanRequest input)
        {
            var calculatedInputs = new CalculatedInputs();

            calculatedInputs.InterestRate = CalculateInterestRate(input);
            calculatedInputs.TransactionCostRate = CalculateTransactionCostRate(input);
            calculatedInputs.CapitalAllocationRate = CalculateAllocationRate();
            calculatedInputs.UsedPayment = CalculateUsedPayment(input, calculatedInputs.TransactionCostRate);

            return calculatedInputs;
        }


        private decimal CalculateInterestRate(CalculateLoanRequest input)
        {
            if (
                (input.ProductType == ProductType.Loan
                    || input.ProductType == ProductType.CD)
                && input.InterestType == InterestType.Fixed)
            {
                return input.InterestRate;
            }

            if (input.TeaserPeriod == 0)
            {
                return input.TeaserSpread;
            }


            return input.InterestSpread + input.TeaserSpread;
        }

        private decimal CalculateTransactionCostRate(CalculateLoanRequest input)
        {
            return input.AvgMonthlyFeeIncome / (1 - input.DiscountFromStandardFee);
        }

        private decimal CalculateAllocationRate()
        {
            return _terms.CreditRiskAllocation == CreditRisk.Capital ?
                _terms.CapitalRiskRateWeight + _terms.MaintenanceRate :
                _terms.MaintenanceRate;
        }


        private decimal CalculateUsedPayment(
            CalculateLoanRequest input,
            decimal transactionCostRate)
        {
            decimal result;
            if (input.InterestType == InterestType.Fixed)
            {
                result = input.Balance * input.InterestSpread;
            }
            else
            {
                result = input.Balance * input.TeaserSpread;
            }

            result += transactionCostRate;
            return result;
        }

    }
}
