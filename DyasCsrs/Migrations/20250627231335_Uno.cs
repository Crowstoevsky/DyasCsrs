using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DyasCsrs.Migrations
{
    /// <inheritdoc />
    public partial class Uno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DNI = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosDevolucion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosDevolucion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosProductoMotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosProductoMotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetodosPago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodosPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductoMotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CC = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Anio = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false),
                    EstadoPMId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoMotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductoMotos_EstadosProductoMotos_EstadoPMId",
                        column: x => x.EstadoPMId,
                        principalTable: "EstadosProductoMotos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoMotos_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleados_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockSucursales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoMotoId = table.Column<int>(type: "int", nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockSucursales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockSucursales_ProductoMotos_ProductoMotoId",
                        column: x => x.ProductoMotoId,
                        principalTable: "ProductoMotos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockSucursales_Sucursales_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    MetodoPagoId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "Decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventas_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventas_MetodosPago_MetodoPagoId",
                        column: x => x.MetodoPagoId,
                        principalTable: "MetodosPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesVentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompraId = table.Column<int>(type: "int", nullable: false),
                    ProductoMotoID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "Decimal(8,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "Decimal(8,2)", nullable: false),
                    StockSucursalId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesVentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesVentas_ProductoMotos_ProductoMotoID",
                        column: x => x.ProductoMotoID,
                        principalTable: "ProductoMotos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesVentas_StockSucursales_StockSucursalId1",
                        column: x => x.StockSucursalId1,
                        principalTable: "StockSucursales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetallesVentas_Ventas_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpcionesDevolucion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VentaId = table.Column<int>(type: "int", nullable: false),
                    FechaSolicitud = table.Column<DateOnly>(type: "date", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EstadoDId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcionesDevolucion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcionesDevolucion_EstadosDevolucion_EstadoDId",
                        column: x => x.EstadoDId,
                        principalTable: "EstadosDevolucion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpcionesDevolucion_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "DNI", "Direccion", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "12345678", "Calle Falsa 123", "Carlos Sánchez", "987111222" },
                    { 2, "87654321", "Calle Verdadera 456", "María Ruiz", "987333444" }
                });

            migrationBuilder.InsertData(
                table: "EstadosDevolucion",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Pendiente" },
                    { 2, "Aprobado" },
                    { 3, "Rechazado" }
                });

            migrationBuilder.InsertData(
                table: "EstadosProductoMotos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Activo" },
                    { 2, "Eliminado" }
                });

            migrationBuilder.InsertData(
                table: "MetodosPago",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Efectivo" },
                    { 2, "Tarjeta" }
                });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "Id", "Email", "Nombre", "Telefono" },
                values: new object[] { 1, "contacto@motoparts.com", "MotoParts SAC", "999888777" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Vendedor" },
                    { 3, "Gerente" }
                });

            migrationBuilder.InsertData(
                table: "Sucursales",
                columns: new[] { "Id", "Nombre", "Ubicacion" },
                values: new object[,]
                {
                    { 1, "Sucursal Lima", "Av. Siempre Viva 123" },
                    { 2, "Sucursal Arequipa", "Av. Los Olivos 456" }
                });

            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "Id", "Email", "Nombre", "Password", "RolId", "Telefono" },
                values: new object[,]
                {
                    { 1, "admin@store.com", "Administrador", "admin123", 1, "987654321" },
                    { 2, "gerente@store.com", "Gerente", "gerente123", 3, "987654321" },
                    { 3, "vendedor@store.com", "Vendedor Default", "vendedor123", 2, "987654321" }
                });

            migrationBuilder.InsertData(
                table: "ProductoMotos",
                columns: new[] { "Id", "Anio", "CC", "Color", "EstadoPMId", "Marca", "Modelo", "Precio", "ProveedorId" },
                values: new object[,]
                {
                    { 1, "2022", "250", "Negro", 1, "Yamaha", "FZ25", 15000.00m, 1 },
                    { 2, "2023", "150", "Rojo", 1, "Honda", "CBR150R", 9000.00m, 1 }
                });

            migrationBuilder.InsertData(
                table: "StockSucursales",
                columns: new[] { "Id", "Cantidad", "ProductoMotoId", "SucursalId" },
                values: new object[,]
                {
                    { 1, 5, 1, 1 },
                    { 2, 3, 2, 1 },
                    { 3, 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Ventas",
                columns: new[] { "Id", "ClienteId", "EmpleadoId", "Fecha", "MetodoPagoId", "Total" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateOnly(2025, 7, 2), 1, 15000.00m },
                    { 2, 2, 2, new DateOnly(2025, 7, 2), 2, 9000.00m }
                });

            migrationBuilder.InsertData(
                table: "DetallesVentas",
                columns: new[] { "Id", "Cantidad", "CompraId", "PrecioUnitario", "ProductoMotoID", "StockSucursalId1", "SubTotal" },
                values: new object[,]
                {
                    { 1, 1, 1, 9500.00m, 1, null, 9500.00m },
                    { 2, 1, 2, 9000.00m, 2, null, 9000.00m }
                });

            migrationBuilder.InsertData(
                table: "OpcionesDevolucion",
                columns: new[] { "Id", "EstadoDId", "FechaSolicitud", "Motivo", "VentaId" },
                values: new object[] { 1, 1, new DateOnly(2025, 7, 3), "Motor con fallas", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVentas_CompraId",
                table: "DetallesVentas",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVentas_ProductoMotoID",
                table: "DetallesVentas",
                column: "ProductoMotoID");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVentas_StockSucursalId1",
                table: "DetallesVentas",
                column: "StockSucursalId1");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_RolId",
                table: "Empleados",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_OpcionesDevolucion_EstadoDId",
                table: "OpcionesDevolucion",
                column: "EstadoDId");

            migrationBuilder.CreateIndex(
                name: "IX_OpcionesDevolucion_VentaId",
                table: "OpcionesDevolucion",
                column: "VentaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductoMotos_EstadoPMId",
                table: "ProductoMotos",
                column: "EstadoPMId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoMotos_ProveedorId",
                table: "ProductoMotos",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_StockSucursales_ProductoMotoId_SucursalId",
                table: "StockSucursales",
                columns: new[] { "ProductoMotoId", "SucursalId" });

            migrationBuilder.CreateIndex(
                name: "IX_StockSucursales_SucursalId",
                table: "StockSucursales",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ClienteId",
                table: "Ventas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_EmpleadoId",
                table: "Ventas",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_MetodoPagoId",
                table: "Ventas",
                column: "MetodoPagoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesVentas");

            migrationBuilder.DropTable(
                name: "OpcionesDevolucion");

            migrationBuilder.DropTable(
                name: "StockSucursales");

            migrationBuilder.DropTable(
                name: "EstadosDevolucion");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "ProductoMotos");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "MetodosPago");

            migrationBuilder.DropTable(
                name: "EstadosProductoMotos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
