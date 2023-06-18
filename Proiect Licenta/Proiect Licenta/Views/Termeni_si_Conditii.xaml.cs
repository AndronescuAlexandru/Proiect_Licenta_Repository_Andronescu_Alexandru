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
    public partial class Termeni_si_Conditii : ContentPage
    {
        public Termeni_si_Conditii(int choice)
        {
            InitializeComponent();

            if (choice == 0)
                dismissButton.IsVisible = false;
            else dismissButton.IsVisible = true;

            Shell.SetBackButtonBehavior(this, new BackButtonBehavior //Seteaza noua comanda pentru butonul de back
            {
                Command = new Command(() =>
                {
                    Navigation.PopToRootAsync();
                }),
                IconOverride = "back.png"
            });
        }
        private async void DismissButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}