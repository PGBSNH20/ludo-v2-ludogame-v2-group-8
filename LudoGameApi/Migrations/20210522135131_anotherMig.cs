using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoGameApi.Migrations
{
    public partial class AnotherMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pieces_Player_PlayerId",
                table: "Pieces");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_SessionName_GameSessionId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_GameSessionId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Pieces_PlayerId",
                table: "Pieces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionName",
                table: "SessionName");

            migrationBuilder.DropColumn(
                name: "GameSessionId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Pieces");

            migrationBuilder.RenameTable(
                name: "SessionName",
                newName: "GameSession");

            migrationBuilder.AddColumn<int>(
                name: "GamePieceId",
                table: "Player",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "GameSession",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameSession",
                table: "GameSession",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Player_GamePieceId",
                table: "Player",
                column: "GamePieceId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSession_Player_PlayerId",
                table: "GameSession");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Pieces_GamePieceId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_GamePieceId",
                table: "Player");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameSession",
                table: "GameSession");

            migrationBuilder.DropIndex(
                name: "IX_GameSession_PlayerId",
                table: "GameSession");

            migrationBuilder.DropColumn(
                name: "GamePieceId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "GameSession");

            migrationBuilder.RenameTable(
                name: "GameSession",
                newName: "SessionName");

            migrationBuilder.AddColumn<int>(
                name: "GameSessionId",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Pieces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionName",
                table: "SessionName",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Player_GameSessionId",
                table: "Player",
                column: "GameSessionId");

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_SessionName_GameSessionId",
                table: "Player",
                column: "GameSessionId",
                principalTable: "SessionName",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
