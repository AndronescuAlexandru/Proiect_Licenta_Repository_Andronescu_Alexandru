using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ZXing.Net.Mobile.Forms;
using ZXing.Net.Mobile;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proiect_Licenta.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profil : ContentPage
    {
        List<Models.User> Users = new List<Models.User>();
        SqlConnection sql;
        public Pagina_Creare_Cont Calling { get; set; }

        protected override void OnAppearing()
        {
            var InternetConnection = Connectivity.NetworkAccess;

            if (InternetConnection != NetworkAccess.Internet)
            {
                ToolbarItems.RemoveAt(0);
                ToolbarItems.Add(new ToolbarItem("Sunteți offline!", "", () =>
                {
                }));
            }

            if (Models.ConnectedUser.IsFromCreareCont == true)
            {
                Users.Add(new Models.User
                {
                    Nume = Models.ConnectedUser.Nume,
                    Prenume = Models.ConnectedUser.Prenume,
                    Email = Models.ConnectedUser.Email,
                    Password = Models.ConnectedUser.Password,
                    IsNowConnected = Models.ConnectedUser.IsNowConnected,
                    IsEmptyUser = false
                });
                Models.ConnectedUser.IsFromCreareCont = false;
            }
            if (Models.ConnectedUser.IsFromLogin == true)
            {
                foreach (Models.User U in Users)
                {
                    if (Models.ConnectedUser.Email == U.Email)
                    {
                        Models.ConnectedUser.IsEmptyUser = false;
                        Models.ConnectedUser.IsNowConnected = true;
                        Models.ConnectedUser.ListaBilete.AddRange(U.ListaBilete);
                    }
                }
            }
            if (Models.ConnectedUser.IsEmptyUser == false && Models.ConnectedUser.IsNowConnected == true)
            {
                UserNotFoundImg.IsVisible = false;
                AccountMsg1.IsVisible = false;
                AccountMsg4.Text = "Sunteti conectat la urmatorul cont : " + Models.ConnectedUser.Email;
                AccountMsg3.IsVisible = true;
                AccountMsg4.IsVisible = true;
                AccountMsg2.IsVisible = false;
                ButtonConectare.IsVisible = false;
                ButtonConectare.IsEnabled = false;
                ButtonCreareCont.IsVisible = false;
                ButtonCreareCont.IsEnabled = false;
                FrameButtonCreareCont.IsVisible = false;
                LogoutButton.IsVisible = true;
                LogoutButton.IsEnabled = true;
                LogoutButton.BackgroundColor = Color.White;
                ButonFacturileMele.IsVisible = true;
                ButonFacturileMele.IsEnabled = true;
                ButonFacturileMele.BackgroundColor = Color.Transparent;
                ButtonSetariCont.IsVisible = true;
                ButtonSetariCont.IsEnabled = true;
                ButtonSetariCont.BackgroundColor = Color.White;


            }
            else
            {
                UserNotFoundImg.IsVisible = true;
                AccountMsg1.Text = "Nu sunteti conectat la niciun cont";
                LogoutButton.IsVisible = false;
                LogoutButton.IsEnabled = false;
                UserNotFoundImg.IsVisible = true;
               // AccountMsg1.Text = "Sunteti conectat la urmatorul cont : " + ConnectedUser.Email + " ConnectedUser IsEmptyUser Value = " + ConnectedUser.IsEmptyUser + " IsFromLogin = " + Mesager.IsFromLogin + " IsFromCreare = " + Mesager.IsFromCreareCont + " Nr useri = " + Users.Count;
                AccountMsg2.IsVisible = true;
                AccountMsg3.IsVisible = false;
                ButtonConectare.IsVisible = true;
                ButtonConectare.IsEnabled = true;
                ButtonConectare.BackgroundColor = Color.Transparent;
                ButtonCreareCont.IsVisible = true;
                ButtonCreareCont.IsEnabled = true;
                FrameButtonCreareCont.IsVisible = true;
                ButtonCreareCont.BackgroundColor = Color.Transparent;
                ButonFacturileMele.IsVisible = false;
                ButonFacturileMele.IsEnabled = false;
                ButtonSetariCont.IsVisible = false;
                ButtonSetariCont.IsEnabled = false;
            }
            if(Models.ConnectedUser.IsPassChanged == true)
            {
                foreach(Models.User U in Users)
                {
                    if(Models.ConnectedUser.Email == U.Email)
                    {
                        U.Password = Models.ConnectedUser.Password;
                        Models.ConnectedUser.IsPassChanged = false;
                    }
                }
            }
            if(Models.ConnectedUser.IsBiletListModified == true)
            {
                foreach(Models.User U in Users)
                {
                    if (Models.ConnectedUser.Email == U.Email)
                    {
                        U.ListaBilete.Clear();
                        U.ListaBilete.AddRange(Models.ConnectedUser.ListaBilete);
                    }
                }
                Models.ConnectedUser.IsBiletListModified = false;
            }
            if (Models.ConnectedUser.IsAccountEdited == true)
            {
                foreach (Models.User U in Users)
                {
                    if (Models.ConnectedUser.Email == U.Email)
                    {
                        U.Nume = Models.ConnectedUser.Nume;
                        U.Prenume = Models.ConnectedUser.Prenume;
                        U.Oras = Models.ConnectedUser.Oras;
                        U.Strada = Models.ConnectedUser.Strada;
                        U.NrStrada = Models.ConnectedUser.NrStrada;
                        U.CodPostal = Models.ConnectedUser.CodPostal;
                        U.NrTelefon = Models.ConnectedUser.NrTelefon;
                    }
                }
                Models.ConnectedUser.IsAccountEdited = false;
            }
            Mesager.IsFromLogin = false;
            Mesager.IsFromCreareCont = false;
        }

        public void GetUsers()
        {
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

            string queryString = "Select * from dbo.Users";
            SqlCommand cmd = new SqlCommand(queryString, sql);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Users.Add(new Models.User
                {
                    Id = Convert.ToUInt32(reader["ID"]),
                    Nume = reader["Nume"].ToString(),
                    Prenume = reader["Prenume"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString(),
                    Tara = reader["Tara"].ToString(),
                    Oras = reader["Oras"].ToString(),
                    Strada = reader["Strada"].ToString(),
                    NrStrada = Convert.ToUInt16(reader["Nr_Strada"]),
                    CodPostal = reader["Cod_Postal"].ToString(),
                    NrTelefon = reader["Nr_Telefon"].ToString(),

                });
            }
            reader.Close();
            sql.Close();
        }

     
        public Profil()
        {
            var InternetConnection = Connectivity.NetworkAccess;

            if (InternetConnection != NetworkAccess.Internet)
                ToolbarItems.Add(new ToolbarItem("Sunteti offline!", "", () =>
                {
                }));

            InitializeComponent();
            OnAppearing();
            GetUsers();

            Users.Add(new Models.User{
                Nume = "Andronescu",
                Prenume = "Alexandru",
                Oras = "Bucuresti",
                Strada = "Baiculesti",
                NrStrada = 25,
                CodPostal = "013195",
                IsEmptyUser = false,
                IsNowConnected = false,
                Tara = "Romania",
                Email = "dev@test.com",
                Password = "test1234",
                Id = 1
            });

        }

        private void LogoutButton_Clicked(object sender, EventArgs e)
        {
            foreach (Models.User U in Users)
            {
                if (Models.ConnectedUser.Email == U.Email)
                {
                    U.IsNowConnected = false;
                    U.ListaBilete.Clear();
                    U.ListaBilete.AddRange(Models.ConnectedUser.ListaBilete);
                    Models.ConnectedUser.ListaBilete.Clear();
                    Models.ConnectedUser.IsNowConnected = false;
                    Models.ConnectedUser.IsEmptyUser = true;
                    Models.ConnectedUser.Email = " ";
                    Models.ConnectedUser.Password = " ";
                }
            }
            UserNotFoundImg.IsVisible = true;
            AccountMsg1.Text = "Nu sunteti conectat la niciun cont";
            AccountMsg1.IsVisible = true;
            AccountMsg2.IsVisible = true;
            AccountMsg3.IsVisible = false;
            AccountMsg4.IsVisible = false;
            LogoutButton.IsVisible = false;
            LogoutButton.IsEnabled = false;
            ButtonConectare.IsVisible = true;
            ButtonConectare.IsEnabled = true;
            ButtonConectare.BackgroundColor = Color.Transparent;
            ButtonCreareCont.IsVisible = true;
            ButtonCreareCont.IsEnabled = true;
            FrameButtonCreareCont.IsVisible = true;
            ButtonCreareCont.BackgroundColor = Color.Transparent;
            ButonFacturileMele.IsVisible = false;
            ButonFacturileMele.IsEnabled = false;
            ButtonSetariCont.IsVisible = false;
            ButtonSetariCont.IsEnabled = false;

        }

        private async void ButonFacturileMele_Clicked(object sender, EventArgs e)
        {
            if (Models.ConnectedUser.IsEmptyUser == false)
                await Navigation.PushAsync(new Facturile_Mele());
            else
                await Navigation.PushAsync(new Pagina_Login(Users));
        }

        private async void ButonRezervarileMele_Clicked(object sender, EventArgs e)
        {
            if (Models.ConnectedUser.IsEmptyUser == false)
                await Navigation.PushAsync(new Rezervari());
            else
                await Navigation.PushAsync(new Pagina_Login(Users));
        }

        private async void ButtonSetariCont_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Edit_Cont(Users));
        }

        private async void Info_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pag_Informatii());
        }

        private async void ButtonConectare_Clicked(object sender, EventArgs e)
        {
            var InternetConnection = Connectivity.NetworkAccess;
            if (InternetConnection != NetworkAccess.Internet)
                await DisplayAlert("Sunteti offline!", "Nu exista conexiune la internet. Verificati setarile de retea si incercati din nou.","Ok");
            else
                await Navigation.PushAsync(new Pagina_Login(Users));
        }

        private void ButtonCreareCont_Clicked(object sender, EventArgs e)
        {
            var InternetConnection = Connectivity.NetworkAccess;
            if (InternetConnection != NetworkAccess.Internet)
                DisplayAlert("Sunteti offline!", "Nu exista conexiune la internet. Verificati setarile de retea si incercati din nou.", "Ok");
            else
                Navigation.PushAsync(new Pagina_Creare_Cont(Users));
        }
    }
}