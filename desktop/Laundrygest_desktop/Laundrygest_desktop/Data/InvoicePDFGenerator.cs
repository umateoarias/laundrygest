using System.Globalization;
using Laundrygest_desktop.Model;
using Microsoft.Win32;
using Prism.Dialogs;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Net;

namespace Laundrygest_desktop.Data
{
    public static class InvoicePdfGenerator
    {
        public static void ExportInvoiceToPdf(Invoice invoice)
        {
            var company = ConfigController.GetSettings().Company;
            var currenctyCulture = CultureInfo.CreateSpecificCulture("es-ES");
            var dialogo = new SaveFileDialog
            {
                Title = "Guardar factura como PDF",
                Filter = "Archivo PDF (*.pdf)|*.pdf",
                FileName = $"Factura_{invoice.Number}.pdf",
                DefaultExt = ".pdf",
                AddExtension = true,
                OverwritePrompt = true
            };
            bool? resultado = dialogo.ShowDialog();
            if (resultado != true || string.IsNullOrWhiteSpace(dialogo.FileName))
            {
                return;
            }
            var path = dialogo.FileName;
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);

                    page.Content()
                        .Column(column =>
                        {
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(column =>
                                {
                                    column.Item()
                                        .Text($"Factura Nº {invoice.Number}")
                                        .FontSize(20).SemiBold().FontColor(Colors.Cyan.Darken2);
                                    column.Item().Text(text =>
                                    {
                                        text.Span("Data factura: ").SemiBold();
                                        text.Span($"{invoice.InvoiceDate:d}");
                                    });
                                });
                                row.ConstantItem(100).Height(50).Placeholder();
                            });
                            column.Spacing(10);
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(column =>
                                {
                                    column.Spacing(2);
                                    column.Item().BorderBottom(1).PaddingBottom(5).Text(company.CompanyName).SemiBold();
                                    column.Item().Text(company.OwnerName);
                                    column.Item().Text(company.OwnerLastName);
                                    column.Item().Text(company.Telephone);
                                    column.Item().Text(company.Nif);
                                    column.Item().Text(company.Address);
                                    column.Item().Text(company.PostalCode);
                                });
                                row.ConstantItem(50);
                                row.RelativeItem().Column(column =>
                                {
                                    column.Spacing(2);
                                    column.Item().BorderBottom(1).PaddingBottom(5).Text("Client").SemiBold();
                                    column.Item().Text(invoice.ClientCodeNavigation.FirstName);
                                    column.Item().Text(invoice.ClientCodeNavigation.LastName);
                                    column.Item().Text(invoice.ClientCodeNavigation.Telephone);
                                    column.Item().Text(invoice.ClientCodeNavigation.Nif);
                                    column.Item()
                                        .Text(
                                            $"{invoice.ClientCodeNavigation.Address}, {invoice.ClientCodeNavigation.Locality}");
                                    column.Item().Text(invoice.ClientCodeNavigation.PostalCode);
                                });
                            });
                            column.Item().LineHorizontal(1);
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("");
                                    header.Cell().Element(CellStyle).AlignRight().Text("Peça");
                                    header.Cell().Element(CellStyle).AlignRight().Text("Quantitat");
                                    header.Cell().Element(CellStyle).AlignRight().Text("P. Unitari");
                                    header.Cell().Element(CellStyle).AlignRight().Text("Total");
                                    static IContainer CellStyle(IContainer container)
                                    {
                                        return container.DefaultTextStyle(x => x.SemiBold())
                                            .PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                                    }
                                });
                                foreach (var collection in invoice.Collections)
                                {
                                    table.Cell().Element(CellStyle).Text("Nº Recollida: " + collection.Number).Bold();
                                    table.Cell().Element(CellStyle).Text("");
                                    table.Cell().Element(CellStyle).Text("");
                                    table.Cell().Element(CellStyle).Text("");
                                    table.Cell().Element(CellStyle).Text("");
                                    foreach (var collectionItem in collection.CollectionItems)
                                    {
                                        table.Cell().Text("");
                                        table.Cell().Element(RowStyle)
                                            .Text(collectionItem.PricelistCodeNavigation.Name);
                                        table.Cell().Element(RowStyle).Text(collectionItem.NumPieces.ToString());
                                        table.Cell().Element(RowStyle)
                                            .Text(collectionItem.PricelistCodeNavigation.UnitPrice.ToString("C",
                                                currenctyCulture));
                                        table.Cell().Element(RowStyle)
                                            .Text((collectionItem.NumPieces *
                                                   collectionItem.PricelistCodeNavigation.UnitPrice)
                                                .ToString("C", currenctyCulture));
                                        static IContainer RowStyle(IContainer container)
                                        {
                                            return container.PaddingVertical(2).AlignRight();
                                        }
                                    }
                                    static IContainer CellStyle(IContainer container)
                                    {
                                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                                            .PaddingVertical(5);
                                    }
                                }
                            });
                            column.Spacing(10);
                            column.Item()
                                .Text($"Base imposable: {invoice.TaxBase.Value.ToString("C", currenctyCulture)}")
                                .AlignRight();
                            column.Item().Text($"IVA: {invoice.TaxAmount.Value.ToString("C", currenctyCulture)}")
                                .AlignRight();
                            column.Item().Text($"Total: {invoice.TotalBase.Value.ToString("C", currenctyCulture)}")
                                .AlignRight();
                        });
                });
            });
            document.GeneratePdf(path);
        }
    }
}