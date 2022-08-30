using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdatedCastEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieCardModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosterUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CastId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCardModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieCardModel_Casts_CastId",
                        column: x => x.CastId,
                        principalTable: "Casts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCardModel_CastId",
                table: "MovieCardModel",
                column: "CastId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieCardModel");
        }
    }
}
