using FitnesPmSuvorov.DB;
using FitnesPmSuvorov.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitnesPmSuvorov.View
{
    /// <summary>
    /// Логика взаимодействия для TranigAddOrEdit.xaml
    /// </summary>
    public partial class TranigAddOrEdit : Window
    {
        private PreviewPages _message;
        private readonly MessagesVM _MessagesVM;
        public TranigAddOrEdit(PreviewPages repairMessages)
        {
            InitializeComponent();
            _MessagesVM = new MessagesVM();
            foreach (var item in App.Current.Windows)
            {
                if (item is MainWindow)
                {
                    this.Owner = item as Window;
                }
            }
            if (repairMessages is null)
            {
                _message = repairMessages = new PreviewPages();
            }
            else
            {
                _message = repairMessages;
            } 
            this.DataContext = repairMessages;
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new RemontSpravEntities())
            {
                try
                {
                    db.PreviewPages.AddOrUpdate(_message);
                    db.SaveChanges();
                    System.Windows.MessageBox.Show("данные успешно сохранены", "успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    _MessagesVM.LoadData();


                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
