using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Janus.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Screens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScreenAppId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ConnectionId = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Slot1 = table.Column<string>(type: "TEXT", nullable: true),
                    Slot2 = table.Column<string>(type: "TEXT", nullable: true),
                    Slot3 = table.Column<string>(type: "TEXT", nullable: true),
                    Slot4 = table.Column<string>(type: "TEXT", nullable: true),
                    Slot5 = table.Column<string>(type: "TEXT", nullable: true),
                    Slot6 = table.Column<string>(type: "TEXT", nullable: true),
                    ScreenId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdSlots_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdSlots_ScreenId",
                table: "AdSlots",
                column: "ScreenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdSlots");

            migrationBuilder.DropTable(
                name: "Screens");
        }
    }
}
