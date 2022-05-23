using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebHookQueue.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebHooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Json = table.Column<string>(type:"TEXT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebHooks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebHooks");
        }
    }
}
