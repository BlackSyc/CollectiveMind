using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectiveMind.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Title = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Content = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatementStatement",
                columns: table => new
                {
                    NegativeArgumentsId = table.Column<Guid>(type: "char(36)", nullable: false),
                    PositiveArgumentsId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementStatement", x => new { x.NegativeArgumentsId, x.PositiveArgumentsId });
                    table.ForeignKey(
                        name: "FK_StatementStatement_Statement_NegativeArgumentsId",
                        column: x => x.NegativeArgumentsId,
                        principalTable: "Statement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatementStatement_Statement_PositiveArgumentsId",
                        column: x => x.PositiveArgumentsId,
                        principalTable: "Statement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatementStatement_PositiveArgumentsId",
                table: "StatementStatement",
                column: "PositiveArgumentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatementStatement");

            migrationBuilder.DropTable(
                name: "Statement");
        }
    }
}
