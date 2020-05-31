using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeedTest.Server.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpeedTestCheckIns",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Download = table.Column<double>(nullable: false),
                    Upload = table.Column<double>(nullable: false),
                    ClientIP = table.Column<string>(nullable: true),
                    ServerID = table.Column<int>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    Ping = table.Column<int>(nullable: false),
                    ClientName = table.Column<string>(nullable: true),
                    ServerName = table.Column<string>(maxLength: 40, nullable: true),
                    TestDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeedTestCheckIns", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpeedTestCheckIns");
        }
    }
}
