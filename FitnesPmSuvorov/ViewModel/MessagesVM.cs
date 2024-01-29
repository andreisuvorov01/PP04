using FitnesPmSuvorov.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitnesPmSuvorov.ViewModel 
{
    public class MessagesVM : BaseVm
    {
        #region привязка полей
        private PreviewPages _SelectedTraining;
        public ObservableCollection<PreviewPages> _RepairMessages;

        public PreviewPages SelectedTraining
        {
            get => _SelectedTraining;
            set
            {
                _SelectedTraining = value;
                OnpropertyChanged(nameof(SelectedTraining));
            }
        }

        public ObservableCollection<PreviewPages> MessageCollection
        {
            get => _RepairMessages;
            set
            {
                _RepairMessages = value;
                OnpropertyChanged(nameof(MessageCollection));
            }
        }

        
        #endregion

        public MessagesVM()
        {
            MessageCollection = new ObservableCollection<PreviewPages>();

            LoadData();
            Trace.WriteLine(SelectedTraining);
        }

        public void LoadData()
        {
            if (_RepairMessages.Count > 0)
            {
                _RepairMessages.Clear();
            }

            var result = DbSingleTone.Db_s.PreviewPages.ToList();

            result.ForEach(a => MessageCollection.Add(a));
        }

        public void DeleteTrainig()
        {
            if (!(SelectedTraining is null))
            {
                using (var db = new RemontSpravEntities())
                {
                    var result = MessageBox.Show("вы действительно хотите удалить этот элемент?", "предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var TrainigForDelete = db.RepairMessages.Find(SelectedTraining.RepairMessageID);
                            if (TrainigForDelete != null)
                            {
                                db.RepairMessages.Remove(TrainigForDelete);
                                db.SaveChanges();
                                
                                LoadData();
                                MessageBox.Show("Краткое описание ошибки успешно удалено", "успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите объект для удаения", "предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
