using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PaySystemsMobile.Models
{
    public class CarouselObjectModel:INotifyPropertyChanged
    {
        private string _foto;
        private string _textLabel1;
        private string _textLabel2;

        public string Foto
        {
            get { return _foto; }
            set
            {
                _foto = value;
                OnPropertyChanged();
            }
        }
        public string TextLabel1
        {
            get => _textLabel1;
            set
            {
                _textLabel1 = value;
                OnPropertyChanged();
            }
        }
        public string TextLabel2
        {
            get => _textLabel2;
            set
            {
                _textLabel2 = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
