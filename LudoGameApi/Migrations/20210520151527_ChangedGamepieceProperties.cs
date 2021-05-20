using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoGameApi.Migrations
{
    public partial class ChangedGamepieceProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPosition",
                table: "Pieces");

            migrationBuilder.DropColumn(
                name: "InnerPosition",
                table: "Pieces");

            migrationBuilder.DropColumn(
                name: "InnerRoute",
                table: "Pieces");

            migrationBuilder.RenameColumn(
                name: "StartPosition",
                table: "Pieces",
                newName: "TopPosition");

            migrationBuilder.RenameColumn(
                name: "EndPosition",
                table: "Pieces",
                newName: "LeftPosition");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TopPosition",
                table: "Pieces",
                newName: "StartPosition");

            migrationBuilder.RenameColumn(
                name: "LeftPosition",
                table: "Pieces",
                newName: "EndPosition");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPosition",
                table: "Pieces",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InnerPosition",
                table: "Pieces",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InnerRoute",
                table: "Pieces",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
