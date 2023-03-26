using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chaos.Data.Migrations
{
    /// <inheritdoc />
    public partial class WoopsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AfterActionReports",
                columns: table => new
                {
                    AggressorId = table.Column<string>(type: "TEXT", nullable: false),
                    DefenderId = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    BattleTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AggressorRecruitLosses = table.Column<int>(type: "INTEGER", nullable: false),
                    AggressorAttackerLosses = table.Column<int>(type: "INTEGER", nullable: false),
                    AggressorToolsLostJson = table.Column<string>(type: "TEXT", nullable: true),
                    DefenderCoinLosses = table.Column<int>(type: "INTEGER", nullable: false),
                    DefenderRecruitLosses = table.Column<int>(type: "INTEGER", nullable: false),
                    DefenderDefenderLosses = table.Column<int>(type: "INTEGER", nullable: false),
                    DefenderToolsLostJson = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AfterActionReports", x => new { x.AggressorId, x.DefenderId });
                });

            migrationBuilder.CreateTable(
                name: "SpyReports",
                columns: table => new
                {
                    SapperId = table.Column<string>(type: "TEXT", nullable: false),
                    SentryId = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    SpyTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SapperStrength = table.Column<double>(type: "REAL", nullable: false),
                    SapperToolsLostJson = table.Column<string>(type: "TEXT", nullable: true),
                    SentryMinimumDefence = table.Column<double>(type: "REAL", nullable: false),
                    SentryMaximumDefence = table.Column<double>(type: "REAL", nullable: false),
                    SentryToolsLostJson = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpyReports", x => new { x.SapperId, x.SentryId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AfterActionReports");

            migrationBuilder.DropTable(
                name: "SpyReports");
        }
    }
}
