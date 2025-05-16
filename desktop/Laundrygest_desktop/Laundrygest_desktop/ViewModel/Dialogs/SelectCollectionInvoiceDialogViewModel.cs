#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Laundrygest_desktop.Data;
using Laundrygest_desktop.Data.Repositories;
using Laundrygest_desktop.Model;
using Laundrygest_desktop.ViewModel.Data;
using Prism.Commands;

namespace Laundrygest_desktop.ViewModel.Dialogs
{
    public class SelectCollectionInvoiceDialogViewModel : INotifyPropertyChanged
    {
        public event Action<bool>? RequestClose;
        public ObservableCollection<InvoiceCollectionViewModel> Collections { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public SelectCollectionInvoiceDialogViewModel(Client c)
        {
            var collectionRepo = new CollectionRepository();
            Collections = new ObservableCollection<InvoiceCollectionViewModel>(
                collectionRepo.GetCollectionsInvoice(c).Result.Select(x => new InvoiceCollectionViewModel(x))
                );
            ConfirmCommand = new DelegateCommand(ConfirmSelection);
            CancelCommand = new DelegateCommand(CancelSelection);
        }

        public void ConfirmSelection()
        {
            if (GetSelectedCollections().Count > 0)
            {
                RequestClose?.Invoke(true);
            }
            else
            {
                MessageBox.Show("Has d'escollir alguna recollida per generar una factura", "Error", MessageBoxButton.OK);
            }
        }

        public void CancelSelection()
        {
            RequestClose?.Invoke(false);
        }


        public List<Collection> GetSelectedCollections()
        {
            var selected =
                Collections.Where(x => x.IsSelected)
                    .Select(x => x.Model).ToList();

            return selected;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
