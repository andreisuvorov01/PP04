using FitnesPmSuvorov.View;
using FitnesPmSuvorov.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using FitnesPmSuvorov.DB;
using System.Collections.ObjectModel;
using System.Web.UI.MobileControls;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using System.Windows.Threading;
using System.Web.Security;
using System.Xml.Serialization;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using ListBox = System.Windows.Controls.ListBox;
using System.Diagnostics;

namespace FitnesPmSuvorov
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Point windowCoordinates;
        private Point mouseCoordinates;
        private Message _RepairMessages;
        private AuthVM _authVM;
        private bool isLogged = false;
        private RepairMessageWindow _repairMessageWindow;
        private RepairMessageAddOrEdit _RepairMessageAddOrEdit;

        private readonly MessagesVM _MessagesVM = new MessagesVM();
        private readonly RepairMessagesVM _RepairMessagesVM = new RepairMessagesVM();
        public MainWindow()
        {
            InitializeComponent();
            Table.SelectedIndex = 0;
            _authVM = new AuthVM();
            _authVM.RoleReceived += AuthVM_RoleRecevied;
            lstBoxMain.BorderThickness = new Thickness(0);
            lstBoxMain.DataContext = _MessagesVM;
            list();
            MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

        }
        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var repairMessageWindow = new RepairMessageWindow(_MessagesVM.SelectedTraining.RepairMessageID);
            repairMessageWindow.Show();
        }

        public void AuthVM_RoleRecevied(object sender, int roleId)
        {
          if (roleId == 2)
            {
                Voidite.Visibility = Visibility.Hidden;
                lstBoxMain.Visibility = Visibility.Visible;
                lstBoxMain.Width = 700;
                Table.Visibility = Visibility.Hidden;
                MainFrame.Visibility = Visibility.Hidden;
            }
            else
            {
                Voidite.Visibility = Visibility.Hidden;
                lstBoxMain.Visibility = Visibility.Hidden;
                Table.Visibility = Visibility.Visible;
                MainFrame.Visibility = Visibility.Visible;
            }
            isLogged = true;
        }
        private void BtnAccount_Click(object sender, RoutedEventArgs e)
        {
           if(isLogged == true)
            {
                var result = System.Windows.MessageBox.Show("Вы уже вошли, сменить аккаунт?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var AuthYes = new AuthWindow();
                    AuthYes.DataContext = _authVM;
                    AuthYes.ShowDialog();
                    list();
                }
            }
            else
            {
                var Auth = new AuthWindow();
                Auth.DataContext = _authVM;
                Auth.ShowDialog();
                list();
            }
           
        }
        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private List<PreviewPages> getPreviewPagesList()
        {
            List<PreviewPages> messages = new List<PreviewPages>();

            using (var db = new RemontSpravEntities())
            {
                var query = from t in db.PreviewPages
                            select t;

                messages = query.ToList();
            }

            return messages;
        }
        private void list()
        {
            List<PreviewPages> trainingList = getPreviewPagesList();
            lstBoxMain.ItemsSource = trainingList;
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
       
        private void Table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(Table.SelectedIndex)
            {
                case 0:
                    {
                        MainFrame.Navigate(new DataRepairMessage { DataContext = new RepairMessagesVM() });
                        break;
                    }
            case 1:
                    {
                        MainFrame.Navigate(new DatePreviewPage { DataContext = new MessagesVM() });
                        break;
                    }
            }
        }
    }
}
