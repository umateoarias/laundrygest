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
    /// Interaction logic for SelectPieceDialog.xaml
    /// </summary>
    public partial class SelectPieceDialog : Window
    {
        public Pricelist SelectedOption { get; set; }
        public SelectPieceDialog(int collectionType)
        {
            InitializeComponent();
            var vm = new SelectPieceViewModel(collectionType);
            vm.OnOptionSelected = (selected) =>
            {
                SelectedOption = selected;
                DialogResult = true;
            };
            DataContext = vm;
        }
    }
}
