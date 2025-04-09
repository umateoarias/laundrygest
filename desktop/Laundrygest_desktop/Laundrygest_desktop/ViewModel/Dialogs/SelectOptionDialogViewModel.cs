using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Laundrygest_desktop.ViewModel.Dialogs
{
    public class SelectOptionDialogViewModel
    {
        public List<string> Options { get; set; }
        public ICommand SelectOptionCommand { get; }

        public Action<string> OnOptionSelected { get; set; }

        public SelectOptionDialogViewModel(List<string> options)
        {
            Options = options;
            SelectOptionCommand = new DelegateCommand<object>(SelectOption);
        }

        private void SelectOption(object selected)
        {
            OnOptionSelected?.Invoke((string)selected);
        }
    }
}
