using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishCalc.Web.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropertyImgUrlComeToFishType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgFishUrl",
                table: "FishPrices");

            migrationBuilder.AddColumn<string>(
                name: "ImgFishUrl",
                table: "FishTypes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgFishUrl",
                table: "FishTypes");

            migrationBuilder.AddColumn<string>(
                name: "ImgFishUrl",
                table: "FishPrices",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
