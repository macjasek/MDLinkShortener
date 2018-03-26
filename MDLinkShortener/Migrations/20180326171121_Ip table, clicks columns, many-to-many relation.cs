using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MDLinkShortener.Migrations
{
    public partial class Iptableclickscolumnsmanytomanyrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Clicks",
                table: "Links",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UniqueClicks",
                table: "Links",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "IpAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientIp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IpLink",
                columns: table => new
                {
                    LinkId = table.Column<int>(nullable: false),
                    IpAddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpLink", x => new { x.LinkId, x.IpAddressId });
                    table.ForeignKey(
                        name: "FK_IpLink_IpAddresses_IpAddressId",
                        column: x => x.IpAddressId,
                        principalTable: "IpAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IpLink_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IpLink_IpAddressId",
                table: "IpLink",
                column: "IpAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IpLink");

            migrationBuilder.DropTable(
                name: "IpAddresses");

            migrationBuilder.DropColumn(
                name: "Clicks",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "UniqueClicks",
                table: "Links");
        }
    }
}
