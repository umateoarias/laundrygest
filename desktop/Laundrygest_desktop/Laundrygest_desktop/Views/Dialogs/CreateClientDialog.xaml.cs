﻿#nullable enable
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
using Laundrygest_desktop.Model;
using Laundrygest_desktop.ViewModel;

namespace Laundrygest_desktop.Views
{
    /// <summary>
    /// Interaction logic for CreateClientDialog.xaml
    /// </summary>
    public partial class CreateClientDialog : Window
    {
        public CreateClientDialog(Client? client)
        {
            InitializeComponent();
            DataContext = new CreateClientDialogViewModel(client);

        }
    }
}
