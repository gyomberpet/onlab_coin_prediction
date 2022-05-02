using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinPrediction.Migrations
{
    public partial class CoinIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CoinId",
                table: "Coins",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coins_CoinId",
                table: "Coins",
                column: "CoinId",
                unique: true,
                filter: "[CoinId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coins_CoinId",
                table: "Coins");

            migrationBuilder.AlterColumn<string>(
                name: "CoinId",
                table: "Coins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
