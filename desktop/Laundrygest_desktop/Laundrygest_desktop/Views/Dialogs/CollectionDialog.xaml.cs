﻿using Laundrygest_desktop.Model;
using Laundrygest_desktop.ViewModel;
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
        public CollectionDialog(bool isQuilts,Collection? c)
        {
            InitializeComponent();
            var vm = new CollectionDialogViewModel(isQuilts,c);
            vm.CloseAction = new Action(Close);
            DataContext = vm;
            WindowState = WindowState.Maximized;
        }
    }
}
