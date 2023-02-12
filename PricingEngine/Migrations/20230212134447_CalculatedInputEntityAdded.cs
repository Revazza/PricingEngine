using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PricingEngine.Migrations
{
    /// <inheritdoc />
    public partial class CalculatedInputEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DbInputs",
                keyColumn: "Id",
                keyValue: new Guid("95a971e0-90f2-43d7-8444-6cd2b60d400f"));

            migrationBuilder.CreateTable(
                name: "UserInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    InterestType = table.Column<int>(type: "int", nullable: false),
                    ProductType = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    OriginalTermInMonths = table.Column<int>(type: "int", nullable: false),
                    CommitmentAmount = table.Column<double>(type: "float", nullable: false),
                    MonthlyFeeIncome = table.Column<double>(type: "float", nullable: false),
                    InterestSpread = table.Column<double>(type: "float", nullable: false),
                    TeaserPeriod = table.Column<int>(type: "int", nullable: false),
                    InterestRate = table.Column<double>(type: "float", nullable: false),
                    TeaserSpread = table.Column<double>(type: "float", nullable: false),
                    AvgMonthlyFeeIncome = table.Column<double>(type: "float", nullable: false),
                    DiscountFromStandardFee = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInputs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DbInputs",
                columns: new[] { "Id", "CapitalRiskRateWeight", "CreditRiskAllocation", "MaintenanceRate", "PrepaymentRate" },
                values: new object[] { new Guid("0c8bc887-4533-408d-8ce7-a1ad2de5b0a1"), 0.014999999999999999, 0, 0.02, 0.070000000000000007 });

            migrationBuilder.InsertData(
                table: "UserInputs",
                columns: new[] { "Id", "AvgMonthlyFeeIncome", "Balance", "CommitmentAmount", "DiscountFromStandardFee", "InterestRate", "InterestSpread", "InterestType", "MonthlyFeeIncome", "OriginalTermInMonths", "PaymentType", "ProductType", "TeaserPeriod", "TeaserSpread" },
                values: new object[] { new Guid("4b336103-f7f4-4a50-9dd2-8b29915ab375"), 5.0, 1000.0, 50.0, 0.029999999999999999, 0.080000000000000002, 0.029999999999999999, 0, 2.0, 9, 0, 0, 3, 0.040000000000000001 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInputs");

            migrationBuilder.DeleteData(
                table: "DbInputs",
                keyColumn: "Id",
                keyValue: new Guid("0c8bc887-4533-408d-8ce7-a1ad2de5b0a1"));

            migrationBuilder.InsertData(
                table: "DbInputs",
                columns: new[] { "Id", "CapitalRiskRateWeight", "CreditRiskAllocation", "MaintenanceRate", "PrepaymentRate" },
                values: new object[] { new Guid("95a971e0-90f2-43d7-8444-6cd2b60d400f"), 1.5, 0, 2.0, 7.0 });
        }
    }
}
