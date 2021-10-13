using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PaySystemsMobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //esta propiedad puede servir para indicar de manera lógica que se está ejecutando alguna tarea
        //por ejemplo se puede alimentar un activity indicator con esta propiedad
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        #region Implementación
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
