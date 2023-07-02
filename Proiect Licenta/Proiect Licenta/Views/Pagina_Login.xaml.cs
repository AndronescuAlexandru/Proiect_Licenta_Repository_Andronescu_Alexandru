using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Data.SqlClient;

namespace Proiect_Licenta.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Pagina_Login : ContentPage
	{
		private List<Models.User> users = new List<Models.User>();
		SqlConnection sql;

		public Pagina_Login (List<Models.User> Users)
		{
			InitializeComponent();		
			users.AddRange(Users);

			Shell.SetBackButtonBehavior(this, new BackButtonBehavior
			{
				Command = new Command(() =>
				{
					Navigation.PopToRootAsync ();
					Navigation.PushAsync(new Profil());
				}),
			});

		}
		private async void Login(object sender, EventArgs e)
        {
			int c = 0;
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

			sql.Open();

			foreach (Models.User U in users)
			{
				if ((EntryAdresaEmail.Text == U.Email) && (EntryParola.Text == U.Password))
				{
					string queryString = "Select * from dbo.BileteRezervate";
					SqlCommand cmd = new SqlCommand(queryString, sql);
					SqlDataReader reader = cmd.ExecuteReader();
					Models.ConnectedUser.ListaBilete.Clear();
					while (reader.Read())
					{
						if (reader["Email"].ToString() == U.Email)
							Models.ConnectedUser.ListaBilete.Add(new Bilet
							{
								Id = Convert.ToUInt32(reader["ID_Bilet"]),
								Clasa = reader["Clasa"].ToString(),
								LocPlecare = reader["Loc_Plecare"].ToString(),
								Destinatie = reader["Destinatie"].ToString(),
								Transport = reader["Tip_Transport"].ToString(),
								DataPlecare = reader["Data_Plecare"].ToString(),
								DataIntoarcere = reader["Data_Retur"].ToString(),
								TipBilet = reader["Tip_Bilet"].ToString(),
								Pasager = reader["Tip_Pasager"].ToString(),
								Scaun = reader["Scaun"].ToString(),
								Scaun2 = reader["Scaun_Retur"].ToString(),
								Nume = reader["Nume"].ToString(),
								Prenume = reader["Prenume"].ToString(),
								OraPlecare = reader["Ora_Plecare"].ToString(),
								OraIntoarcere = reader["Ora_Retur"].ToString(),
								NrTransport = reader["Nr_Transport"].ToString(),
								NrTransport2 = reader["Nr_Transport_Retur"].ToString(),
								//Peron = Convert.ToByte(reader["Peron"]),
								//Peron2 = Convert.ToByte(reader["Peron_Retur"]),
								QRCodeTextValue = reader["QR_Code"].ToString(),
								Activ = Convert.ToBoolean(reader["Activ"]),
							});
					}

					reader.Close();

					queryString = "Select * from dbo.Facturi";
					cmd = new SqlCommand(queryString, sql);
					reader = cmd.ExecuteReader();
					Models.ConnectedUser.ListaFacturi.Clear();
					while (reader.Read())
					{
						if (reader["Email"].ToString() == U.Email)
							Models.ConnectedUser.ListaFacturi.Add(new Models.Factura
							{
								Id = Convert.ToUInt32(reader["ID"]),
								Nume = reader["Nume"].ToString(),
								Prenume = reader["Prenume"].ToString(),
								Email = reader["Email"].ToString(),
								IdBilet = Convert.ToUInt32(reader["Id_Bilet"]),
								SumaDePlata = Convert.ToUInt16(reader["Suma_De_Plata"]),
								Tara = reader["Tara"].ToString(),
								Oras = reader["Oras"].ToString(),
								Strada = reader["Strada"].ToString(),
								NrStrada = Convert.ToUInt16(reader["Nr_Strada"]),
								CodPostal = reader["Cod_Postal"].ToString(),
								NrTelefon = reader["Nr_Telefon"].ToString(),
								PlecareDin = reader["Loc_Plecare"].ToString(),
								Destinatie = reader["Destinatie"].ToString(),
								TipTransport = reader["Tip_Transport"].ToString(),
								TipBilet = reader["Tip_Bilet"].ToString(),
								IsMadeWithoutAccount = Convert.ToBoolean(reader["Is_Made_Without_Account"]),
							});
					}

					sql.Close();

					Models.ConnectedUser.Email = U.Email;
					Models.ConnectedUser.Password = U.Password;
					Models.ConnectedUser.Prenume = U.Prenume;
					Models.ConnectedUser.Nume = U.Nume;
					Models.ConnectedUser.Id = U.Id;
					Models.ConnectedUser.CodPostal = U.CodPostal;
					Models.ConnectedUser.Oras = U.Oras;
					Models.ConnectedUser.Strada = U.Strada;
					Models.ConnectedUser.IsEmptyUser = false;
					Models.ConnectedUser.IsNowConnected = true;
					Models.ConnectedUser.NrStrada = U.NrStrada;
					Models.ConnectedUser.NrTelefon = U.NrTelefon;
					Models.ConnectedUser.Tara = U.Tara;
					Models.ConnectedUser.Username = U.Username;
					Models.ConnectedUser.IsFromLogin = true;

					await Navigation.PopAsync();
				}
				else
					c++;
					
			}
			if(c == users.Count)
				DisplayAlert("Parolă sau email gresită", "Din păcate ați introdus o parolă sau o adresă de email greșită. Va rugăm să verificați datele introduse.", "Ok");
			//c = 0;
		}
		private async void Creare(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new Pagina_Creare_Cont(users));
		}

        private async void Button_AmUitatParola(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new Pag_Revendicare_Parola(users, "Ati uitat parola?", "Reseteaza parola."));
        }
    }
}