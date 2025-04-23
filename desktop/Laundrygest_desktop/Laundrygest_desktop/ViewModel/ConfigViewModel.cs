using Laundrygest_desktop.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Laundrygest_desktop
{
    public class ConfigViewModel : INotifyPropertyChanged
    {
        private string _urlApiTextBox;
        public ConfigViewModel() {
            UrlApiTextBox = BaseRepository.urlApi;
            ConnectCommand = new DelegateCommand(TryConnect);
        }

        public string UrlApiTextBox
        {
            get { return _urlApiTextBox; }
            set { _urlApiTextBox = value; OnPropertyChanged(); }
        }

        public ICommand ConnectCommand { get; }

        public async void TryConnect()
        {
            var result = await BaseRepository.ConnectAsync(UrlApiTextBox);
            if (result)
            {
                MessageBox.Show("S'ha connectat a l'API correctament", "", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("No s'ha pogut connectar a l'API", "", MessageBoxButton.OK);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
