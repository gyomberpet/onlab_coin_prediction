using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinPrediction.Migrations
{
    public partial class PairBTCUSDT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceStories");

            migrationBuilder.CreateTable(
                name: "BinanceHourBTCUSDT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Open = table.Column<double>(type: "float", nullable: false),
                    High = table.Column<double>(type: "float", nullable: false),
                    Low = table.Column<double>(type: "float", nullable: false),
                    Close = table.Column<double>(type: "float", nullable: false),
                    VolumeBTC = table.Column<double>(type: "float", nullable: false),
                    VolumeUSDT = table.Column<double>(type: "float", nullable: false),
                    TradeCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinanceHourBTCUSDT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BinanceMinuteBTCUSDT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Open = table.Column<double>(type: "float", nullable: false),
                    High = table.Column<double>(type: "float", nullable: false),
                    Low = table.Column<double>(type: "float", nullable: false),
                    Close = table.Column<double>(type: "float", nullable: false),
                    VolumeBTC = table.Column<double>(type: "float", nullable: false),
                    VolumeUSDT = table.Column<double>(type: "float", nullable: false),
                    TradeCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinanceMinuteBTCUSDT", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinanceHourBTCUSDT");

            migrationBuilder.DropTable(
                name: "BinanceMinuteBTCUSDT");

            migrationBuilder.CreateTable(
                name: "PriceStories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoinId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceStories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceStories_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceStories_CoinId",
                table: "PriceStories",
                column: "CoinId");
        }
    }
}
