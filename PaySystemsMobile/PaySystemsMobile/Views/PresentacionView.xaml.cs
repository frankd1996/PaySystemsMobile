using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PaySystemsMobile.AppConstant;
using Xamarin.Auth;
using Newtonsoft.Json;
using PaySystemsMobile.AuthHelpers;
using System.Diagnostics;
using PaySystemsMobile.ViewModels;

namespace PaySystemsMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PresentacionView : ContentPage
	{
		public PresentacionView()
        {			
			InitializeComponent();			
			this.BindingContext = new PresentacionViewModel(Navigation);						
		}

        /// <summary>
		/// Método que se ejecuta un instante antes que el evento SizeChanged de la página.
		/// Aquí controlamos si el Label2 del carrusel es visible, en función de la orietación 
		/// de la pantalla.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
			DisplayInfo mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
			if (mainDisplayInfo.Orientation == DisplayOrientation.Landscape)
            {
				this.Resources["Label2IsVisible"] = false;
			}
			else if (mainDisplayInfo.Orientation == DisplayOrientation.Portrait)
			{
				this.Resources["Label2IsVisible"] = true;
			}
		}                
    }
}