using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Laundrygest_desktop.Data.Repositories;
using Laundrygest_desktop.Model;
using Laundrygest_desktop.ViewModel.Dialogs;
using Laundrygest_desktop.Views;
using Laundrygest_desktop.Views.Dialogs;

namespace Laundrygest_desktop
{
    public class StatsViewModel : INotifyPropertyChanged
    {
        public StatsRepository statsRepo = new StatsRepository();
        public StatsViewModel() {
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
            var dialogStats = new StatsTableDialog(monthlyStats.Result,columns);
            dialogStats.ShowDialog();

        }
        public void OpenCreateInvoice() { }

        public void OpenModifyClients()
        {
            var dialog = new SearchClientDialog();
            var result = dialog.ShowDialog();

            if (result != true) return;
            var selected = dialog.SelectedOption;

            if (selected != null)
            {
                var dialogUpdate = new CreateClientDialog(selected);
                dialogUpdate.ShowDialog();
            }
        }
    }
}
