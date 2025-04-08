using Laundrygest_desktop.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Laundrygest_desktop.ViewModel
{
    public class CollectionDialogViewModel
    {
        public CollectionDialogViewModel()
        {
            SearchClientCommand = new DelegateCommand(OpenSearchClientDialog);

        }

        public ICommand SearchClientCommand { get; }

        public void OpenSearchClientDialog()
        {
            var dialog = new SearchClientDialog(1);
            dialog.WindowState = System.Windows.WindowState.Maximized;
            dialog.ShowDialog();
                        
        }    

    }
}
