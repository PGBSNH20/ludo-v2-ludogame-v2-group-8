using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoGameApi.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_SessionName_GameSessionId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "CurrentPosition",
                table: "Pieces");

            migrationBuilder.DropColumn(
                name: "EndPosition",
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
                newName: "PositionOnBoard");

            migrationBuilder.AlterColumn<int>(
                name: "GameSessionId",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LeftPosition",
                table: "Pieces",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TopPosition",
                table: "Pieces",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_SessionName_GameSessionId",
                table: "Player",
                column: "GameSessionId",
                principalTable: "SessionName",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_SessionName_GameSessionId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "LeftPosition",
                table: "Pieces");

            migrationBuilder.DropColumn(
                name: "TopPosition",
                table: "Pieces");

            migrationBuilder.RenameColumn(
                name: "PositionOnBoard",
                table: "Pieces",
                newName: "StartPosition");

            migrationBuilder.AlterColumn<int>(
                name: "GameSessionId",
                table: "Player",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPosition",
                table: "Pieces",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndPosition",
                table: "Pieces",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Player_SessionName_GameSessionId",
                table: "Player",
                column: "GameSessionId",
                principalTable: "SessionName",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
