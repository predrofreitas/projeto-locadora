using Microsoft.EntityFrameworkCore.Migrations;

namespace Locadora.Dados.Migrations
{
    public partial class Authentication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Cliente");
        }
    }
}
