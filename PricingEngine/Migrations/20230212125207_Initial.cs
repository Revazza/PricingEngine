using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PricingEngine.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaintenanceRate = table.Column<double>(type: "float", nullable: false),
                    PrepaymentRate = table.Column<double>(type: "float", nullable: false),
                    CreditRiskAllocation = table.Column<int>(type: "int", nullable: false),
                    CapitalRiskRateWeight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbInputs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DbInputs",
                columns: new[] { "Id", "CapitalRiskRateWeight", "CreditRiskAllocation", "MaintenanceRate", "PrepaymentRate" },
                values: new object[] { new Guid("95a971e0-90f2-43d7-8444-6cd2b60d400f"), 1.5, 0, 2.0, 7.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbInputs");
        }
    }
}
