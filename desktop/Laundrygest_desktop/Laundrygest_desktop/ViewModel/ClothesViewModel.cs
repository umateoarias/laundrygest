using Laundrygest_desktop.Views;
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
        }

        public ICommand CreateCollectionCommand { get; }

        public void OpenCollectionDialog()
        {
            var dialog = new CollectionDialog();
            dialog.ShowDialog();
            dialog.WindowState = System.Windows.WindowState.Maximized;
        }
    }
}
