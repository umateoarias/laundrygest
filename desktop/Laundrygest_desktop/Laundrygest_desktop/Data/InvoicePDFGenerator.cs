using Laundrygest_desktop.Model;
using Microsoft.Win32;
using Prism.Dialogs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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
                            column.Spacing(10);

                            column.Item().Text($"Factura N.º: {invoice.Number}").FontSize(20).Bold();
                            column.Item().Text($"Código cliente: {invoice.ClientCode}");
                            column.Item().Text($"Cliente: {invoice.ClientCodeNavigation?.FirstName} {invoice.ClientCodeNavigation?.LastName}");
                            column.Item().Text($"Telefono: {invoice.ClientCodeNavigation?.Telephone}");
                            column.Item().Text($"NIF: {invoice.ClientCodeNavigation?.Nif}");

                            column.Item().LineHorizontal(1);

                            column.Item().Text($"Base imponible: {invoice.TaxBase:C}");
                            column.Item().Text($"Impuesto: {invoice.TaxAmount:C}");
                            column.Item().Text($"Total: {invoice.TotalBase:C}");
                        });
                });
            });

            document.GeneratePdf(path);
        }
    }
}
