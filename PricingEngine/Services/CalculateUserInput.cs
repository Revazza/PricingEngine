using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PricingEngine.Db;
using PricingEngine.Db.Entities;
using PricingEngine.Models.Requests;
using System.Security.AccessControl;

namespace PricingEngine.Services
{

    public interface ICalculateUserInput
    {
        Task CalculateInputsAsync();

        Task SaveChangesAsync();
    }

    public class CalculateUserInput : ICalculateUserInput
    {
        private readonly PricingEngineDbContext _context;

        public CalculateUserInput(PricingEngineDbContext context)
        {
            _context = context;
        }



        private double CalculateInterestRate(UserInputEntity u)
        {
            if (
                (u.ProductType == ProductType.Loan
                    || u.ProductType == ProductType.CD)
                && u.InterestType == InterestType.Fixed)
            {
                return u.InterestRate;
            }


            return (u.TeaserPeriod == 0) ? u.TeaserSpread : u.InterestSpread + u.TeaserSpread;

        }

        private double CalculateTransactionCostRate(UserInputEntity u)
        {
            return u.AvgMonthlyFeeIncome / (1 - u.DiscountFromStandardFee);
        }

        private async Task<double> CalculateAllocationRateAsync()
        {
            var db = await _context.DbInputs.FirstAsync();
            return db.CreditRiskAllocation == CreditRisk.Capital ?
                db.CapitalRiskRateWeight + db.MaintenanceRate :
                db.MaintenanceRate;
        }

        private double CalculateUsedPayment(
            UserInputEntity u,
            double transactionCostRate)
        {
            double result;
            if (u.InterestType == InterestType.Fixed)
            {
                result = u.Balance * u.InterestSpread;
            }
            else
            {
                result = u.Balance * u.TeaserSpread;
            }

            result += transactionCostRate;
            return result;
        }

        public async Task CalculateInputsAsync()
        {
            var userInput = await _context.UserInputs.FirstAsync();

            var calculatedInput = new CalculatedInputEntity()
            {
                InterestRate = CalculateInterestRate(userInput),
                TransactionCostRate = CalculateTransactionCostRate(userInput),
                CapitalAllocationRate = await CalculateAllocationRateAsync(),
            };

            calculatedInput.UsedPayment = CalculateUsedPayment(userInput, calculatedInput.TransactionCostRate);

            await _context.CalculatedUserInputs.AddAsync(calculatedInput);


        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
