﻿using System;
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
using Laundrygest_desktop.ViewModel.Dialogs;

namespace Laundrygest_desktop.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for SelectCollectionsInvoiceDialog.xaml
    /// </summary>
    public partial class SelectCollectionsInvoiceDialog : Window
    {
        public SelectCollectionsInvoiceDialog(SelectCollectionInvoiceDialogViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;

            vm.RequestClose += result =>
            {
                DialogResult = result;
                Close();
            };
        }
    }
}
