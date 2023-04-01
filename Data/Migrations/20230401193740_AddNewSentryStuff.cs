using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chaos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewSentryStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SapperRecruitLosses",
                table: "SpyReports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SapperSapperLosses",
                table: "SpyReports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SentryRecruitLosses",
                table: "SpyReports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SentrySentryLosses",
                table: "SpyReports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SapperRecruitLosses",
                table: "SpyReports");

            migrationBuilder.DropColumn(
                name: "SapperSapperLosses",
                table: "SpyReports");

            migrationBuilder.DropColumn(
                name: "SentryRecruitLosses",
                table: "SpyReports");

            migrationBuilder.DropColumn(
                name: "SentrySentryLosses",
                table: "SpyReports");
        }
    }
}
