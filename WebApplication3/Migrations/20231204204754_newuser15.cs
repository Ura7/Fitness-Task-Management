using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    public partial class newuser15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_danışmanBilgileris_AspNetUsers_DanismanBilgiId",
                table: "danışmanBilgileris");

            migrationBuilder.DropIndex(
                name: "IX_danışmanBilgileris_DanismanBilgiId",
                table: "danışmanBilgileris");

            migrationBuilder.DropColumn(
                name: "DanismanBilgiId",
                table: "danışmanBilgileris");

            migrationBuilder.CreateIndex(
                name: "IX_danışmanBilgileris_UserID",
                table: "danışmanBilgileris",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_danışmanBilgileris_AspNetUsers_UserID",
                table: "danışmanBilgileris",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_danışmanBilgileris_AspNetUsers_UserID",
                table: "danışmanBilgileris");

            migrationBuilder.DropIndex(
                name: "IX_danışmanBilgileris_UserID",
                table: "danışmanBilgileris");

            migrationBuilder.AddColumn<string>(
                name: "DanismanBilgiId",
                table: "danışmanBilgileris",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_danışmanBilgileris_DanismanBilgiId",
                table: "danışmanBilgileris",
                column: "DanismanBilgiId");

            migrationBuilder.AddForeignKey(
                name: "FK_danışmanBilgileris_AspNetUsers_DanismanBilgiId",
                table: "danışmanBilgileris",
                column: "DanismanBilgiId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
