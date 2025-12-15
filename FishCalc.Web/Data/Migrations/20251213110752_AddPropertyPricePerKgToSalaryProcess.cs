using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishCalc.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyPricePerKgToSalaryProcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PricePerKg",
                table: "SalaryProcesses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerKg",
                table: "SalaryProcesses");
        }
    }
}
