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
                                        .Text($"Invoice #{invoice.Number}")
                                        .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                                    //AQUI FALTA GENERAR DATE EN INVOICE
                                    //column.Item().Text(text =>
                                    //{
                                    //    text.Span("Issue date: ").SemiBold();
                                    //    text.Span($"{invoice:d}");
                                    //});

                                    //column.Item().Text(text =>
                                    //{
                                    //    text.Span("Due date: ").SemiBold();
                                    //    text.Span($"{Model.DueDate:d}");
                                    //});
                                });

                                row.ConstantItem(100).Height(50).Placeholder();
                            });
                            column.Spacing(10);

                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(column =>
                                {
                                    column.Spacing(2);

                                    column.Item().BorderBottom(1).PaddingBottom(5).Text("Client").SemiBold();

                                    column.Item().Text("Destaca");
                                    column.Item().Text("Joan Mateo Martinez");
                                    column.Item().Text("123456789");
                                    column.Item().Text("87249825T");
                                    column.Item().Text("Avinguda Espanya, 2, Montcada");
                                    column.Item().Text("08420");
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
                                    column.Item().Text($"{invoice.ClientCodeNavigation.Address}, {invoice.ClientCodeNavigation.Locality}");
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
                                    header.Cell().Element(CellStyle).Text("Peça");
                                    header.Cell().Element(CellStyle).Text("Quantitat");
                                    header.Cell().Element(CellStyle).Text("P. Unitari");
                                    header.Cell().Element(CellStyle).Text("Total");

                                    static IContainer CellStyle(IContainer container)
                                    {
                                        return container.DefaultTextStyle(x => x.SemiBold())
                                            .PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                                    }
                                });
                                foreach (var collection in invoice.Collections)
                                {
                                    table.Cell().Element(CellStyle).Text("Nº Recollida: " + collection.Number);
                                    table.Cell().Element(CellStyle).Text("");
                                    table.Cell().Element(CellStyle).Text("");
                                    table.Cell().Element(CellStyle).Text("");
                                    table.Cell().Element(CellStyle).Text("");
                                    foreach (var collectionItem in collection.CollectionItems)
                                    {
                                        table.Cell().Text("");
                                        table.Cell().Text(collectionItem.PricelistCodeNavigation.Name);
                                        table.Cell().Text(collectionItem.NumPieces.ToString());
                                        table.Cell().Text(collectionItem.PricelistCodeNavigation.UnitPrice.ToString(CultureInfo.CurrentCulture));
                                        table.Cell()
                                            .Text((collectionItem.NumPieces *
                                                   collectionItem.PricelistCodeNavigation.UnitPrice)
                                                .ToString(CultureInfo.CurrentCulture));
                                    }
                                    static IContainer CellStyle(IContainer container)
                                    {
                                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                                            .PaddingVertical(5);
                                    }
                                }
                            });

                            column.Item().Text($"Base imponible: {invoice.TaxBase:C}");
                            column.Item().Text($"Impuesto: {invoice.TaxAmount:C}");
                            column.Item().Text($"Total: {invoice.TotalBase:C}");
                        });
                });
            });

            document.GeneratePdf(path);
        }
        public static IComponent ComposeAdress(IContainer container, string title, Invoice invoice)
        {
            container
        }
    }
}
