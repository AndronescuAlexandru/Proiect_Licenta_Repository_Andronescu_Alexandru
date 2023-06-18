using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proiect_Licenta.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home_Page : ContentPage
    {
		void RedirectCautareAvion(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pag_Cautare(1));
        }
        void RedirectCautareTren(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pag_Cautare(0));
        }
        void RedirectCautareAutocar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pag_Cautare(2));
        }
		void ImageClicked(object sender, EventArgs e)
        {
			var button = (ImageButton)sender;
			var bilet = (Oferta)button.BindingContext;
			Random numarR = new Random();
			Mesager.Id = bilet.Id;
			Mesager.Clasa = bilet.Clasa;
			Mesager.TipBilet = bilet.TipBilet;
			Mesager.IsAvion = bilet.IsAvion;
			Mesager.Transport = bilet.Transport;
			Mesager.Pasager = bilet.Pasager;
			Mesager.Pret = bilet.Pret;
			Mesager.oldPret = bilet.Pret;
			Mesager.Activ = true;
			Mesager.DataPlecare =bilet.DataPlecare;
			Mesager.DataIntoarcere = bilet.DataIntoarcere;
			Mesager.Destinatie = bilet.Destinatie;
			Mesager.Plecare = bilet.LocPlecare;
			Mesager.NrTransport = bilet.NrTransport;
			Mesager.Distance = bilet.Distance;
			Mesager.DurataDrum = bilet.DurataDrum;
			Mesager.OraPlecare = bilet.OraPlecare;
			Mesager.OraIntoarcere = bilet.OraIntoarcere;
			Mesager.Peron = bilet.Peron;
			Mesager.Scaun = bilet.Scaun;
			Mesager.Icon = bilet.Icon;
			Mesager.Icon2 = bilet.Icon2;
			Mesager.Color = bilet.Color;
			Mesager.IsFromOferta = true;
			if (bilet.TipBilet == "Dus")
				Mesager.IsIntoarcere = false;
			else
			{
				Mesager.IsIntoarcere = true;
				Mesager.Peron2 = bilet.Peron2;
				Mesager.Scaun2 = bilet.Scaun2;
				Mesager.NrTransport2 = bilet.NrTransport2;
			}			
			//Mesager.Icon = bilet.Icon;
			Navigation.PushAsync(new Rezultate_Cautare());
        }
		public async void BookingSearch(object sender, System.EventArgs e)
        {
            try
            {
				await Browser.OpenAsync("https://www.booking.com/", new BrowserLaunchOptions
				{
					LaunchMode = BrowserLaunchMode.SystemPreferred,
					TitleMode = BrowserTitleMode.Show,
					PreferredToolbarColor = Color.AliceBlue,
					PreferredControlColor = Color.Violet
				});
			}
			catch (Exception)
            {

            }
        }
		private async void RentalCars(object sender, EventArgs e)
		{
			try
			{
				await Browser.OpenAsync("https://www.rentalcars.com/", new BrowserLaunchOptions
				{
					LaunchMode = BrowserLaunchMode.SystemPreferred,
					TitleMode = BrowserTitleMode.Show,
					PreferredToolbarColor = Color.AliceBlue,
					PreferredControlColor = Color.Violet
				});
			}
			catch (Exception)
			{

			}
		}
		private async void AsigurariSanatate(object sender, EventArgs e)
		{
			try
			{
				await Browser.OpenAsync("https://www.asigurari.ro/asigurare/travel", new BrowserLaunchOptions
				{
					LaunchMode = BrowserLaunchMode.SystemPreferred,
					TitleMode = BrowserTitleMode.Show,
					PreferredToolbarColor = Color.AliceBlue,
					PreferredControlColor = Color.Violet
				});
			}
			catch (Exception)
			{

			}
		}

		protected override void OnAppearing()
        {
			var InternetConnection = Connectivity.NetworkAccess;

			if (Models.ConnectedUser.IsNowConnected != false && InternetConnection == NetworkAccess.Internet && ToolbarItems.Count>0)
				ToolbarItems.RemoveAt(0);
			else if(Models.ConnectedUser.IsNowConnected == false && InternetConnection == NetworkAccess.Internet && ToolbarItems.Count == 0)
				ToolbarItems.Add(new ToolbarItem("Sign In", "Sign In", () =>
				{
					Navigation.PushAsync(new Profil());
				}));
		}

		public Home_Page()
        {
			var InternetConnection = Connectivity.NetworkAccess;
            InitializeComponent();
			if (InternetConnection != NetworkAccess.Internet)
				ToolbarItems.Add(new ToolbarItem("Sunteți offline!", "", () =>
				{
				}));
			else
			{
				BindingContext = new ViewModels.OferteViewModel(6);

				ToolbarItems.Add(new ToolbarItem("Sign In", "Sign In", () =>
				{
					Navigation.PushAsync(new Profil());
				}));
			}
		}
    }
}