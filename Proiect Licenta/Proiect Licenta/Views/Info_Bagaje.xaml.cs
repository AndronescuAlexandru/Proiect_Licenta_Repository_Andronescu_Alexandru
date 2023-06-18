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
    public partial class Info_Bagaje : ContentPage
    {
        public Info_Bagaje()
        {
            InitializeComponent();
        }
        private async void DismissButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}