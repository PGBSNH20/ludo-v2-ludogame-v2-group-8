using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoGameApi.Migrations
{
    public partial class AnotherMig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSession_Player_PlayerId",
                table: "GameSession");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Pieces_GamePieceId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_GameSession_PlayerId",
                table: "GameSession");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "GameSession");

            migrationBuilder.RenameColumn(
                name: "GamePieceId",
                table: "Player",
                newName: "GameSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Player_GamePieceId",
                table: "Player",
                newName: "IX_Player_GameSessionId");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Pieces",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pieces_PlayerId",
                table: "Pieces",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pieces_Player_PlayerId",
                table: "Pieces",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_GameSession_GameSessionId",
                table: "Player",
                column: "GameSessionId",
                principalTable: "GameSession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pieces_Player_PlayerId",
                table: "Pieces");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_GameSession_GameSessionId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Pieces_PlayerId",
                table: "Pieces");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Pieces");

            migrationBuilder.RenameColumn(
                name: "GameSessionId",
                table: "Player",
                newName: "GamePieceId");

            migrationBuilder.RenameIndex(
                name: "IX_Player_GameSessionId",
                table: "Player",
                newName: "IX_Player_GamePieceId");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "GameSession",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameSession_PlayerId",
                table: "GameSession",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameSession_Player_PlayerId",
                table: "GameSession",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Pieces_GamePieceId",
                table: "Player",
                column: "GamePieceId",
                principalTable: "Pieces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
