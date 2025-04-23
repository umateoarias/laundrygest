using Laundrygest_desktop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.ViewModel.Dialogs
{
    public class SelectCollectionDialogViewModel
    {
        private Client _selectedClient;
        private ObservableCollection<Collection> _collections;
        public SelectCollectionDialogViewModel(Client selectedClient)
        {
            _selectedClient = selectedClient;
        }
    }
}
