using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    public partial class newuser14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_danışmanBilgileris_AspNetUsers_DanismanId",
                table: "danışmanBilgileris");

            migrationBuilder.RenameColumn(
                name: "DanismanId",
                table: "danışmanBilgileris",
                newName: "DanismanBilgiId");

            migrationBuilder.RenameIndex(
                name: "IX_danışmanBilgileris_DanismanId",
                table: "danışmanBilgileris",
                newName: "IX_danışmanBilgileris_DanismanBilgiId");

            migrationBuilder.AddForeignKey(
                name: "FK_danışmanBilgileris_AspNetUsers_DanismanBilgiId",
                table: "danışmanBilgileris",
                column: "DanismanBilgiId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_danışmanBilgileris_AspNetUsers_DanismanBilgiId",
                table: "danışmanBilgileris");

            migrationBuilder.RenameColumn(
                name: "DanismanBilgiId",
                table: "danışmanBilgileris",
                newName: "DanismanId");

            migrationBuilder.RenameIndex(
                name: "IX_danışmanBilgileris_DanismanBilgiId",
                table: "danışmanBilgileris",
                newName: "IX_danışmanBilgileris_DanismanId");

            migrationBuilder.AddForeignKey(
                name: "FK_danışmanBilgileris_AspNetUsers_DanismanId",
                table: "danışmanBilgileris",
                column: "DanismanId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
