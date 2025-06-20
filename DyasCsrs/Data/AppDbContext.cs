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
            modelBuilder.Entity<Venta>()
                .HasOne(c => c.OpcionDevolucion)
                .WithOne(d => d.Venta)
                .HasForeignKey<OpcionDevolucion>(d => d.VentaId);

            modelBuilder.Entity<StockSucursal>()
                .HasIndex(s => new { s.ProductoMotoId, s.SucursalId });
        }
    }
}