using Laundrygest_desktop.Model;
using Laundrygest_desktop.Views.Dialogs;
using Laundrygest_desktop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laundrygest_desktop
{
    public class QuiltsViewModel : INotifyPropertyChanged
    {
        public QuiltsViewModel() {
            CreateCollectionCommand = new DelegateCommand(OpenCollectionDialog);
            FinishCollectionCommand = new DelegateCommand(FinishCollectionDialog);
            ModifyPriceListCommand = new DelegateCommand(ModifyPriceListDialog);
        }

        public ICommand CreateCollectionCommand { get; }
        public ICommand FinishCollectionCommand { get; }
        public ICommand ModifyPriceListCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OpenCollectionDialog()
        {
            var dialog = new CollectionDialog(true, null);
            dialog.Show();
            dialog.WindowState = System.Windows.WindowState.Maximized;
        }

        public void FinishCollectionDialog()
        {
            var dialog = new SearchClientDialog();
            var result = dialog.ShowDialog();

            if (result != true) return;
            var selected = dialog.SelectedOption;
            if (selected == null) return;
            var selectCollectionDialog = new SelectCollectionDialog(selected);
            var result2 = selectCollectionDialog.ShowDialog();

            if (result2 != true) return;
            var collection = selectCollectionDialog.selectedCollection;
            if (collection == null) return;
            var deliveryDialog = new CollectionDialog(true, collection);
            deliveryDialog.ShowDialog();
        }

        public void ModifyPriceListDialog()
        {
            var dialog = new ModifyPriceListDialog(true);
            dialog.ShowDialog();
        }
    }
}
