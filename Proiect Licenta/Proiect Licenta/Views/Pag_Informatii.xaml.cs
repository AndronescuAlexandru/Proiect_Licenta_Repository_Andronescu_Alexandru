using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proiect_Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pag_Informatii : ContentPage
    {
        public static void SendEmail(int Rating)
        {
            try
            {
                MailMessage messageEmail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                messageEmail.From = new MailAddress("andronescudaniela@yahoo.com");
                messageEmail.To.Add(new MailAddress("alex_2001_22@yahoo.ro"));
                messageEmail.Subject = "Mesaj din aplicatie";
                messageEmail.Body = "Ai primit un rating de " + Rating.ToString() + " stele ";
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.mail.yahoo.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("andronescudaniela@yahoo.com", "nbfzjiprmyfrpasr");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(messageEmail);
            }
            catch (Exception)
            {

            }
        }
        public Pag_Informatii()
        {
            var InternetConnection = Connectivity.NetworkAccess;
            InitializeComponent();
            if (InternetConnection != NetworkAccess.Internet)
                ToolbarItems.Add(new ToolbarItem("Sunteti offline!", "", () =>
                {
                }));
            else if (Models.ConnectedUser.IsNowConnected == false && InternetConnection == NetworkAccess.Internet)
                ToolbarItems.Add(new ToolbarItem("Sign In", "Sign In", () =>
                {
                    Navigation.PushAsync(new Profil());
                }));
        }

        private void Button_Contact(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Contact());
        }
        private void Button_Evaluare(object sender, EventArgs e)
        {
            RatingBar.IsVisible = true;
            TrimiteEvaluarea.IsVisible = true;
        }

        private void TrimiteEvaluarea_Clicked(object sender, EventArgs e)
        {
            var InternetConnection = Connectivity.NetworkAccess;
            if (InternetConnection != NetworkAccess.Internet)
                DisplayAlert("Sunteti offline!", "Nu exista conexiune la internet. Verificati setarile de retea si incercati din nou.", "Ok");
            else
            {
                SendEmail(RatingBar.SelectedStarValue);
                TrimiteEvaluarea.IsEnabled = false;
                TrimiteEvaluarea.IsVisible = false;
                RatingBar.IsVisible = false;
            }
        }

        private void Redirect_Termeni_si_Conditii(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Termeni_si_Conditii(0));
        }

        private void Redirect_Despre_Aplicatie(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Despre_Aplicatie());
        }
    }
}