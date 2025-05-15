using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Laundrygest_desktop.Data;
using Laundrygest_desktop.Data.Repositories;
using Laundrygest_desktop.Model;
using Laundrygest_desktop.ViewModel.Dialogs;
using Laundrygest_desktop.Views;
using Laundrygest_desktop.Views.Dialogs;
using Microsoft.Win32;

namespace Laundrygest_desktop
{
    public class StatsViewModel : INotifyPropertyChanged
    {
        public StatsRepository statsRepo = new StatsRepository();
        public InvoiceRepository invoiceRepo = new InvoiceRepository();
        public StatsViewModel()
        {
            GeneralStatsCommand = new DelegateCommand(OpenGeneralStats);
            MonthlyStatsCommand = new DelegateCommand(OpenMonthlyStats);
            CreateInvoiceCommand = new DelegateCommand(OpenCreateInvoice);
            ModifyClientsCommand = new DelegateCommand(OpenModifyClients);
        }

        public ICommand GeneralStatsCommand { get; set; }
        public ICommand MonthlyStatsCommand { get; set; }
        public ICommand CreateInvoiceCommand { get; set; }
        public ICommand ModifyClientsCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OpenGeneralStats()
        {
            var vm = new SelectDateRangeDialogViewModel();
            var dialog = new SelectDateRangeDialog(vm);
            var result = dialog.ShowDialog();

            if (result != true) return;
            var pricelistStats = statsRepo.GetPricelistStats(vm.FromDate, vm.ToDate);
            var columns = new List<ColumnDescription>
            {
                new ColumnDescription { Header = "Nom peça", PropertyName = "namePricelist" },
                new ColumnDescription { Header = "Nº peces", PropertyName = "numPieces" },
                new ColumnDescription { Header = "Import total", PropertyName = "totalAmount" }
            };
            var dialogStats = new StatsTableDialog(pricelistStats.Result, columns);
            dialogStats.ShowDialog();
        }

        public void OpenMonthlyStats()
        {
            var vm = new SelectDateRangeDialogViewModel();
            var dialog = new SelectDateRangeDialog(vm);
            var result = dialog.ShowDialog();

            if (result != true) return;
            var monthlyStats = statsRepo.GetMonthlyStats(vm.FromDate, vm.ToDate);
            var columns = new List<ColumnDescription>
            {
                new ColumnDescription { Header = "Data", PropertyName = "dateName" },
                new ColumnDescription { Header = "Total", PropertyName = "totalAmount" },
                new ColumnDescription { Header = "IVA", PropertyName = "taxAmount" },
                new ColumnDescription { Header = "Base imposable", PropertyName = "baseAmount" }
            };
            var dialogStats = new StatsTableDialog(monthlyStats.Result, columns);
            dialogStats.ShowDialog();

        }

        public void OpenCreateInvoice()
        {
            var dialogClient = new SearchClientDialog();
            var result = dialogClient.ShowDialog();

            if (result != true) return;
            var selected = dialogClient.SelectedOption;

            if (selected == null) return;
            var collectionVm = new SelectCollectionInvoiceDialogViewModel(dialogClient.SelectedOption);
            var dialogCollections = new SelectCollectionsInvoiceDialog(collectionVm);
            var result2 = dialogCollections.ShowDialog();

            if (result2 != true) return;
            var collectionsInvoice = collectionVm.GetSelectedCollections();
            decimal cTaxBase = 0, cTaxAmount = 0, cTotal = 0;
            foreach (var c in collectionsInvoice)
            {
                if (c.TaxBase != null) cTaxBase += c.TaxBase.Value;
                if (c.TaxAmount != null) cTaxAmount += c.TaxAmount.Value;
                if (c.Total != null) cTotal += c.Total.Value;
            }
            Invoice invoice = new Invoice
            {
                InvoiceDate = DateTime.Now
            };
            var newInvoice = invoiceRepo.PostInvoice(invoice).Result;

            if (newInvoice != null)
            {
                newInvoice.ClientCode = selected.Code;
                newInvoice.Collections = collectionsInvoice;
                newInvoice.TotalBase = cTotal;
                newInvoice.TaxAmount = cTaxAmount;
                newInvoice.TaxBase = cTaxBase;
                var resultPut = invoiceRepo.PutClient(newInvoice.Id, newInvoice).Result;
                if (resultPut)
                {
                    var printInvoice = invoiceRepo.GetInvoice(newInvoice.Id).Result;
                    if (printInvoice.ClientCode != null)
                        printInvoice.ClientCodeNavigation =
                            new ClientRepository().GetClient(printInvoice.ClientCode.Value).Result;
                    InvoicePdfGenerator.ExportInvoiceToPdf(printInvoice);
                }
            }

        }

        public void OpenModifyClients()
        {
            var dialog = new SearchClientDialog();
            var result = dialog.ShowDialog();

            if (result != true) return;
            var selected = dialog.SelectedOption;

            if (selected == null) return;
            var dialogUpdate = new CreateClientDialog(selected);
            dialogUpdate.ShowDialog();
        }
    }
}
