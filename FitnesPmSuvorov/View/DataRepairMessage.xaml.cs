﻿using FitnesPmSuvorov.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitnesPmSuvorov.View
{
    /// <summary>
    /// Логика взаимодействия для DataRepairMessage.xaml
    /// </summary>
    public partial class DataRepairMessage : Page
    {
        public DataRepairMessage()
        {
            InitializeComponent();
            this.DataContext = new RepairMessagesVM();
        }
    }
}
