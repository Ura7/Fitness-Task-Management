using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication3.Migrations
{
    public partial class newuser8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "danışmanBilgileris",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<string>(type: "text", nullable: false),
                    DanismanId = table.Column<string>(type: "text", nullable: true),
                    Yas = table.Column<string>(type: "text", nullable: false),
                    UzmanlıkAlanı = table.Column<string>(type: "text", nullable: false),
                    Deneyim = table.Column<string>(type: "text", nullable: false),
                    Egitim = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_danışmanBilgileris", x => x.ID);
                    table.ForeignKey(
                        name: "FK_danışmanBilgileris_AspNetUsers_DanismanId",
                        column: x => x.DanismanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_danışmanBilgileris_DanismanId",
                table: "danışmanBilgileris",
                column: "DanismanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "danışmanBilgileris");
        }
    }
}
