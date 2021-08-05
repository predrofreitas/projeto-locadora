using Microsoft.EntityFrameworkCore.Migrations;

namespace Locadora.Dados.Migrations
{
    public partial class StatusAluguel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluguel_Cliente_ClienteId",
                table: "Aluguel");

            migrationBuilder.DropForeignKey(
                name: "FK_AluguelItem_Aluguel_AluguelId",
                table: "AluguelItem");

            migrationBuilder.DropForeignKey(
                name: "FK_AluguelItem_Item_ItemId",
                table: "AluguelItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Item_ItemId",
                table: "Estoque");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Estoque",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "AluguelItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AluguelId",
                table: "AluguelItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Aluguel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Aluguel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Aluguel_Cliente_ClienteId",
                table: "Aluguel",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AluguelItem_Aluguel_AluguelId",
                table: "AluguelItem",
                column: "AluguelId",
                principalTable: "Aluguel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AluguelItem_Item_ItemId",
                table: "AluguelItem",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estoque_Item_ItemId",
                table: "Estoque",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluguel_Cliente_ClienteId",
                table: "Aluguel");

            migrationBuilder.DropForeignKey(
                name: "FK_AluguelItem_Aluguel_AluguelId",
                table: "AluguelItem");

            migrationBuilder.DropForeignKey(
                name: "FK_AluguelItem_Item_ItemId",
                table: "AluguelItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Item_ItemId",
                table: "Estoque");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Aluguel");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Estoque",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "AluguelItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AluguelId",
                table: "AluguelItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Aluguel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluguel_Cliente_ClienteId",
                table: "Aluguel",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AluguelItem_Aluguel_AluguelId",
                table: "AluguelItem",
                column: "AluguelId",
                principalTable: "Aluguel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AluguelItem_Item_ItemId",
                table: "AluguelItem",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estoque_Item_ItemId",
                table: "Estoque",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
