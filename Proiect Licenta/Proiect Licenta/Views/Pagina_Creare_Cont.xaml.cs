using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Proiect_Licenta.Models;

namespace Proiect_Licenta.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Pagina_Creare_Cont : ContentPage
	{
		short CodEmail, nume = 0, prenume = 0, email = 0, parola = 0,tara = 0, oras = 0, strada = 0, nr_strada = 0, cod_postal = 0, nr_telefon = 0;
		private List<Models.User> users = new List<Models.User>();

		bool check = true;

		SqlConnection sql;
		public static void SendEmail(string Message, string EmailAdress)
        {
            try
            {
				MailMessage messageEmail = new MailMessage();
				SmtpClient smtpClient = new SmtpClient();
				messageEmail.From = new MailAddress("proiect_licenta_esky_travel@yahoo.com");
				messageEmail.To.Add(new MailAddress(EmailAdress));
				messageEmail.Subject = "Codul pentru verificarea contului al aplicatiei Proiect Licenta";
				messageEmail.Body = Message;
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
        public Pagina_Creare_Cont (List<Models.User> Users)
		{
			InitializeComponent ();

			users.AddRange(Users);

			Shell.SetBackButtonBehavior(this, new BackButtonBehavior
			{
				Command = new Command(() =>
				{
					Navigation.PopToRootAsync();
					Navigation.PushAsync(new Profil());
				}),
			});

			try
			{
				string srv_db_name = "DB_Proiect_Licenta";
				string srv_ip = "192.168.1.2";
				string srv_username = "Alex";
				string srv_password = "Alex1234";

				string sql_connection = $"Data Source={srv_ip};Initial Catalog={srv_db_name};User Id={srv_username};Password={srv_password}";

				sql = new SqlConnection(sql_connection);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		private void CreareCont(object sender, EventArgs e)
        {
			Random randomNumber = new Random();
			CodEmail = (short)randomNumber.Next(1, 10000);
			string msg = "Codul dvs. pentru activarea contului este " + CodEmail.ToString();
			if (nume == 1 && prenume == 1 && email == 1 && parola == 1)
            {
				foreach(Models.User U in users)
                {
					if (U.Email == AdresaEmailCont.Text)
					{
						DisplayAlert("Adresa de email invalidă", "Există deja un cont cu adresa de email introdusă. Vă rugăm să folosiți altă adresă.", "Ok");
						check = false;
					}

                }
				if(check == true)
                {
					SendEmail(msg, AdresaEmailCont.Text);
					HeaderPagina.Text = "Introduceti codul primit la adresa de e-mail pentru a valida contul";
					NumeCont.IsVisible = false;
					NumeCont.IsEnabled = false;
					PrenumeCont.IsEnabled = false;
					PrenumeCont.IsVisible = false;
					AdresaEmailCont.IsEnabled = false;
					AdresaEmailCont.IsVisible = false;
					ParolaCont.IsEnabled = false;
					ParolaCont.IsVisible = false;
					OrasCont.IsVisible = false;
					StradaCont.IsVisible = false;
					Nr_StradaCont.IsVisible = false;
					Cod_PostalCont.IsVisible = false;
					Nr_TelefonCont.IsVisible = false;
					TaraCont.IsVisible = false;
					ButonConectareCont.IsEnabled = false;
					ButonConectareCont.IsVisible = false;
					ButonCreareCont.IsEnabled = false;
					ButonCreareCont.IsVisible = false;
					LabelConectareCont.IsVisible = false;
					EntryCodEmail.IsVisible = true;
					EntryCodEmail.IsEnabled = true;
					ButonCodEmail.IsVisible = true;
					ButonCodEmail.IsEnabled = true;
					ButonCodEmail.BackgroundColor = Color.FromHex("#4383b2");
				}
			}
			else
				DisplayAlert("Câmpuri incomplete", "Nu ați completat toate câmpurile de mai sus. Va rugăm să introduceți datele lipsă.", "Ok");
			check = true;

		}

		private void AdaugareCont(object sender, EventArgs e)
        {
			Random randomNumber = new Random();
			if (CodEmail == Convert.ToInt16(EntryCodEmail.Text))
			{
				try
				{
					sql.Open();
					using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Users VALUES(@ID,@Nume,@Prenume,@Email,@Password,@Tara,@Oras,@Strada,@Nr_Strada,@Cod_Postal,@Nr_Telefon)", sql))
					{
						cmd.Parameters.Add(new SqlParameter("ID", randomNumber.Next(1,9999999)));
						cmd.Parameters.Add(new SqlParameter("Nume", NumeCont.Text.ToString()));
						cmd.Parameters.Add(new SqlParameter("Prenume", PrenumeCont.Text.ToString()));
						cmd.Parameters.Add(new SqlParameter("Email", AdresaEmailCont.Text.ToString()));
						cmd.Parameters.Add(new SqlParameter("Password", ParolaCont.Text.ToString()));
						cmd.Parameters.Add(new SqlParameter("Tara", TaraCont.Text));
						cmd.Parameters.Add(new SqlParameter("Oras", OrasCont.Text.ToString()));
						cmd.Parameters.Add(new SqlParameter("Strada", StradaCont.Text.ToString()));
						cmd.Parameters.Add(new SqlParameter("Nr_Strada", Convert.ToInt32(Nr_StradaCont.Text)));
						cmd.Parameters.Add(new SqlParameter("Cod_Postal", Cod_PostalCont.Text.ToString()));
						cmd.Parameters.Add(new SqlParameter("Nr_Telefon", Nr_TelefonCont.Text.ToString()));
						cmd.ExecuteNonQuery();
					};
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}

				ConnectedUser.Nume = NumeCont.Text;
				ConnectedUser.Prenume = PrenumeCont.Text;
				ConnectedUser.Password = ParolaCont.Text;
				ConnectedUser.Email = AdresaEmailCont.Text;
				ConnectedUser.IsNowConnected = true;
				ConnectedUser.IsEmptyUser = false;
				ConnectedUser.IsFromCreareCont = true;
				sql.Close();
				Navigation.PopToRootAsync();
			}
			else
			{
				DisplayAlert("Cod incorect", "Din pacate ati introdus un cod gresit!. Va rugăm să introduceți codul primit la adresa de email introdusă.", "Ok");
			}

		}

		private void Conectare(object sender, EventArgs e)
        {
			Navigation.PushAsync(new Pagina_Login(users));
        }

        private void NumeCont_Completed(object sender, EventArgs e)
        {
			nume = 1;
        }

        private void PrenumeCont_Completed(object sender, EventArgs e)
        {
			prenume = 1;
        }

        private void AdresaEmailCont_Completed(object sender, EventArgs e)
        {
			email = 1;
        }

        private void ParolaCont_Completed(object sender, EventArgs e)
        {
			parola = 1;
        }
		private void TaraCont_Completed(object sender, EventArgs e)
		{
			tara = 1;
		}

		private void OrasCont_Completed(object sender, EventArgs e)
		{
			oras = 1;
		}

		private void StradaCont_Completed(object sender, EventArgs e)
		{
			strada = 1;
		}

		private void Nr_StradaCont_Completed(object sender, EventArgs e)
		{
			nr_strada = 1;
		}
		private void Cod_PostalCont_Completed(object sender, EventArgs e)
		{
			cod_postal = 1;
		}

		private void Nr_TelefonCont_Completed(object sender, EventArgs e)
		{
			nr_telefon = 1;
		}
		
	}
}