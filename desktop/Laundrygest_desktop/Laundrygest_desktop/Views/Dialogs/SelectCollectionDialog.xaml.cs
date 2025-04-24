using Laundrygest_desktop.Model;
using Laundrygest_desktop.ViewModel.Dialogs;
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

namespace Laundrygest_desktop.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for SelectCollectionDialog.xaml
    /// </summary>
    public partial class SelectCollectionDialog : Window
    {
        public Collection selectedCollection;
        public SelectCollectionDialog(Client selectedClient)
        {
            InitializeComponent();
            var vm = new SelectCollectionDialogViewModel(selectedClient);
            vm.OnCollectionSelected = (selected) =>
            {
                selectedCollection = selected;
                DialogResult = true;
            };
            DataContext = vm;
        }
    }
}
