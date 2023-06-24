using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proiect_Licenta.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Edit_Cont : ContentPage
	{
        private List<Models.User> users = new List<Models.User>();
        int oras = 0, strada = 0, nrstrada = 0, codpostal = 0, nrtelefon = 0;

        SqlConnection sql;
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

        private void ConnectToDB()
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
        }

        private void Nume_TextChanged(object sender, TextChangedEventArgs e)
        {
            Models.ConnectedUser.Nume = Nume.Text;
            Models.ConnectedUser.IsAccountEdited = true;
            ConnectToDB();

            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.Users " + "SET Nume=@Nume " + "WHERE Email=@ModelsConnectedUserEmail", sql))
                {
                    cmd.Parameters.Add(new SqlParameter("Nume", Nume.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("ModelsConnectedUserEmail", Models.ConnectedUser.Email.ToString()));

                    cmd.ExecuteNonQuery();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            sql.Close();
        }

        private void Prenume_TextChanged(object sender, TextChangedEventArgs e)
        {
            Models.ConnectedUser.Prenume = Prenume.Text;
            Models.ConnectedUser.IsAccountEdited = true;

            ConnectToDB();

            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.Users " + "SET Prenume=@Prenume " + "WHERE Email=@ModelsConnectedUserEmail", sql))
                {
                    cmd.Parameters.Add(new SqlParameter("Prenume", Prenume.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("ModelsConnectedUserEmail", Models.ConnectedUser.Email.ToString()));

                    cmd.ExecuteNonQuery();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            sql.Close();
        }

        private void Oras_TextChanged(object sender, TextChangedEventArgs e)
        {
            Models.ConnectedUser.Oras = Oras.Text;
            Models.ConnectedUser.IsAccountEdited = true;
            oras = 1;

            ConnectToDB();

            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.Users " + "SET Oras=@Oras " + "WHERE Email=@ModelsConnectedUserEmail", sql))
                {
                    cmd.Parameters.Add(new SqlParameter("Oras", Oras.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("ModelsConnectedUserEmail", Models.ConnectedUser.Email.ToString()));

                    cmd.ExecuteNonQuery();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            sql.Close();
        }

        private void Strada_TextChanged(object sender, TextChangedEventArgs e)
        {
            Models.ConnectedUser.Strada = Strada.Text;
            Models.ConnectedUser.IsAccountEdited = true;
            strada = 1;

            ConnectToDB();

            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.Users " + "SET Strada=@Strada " + "WHERE Email=@ModelsConnectedUserEmail", sql))
                {
                    cmd.Parameters.Add(new SqlParameter("Strada", Strada.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("ModelsConnectedUserEmail", Models.ConnectedUser.Email.ToString()));

                    cmd.ExecuteNonQuery();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            sql.Close();
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

                    ConnectToDB();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE dbo.Users " + "SET Nr_Strada=@Nr_Strada " + "WHERE Email=@ModelsConnectedUserEmail", sql))
                        {
                            cmd.Parameters.Add(new SqlParameter("Nr_Strada", Convert.ToInt32(input)));
                            cmd.Parameters.Add(new SqlParameter("ModelsConnectedUserEmail", Models.ConnectedUser.Email.ToString()));

                            cmd.ExecuteNonQuery();
                        };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    sql.Close();
                }

        }

        private void CodPostal_TextChanged(object sender, TextChangedEventArgs e)
        {
            Models.ConnectedUser.CodPostal = CodPostal.Text;
            Models.ConnectedUser.IsAccountEdited = true;
            codpostal = 1;

            ConnectToDB();

            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.Users " + "SET Cod_Postal=@Cod_Postal " + "WHERE Email=@ModelsConnectedUserEmail", sql))
                {
                    cmd.Parameters.Add(new SqlParameter("Cod_Postal", CodPostal.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("ModelsConnectedUserEmail", Models.ConnectedUser.Email.ToString()));

                    cmd.ExecuteNonQuery();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            sql.Close();
        }

        private void NrTelefon_TextChanged(object sender, TextChangedEventArgs e)
        {
            Models.ConnectedUser.NrTelefon = NrTelefon.Text;
            Models.ConnectedUser.IsAccountEdited = true;
            nrtelefon = 1;

            ConnectToDB();

            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.Users " + "SET Nr_Telefon=@Nr_Telefon " + "WHERE Email=@ModelsConnectedUserEmail", sql))
                {
                    cmd.Parameters.Add(new SqlParameter("Nr_Telefon", NrTelefon.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("ModelsConnectedUserEmail", Models.ConnectedUser.Email.ToString()));

                    cmd.ExecuteNonQuery();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            sql.Close();
        }

        private void ChangePass_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pag_Revendicare_Parola(users,"Schimbare Parola", "Schimba parola."));
        }
    }
}