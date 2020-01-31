using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mvvm
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        Phone _selectedPhone;

        readonly IFileService _fileService;
        readonly IDialogService _dialogService;

        public ObservableCollection<Phone> Phones { get; set; }

        // команда сохранения файла
        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                  (_saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (_dialogService.SaveFileDialog() == true)
                          {
                              _fileService.Save(_dialogService.FilePath, Phones.ToList());
                              _dialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          _dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        // команда открытия файла
        private RelayCommand _openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return _openCommand ??
                  (_openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (_dialogService.OpenFileDialog() == true)
                          {
                              var phones = _fileService.Open(_dialogService.FilePath);
                              Phones.Clear();
                              foreach (var p in phones)
                                  Phones.Add(p);
                              _dialogService.ShowMessage("Файл открыт");
                          }
                      }
                      catch (Exception ex)
                      {
                          _dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        // команда добавления нового объекта
        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand(obj =>
                  {
                      var phone = new Phone();
                      Phones.Insert(0, phone);
                      SelectedPhone = phone;
                  }));
            }
        }

        private RelayCommand _removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??
                  (_removeCommand = new RelayCommand(obj =>
                  {
                      if (obj is Phone phone)
                      {
                          Phones.Remove(phone);
                      }
                  },
                 (obj) => Phones.Count > 0));
            }
        }
        private RelayCommand _doubleCommand;
        public RelayCommand DoubleCommand
        {
            get
            {
                return _doubleCommand ??
                  (_doubleCommand = new RelayCommand(obj =>
                  {
                      if (obj is Phone phone)
                      {
                          var phoneCopy = new Phone
                          {
                              Company = phone.Company,
                              Price = phone.Price,
                              Title = phone.Title
                          };
                          Phones.Insert(0, phoneCopy);
                      }
                  }));
            }
        }

        public Phone SelectedPhone
        {
            get => _selectedPhone;
            set
            {
                _selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this._dialogService = dialogService;
            this._fileService = fileService;

            // данные по умлолчанию
            Phones = new ObservableCollection<Phone>
            {
                new Phone { Title="iPhone 7", Company="Apple", Price=56000 },
                new Phone {Title="Galaxy S7 Edge", Company="Samsung", Price =60000 },
                new Phone {Title="Elite x3", Company="HP", Price=56000 },
                new Phone {Title="Mi5S", Company="Xiaomi", Price=35000 }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}