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
    /// Логика взаимодействия для DatePreviewPage.xaml
    /// </summary>
    public partial class DatePreviewPage : Page
    {
        public MessagesVM _PreviewPagesVm;
        public DatePreviewPage()
        {
            InitializeComponent();
            _PreviewPagesVm = new MessagesVM();
            this.DataContext = _PreviewPagesVm;
            DataGridPreview.SelectionChanged += (sender, args) =>
            {
                if (args.AddedItems.Count > 0)
                {
                    _PreviewPagesVm.SelectedTraining = args.AddedItems[0] as PreviewPages;
                    _PreviewPagesVm.OnpropertyChanged(nameof(_PreviewPagesVm.SelectedTraining));
                }
            };
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _PreviewPagesVm.DeleteTrainig();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addRepair = new PreviewPageAddOrEdit(null);
            addRepair.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var RepaiMessage = new PreviewPageAddOrEdit(_PreviewPagesVm.SelectedTraining);
            RepaiMessage.Show();
        }

    }
}
