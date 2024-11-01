using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication3.Migrations
{
    public partial class NewUser3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AntrenmanProgramlarıs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    MusteriID = table.Column<string>(type: "text", nullable: false),
                    EgzersizAdı = table.Column<string>(type: "text", nullable: false),
                    Hedef = table.Column<string>(type: "text", nullable: false),
                    Setsayısı = table.Column<int>(type: "integer", nullable: false),
                    VideoURL = table.Column<string>(type: "text", nullable: false),
                    Baslangic = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProgramSüresi = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntrenmanProgramlarıs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntrenmanProgramlarıs");
        }
    }
}
