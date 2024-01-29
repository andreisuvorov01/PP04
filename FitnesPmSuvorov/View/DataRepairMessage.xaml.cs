using FitnesPmSuvorov.ViewModel;
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
using FitnesPmSuvorov.DB;

namespace FitnesPmSuvorov.View
{
    /// <summary>
    /// Логика взаимодействия для DataRepairMessage.xaml
    /// </summary>
    public partial class DataRepairMessage : Page
    {
        public RepairMessagesVM _RepairMessagesVm;

        public DataRepairMessage()
        {
            InitializeComponent();
            _RepairMessagesVm = new RepairMessagesVM();
            this.DataContext = _RepairMessagesVm;

            RepairGrid.SelectionChanged += (sender, args) =>
            {
                if (args.AddedItems.Count >= 0)
                {
                    _RepairMessagesVm.SelectedRepairMessage = args.AddedItems[0] as RepairMessages;
                    _RepairMessagesVm.OnpropertyChanged(nameof(_RepairMessagesVm.SelectedRepairMessage));
                }
            };
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _RepairMessagesVm.DeleteTrainig();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addRepair = new RepairMessageAddOrEdit(null);
            addRepair.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var RepaiMessage = new RepairMessageAddOrEdit(_RepairMessagesVm.SelectedRepairMessage);
            RepaiMessage.Show();
        }
    }
}
