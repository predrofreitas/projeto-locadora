using Microsoft.EntityFrameworkCore.Migrations;

namespace Locadora.Dados.Migrations
{
    public partial class CriaAluguelItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluguel_Midia_MidiaId",
                table: "Aluguel");

            migrationBuilder.DropIndex(
                name: "IX_Aluguel_MidiaId",
                table: "Aluguel");

            migrationBuilder.DropColumn(
                name: "MidiaId",
                table: "Aluguel");

            migrationBuilder.CreateTable(
                name: "AluguelItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AluguelId = table.Column<int>(type: "int", nullable: true),
                    MidiaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AluguelItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AluguelItem_Aluguel_AluguelId",
                        column: x => x.AluguelId,
                        principalTable: "Aluguel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AluguelItem_Midia_MidiaId",
                        column: x => x.MidiaId,
                        principalTable: "Midia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AluguelItem_AluguelId",
                table: "AluguelItem",
                column: "AluguelId");

            migrationBuilder.CreateIndex(
                name: "IX_AluguelItem_MidiaId",
                table: "AluguelItem",
                column: "MidiaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AluguelItem");

            migrationBuilder.AddColumn<int>(
                name: "MidiaId",
                table: "Aluguel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aluguel_MidiaId",
                table: "Aluguel",
                column: "MidiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluguel_Midia_MidiaId",
                table: "Aluguel",
                column: "MidiaId",
                principalTable: "Midia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
