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

    public class AccountVM : BaseVm
    {
        public Accounts _SelectedAccount;
        public ObservableCollection<Accounts> _Accounts;

        public Accounts SelectedAccount
        {
            get => _SelectedAccount;
            set
            {
                _SelectedAccount = value;
                OnpropertyChanged(nameof(SelectedAccount));
            }
        }

        public ObservableCollection<Accounts> Accounts
        {
            get => _Accounts;
            set
            {
                _Accounts = value;
                OnpropertyChanged(nameof(Accounts));
            }
        }
        public AccountVM()
        {
            Accounts = new ObservableCollection<Accounts>();

            LoadData();
            Trace.WriteLine(Accounts);
        }

        public void LoadData()
        {
            if(_Accounts.Count > 0)
            {
                _Accounts.Clear();
            }

            var result = DbSingleTone.Db_s.Accounts.ToList();
            result.ForEach(account => Accounts.Add(account));
        }

        public void DeleteTrainig()
        {
            if (!(SelectedAccount is null))
            {
                using (var db = new RemontSpravEntities())
                {
                    var result = MessageBox.Show("вы действительно хотите удалить этот элемент?", "предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var AccountForDelete = db.Accounts.Find(SelectedAccount.AccountID);
                            if (AccountForDelete != null)
                            {
                                db.Accounts.Remove(AccountForDelete);
                                db.SaveChanges();
                                SelectedAccount = null;
                                LoadData();
                                MessageBox.Show("аккаунт успешно удалена", "успешно", MessageBoxButton.OK, MessageBoxImage.Information);
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
