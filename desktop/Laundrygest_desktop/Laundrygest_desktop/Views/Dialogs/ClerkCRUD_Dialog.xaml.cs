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

namespace Laundrygest_desktop.ViewModel.Dialogs
{
    /// <summary>
    /// Interaction logic for ClerkCRUD_Dialog.xaml
    /// </summary>
    public partial class ClerkCRUD_Dialog : Window
    {
        public ClerkCRUD_Dialog(ClerkCrudViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;

            vm.RequestClose += result =>
            {
                DialogResult = result;
                Close();
            };
        }
    }
}
