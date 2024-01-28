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
    public class RepairMessagesVM : BaseVm
    {
        private RepairMessages _selectedRepairMessages;
        public ObservableCollection<RepairMessages> _RepairMessages;

        public RepairMessages SelectedRepairMessages
        {
            get => _selectedRepairMessages;
            set
            {
                _selectedRepairMessages = value;
                OnpropertyChanged(nameof(SelectedRepairMessages));
            }
        }

        public ObservableCollection<RepairMessages> RepairMessagesCollection
        {
            get => _RepairMessages;
            set
            {
                _RepairMessages = value;
                OnpropertyChanged(nameof(RepairMessagesCollection));
            }
        }

        public RepairMessagesVM()
        {
            RepairMessagesCollection = new ObservableCollection<RepairMessages>();

            LoadData();
            Trace.WriteLine(RepairMessagesCollection);
        }

        public void LoadData()
        {
            if (_RepairMessages.Count > 0)
            {
                _RepairMessages.Clear();
            }

            var result = DbSingleTone.Db_s.RepairMessages.ToList();
            if (result != null)
            {
                result.ForEach(a => RepairMessagesCollection.Add(a));
            }
            else
            {
                MessageBox.Show("Данных нет");
            }

        }

        public void DeleteTrainig()
        {
            if (SelectedRepairMessages is null)
            {
                MessageBox.Show("Выберите объект для удаления", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить этот элемент?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var db = new RemontSpravEntities())
                    {
                        var trainigForDelete = db.RepairMessages.Find(SelectedRepairMessages.RepairMessageID);
                        if (trainigForDelete != null)
                        {
                            db.RepairMessages.Remove(trainigForDelete);
                            db.SaveChanges();
                            SelectedRepairMessages = null; // сбрасываем текущий выбранный элемент на null
                            LoadData(); // перезагружаем данные для обновления
                            MessageBox.Show("Тренировка успешно удалена", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
    }
