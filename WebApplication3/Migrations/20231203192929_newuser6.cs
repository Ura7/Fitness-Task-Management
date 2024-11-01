using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    public partial class newuser6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DanismanMusteriAtamas_DanismanID",
                table: "DanismanMusteriAtamas",
                column: "DanismanID");

            migrationBuilder.CreateIndex(
                name: "IX_DanismanMusteriAtamas_MusteriID",
                table: "DanismanMusteriAtamas",
                column: "MusteriID");

            migrationBuilder.AddForeignKey(
                name: "FK_DanismanMusteriAtamas_AspNetUsers_DanismanID",
                table: "DanismanMusteriAtamas",
                column: "DanismanID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DanismanMusteriAtamas_AspNetUsers_MusteriID",
                table: "DanismanMusteriAtamas",
                column: "MusteriID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DanismanMusteriAtamas_AspNetUsers_DanismanID",
                table: "DanismanMusteriAtamas");

            migrationBuilder.DropForeignKey(
                name: "FK_DanismanMusteriAtamas_AspNetUsers_MusteriID",
                table: "DanismanMusteriAtamas");

            migrationBuilder.DropIndex(
                name: "IX_DanismanMusteriAtamas_DanismanID",
                table: "DanismanMusteriAtamas");

            migrationBuilder.DropIndex(
                name: "IX_DanismanMusteriAtamas_MusteriID",
                table: "DanismanMusteriAtamas");
        }
    }
}
