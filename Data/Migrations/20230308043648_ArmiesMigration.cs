using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chaos.Data.Migrations
{
    /// <inheritdoc />
    public partial class ArmiesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Armies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecruitRate = table.Column<int>(type: "INTEGER", nullable: false),
                    LastRecruitment = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Coins = table.Column<int>(type: "INTEGER", nullable: false),
                    CoinGenerationRate = table.Column<int>(type: "INTEGER", nullable: false),
                    LastCoinGeneration = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Recruits = table.Column<int>(type: "INTEGER", nullable: false),
                    Attackers = table.Column<int>(type: "INTEGER", nullable: false),
                    Defenders = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserArmies",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ArmyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArmies", x => new { x.UserId, x.ArmyId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Armies");

            migrationBuilder.DropTable(
                name: "UserArmies");
        }
    }
}
