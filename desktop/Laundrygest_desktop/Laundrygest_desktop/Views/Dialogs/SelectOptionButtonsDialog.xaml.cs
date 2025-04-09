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
    /// Interaction logic for SelectOptionButtonsDialog.xaml
    /// </summary>
    /// 
    public partial class SelectOptionButtonsDialog : Window
    {
        public string SelectedOption { get; private set; }
        public SelectOptionButtonsDialog(List<string> options)
        {
            InitializeComponent();

            var vm = new SelectOptionDialogViewModel(options);
            vm.OnOptionSelected = (selected) =>
            {
                SelectedOption = selected;
                DialogResult = true;
            };

            DataContext = vm;
        }
    }
}
