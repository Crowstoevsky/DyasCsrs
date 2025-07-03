using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using DyasCsrs.Models;
using System;

namespace DyasCsrs.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<DetallesVenta> DetallesVentas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<OpcionDevolucion> OpcionesDevolucion { get; set; }
        public DbSet<ProductoMoto> ProductoMotos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<StockSucursal> StockSucursales { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<EstadoProductoMoto> EstadosProductoMotos { get; set; }
        public DbSet<EstadoDevolucion> EstadosDevolucion { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaciones clave
            modelBuilder.Entity<Venta>()
                .HasOne(c => c.OpcionDevolucion)
                .WithOne(d => d.Venta)
                .HasForeignKey<OpcionDevolucion>(d => d.VentaId);

            modelBuilder.Entity<StockSucursal>()
                .HasIndex(s => new { s.ProductoMotoId, s.SucursalId });
            modelBuilder.Entity<DetallesVenta>()
                .HasOne(d => d.StockSucursal)
                .WithMany(s => s.DetallesVentas)
                .HasForeignKey(d => d.StockSucursalId)
                .OnDelete(DeleteBehavior.Restrict); 
            // Roles
            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Nombre = "Administrador" },
                new Rol { Id = 2, Nombre = "Gerente" },
                new Rol { Id = 3, Nombre = "Vendedor" }
            );

            // Métodos de pago
            modelBuilder.Entity<MetodoPago>().HasData(
                new MetodoPago { Id = 1, Nombre = "Efectivo" },
                new MetodoPago { Id = 2, Nombre = "Tarjeta" }
            );

            // Estado del producto
            modelBuilder.Entity<EstadoProductoMoto>().HasData(
                new EstadoProductoMoto { Id = 1, Nombre = "Activo" },
                new EstadoProductoMoto { Id = 2, Nombre = "Eliminado" }
            );

            // Estado devolución
            modelBuilder.Entity<EstadoDevolucion>().HasData(
                new EstadoDevolucion { Id = 1, Nombre = "Pendiente" },
                new EstadoDevolucion { Id = 2, Nombre = "Aprobado" },
                new EstadoDevolucion { Id = 3, Nombre = "Rechazado" }
            );

            // Proveedores
            modelBuilder.Entity<Proveedor>().HasData(
                new Proveedor { Id = 1, Nombre = "MotoParts SAC", Telefono = "999888777", Email = "contacto@motoparts.com" }
            );

            // Sucursales
            modelBuilder.Entity<Sucursal>().HasData(
                new Sucursal { Id = 1, Nombre = "Sucursal Lima", Ubicacion = "Av. Siempre Viva 123" },
                new Sucursal { Id = 2, Nombre = "Sucursal Arequipa", Ubicacion = "Av. Los Olivos 456" }
            );

            // Empleados
            modelBuilder.Entity<Empleado>().HasData(
                new Empleado { Id = 1, Nombre = "Administrador", Email = "admin@store.com", Password = "admin123", Telefono = "987654321", RolId = 1 },
                new Empleado { Id = 2, Nombre = "Gerente", Email = "gerente@store.com", Password = "gerente123", Telefono = "987654321", RolId = 3 },
                new Empleado { Id = 3, Nombre = "Vendedor Default", Email = "vendedor@store.com", Password = "vendedor123", Telefono = "987654321", RolId = 2 }
            );

            // Clientes
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id = 1, Nombre = "Carlos Sánchez", Telefono = "987111222", Direccion = "Calle Falsa 123", DNI = "12345678" },
                new Cliente { Id = 2, Nombre = "María Ruiz", Telefono = "987333444", Direccion = "Calle Verdadera 456", DNI = "87654321" }
            );

            // Productos
            modelBuilder.Entity<ProductoMoto>().HasData(
                new ProductoMoto { Id = 1, Marca = "Yamaha", Modelo = "FZ25", CC = "250", Anio = "2022", Color = "Negro", Precio = 15000.00m, ProveedorId = 1, EstadoPMId = 1 },
                new ProductoMoto { Id = 2, Marca = "Honda", Modelo = "CBR150R", CC = "150", Anio = "2023", Color = "Rojo", Precio = 9000.00m, ProveedorId = 1, EstadoPMId = 1 }
            );

            // Stock por sucursal
            modelBuilder.Entity<StockSucursal>().HasData(
                new StockSucursal { Id = 1, ProductoMotoId = 1, SucursalId = 1, Cantidad = 5 },
                new StockSucursal { Id = 2, ProductoMotoId = 2, SucursalId = 1, Cantidad = 3 },
                new StockSucursal { Id = 3, ProductoMotoId = 2, SucursalId = 2, Cantidad = 2 }
            );

            // Ventas
            modelBuilder.Entity<Venta>().HasData(
                new Venta { Id = 1, Fecha = DateOnly.Parse("2025-07-02"), ClienteId = 1, EmpleadoId = 1, MetodoPagoId = 1, Total = 15000.00m },
                new Venta { Id = 2, Fecha = DateOnly.Parse("2025-07-02"), ClienteId = 2, EmpleadoId = 2, MetodoPagoId = 2, Total = 9000.00m }
            );

            // Detalles de venta
            modelBuilder.Entity<DetallesVenta>().HasData(
                new DetallesVenta
                {
                    Id = 1,
                    CompraId = 1,
                    ProductoMotoID = 1,
                    Cantidad = 1,
                    PrecioUnitario = 9500.00m,
                    SubTotal = 9500.00m,
                    StockSucursalId = 1 // Producto 1 en Sucursal 1
                },
                new DetallesVenta
                {
                    Id = 2,
                    CompraId = 2,
                    ProductoMotoID = 2,
                    Cantidad = 1,
                    PrecioUnitario = 9000.00m,
                    SubTotal = 9000.00m,
                    StockSucursalId = 2 // Producto 2 en Sucursal 1
                }
            );


            // Opciones de devolución
            modelBuilder.Entity<OpcionDevolucion>().HasData(
                new OpcionDevolucion
                {
                    Id = 1,
                    VentaId = 1,
                    FechaSolicitud = DateOnly.Parse("2025-07-03"),
                    Motivo = "Motor con fallas",
                    EstadoDId = 1
                }
            );
        }

    }
}