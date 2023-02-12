using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PricingEngine.Migrations
{
    /// <inheritdoc />
    public partial class CalculatedInputEntityAdded1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DbInputs",
                keyColumn: "Id",
                keyValue: new Guid("0c8bc887-4533-408d-8ce7-a1ad2de5b0a1"));

            migrationBuilder.DeleteData(
                table: "UserInputs",
                keyColumn: "Id",
                keyValue: new Guid("4b336103-f7f4-4a50-9dd2-8b29915ab375"));

            migrationBuilder.CreateTable(
                name: "CalculatedUserInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InterestRate = table.Column<double>(type: "float", nullable: false),
                    TransactionCostRate = table.Column<double>(type: "float", nullable: false),
                    CapitalAllocationRate = table.Column<double>(type: "float", nullable: false),
                    UsedPayment = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatedUserInputs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DbInputs",
                columns: new[] { "Id", "CapitalRiskRateWeight", "CreditRiskAllocation", "MaintenanceRate", "PrepaymentRate" },
                values: new object[] { new Guid("3ab7668e-9dce-4aa8-a7be-41776e327a4d"), 0.014999999999999999, 0, 0.02, 0.070000000000000007 });

            migrationBuilder.InsertData(
                table: "UserInputs",
                columns: new[] { "Id", "AvgMonthlyFeeIncome", "Balance", "CommitmentAmount", "DiscountFromStandardFee", "InterestRate", "InterestSpread", "InterestType", "MonthlyFeeIncome", "OriginalTermInMonths", "PaymentType", "ProductType", "TeaserPeriod", "TeaserSpread" },
                values: new object[] { new Guid("24d334ba-6b6a-4933-88cf-95392872438d"), 5.0, 1000.0, 50.0, 0.029999999999999999, 0.080000000000000002, 0.029999999999999999, 0, 2.0, 9, 0, 0, 3, 0.040000000000000001 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculatedUserInputs");

            migrationBuilder.DeleteData(
                table: "DbInputs",
                keyColumn: "Id",
                keyValue: new Guid("3ab7668e-9dce-4aa8-a7be-41776e327a4d"));

            migrationBuilder.DeleteData(
                table: "UserInputs",
                keyColumn: "Id",
                keyValue: new Guid("24d334ba-6b6a-4933-88cf-95392872438d"));

            migrationBuilder.InsertData(
                table: "DbInputs",
                columns: new[] { "Id", "CapitalRiskRateWeight", "CreditRiskAllocation", "MaintenanceRate", "PrepaymentRate" },
                values: new object[] { new Guid("0c8bc887-4533-408d-8ce7-a1ad2de5b0a1"), 0.014999999999999999, 0, 0.02, 0.070000000000000007 });

            migrationBuilder.InsertData(
                table: "UserInputs",
                columns: new[] { "Id", "AvgMonthlyFeeIncome", "Balance", "CommitmentAmount", "DiscountFromStandardFee", "InterestRate", "InterestSpread", "InterestType", "MonthlyFeeIncome", "OriginalTermInMonths", "PaymentType", "ProductType", "TeaserPeriod", "TeaserSpread" },
                values: new object[] { new Guid("4b336103-f7f4-4a50-9dd2-8b29915ab375"), 5.0, 1000.0, 50.0, 0.029999999999999999, 0.080000000000000002, 0.029999999999999999, 0, 2.0, 9, 0, 0, 3, 0.040000000000000001 });
        }
    }
}
