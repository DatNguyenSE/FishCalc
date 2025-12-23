using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishCalc.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyStatusToSalaryProcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SalaryProcesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SalaryProcesses");
        }
    }
}
