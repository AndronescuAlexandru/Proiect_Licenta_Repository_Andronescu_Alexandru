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
    public partial class Conditii_Rezervare : ContentPage
    {
        public Conditii_Rezervare()
        {
            InitializeComponent();
        }
        private async void DismissButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}