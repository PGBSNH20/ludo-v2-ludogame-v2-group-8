using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoGameApi.Migrations
{
    public partial class AddedGameSessionIdPropInPlayerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_SessionName_GameSessionId",
                table: "Player");

            migrationBuilder.AlterColumn<int>(
                name: "GameSessionId",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "GameSessionId",
                table: "Player",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
