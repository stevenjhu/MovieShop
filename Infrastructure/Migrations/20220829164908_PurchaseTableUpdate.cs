using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class PurchaseTableUpdate : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //repetitive table creation because data table was manually dropped in SSMS 
            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    PurchaseNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 9.9m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => new { x.MovieId, x.UserId });
                    table.UniqueConstraint("AK_Purchases_PurchaseNumber", x => x.PurchaseNumber);
                    table.ForeignKey(
                        name: "FK_Purchases_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchases_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_UserId",
                table: "Purchases",
                column: "UserId");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Purchases_PurchaseNumber",
                table: "Purchases");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseNumber",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PurchaseNumber",
                table: "Purchases",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Purchases_PurchaseNumber",
                table: "Purchases",
                column: "PurchaseNumber");
        }
    }
}
