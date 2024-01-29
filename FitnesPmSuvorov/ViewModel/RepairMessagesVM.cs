using FitnesPmSuvorov.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace FitnesPmSuvorov.ViewModel
{
    public class RepairMessagesVM : BaseVm
    {
        public RepairMessagesVM()
        {
            RepairMessages = new ObservableCollection<RepairMessages>();

            LoadData();
            Trace.WriteLine(SelectedRepairMessage);
        }
            public ObservableCollection<RepairMessages> repairMessagesPrivate;
            private RepairMessages selectedRepairMessagePrivate;

            public ObservableCollection<RepairMessages> RepairMessages
            {
                get =>  repairMessagesPrivate; 
                set
                {
                    repairMessagesPrivate = value;
                    OnpropertyChanged(nameof(RepairMessages));
                }
            }

            public RepairMessages SelectedRepairMessage
            {
            get =>  selectedRepairMessagePrivate; 
                set
                {
                    selectedRepairMessagePrivate = value;
                    OnpropertyChanged(nameof(SelectedRepairMessage));
                }
            }


            public void LoadData()
        {
            if (RepairMessages?.Count > 0)
            {
                RepairMessages.Clear();
            }

            var result = DbSingleTone.Db_s.RepairMessages.ToList();
            if (result != null)
            {
                result.ForEach(a => RepairMessages.Add(a));
            }
            else
            {
                MessageBox.Show("Данных нет");
            }

        }

        public void DeleteTrainig()
        {
            Trace.WriteLine(SelectedRepairMessage);
            if (SelectedRepairMessage != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить этот элемент?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (var db = new RemontSpravEntities())
                        {
                            var trainigForDelete = db.RepairMessages.Find(SelectedRepairMessage.RepairMessageID);
                            if (trainigForDelete != null)
                            {
                                db.RepairMessages.Remove(trainigForDelete);
                                db.SaveChanges();
                                
                                LoadData(); // перезагружаем данные для обновления
                                MessageBox.Show("Решение успешно удалено", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show( "Ошибка","не выбран элемент", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }
    }
    }
