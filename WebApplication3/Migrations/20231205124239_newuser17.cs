using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication3.Migrations
{
    public partial class newuser17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanismanBilgileris");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanismanBilgileris",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<string>(type: "text", nullable: false),
                    Deneyim = table.Column<string>(type: "text", nullable: false),
                    Egitim = table.Column<string>(type: "text", nullable: false),
                    UzmanlıkAlanı = table.Column<string>(type: "text", nullable: false),
                    Yas = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanismanBilgileris", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DanismanBilgileris_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DanismanBilgileris_UserID",
                table: "DanismanBilgileris",
                column: "UserID");
        }
    }
}
