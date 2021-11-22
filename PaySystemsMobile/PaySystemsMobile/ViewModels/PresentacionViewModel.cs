using PaySystemsMobile.Models;
using PaySystemsMobile.Services.Authentication;
using PaySystemsMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PaySystemsMobile.ViewModels
{
    public class PresentacionViewModel
    {
        public ObservableCollection<CarouselObjectModel> CarouselObject { get; set; }
        public ICommand NavegaraRegistroCommand { get; set; }
        public ICommand NavegaraLoginCommand { get; set; }
        public INavigation Navigation { get; set; }    

        public PresentacionViewModel(INavigation navigation)
        {
            CarouselObject = new ObservableCollection<CarouselObjectModel>()
            {
                new CarouselObjectModel
                {
                    Foto = "img_laptopcarrito.jpg",
                    TextLabel1="Lorem\nIpsum",
                    TextLabel2="Loren Ipsum",
                },
                new CarouselObjectModel
                {
                    Foto = "img_laptopcarrito.jpg",
                    TextLabel1="Lorem\nIpsum",
                    TextLabel2="Loren Ipsum",
                },
                new CarouselObjectModel
                {
                    Foto = "img_laptopcarrito.jpg",
                    TextLabel1="Lorem\nIpsum",
                    TextLabel2="Loren Ipsum",
                },
                new CarouselObjectModel
                {
                    Foto = "img_laptopcarrito.jpg",
                    TextLabel1="Lorem\nIpsum",
                    TextLabel2="Loren Ipsum",
                },
            };
            NavegaraRegistroCommand = new Command(async() => await Navigation.PushAsync(new RegistroView()));
            NavegaraLoginCommand = new Command(async () => await Navigation.PushAsync(new LoginView()));
            Navigation = navigation;            
        }          
    }    
}
