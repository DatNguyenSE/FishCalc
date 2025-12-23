using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishCalc.Web.Migrations
{
    /// <inheritdoc />
    public partial class Update1toManyRelationshipInFishType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FishPrices_FishTypeId",
                table: "FishPrices");

            migrationBuilder.CreateIndex(
                name: "IX_FishPrices_FishTypeId",
                table: "FishPrices",
                column: "FishTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FishPrices_FishTypeId",
                table: "FishPrices");

            migrationBuilder.CreateIndex(
                name: "IX_FishPrices_FishTypeId",
                table: "FishPrices",
                column: "FishTypeId",
                unique: true);
        }
    }
}
