using Laundrygest_desktop.Model;
using Laundrygest_desktop.Views;
using Laundrygest_desktop.Views.Dialogs;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Laundrygest_desktop
{
    public class ClothesViewModel
    {
        public ClothesViewModel() {
            CreateCollectionCommand = new DelegateCommand(OpenCollectionDialog);
            FinishCollectionCommand = new DelegateCommand(OpenCollectionDialog);
            ModifyPriceListCommand = new DelegateCommand(ModifyPriceListDialog);
        }

        public ICommand CreateCollectionCommand { get; }
        public ICommand FinishCollectionCommand { get; }

        public ICommand ModifyPriceListCommand { get; }

        public void OpenCollectionDialog()
        {
            var dialog = new CollectionDialog(false);
            dialog.Show();
            dialog.WindowState = System.Windows.WindowState.Maximized;
        }

        public void FinishCollectionDialog()
        {
            var dialog = new SearchClientDialog();
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                Client selected = dialog.SelectedOption;
            }
        }

        public void ModifyPriceListDialog()
        {
            var dialog = new ModifyPriceListDialog(false);
            dialog.ShowDialog();
        }
    }
}
