using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mvvm
{
    public class PhoneViewModel : INotifyPropertyChanged
    {
        private readonly Phone _phone;

        public PhoneViewModel(Phone p)
        {
            _phone = p;
        }

        public string Title
        {
            get => _phone.Title;
            set
            {
                _phone.Title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Company
        {
            get => _phone.Company;
            set
            {
                _phone.Company = value;
                OnPropertyChanged("Company");
            }
        }
        public int Price
        {
            get => _phone.Price;
            set
            {
                _phone.Price = value;
                OnPropertyChanged("Price");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}