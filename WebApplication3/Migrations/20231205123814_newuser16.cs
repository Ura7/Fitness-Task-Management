using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    public partial class newuser16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_danışmanBilgileris_AspNetUsers_UserID",
                table: "danışmanBilgileris");

            migrationBuilder.DropPrimaryKey(
                name: "PK_danışmanBilgileris",
                table: "danışmanBilgileris");

            migrationBuilder.RenameTable(
                name: "danışmanBilgileris",
                newName: "DanismanBilgileris");

            migrationBuilder.RenameIndex(
                name: "IX_danışmanBilgileris_UserID",
                table: "DanismanBilgileris",
                newName: "IX_DanismanBilgileris_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DanismanBilgileris",
                table: "DanismanBilgileris",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_DanismanBilgileris_AspNetUsers_UserID",
                table: "DanismanBilgileris",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DanismanBilgileris_AspNetUsers_UserID",
                table: "DanismanBilgileris");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DanismanBilgileris",
                table: "DanismanBilgileris");

            migrationBuilder.RenameTable(
                name: "DanismanBilgileris",
                newName: "danışmanBilgileris");

            migrationBuilder.RenameIndex(
                name: "IX_DanismanBilgileris_UserID",
                table: "danışmanBilgileris",
                newName: "IX_danışmanBilgileris_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_danışmanBilgileris",
                table: "danışmanBilgileris",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_danışmanBilgileris_AspNetUsers_UserID",
                table: "danışmanBilgileris",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
