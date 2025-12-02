using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishCalc.Web.Migrations
{
    /// <inheritdoc />
    public partial class RenameSalaryProcesseToSalaryProcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalaryProcesseId",
                table: "SalaryProcesses",
                newName: "SalaryProcessId");

            migrationBuilder.RenameColumn(
                name: "SalaryProcesseId",
                table: "Receipts",
                newName: "SalaryProcessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalaryProcessId",
                table: "SalaryProcesses",
                newName: "SalaryProcesseId");

            migrationBuilder.RenameColumn(
                name: "SalaryProcessId",
                table: "Receipts",
                newName: "SalaryProcesseId");
        }
    }
}
