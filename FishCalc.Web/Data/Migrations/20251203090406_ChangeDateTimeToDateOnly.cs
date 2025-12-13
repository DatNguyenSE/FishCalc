using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishCalc.Web.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateTimeToDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "SalaryProcesses",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "SalaryProcesses",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
