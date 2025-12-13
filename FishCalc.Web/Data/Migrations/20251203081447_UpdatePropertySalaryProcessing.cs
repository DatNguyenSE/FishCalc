using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishCalc.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertySalaryProcessing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryProcesses_ProcessingUnits_ProcessingUnitUnitId",
                table: "SalaryProcesses");

            migrationBuilder.DropIndex(
                name: "IX_SalaryProcesses_ProcessingUnitUnitId",
                table: "SalaryProcesses");

            migrationBuilder.DropColumn(
                name: "FishId",
                table: "SalaryProcesses");

            migrationBuilder.DropColumn(
                name: "ProcessingUnitUnitId",
                table: "SalaryProcesses");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "SalaryProcesses",
                newName: "ProcessingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryProcesses_ProcessingUnitId",
                table: "SalaryProcesses",
                column: "ProcessingUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryProcesses_ProcessingUnits_ProcessingUnitId",
                table: "SalaryProcesses",
                column: "ProcessingUnitId",
                principalTable: "ProcessingUnits",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryProcesses_ProcessingUnits_ProcessingUnitId",
                table: "SalaryProcesses");

            migrationBuilder.DropIndex(
                name: "IX_SalaryProcesses_ProcessingUnitId",
                table: "SalaryProcesses");

            migrationBuilder.RenameColumn(
                name: "ProcessingUnitId",
                table: "SalaryProcesses",
                newName: "UnitId");

            migrationBuilder.AddColumn<int>(
                name: "FishId",
                table: "SalaryProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProcessingUnitUnitId",
                table: "SalaryProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SalaryProcesses_ProcessingUnitUnitId",
                table: "SalaryProcesses",
                column: "ProcessingUnitUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryProcesses_ProcessingUnits_ProcessingUnitUnitId",
                table: "SalaryProcesses",
                column: "ProcessingUnitUnitId",
                principalTable: "ProcessingUnits",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
