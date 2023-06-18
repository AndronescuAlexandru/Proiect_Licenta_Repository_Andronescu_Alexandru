using Proiect_Licenta.ViewModels;
using Proiect_Licenta.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Proiect_Licenta
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Rezervari), typeof(Rezervari));
            Routing.RegisterRoute(nameof(Pag_Cautare), typeof(Pag_Cautare));
            Routing.RegisterRoute(nameof(Profil), typeof(Profil));

        }

        private async void ProfilMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Profil");
        }
    }
}
