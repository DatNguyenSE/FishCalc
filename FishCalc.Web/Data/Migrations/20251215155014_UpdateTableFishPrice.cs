using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishCalc.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableFishPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfMeasure",
                table: "FishTypes");

            migrationBuilder.RenameColumn(
                name: "PricePerKg",
                table: "SalaryProcesses",
                newName: "PricePerUnit");

            migrationBuilder.RenameColumn(
                name: "PricePerUnitOfMeasure",
                table: "FishPrices",
                newName: "PricePerUnit");

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                table: "FishPrices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasure",
                table: "FishPrices",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "FishPrices");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasure",
                table: "FishPrices");

            migrationBuilder.RenameColumn(
                name: "PricePerUnit",
                table: "SalaryProcesses",
                newName: "PricePerKg");

            migrationBuilder.RenameColumn(
                name: "PricePerUnit",
                table: "FishPrices",
                newName: "PricePerUnitOfMeasure");

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasure",
                table: "FishTypes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
