using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Closavy.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacter_RaceDeityClassEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Class",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Deity",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Race",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Deity",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Race",
                table: "Character");
        }
    }
}
