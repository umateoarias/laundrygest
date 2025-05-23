﻿using Laundrygest_desktop.ViewModel;
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

namespace Laundrygest_desktop.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for ModifyPriceListDialog.xaml
    /// </summary>
    public partial class ModifyPriceListDialog : Window
    {
        public ModifyPriceListDialog(bool isQuilts)
        {
            InitializeComponent();
            DataContext = new ModifyPriceListViewModel(isQuilts);
        }
    }
}
