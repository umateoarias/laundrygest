using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Laundrygest_desktop.Model;

namespace Laundrygest_desktop.ViewModel.Dialogs
{
    public class StatsTableDialogViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<object> Items { get; set; } = new ObservableCollection<object>();

        public ObservableCollection<ColumnDescription> Columns { get; set; } =
            new ObservableCollection<ColumnDescription>();
        public StatsTableDialogViewModel(IEnumerable<object> items, IEnumerable<ColumnDescription> columns)
        {
            foreach (var item in items)
            {
                Items.Add(item);
            }

            foreach (var column in columns)
            {
                Columns.Add(column);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
