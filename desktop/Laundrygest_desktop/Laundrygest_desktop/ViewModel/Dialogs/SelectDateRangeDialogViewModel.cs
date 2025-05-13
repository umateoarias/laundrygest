using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace Laundrygest_desktop.ViewModel.Dialogs
{
    public class SelectDateRangeDialogViewModel : INotifyPropertyChanged
    {
        public event Action<bool?> RequestClose;
        private DateTime _fromDate;
        private DateTime _toDate;

        public ICommand ConfirmCommand { get; set; }
        public SelectDateRangeDialogViewModel()
        {
            var today = DateTime.Now;
            FromDate = new DateTime(today.Year, today.Month,1);
            ToDate = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            ConfirmCommand = new DelegateCommand(ConfirmSelection);
        }

        public DateTime FromDate
        {
            get => _fromDate;
            set
            {
                _fromDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime ToDate
        {
            get => _toDate;
            set
            {
                _toDate = value;
                OnPropertyChanged();
            }
        }

        public void ConfirmSelection()
        {
            RequestClose?.Invoke(true);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
