using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Net.Mail;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proiect_Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pag_Revendicare_Parola : ContentPage
    {
        List<Models.User> users = new List<Models.User>();
        short CodEmail;
        public static void SendEmail(string Message, string EmailAdress)
        {
            try
            {
                MailMessage messageEmail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                messageEmail.From = new MailAddress("alex_2001_22@yahoo.ro");
                messageEmail.To.Add(new MailAddress(EmailAdress));
                messageEmail.Subject = "Codul pentru verificarea contului al aplicatiei Proiect Licenta";
                messageEmail.Body = Message;
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.mail.yahoo.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("alex_2001_22@yahoo.ro", "fubeempiwinoyakc");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(messageEmail);
            }
            catch (Exception)
            {

            }
        }
        public Pag_Revendicare_Parola(List<Models.User> Users, string Header, string PassResetButton)
        {
            InitializeComponent();
            foreach (Models.User U in Users)
            {
                users.Add(new Models.User
                {
                    Nume = U.Nume,
                    Password = U.Password,
                    Prenume = U.Prenume,
                    Email = U.Email,
                    IsEmptyUser = U.IsEmptyUser,
                    IsNowConnected = U.IsNowConnected,
                });
            }
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior
            {
                Command = new Command(() =>
                {
                    Navigation.PopToRootAsync();
                    Navigation.PushAsync(new Profil());
                }),
            });
            HeaderPagina.Text = Header;
            ButonSendPassResetEmail.Text = PassResetButton;
            ResetareParola.Text = PassResetButton;
        }
        private void Timer(int Seconds)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            if (timer.Elapsed.Seconds == Seconds)
                LabelCodGresit.IsVisible = false;
        }
        private void ButonSendPassResetEmail_Clicked(object sender, EventArgs e)
        {
            Random randomNumber = new Random();
            CodEmail = (short)randomNumber.Next(1, 10000);
            string msg = "Codul dvs. pentru activarea contului este " + CodEmail.ToString();
            foreach(Models.User U in users)
            {
                if(U.Email == AdresaEmailCont.Text)
                {
                    SendEmail(msg, AdresaEmailCont.Text);
                    HeaderPagina.Text = "Introduceți codul primit la adresa de email pentru a valida contul";
                    Msg1.IsVisible = false;
                    Msg2.IsVisible = false;
                    AdresaEmailCont.IsEnabled = false;
                    AdresaEmailCont.IsVisible = false;
                    ButonSendPassResetEmail.IsVisible = false;
                    ButonSendPassResetEmail.IsEnabled = false;
                    EntryCodEmail.IsVisible = true;
                    EntryCodEmail.IsEnabled = true;
                    ButonCodEmail.IsVisible = true;
                    ButonCodEmail.IsEnabled = true;
                    ButonCodEmail.BackgroundColor = Color.FromHex("#248cd9");
                }
                else
                {
                    LabelCodGresit.Text = "Nu există un cont cu adresa de e-mail introdusă.";
                    LabelCodGresit.IsVisible = true;
                    Timer(5);
                }
            }
        }

        private void ButonCodEmail_Clicked(object sender, EventArgs e)
        {
            if (CodEmail == Convert.ToInt16(EntryCodEmail.Text))
            {
                HeaderPagina.Text = "Introduceți noua parolă.";
                EntryCodEmail.IsVisible = false;
                EntryCodEmail.IsEnabled = false;
                ButonCodEmail.IsVisible = false;
                ButonCodEmail.IsEnabled = false;
                ParolaVecheCont.IsEnabled = true;
                ParolaVecheCont.IsVisible = true;
                ParolaNouaCont.IsEnabled = true;
                ParolaNouaCont.IsVisible = true;
                ConfirmareParolaCont.IsEnabled = true;
                ConfirmareParolaCont.IsVisible = true;
                ResetareParola.IsEnabled = true;
                ResetareParola.IsVisible = true;
                ResetareParola.BackgroundColor = Color.FromHex("#248cd9");
                foreach (Models.User U in users)
                {
                    if(AdresaEmailCont.Text == U.Email)
                    {
                        Models.ConnectedUser.Nume = U.Nume;
                        Models.ConnectedUser.Prenume = U.Prenume;
                        Models.ConnectedUser.Password = U.Password;
                        Models.ConnectedUser.Email = U.Email;
                        Models.ConnectedUser.IsNowConnected = false;
                        Models.ConnectedUser.IsEmptyUser = false;
                    }
                }
                LabelCodGresit.IsVisible = false;
            }
            else
            {
                LabelCodGresit.Text = "Din pacate ați introdus un cod greșit!";
                LabelCodGresit.IsVisible = true;
                Timer(5);
            }
        } 
        private void ResetareParola_Clicked(object sender, EventArgs e)
        {
            Stopwatch timer = new Stopwatch();
            if(ParolaVecheCont.Text == Models.ConnectedUser.Password && ParolaNouaCont.Text == ConfirmareParolaCont.Text)
            {
                Models.ConnectedUser.Password = ParolaNouaCont.Text;
                Models.ConnectedUser.IsPassChanged = true;
                LabelCodGresit.IsVisible = false;
                Navigation.PopToRootAsync();
            }
            else
            {
                LabelCodGresit.Text = "Unul din câmpurile de mai sus nu este completat corect.";
                LabelCodGresit.IsVisible = true;
                Timer(5);
            }
        }
    }
}