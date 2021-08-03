using Microsoft.EntityFrameworkCore.Migrations;

namespace Locadora.Dados.Migrations
{
    public partial class AlugueisFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Midia_Aluguel_AluguelId",
                table: "Midia");

            migrationBuilder.DropIndex(
                name: "IX_Midia_AluguelId",
                table: "Midia");

            migrationBuilder.DropColumn(
                name: "AluguelId",
                table: "Midia");

            migrationBuilder.AddColumn<int>(
                name: "MidiaId",
                table: "Aluguel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluguel_Midia_MidiaId",
                table: "Aluguel");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Aluguel_MidiaId",
                table: "Aluguel");

            migrationBuilder.DropColumn(
                name: "MidiaId",
                table: "Aluguel");

            migrationBuilder.AddColumn<int>(
                name: "AluguelId",
                table: "Midia",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Midia_AluguelId",
                table: "Midia",
                column: "AluguelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Midia_Aluguel_AluguelId",
                table: "Midia",
                column: "AluguelId",
                principalTable: "Aluguel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
