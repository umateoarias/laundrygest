using Laundrygest_desktop.Model;
using Laundrygest_desktop.Views;
using Laundrygest_desktop.Views.Dialogs;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Laundrygest_desktop
{
    public class ClothesViewModel : INotifyPropertyChanged
    {
        public ClothesViewModel()
        {
            CreateCollectionCommand = new DelegateCommand(OpenCollectionDialog);
            FinishCollectionCommand = new DelegateCommand(FinishCollectionDialog);
            ModifyPriceListCommand = new DelegateCommand(ModifyPriceListDialog);
        }

        public ICommand CreateCollectionCommand { get; }
        public ICommand FinishCollectionCommand { get; }
        public ICommand ModifyCollectionCommand { get; }
        public ICommand ModifyPriceListCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OpenCollectionDialog()
        {
            var dialog = new CollectionDialog(false,null);
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
                if (selected != null)
                {
                    var selectCollectionDialog = new SelectCollectionDialog(selected);
                    bool? result2 = selectCollectionDialog.ShowDialog();

                    if (result2 == true)
                    {
                        Collection collection = selectCollectionDialog.selectedCollection;
                        if (collection != null)
                        {
                            var deliveryDialog = new CollectionDialog(false,collection);
                            deliveryDialog.ShowDialog();
                        }
                    }
                }
            }
        }

        public void ModifyPriceListDialog()
        {
            var dialog = new ModifyPriceListDialog(false);
            dialog.ShowDialog();
        }
    }
}
