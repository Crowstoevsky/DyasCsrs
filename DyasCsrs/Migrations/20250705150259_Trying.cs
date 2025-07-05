using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DyasCsrs.Migrations
{
    /// <inheritdoc />
    public partial class Trying : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Roles_RolId",
                table: "Empleados");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "MetodosPago",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "MetodosPago",
                keyColumn: "Id",
                keyValue: 1,
                column: "Activo",
                value: true);

            migrationBuilder.UpdateData(
                table: "MetodosPago",
                keyColumn: "Id",
                keyValue: 2,
                column: "Activo",
                value: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Roles_RolId",
                table: "Empleados",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Roles_RolId",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "MetodosPago");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Roles_RolId",
                table: "Empleados",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
