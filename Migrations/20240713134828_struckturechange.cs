using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class struckturechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Userdata_AspNetUsers_UserId",
                table: "Userdata");

            migrationBuilder.DropForeignKey(
                name: "FK_Userdata_Quests_QuestId",
                table: "Userdata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Userdata",
                table: "Userdata");

            migrationBuilder.RenameTable(
                name: "Userdata",
                newName: "Userdatas");

            migrationBuilder.RenameIndex(
                name: "IX_Userdata_UserId",
                table: "Userdatas",
                newName: "IX_Userdatas_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Userdata_QuestId",
                table: "Userdatas",
                newName: "IX_Userdatas_QuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Userdatas",
                table: "Userdatas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Userdatas_AspNetUsers_UserId",
                table: "Userdatas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Userdatas_Quests_QuestId",
                table: "Userdatas",
                column: "QuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Userdatas_AspNetUsers_UserId",
                table: "Userdatas");

            migrationBuilder.DropForeignKey(
                name: "FK_Userdatas_Quests_QuestId",
                table: "Userdatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Userdatas",
                table: "Userdatas");

            migrationBuilder.RenameTable(
                name: "Userdatas",
                newName: "Userdata");

            migrationBuilder.RenameIndex(
                name: "IX_Userdatas_UserId",
                table: "Userdata",
                newName: "IX_Userdata_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Userdatas_QuestId",
                table: "Userdata",
                newName: "IX_Userdata_QuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Userdata",
                table: "Userdata",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Userdata_AspNetUsers_UserId",
                table: "Userdata",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Userdata_Quests_QuestId",
                table: "Userdata",
                column: "QuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
