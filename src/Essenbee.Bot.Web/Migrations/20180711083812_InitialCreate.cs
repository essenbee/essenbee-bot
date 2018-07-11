using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Essenbee.Bot.Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StartupMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartupMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimedMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Delay = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimedMessages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StartupMessages");

            migrationBuilder.DropTable(
                name: "TimedMessages");
        }
    }
}
