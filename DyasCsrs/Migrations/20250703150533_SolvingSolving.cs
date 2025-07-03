using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DyasCsrs.Migrations
{
    /// <inheritdoc />
    public partial class SolvingSolving : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockSucursales_Sucursales_SucursalId",
                table: "StockSucursales");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Sucursales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "SucursalId",
                table: "StockSucursales",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nombre",
                value: "Gerente");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nombre",
                value: "Vendedor");

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 1,
                column: "Activo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 2,
                column: "Activo",
                value: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockSucursales_Sucursales_SucursalId",
                table: "StockSucursales",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockSucursales_Sucursales_SucursalId",
                table: "StockSucursales");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Sucursales");

            migrationBuilder.AlterColumn<int>(
                name: "SucursalId",
                table: "StockSucursales",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nombre",
                value: "Vendedor");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nombre",
                value: "Gerente");

            migrationBuilder.AddForeignKey(
                name: "FK_StockSucursales_Sucursales_SucursalId",
                table: "StockSucursales",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
