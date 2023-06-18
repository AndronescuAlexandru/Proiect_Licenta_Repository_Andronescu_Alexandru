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
	public partial class Edit_Cont : ContentPage
	{
        private List<Models.User> users = new List<Models.User>();
        int oras = 0, strada = 0, nrstrada = 0, codpostal = 0, nrtelefon = 0;
        public Edit_Cont (List<Models.User> Users)
		{
			InitializeComponent ();

            users.AddRange (Users);

            Nume.Text = Models.ConnectedUser.Nume;
            Prenume.Text = Models.ConnectedUser.Prenume;
            Oras.Text = Models.ConnectedUser.Oras;
            Strada.Text = Models.ConnectedUser.Strada;
            NrStrada.Text = Models.ConnectedUser.NrStrada.ToString();
            CodPostal.Text = Models.ConnectedUser.CodPostal;
            NrTelefon.Text = Models.ConnectedUser.NrTelefon;

            Shell.SetBackButtonBehavior(this, new BackButtonBehavior //Seteaza noua comanda pentru butonul de back
            {
                Command = new Command(() =>
                {
                    Navigation.PopAsync();
                    if(oras == 1 && strada == 1 && nrstrada == 1 && nrtelefon == 1 && codpostal == 1)
                        Models.ConnectedUser.AccountDataIsCompleted = true;
                }),
            });
        }

        private void Nume_TextChanged(object sender, TextChangedEventArgs e)
        {
            Models.ConnectedUser.Nume = Nume.Text;
            Models.ConnectedUser.IsAccountEdited = true;
        }

        private void Prenume_TextChanged(object sender, TextChangedEventArgs e)
        {
            Models.ConnectedUser.Prenume = Prenume.Text;
            Models.ConnectedUser.IsAccountEdited = true;
        }

        private void Oras_TextChanged(object sender, TextChangedEventArgs e)
        {
            Models.ConnectedUser.Oras = Oras.Text;
            Models.ConnectedUser.IsAccountEdited = true;
            oras = 1;
        }

        private void Strada_TextChanged(object sender, TextChangedEventArgs e)
        {
            Models.ConnectedUser.Strada = Strada.Text;
            Models.ConnectedUser.IsAccountEdited = true;
            strada = 1;
        }

        private void NrStrada_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = NrStrada.Text;
            if (!string.IsNullOrEmpty(input) && (input != "-" && input != "+"))
                if (Convert.ToInt16(input) > 0)
                {
                    Models.ConnectedUser.NrStrada = Convert.ToUInt16(input);
                    Models.ConnectedUser.IsAccountEdited = true;
                    nrstrada = 1;
                }

        }

        private void CodPostal_TextChanged(object sender, TextChangedEventArgs e)
        {
            Models.ConnectedUser.CodPostal = CodPostal.Text;
            Models.ConnectedUser.IsAccountEdited = true;
            codpostal = 1;
        }

        private void NrTelefon_TextChanged(object sender, TextChangedEventArgs e)
        {
            Models.ConnectedUser.NrTelefon = NrTelefon.Text;
            Models.ConnectedUser.IsAccountEdited = true;
            nrtelefon = 1;
        }

        private void ChangePass_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pag_Revendicare_Parola(users,"Schimbare Parola", "Schimba parola."));
        }
    }
}