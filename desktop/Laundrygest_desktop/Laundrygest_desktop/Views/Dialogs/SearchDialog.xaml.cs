using Laundrygest_desktop.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laundrygest_desktop.Views
{
    /// <summary>
    /// Interaction logic for SearchClientDialog.xaml
    /// </summary>
    public partial class SearchClientDialog : Window
    {
        public SearchClientDialog(int mode)
        {
            InitializeComponent();
            DataContext = new SearchDialogViewModel(mode);
            this.WindowState = System.Windows.WindowState.Maximized;
        }
    }
}
