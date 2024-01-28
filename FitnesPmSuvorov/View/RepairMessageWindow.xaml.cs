using FitnesPmSuvorov.DB;
using FitnesPmSuvorov.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace FitnesPmSuvorov.View
{
    /// <summary>
    /// Логика взаимодействия для RepairMessageWindow.xaml
    /// </summary>
    public partial class RepairMessageWindow : Window 
    {
        public ObservableCollection<RepairMessages> _RepairMessages;
       
        public RepairMessageWindow(int repairMessageId) 
        {
            InitializeComponent();
            LoadDataFromDatabase(repairMessageId);
        }
        private void LoadDataFromDatabase(int repairMessageId)
        {
            using (var db = new RemontSpravEntities())
            {
                var result = db.RepairMessages.Find(repairMessageId);
                txtText.Text = result.MessageText;
            }
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    }
    
