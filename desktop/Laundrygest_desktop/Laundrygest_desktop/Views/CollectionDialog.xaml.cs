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

namespace Laundrygest_desktop.Views
{
    /// <summary>
    /// Interaction logic for CollectionDialog.xaml
    /// </summary>
    public partial class CollectionDialog : Window
    {
        public CollectionDialog()
        {
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Maximized;
        }
    }
}
