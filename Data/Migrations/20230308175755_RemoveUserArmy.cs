using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chaos.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserArmy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserArmies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Armies",
                table: "Armies");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Armies",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Armies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Armies",
                table: "Armies",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Armies",
                table: "Armies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Armies");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Armies",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Armies",
                table: "Armies",
                column: "Id");

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
    }
}
