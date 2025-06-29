﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DyasCsrs.Models
{
    public class DetallesVenta
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [NotMapped]
        public int StockSucursalId { get;set; }
        public int CompraId { get; set; }

        [ForeignKey("CompraId")]
        public Venta Venta { get; set; }
        public int ProductoMotoID { get; set; }
        [ForeignKey("ProductoMotoID")]

        public ProductoMoto ProductoMoto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required,Column(TypeName ="Decimal(8,2)")]
        public decimal PrecioUnitario { get; set; }
        [Required, Column(TypeName = "Decimal(8,2)")]
        public decimal SubTotal { get; set; }
    }
}
