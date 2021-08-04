using Microsoft.EntityFrameworkCore.Migrations;

namespace Locadora.Dados.Migrations
{
    public partial class TipoMidia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoMidia",
                table: "Midia",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoMidia",
                table: "Midia");
        }
    }
}
