using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishCalc.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddImgFishUrlToFishAndRenameSalaryPaymentToSalaryProcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryPayments");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Receipts",
                newName: "SalaryProcesseId");

            migrationBuilder.AddColumn<string>(
                name: "ImgFishUrl",
                table: "FishPrices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalaryProcesses",
                columns: table => new
                {
                    SalaryProcesseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FishId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    TotalQuantityProcessed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FishTypeId = table.Column<int>(type: "int", nullable: false),
                    ProcessingUnitUnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryProcesses", x => x.SalaryProcesseId);
                    table.ForeignKey(
                        name: "FK_SalaryProcesses_FishTypes_FishTypeId",
                        column: x => x.FishTypeId,
                        principalTable: "FishTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaryProcesses_ProcessingUnits_ProcessingUnitUnitId",
                        column: x => x.ProcessingUnitUnitId,
                        principalTable: "ProcessingUnits",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryProcesses_FishTypeId",
                table: "SalaryProcesses",
                column: "FishTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryProcesses_ProcessingUnitUnitId",
                table: "SalaryProcesses",
                column: "ProcessingUnitUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryProcesses");

            migrationBuilder.DropColumn(
                name: "ImgFishUrl",
                table: "FishPrices");

            migrationBuilder.RenameColumn(
                name: "SalaryProcesseId",
                table: "Receipts",
                newName: "PaymentId");

            migrationBuilder.CreateTable(
                name: "SalaryPayments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FishTypeId = table.Column<int>(type: "int", nullable: false),
                    ProcessingUnitUnitId = table.Column<int>(type: "int", nullable: false),
                    FishId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalQuantityProcessed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryPayments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_SalaryPayments_FishTypes_FishTypeId",
                        column: x => x.FishTypeId,
                        principalTable: "FishTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaryPayments_ProcessingUnits_ProcessingUnitUnitId",
                        column: x => x.ProcessingUnitUnitId,
                        principalTable: "ProcessingUnits",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPayments_FishTypeId",
                table: "SalaryPayments",
                column: "FishTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPayments_ProcessingUnitUnitId",
                table: "SalaryPayments",
                column: "ProcessingUnitUnitId");
        }
    }
}
