using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using DyasCsrs.Models;
using QuestPDF.Drawing;

namespace DyasCsrs.Services
{
    public class PdfGeneratorService
    {
        public PdfGeneratorService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] GenerarComprobanteVenta(Venta venta)
        {
            var gray = Colors.Grey.Lighten2;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(12));
                    page.Header().Element(ComposeHeader);
                    page.Content().Element(c => ComposeContent(c, venta));
                    page.Footer().AlignCenter().Text("Gracias por su compra - Tienda de Motos DyasCSRS");
                });
            }).GeneratePdf();
        }

        void ComposeHeader(IContainer container)
        {
            container.PaddingBottom(10).Column(col =>
            {
                col.Item().Text("COMPROBANTE DE VENTA").FontSize(18).Bold().AlignCenter();
                col.Item().LineHorizontal(1);
            });
        }

        void ComposeContent(IContainer container, Venta venta)
        {
            container.PaddingVertical(15).Column(col =>
            {
                // Cliente y venta
                col.Item().Row(row =>
                {
                    row.RelativeItem().Column(col1 =>
                    {
                        col1.Item().Text($"Cliente: {venta.Cliente.Nombre}");
                        col1.Item().Text($"DNI: {venta.Cliente.DNI}");
                        col1.Item().Text($"Dirección: {venta.Cliente.Direccion}");
                    });

                    row.ConstantItem(200).Column(col2 =>
                    {
                        col2.Item().Text($"Comprobante #: {venta.Id}").Bold();
                        col2.Item().Text($"Fecha: {venta.Fecha}");
                        col2.Item().Text($"Método de pago: {venta.MetodoPago?.Nombre ?? "N/A"}");
                    });
                });

                col.Item().PaddingVertical(10).LineHorizontal(0.5f);

                // Detalle tabla
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(); // Producto
                        columns.ConstantColumn(60); // Cantidad
                        columns.ConstantColumn(80); // P. Unitario
                        columns.ConstantColumn(80); // Subtotal
                    });

                    // Encabezados
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("Producto").Bold();
                        header.Cell().Element(CellStyle).AlignCenter().Text("Cant.").Bold();
                        header.Cell().Element(CellStyle).AlignRight().Text("Precio U.").Bold();
                        header.Cell().Element(CellStyle).AlignRight().Text("Subtotal").Bold();

                        IContainer CellStyle(IContainer cell)
                        {
                            return cell
                                .PaddingVertical(5)
                                .PaddingHorizontal(2)
                                .Background(Colors.Grey.Lighten3)
                                .BorderBottom(1)
                                .BorderColor(Colors.Grey.Lighten1);
                        }
                    });

                    foreach (var d in venta.Detalles)
                    {
                        table.Cell().PaddingVertical(2).Text($"{d.ProductoMoto.Marca} {d.ProductoMoto.Modelo}");
                        table.Cell().AlignCenter().Text(d.Cantidad.ToString());
                        table.Cell().AlignRight().Text(d.PrecioUnitario.ToString("C"));
                        table.Cell().AlignRight().Text(d.SubTotal.ToString("C"));
                    }
                });

                col.Item().AlignRight().PaddingTop(10).Text($"TOTAL: {venta.Total:C}").FontSize(14).Bold();
            });
        }
    }
}
