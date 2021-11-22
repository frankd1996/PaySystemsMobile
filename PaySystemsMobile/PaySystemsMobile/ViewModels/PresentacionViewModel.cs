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
                    TextLabel1="Compra en línea\npagando con tu\nmoneda local",
                    TextLabel2="Compra donde quieras, sin\ntarjeta de crédito",
                },
                new CarouselObjectModel
                {
                    Foto = "img_laptopfondoverde.png",
                    TextLabel1="Escoge tu plataforma\nde confianza",
                    TextLabel2="Compra para tí o tu negocio,\nen minutos",
                },
                new CarouselObjectModel
                {
                    Foto = "img_streaming.jpg",
                    TextLabel1="Streaming sin\nlímites",
                    TextLabel2="Adquiere tus plataformas favoritas\nde entretenimiento, con un click",
                },
                new CarouselObjectModel
                {
                    Foto = "img_laptopcarrito1.jpg",
                    TextLabel1="Paga como\nquieras",
                    TextLabel2="No te preocupes más por la forma pago\n¡Feliz compra!",
                }
            };
            NavegaraRegistroCommand = new Command(async() => await Navigation.PushAsync(new RegistroView()));
            NavegaraLoginCommand = new Command(async () => await Navigation.PushAsync(new LoginView()));
            Navigation = navigation;            
        }          
    }    
}
