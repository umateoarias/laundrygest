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
    /// Interaction logic for PaymentDialog.xaml
    /// </summary>
    public partial class PaymentDialog : Window
    {
        public string PaymentMode { get; private set; }
        public decimal RemainingAmount { get; private set; }
        public PaymentDialog(decimal totalPrice, decimal dueTotal)
        {
            InitializeComponent();

            var vm = new PaymentDialogViewModel(totalPrice,dueTotal);
            vm.PaymentModeReturn = (payment, remaining)=>{
                PaymentMode = payment;
                RemainingAmount = remaining;
                DialogResult = true;
            };

            DataContext = vm;
            
        }
    }
}
