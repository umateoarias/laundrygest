using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Laundrygest_desktop.Views;

namespace Laundrygest_desktop
{
    public class StatsViewModel : INotifyPropertyChanged
    {
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

        }

        public void OpenMonthlyStats() { 
        
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

            }
        }
    }
}
