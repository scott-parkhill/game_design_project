using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chaos.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixAfterActionReportHopefully : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AfterActionReports",
                table: "AfterActionReports");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AfterActionReports",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "DefenderId",
                table: "AfterActionReports",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "AggressorId",
                table: "AfterActionReports",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AfterActionReports",
                table: "AfterActionReports",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AfterActionReports_AggressorId",
                table: "AfterActionReports",
                column: "AggressorId");

            migrationBuilder.CreateIndex(
                name: "IX_AfterActionReports_DefenderId",
                table: "AfterActionReports",
                column: "DefenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AfterActionReports_AspNetUsers_AggressorId",
                table: "AfterActionReports",
                column: "AggressorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AfterActionReports_AspNetUsers_DefenderId",
                table: "AfterActionReports",
                column: "DefenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AfterActionReports_AspNetUsers_AggressorId",
                table: "AfterActionReports");

            migrationBuilder.DropForeignKey(
                name: "FK_AfterActionReports_AspNetUsers_DefenderId",
                table: "AfterActionReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AfterActionReports",
                table: "AfterActionReports");

            migrationBuilder.DropIndex(
                name: "IX_AfterActionReports_AggressorId",
                table: "AfterActionReports");

            migrationBuilder.DropIndex(
                name: "IX_AfterActionReports_DefenderId",
                table: "AfterActionReports");

            migrationBuilder.AlterColumn<string>(
                name: "DefenderId",
                table: "AfterActionReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AggressorId",
                table: "AfterActionReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AfterActionReports",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AfterActionReports",
                table: "AfterActionReports",
                columns: new[] { "AggressorId", "DefenderId" });
        }
    }
}
