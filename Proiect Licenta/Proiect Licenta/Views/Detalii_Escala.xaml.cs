using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proiect_Licenta.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Detalii_Escala : ContentPage
	{
		ViewModels.BiletViewModel Bilet = new ViewModels.BiletViewModel();
		public Detalii_Escala ()
		{
			InitializeComponent ();
			BindingContext = Bilet;
			Shell.SetBackButtonBehavior(this, new BackButtonBehavior //Seteaza noua comanda pentru butonul de back
			{
				Command = new Command(() =>
				{
					Navigation.PopAsync();
				}),
			});
		}
	}
}