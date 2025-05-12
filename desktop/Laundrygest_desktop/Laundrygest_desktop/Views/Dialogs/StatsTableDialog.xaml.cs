using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Laundrygest_desktop.Model;
using Laundrygest_desktop.ViewModel.Dialogs;

namespace Laundrygest_desktop.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for StatsTableDialog.xaml
    /// </summary>
    public partial class StatsTableDialog : Window
    {

        public StatsTableDialog(IEnumerable<object> items, List<ColumnDescription> columns)
        {
            InitializeComponent();
            DataContext = new StatsTableDialogViewModel(items, columns);

            foreach (var column in columns)
            {
                DataGridStats.Columns.Add(new DataGridTextColumn
                {
                    Header = column.Header,
                    Binding = new Binding(column.PropertyName)
                });


            }
        }
    }
}



