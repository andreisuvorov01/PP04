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
        private AccountAddOrEdit _accountAddOrEdit;
        private PreviewPageAddOrEdit _TranigAddOrEdit;
        private RepairMessageWindow _repairMessageWindow;
        private RepairMessageAddOrEdit _RepairMessageAddOrEdit;

        private readonly MessagesVM _MessagesVM;
        private RepairMessagesVM _RepairMessagesVM = new RepairMessagesVM();
        private readonly AccountVM _AccountVM;
        public MainWindow()
        {
            InitializeComponent();
            Table.SelectedIndex = 0;

            _MessagesVM = new MessagesVM();
            _AccountVM = new AccountVM();

            _authVM = new AuthVM();
            _authVM.AuthSuccess += AuthVM_AuthSuccess;
            _authVM.RoleReceived += AuthVM_RoleRecevied;
            lstBoxMain.BorderThickness = new Thickness(0);
            lstBoxMain.DataContext = _MessagesVM;
            list();

            MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        public void AuthVM_AuthSuccess(object sender, bool e)
        {
           
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
                AllTraning.Visibility = Visibility.Visible;

                AddButton.Visibility = Visibility.Hidden;
                EditButton.Visibility = Visibility.Hidden;
                DeleteButton.Visibility = Visibility.Hidden;
                Table.Visibility = Visibility.Hidden;
                MainFrame.Visibility = Visibility.Hidden;
            }
            else
            {
                Voidite.Visibility = Visibility.Hidden;
                lstBoxMain.Visibility = Visibility.Hidden;
                AllTraning.Visibility = Visibility.Hidden;

                AddButton.Visibility = Visibility.Visible;
                EditButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
                Table.Visibility = Visibility.Visible;
                MainFrame.Visibility = Visibility.Visible;
            }
            isLogged = true;
        }
        private void BtnAccount_Click(object sender, RoutedEventArgs e)
        {
           if(isLogged == true)
            {
                var result = System.Windows.MessageBox.Show("Вы уже вошли, сменить апккаунт?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if(_accountAddOrEdit != null)
                    {
                        _accountAddOrEdit.Close();
                    }

                    if(_TranigAddOrEdit != null)
                    {
                        _TranigAddOrEdit.Close();
                    }
                    var AuthYes = new AuthWindow();
                    AuthYes.DataContext = _authVM;
                    AuthYes.ShowDialog();
                }
            }
            else
            {
                var Auth = new AuthWindow();
                Auth.DataContext = _authVM;

                Auth.ShowDialog();
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
        private List<PreviewPages> GetTrainingList()
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
            List<PreviewPages> trainingList = GetTrainingList();
            lstBoxMain.ItemsSource = trainingList;
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AllTraning_Click(object sender, RoutedEventArgs e)
        {
            list();
        }

        

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            switch(Table.SelectedIndex)
            {
                case 0:
                    {
                        var TranigWindows = new TranigAddOrEdit(null);
                        TranigWindows.Show();
                    break;
                }
                case 1:
                    {
                        var PreviewWindows = new PreviewPageAddOrEdit(null);
                        PreviewWindows.Show();
                        break;
                    }
            }
            
        }

            private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            switch (Table.SelectedIndex)
            {
                case 0:
                    {
                        var RepairEdit = new RepairMessageAddOrEdit(_RepairMessagesVM.SelectedRepairMessages);
                        Trace.WriteLine(_MessagesVM.SelectedTraining);
                        _RepairMessageAddOrEdit = RepairEdit;
                        RepairEdit.Show();
                        break;
                    }
                case 1:
                    {
                        var PreviewWindows = new PreviewPageAddOrEdit(_MessagesVM.SelectedTraining);
                        Trace.WriteLine(_MessagesVM.SelectedTraining);
                        _TranigAddOrEdit = PreviewWindows;
                        PreviewWindows.Show();
                        break;
                    }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            switch (Table.SelectedIndex)
            {
                case 0:
                    {
                        _MessagesVM.DeleteTrainig();                        
                        break;
                    }
                case 1:
                    {
                        _AccountVM.DeleteTrainig();
                        break;
                    }
            }
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
