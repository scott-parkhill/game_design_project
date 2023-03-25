using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chaos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserWeapons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserWeaponsJsonData",
                table: "Armies",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserWeaponsJsonData",
                table: "Armies");
        }
    }
}
