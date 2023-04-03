using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chaos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeysToSpyReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpyReports",
                table: "SpyReports");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SpyReports",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "SentryId",
                table: "SpyReports",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "SapperId",
                table: "SpyReports",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpyReports",
                table: "SpyReports",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SpyReports_SapperId",
                table: "SpyReports",
                column: "SapperId");

            migrationBuilder.CreateIndex(
                name: "IX_SpyReports_SentryId",
                table: "SpyReports",
                column: "SentryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpyReports_AspNetUsers_SapperId",
                table: "SpyReports",
                column: "SapperId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpyReports_AspNetUsers_SentryId",
                table: "SpyReports",
                column: "SentryId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpyReports_AspNetUsers_SapperId",
                table: "SpyReports");

            migrationBuilder.DropForeignKey(
                name: "FK_SpyReports_AspNetUsers_SentryId",
                table: "SpyReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpyReports",
                table: "SpyReports");

            migrationBuilder.DropIndex(
                name: "IX_SpyReports_SapperId",
                table: "SpyReports");

            migrationBuilder.DropIndex(
                name: "IX_SpyReports_SentryId",
                table: "SpyReports");

            migrationBuilder.AlterColumn<string>(
                name: "SentryId",
                table: "SpyReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SapperId",
                table: "SpyReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SpyReports",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpyReports",
                table: "SpyReports",
                columns: new[] { "SapperId", "SentryId" });
        }
    }
}
