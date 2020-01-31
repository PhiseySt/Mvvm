﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mvvm
{
    public class Phone : INotifyPropertyChanged
    {
        private string _title;
        private string _company;
        private int _price;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Company
        {
            get => _company;
            set
            {
                _company = value;
                OnPropertyChanged("Company");
            }
        }
        public int Price
        {
            get => _price;
            set
            {
                _price = value;
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