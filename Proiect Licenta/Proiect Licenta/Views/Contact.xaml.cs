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
    public partial class Contact : ContentPage
    {
        public static void SendEmail(string Message, string EmailAdress, string Nume, string Prenume)
        {
            try
            {
                MailMessage messageEmail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                messageEmail.From = new MailAddress("proiect_licenta_esky_travel@yahoo.com");
                messageEmail.To.Add(new MailAddress("alex_2001_22@yahoo.ro"));
                messageEmail.Subject = "Mesaj din aplicatie";
                messageEmail.Body = "Mesaj primit din aplicatia Proiect Licenta de la utilizatorul " + Nume + " " + Prenume + " " + EmailAdress + " : " + Message;
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.mail.yahoo.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("proiect_licenta_esky_travel@yahoo.com", "tirjvtdltbcswopt");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(messageEmail);
            }
            catch (Exception)
            {

            }
        }
        public Contact()
        {
            var InternetConnection = Connectivity.NetworkAccess;
            InitializeComponent();
            /*Shell.SetBackButtonBehavior(this, new BackButtonBehavior
            {
                Command = new Command(() =>
                {
                    Navigation.PopToRootAsync();
                }),
            });*/
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

        private void Button_Trimite(object sender, EventArgs e)
        {
            var InternetConnection = Connectivity.NetworkAccess;
            if (InternetConnection != NetworkAccess.Internet)
                DisplayAlert("Sunteti offline!", "Nu exista conexiune la internet. Verificati setarile de retea si incercati din nou.", "Ok");
            else
                SendEmail(EntryMessageEmail.Text, EntryEmail.Text, EntryNumeClient.Text, EntryPrenumeClient.Text);
        }

        private void EntryMessageEmail_SizeChanged(object sender, EventArgs e)
        {
            EditorBorder.HeightRequest = EntryMessageEmail.HeightRequest;
        }
    }
}