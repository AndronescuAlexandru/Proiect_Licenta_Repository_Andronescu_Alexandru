using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proiect_Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Adaugare_Bilet : ContentPage 
    {
        ViewModels.BiletViewModel NewBilet = new ViewModels.BiletViewModel(); //Creeaza un nou viewmodel pentru bilet

        bool IsCompleted = false, CheckBoxChecked = false, SwitchWasToggled = false;
        bool CheckboxRezervare = false, CheckboxTermeniConditii = false, TipCardSelected = false;
        bool PickerDocumenteSelected = false, DetaliiDocumente = false, Nationalitate = false, ContPlataTransfer = false, CIF = false, RegistrulComertului = false;

        int n1 = 0, n2 = 0, p1 = 0, p2 = 0, titlu = 0, tara = 0, codp = 0, oras = 0, str = 0, nrstr = 0, nrtel = 0, email = 0, platacard = 0, CNP = 0;
        SqlConnection sql;
        public Adaugare_Bilet()
        {
            InitializeComponent();
            BindingContext = NewBilet;

            if (Models.ConnectedUser.IsNowConnected == true)
                FrameHeaderMsg.IsVisible = false;


            Shell.SetBackButtonBehavior(this, new BackButtonBehavior //Seteaza noua comanda pentru butonul de back
            {
                Command = new Command(() =>
                {
                    Navigation.PopAsync();
                }),
            });
        }
        public string SetHtmlMessageBody()
        {
            //Titlul mesajului
            string messageBody = "<h1> Biletul dvs. a fost rezervat cu succes.</h1 <h2>Aveti mai jos detaliile biletului.</h2> ";

            messageBody += "<div style=\"width: 400px; padding: 20px; border - radius:25px; border: 2px solid #ccc; background-color: #F2F4F8;" +
                " box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1); margin: 20px auto;\">" + "<div style=\"text - align: center; margin - bottom: 40px;\">" +
                "<h2 style=\"font - size: 28px; color: #000000 margin: 0;\">Bilet de transport eSky Travel</h2> </div>" +
                "<div style=\"margin - bottom: 30px; \"><p>Nr. bilet : " + NewBilet.Bilet[0].Id + "</p>" + "<p>Nume si prenume pasager : "
                + Mesager.Nume + " " + Mesager.Prenume + "</p>" +
                "<p>Plecare din : " + NewBilet.Bilet[0].LocPlecare + "</p>" + "<p>Destinatie : " + NewBilet.Bilet[0].Destinatie + "</p>" +
                "<p>Tip bilet : " + NewBilet.Bilet[0].TipBilet +"<p>Tip transport : " + NewBilet.Bilet[0].Transport + "</p>" + "<p>Clasa : " 
                + NewBilet.Bilet[0].Clasa + "</p> <p>Scaun : " + NewBilet.Bilet[0].Scaun + "</p>" + "<p>Data plecare : " + NewBilet.Bilet[0].DataPlecare + "</p>" +
                "<p>Ora plecare : " + NewBilet.Bilet[0].OraPlecare + "</p>";

            /*
            //Stilizarea prin CSS a biletului trimis la adresa de email

            messageBody += "<head><style>.ticket {width: 400px;padding: 20px;border: 1px solid #ccc;background - color: #fff;box - shadow: 0 2px 5px rgba(0, 0, 0, 0.1);" +
                "margin: 20px auto;}.ticket-header {text - align: center;margin - bottom: 20px;}.ticket-header h2 {font - size: 28px;color: #333;margin: 0;}" +
                ".ticket-info {margin - bottom: 30px;}.ticket-info p {margin: 5px 0;font - size: 16px;color: #555;}.ticket-footer {text - align: center;color: #888;}" +
                ".ticket-footer p {font - size: 12px;margin: 5px 0;}</style> </head>";

            //Informatiile biletului adaugate cu HTML

            messageBody += "<body> <div class=\"ticket\"> <div class=\"ticket - header\"><h2> Bilet de transport</h2></div>" +
                "<div class=\"ticket - info\"> <p>Nr. bilet : " + NewBilet.Bilet[0].Id + "</p>" + "<p>Nume si prenume pasager : " 
                + Mesager.Nume + " " + Mesager.Prenume + "</p>";
            messageBody += "<p>Plecare din : " + NewBilet.Bilet[0].LocPlecare + "</p>" + "<p>Destinatie : " + NewBilet.Bilet[0].Destinatie + "</p>";
            messageBody += "<p>Tip bilet : " + NewBilet.Bilet[0].TipBilet;
            messageBody += "<p>Tip transport : " + NewBilet.Bilet[0].Transport + "</p>" + "<p>Clasa : " + NewBilet.Bilet[0].Clasa + "</p>";
            messageBody += "<p>Scaun : " + NewBilet.Bilet[0].Scaun + "</p>" + "<p>Data plecare : " + NewBilet.Bilet[0].DataPlecare + "</p>";
            messageBody += "<p>Ora plecare : " + NewBilet.Bilet[0].OraPlecare + "</p>";

            if (NewBilet.Bilet[0].IsIntoarcere == true)
                messageBody += "<p>Data intoarcere : " + NewBilet.Bilet[0].DataIntoarcere + "</p>" + "<p>Ora intoarcere : " + NewBilet.Bilet[0].OraIntoarcere + "</p>";

            messageBody += "<p>Pretul biletului : " + NewBilet.Bilet[0].Pret + " €</p> <div class=\"ticket - footer\"><p> " +
                "Vă rugăm să păstrați acest bilet până la sfârșitul călătoriei.</p> </div> </div> </div> </body>";*/

            return messageBody; // returneaza mesaj sub forma de HTML
        }
        public static void SendEmail(string Message, string EmailAdress)
        {
            try
            {
                MailMessage messageEmail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                messageEmail.From = new MailAddress("proiect_licenta_esky_travel@yahoo.com");
                messageEmail.To.Add(new MailAddress(EmailAdress));
                messageEmail.Subject = "Detaliile biletului rezervat";
                messageEmail.IsBodyHtml = true;
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

        public void ConnectToDB()
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

        }
        void RezervareBilet(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var bilet = (Bilet)button.BindingContext;
            string Message = SetHtmlMessageBody();

            string qr_code_value = "Bilet cu nr. " + NewBilet.Bilet[0].Id + "     Bilet " + NewBilet.Bilet[0].TipBilet + "   "
                        + NewBilet.Bilet[0].Transport + " cu nr. " + NewBilet.Bilet[0].NrTransport + "   " + " Plecare din " + NewBilet.Bilet[0].LocPlecare
                        + " Spre " + NewBilet.Bilet[0].Destinatie + "    " + " Ora plecare : " + NewBilet.Bilet[0].OraPlecare
                        + "       Ora intoarcere " + NewBilet.Bilet[0].OraIntoarcere + " Pasager " + NewBilet.Bilet[0].Pasager + " " + Mesager.Titlu + " "
                        + Mesager.Nume + " " + Mesager.Prenume + "              Bilet rezervat prin aplicatia lui Andro!";

            Random random = new Random();
            Mesager.Pret = (ushort)NewBilet.Bilet[0].Pret; //Mesagerul preia din viewmodel-ul NewBilet valoarea pretului curent 

            if((platacard > 0 || ContPlataTransfer == true) && (n1 == 1 && n2 == 1) && (p1 == 1 && p2 == 1) && (titlu == 1 && tara == 1) 
                && (codp == 1 && oras == 1) && (str == 1 && nrstr == 1) && (nrtel == 1 && email == 1) &&
                PickerDocumenteSelected == true && DetaliiDocumente == true && Nationalitate == true && (CNP == 1 || CIF == true || RegistrulComertului == true))
                IsCompleted = true;

            int id_Factura = random.Next();

            ConnectToDB();

            if (CheckBoxChecked == false)
            {
                if (Models.ConnectedUser.IsEmptyUser == false && Models.ConnectedUser.IsNowConnected == true && CheckboxRezervare == true && 
                    CheckboxTermeniConditii == true && (IsCompleted == true || Models.ConnectedUser.Email == "dev@test.com")) //Daca este un user conectat si toate campurile sunt complete atunci se adauga un bilet in lista acestuia
                {

                    sql.Open();

                    try
                    {
                        
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.BileteRezervate VALUES(@ID_Rezervare,@Email,@Tip_Transport,@Loc_Plecare,@Destinatie,@Nume," +
                            "@Prenume,@Data_Plecare,@Data_Retur,@Scaun,@Scaun_Retur,@Nr_Transport,@Clasa,@Tip_Pasager,@Peron,@Tip_Bilet,@Nr_Transport_Retur," +
                            "@Peron_Retur,@Ora_Plecare,@Ora_Retur,@Tip_Tren_Companie_Zbor,@ID_Bilet,@QR_Code,@Activ,@Has_Account)", sql))
                        {
                            cmd.Parameters.Add(new SqlParameter("ID_Rezervare", Convert.ToInt32(random.Next(1,999999))));
                            cmd.Parameters.Add(new SqlParameter("Email", Models.ConnectedUser.Email.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Tip_Transport", NewBilet.Bilet[0].Transport.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Loc_Plecare", NewBilet.Bilet[0].LocPlecare.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Destinatie", NewBilet.Bilet[0].Destinatie.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Nume", Models.ConnectedUser.Nume.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Prenume", Models.ConnectedUser.Prenume.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Data_Plecare", NewBilet.Bilet[0].DataPlecare.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Data_Retur", NewBilet.Bilet[0].DataIntoarcere.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Scaun", NewBilet.Bilet[0].Scaun.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Scaun_Retur", NewBilet.Bilet[0].Scaun2.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Nr_Transport", NewBilet.Bilet[0].NrTransport.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Clasa", NewBilet.Bilet[0].Clasa.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Tip_Pasager", NewBilet.Bilet[0].Pasager.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Peron", NewBilet.Bilet[0].Peron.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Tip_Bilet", NewBilet.Bilet[0].TipBilet.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Nr_Transport_Retur", NewBilet.Bilet[0].NrTransport2.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Peron_Retur", NewBilet.Bilet[0].Peron2.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Ora_Plecare", NewBilet.Bilet[0].OraPlecare.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Ora_Retur", NewBilet.Bilet[0].OraIntoarcere.ToString()));
                            if (NewBilet.Bilet[0].Transport == "Tren")
                                cmd.Parameters.Add(new SqlParameter("Tip_Tren_Companie_Zbor", NewBilet.Bilet[0].TipTren.ToString()));
                            else
                                cmd.Parameters.Add(new SqlParameter("Tip_Tren_Companie_Zbor", NewBilet.Bilet[0].CompanieZbor.ToString()));
                            cmd.Parameters.Add(new SqlParameter("ID_Bilet", Convert.ToInt32(NewBilet.Bilet[0].Id)));
                            cmd.Parameters.Add(new SqlParameter("QR_Code", qr_code_value));
                            cmd.Parameters.Add(new SqlParameter("Activ", true));
                            cmd.Parameters.Add(new SqlParameter("Has_Account", true));
                            cmd.ExecuteNonQuery();
                        };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    try
                    {

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Facturi VALUES(@ID,@Nume,@Prenume,@Email,@Id_Bilet," +
                            "@Suma_De_Plata,@Tara,@Oras,@Strada,@Nr_Strada,@Cod_Postal,@Nr_Telefon,@Loc_Plecare,@Destinatie,@Tip_Transport,@Tip_Bilet," +
                            "@Is_Made_Without_Account,@CNP)", sql))
                        {
                            cmd.Parameters.Add(new SqlParameter("ID", id_Factura));
                            cmd.Parameters.Add(new SqlParameter("Nume", Mesager.Names[0].ToString()));
                            cmd.Parameters.Add(new SqlParameter("Prenume", Mesager.Names[1].ToString()));
                            cmd.Parameters.Add(new SqlParameter("Email", Models.ConnectedUser.Email.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Id_Bilet", Convert.ToInt32(NewBilet.Bilet[0].Id)));
                            cmd.Parameters.Add(new SqlParameter("Suma_De_Plata", NewBilet.Bilet[0].Pret));
                            cmd.Parameters.Add(new SqlParameter("Tara", Mesager.Tara.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Oras", Mesager.Oras.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Strada", Mesager.Strada.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Nr_Strada", Mesager.NrStrada));
                            cmd.Parameters.Add(new SqlParameter("Cod_Postal", Mesager.CodPostal.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Nr_Telefon", Mesager.NrTelefon.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Loc_Plecare", NewBilet.Bilet[0].LocPlecare.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Destinatie", NewBilet.Bilet[0].Destinatie.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Tip_Transport", NewBilet.Bilet[0].Transport.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Tip_Bilet", NewBilet.Bilet[0].TipBilet.ToString()));
                            cmd.Parameters.Add(new SqlParameter("Is_Made_Without_Account", false));
                            cmd.Parameters.Add(new SqlParameter("CNP", Mesager.CNP));

                            cmd.ExecuteNonQuery();
                        };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    sql.Close();

                    Models.ConnectedUser.ListaBilete.Add(new Bilet
                    {
                        Id = NewBilet.Bilet[0].Id,
                        Clasa = NewBilet.Bilet[0].Clasa,
                        LocPlecare = NewBilet.Bilet[0].LocPlecare,
                        Destinatie = NewBilet.Bilet[0].Destinatie,
                        Transport = NewBilet.Bilet[0].Transport,
                        DataPlecare = NewBilet.Bilet[0].DataPlecare,
                        DataIntoarcere = NewBilet.Bilet[0].DataIntoarcere,
                        Pret = NewBilet.Bilet[0].Pret,
                        Activ = NewBilet.Bilet[0].Activ,
                        TipBilet = NewBilet.Bilet[0].TipBilet,
                        Pasager = NewBilet.Bilet[0].Pasager,
                        Icon = NewBilet.Bilet[0].Icon,
                        Scaun = NewBilet.Bilet[0].Scaun,
                        Scaun2 = NewBilet.Bilet[0].Scaun2,
                        IsAvion = NewBilet.Bilet[0].IsAvion,
                        IsIntoarcere = NewBilet.Bilet[0].IsIntoarcere,
                        Icon2 = NewBilet.Bilet[0].Icon2,
                        BagajCala = NewBilet.Bilet[0].BagajCala,
                        Nume = Mesager.Nume,
                        Prenume = Mesager.Prenume,
                        Titlu = Mesager.Titlu,
                        OraPlecare = NewBilet.Bilet[0].OraPlecare,
                        OraIntoarcere = NewBilet.Bilet[0].OraIntoarcere,
                        Color = NewBilet.Bilet[0].Color,
                        NrTransport = NewBilet.Bilet[0].NrTransport,
                        NrTransport2 = NewBilet.Bilet[0].NrTransport2,
                        Peron = NewBilet.Bilet[0].Peron,
                        Peron2 = NewBilet.Bilet[0].Peron2,
                        QRCodeTextValue = qr_code_value
                    });
                    Models.ConnectedUser.ListaFacturi.Add(new Models.Factura
                    {
                        Id = (ushort)id_Factura,
                        IdBilet = NewBilet.Bilet[0].Id,
                        Nume = Mesager.Names[0],
                        Prenume = Mesager.Names[1],
                        Email = Mesager.Email,
                        SumaDePlata = (ushort)NewBilet.Bilet[0].Pret,
                        Tara = Mesager.Tara,
                        Oras = Mesager.Oras,
                        Strada = Mesager.Strada,
                        NrStrada = (ushort)Mesager.NrStrada,
                        NrTelefon = Mesager.NrTelefon,
                        CodPostal = Mesager.CodPostal,
                        PlecareDin = NewBilet.Bilet[0].LocPlecare,
                        Destinatie = NewBilet.Bilet[0].Destinatie,
                        TipBilet = NewBilet.Bilet[0].TipBilet,
                        TipTransport = NewBilet.Bilet[0].Transport,
                    });
                    Models.ConnectedUser.IsBiletListModified = true;
                    Mesager.Received = true;
                    Mesager.ReceivedNewFactura = true;
                    Navigation.PopToRootAsync();
                }
                else if (Mesager.Email != null && IsCompleted == true && CheckboxRezervare == true && CheckboxTermeniConditii == true)
                {
                    Mesager.ReceivedNewFactura = true;
                    SendEmail(SetHtmlMessageBody(), Mesager.Email);
                    Navigation.PushAsync(new Facturile_Mele());
                }
                else
                    DisplayAlert("Ups :(", "Nu ati completat datele de mai sus sau sunt incorecte. Verificati datele introduse si incercati din nou.", "Ok");
            }
            else
            {
                sql.Open();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.BileteRezervate VALUES(@Email,@Tip_Transport,@Loc_Plecare,@Destinatie,@Nume," +
                            "@Prenume,@Data_Plecare,@Data_Retur,@Scaun,@Scaun_Retur,@Nr_Transport,@Clasa,@Tip_Pasager,@Peron,@Tip_Bilet,@Nr_Transport_Retur," +
                            "@Peron_Retur,@Ora_Plecare,@Ora_Retur,@Tip_Tren_Companie_Zbor,@ID_Bilet,@QR_Code,@Activ,@Has_Account)", sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("Email", Models.ConnectedUser.Email.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Tip_Transport", NewBilet.Bilet[0].Transport.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Loc_Plecare", NewBilet.Bilet[0].LocPlecare.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Destinatie", NewBilet.Bilet[0].Destinatie.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Nume", Models.ConnectedUser.Nume.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Prenume", Models.ConnectedUser.Prenume.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Data_Plecare", NewBilet.Bilet[0].DataPlecare.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Data_Retur", NewBilet.Bilet[0].DataIntoarcere.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Scaun", NewBilet.Bilet[0].Scaun.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Scaun_Retur", NewBilet.Bilet[0].Scaun2.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Nr_Transport", NewBilet.Bilet[0].NrTransport.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Clasa", NewBilet.Bilet[0].Clasa.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Tip_Pasager", NewBilet.Bilet[0].Pasager.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Peron", NewBilet.Bilet[0].Peron.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Tip_Bilet", NewBilet.Bilet[0].TipBilet.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Nr_Transport_Retur", NewBilet.Bilet[0].NrTransport2.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Peron_Retur", NewBilet.Bilet[0].Peron2.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Ora_Plecare", NewBilet.Bilet[0].OraPlecare.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Ora_Retur", NewBilet.Bilet[0].OraIntoarcere.ToString()));
                        if (NewBilet.Bilet[0].Transport == "Tren")
                            cmd.Parameters.Add(new SqlParameter("Tip_Tren_Companie_Zbor", NewBilet.Bilet[0].TipTren.ToString()));
                        else
                            cmd.Parameters.Add(new SqlParameter("Tip_Tren_Companie_Zbor", NewBilet.Bilet[0].CompanieZbor.ToString()));
                        cmd.Parameters.Add(new SqlParameter("ID_Bilet", Convert.ToInt32(NewBilet.Bilet[0].Id)));
                        cmd.Parameters.Add(new SqlParameter("QR_Code", qr_code_value));
                        cmd.Parameters.Add(new SqlParameter("Activ", true));
                        cmd.Parameters.Add(new SqlParameter("Has_Account", false));

                        cmd.ExecuteNonQuery();
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Facturi VALUES(@ID,@Nume,@Prenume,@Email,@Id_Bilet," +
                        "@Suma_De_Plata,@Tara,@Oras,@Strada,@Nr_Strada,@Cod_Postal,@Nr_Telefon,@Loc_Plecare,@Destinatie,@Tip_Transport,@Tip_Bilet," +
                        "@Is_Made_Without_Account,@CNP)", sql))
                    {
                        cmd.Parameters.Add(new SqlParameter("ID", id_Factura));
                        cmd.Parameters.Add(new SqlParameter("Nume", Mesager.Names[0].ToString()));
                        cmd.Parameters.Add(new SqlParameter("Prenume", Mesager.Names[1].ToString()));
                        cmd.Parameters.Add(new SqlParameter("Email", Mesager.Email.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Id_Bilet", Convert.ToInt32(NewBilet.Bilet[0].Id)));
                        cmd.Parameters.Add(new SqlParameter("Suma_De_Plata", NewBilet.Bilet[0].Pret));
                        cmd.Parameters.Add(new SqlParameter("Tara", Mesager.Tara.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Oras", Mesager.Oras.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Strada", Mesager.Strada.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Nr_Strada", Mesager.NrStrada));
                        cmd.Parameters.Add(new SqlParameter("Cod_Postal", Mesager.CodPostal.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Nr_Telefon", Mesager.NrTelefon.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Loc_Plecare", NewBilet.Bilet[0].LocPlecare.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Destinatie", NewBilet.Bilet[0].Destinatie.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Tip_Transport", NewBilet.Bilet[0].Transport.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Tip_Bilet", NewBilet.Bilet[0].TipBilet.ToString()));
                        cmd.Parameters.Add(new SqlParameter("Is_Made_Without_Account", true));
                        cmd.Parameters.Add(new SqlParameter("CNP", Mesager.CNP));
                        cmd.ExecuteNonQuery();
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                sql.Close();

                Models.ConnectedUser.ListaBilete.Add(new Bilet
                {
                    Id = NewBilet.Bilet[0].Id,
                    Clasa = NewBilet.Bilet[0].Clasa,
                    LocPlecare = NewBilet.Bilet[0].LocPlecare,
                    Destinatie = NewBilet.Bilet[0].Destinatie,
                    Transport = NewBilet.Bilet[0].Transport,
                    DataPlecare = NewBilet.Bilet[0].DataPlecare,
                    DataIntoarcere = NewBilet.Bilet[0].DataIntoarcere,
                    Pret = NewBilet.Bilet[0].Pret,
                    Activ = NewBilet.Bilet[0].Activ,
                    TipBilet = NewBilet.Bilet[0].TipBilet,
                    Pasager = NewBilet.Bilet[0].Pasager,
                    Icon = NewBilet.Bilet[0].Icon,
                    Scaun = NewBilet.Bilet[0].Scaun,
                    Scaun2 = NewBilet.Bilet[0].Scaun2,
                    IsAvion = NewBilet.Bilet[0].IsAvion,
                    IsIntoarcere = NewBilet.Bilet[0].IsIntoarcere,
                    Icon2 = NewBilet.Bilet[0].Icon2,
                    BagajCala = NewBilet.Bilet[0].BagajCala,
                    Nume = Mesager.Nume,
                    Prenume = Mesager.Prenume,
                    Titlu = Mesager.Titlu,
                    OraPlecare = NewBilet.Bilet[0].OraPlecare,
                    OraIntoarcere = NewBilet.Bilet[0].OraIntoarcere,
                    Color = NewBilet.Bilet[0].Color,
                    NrTransport = NewBilet.Bilet[0].NrTransport,
                    NrTransport2 = NewBilet.Bilet[0].NrTransport2,
                    Peron = NewBilet.Bilet[0].Peron,
                    Peron2 = NewBilet.Bilet[0].Peron2,
                    QRCodeTextValue = qr_code_value
                });
                Models.ConnectedUser.ListaFacturi.Add(new Models.Factura
                {
                    Id = (ushort)random.Next(1, 1000),
                    IdBilet = NewBilet.Bilet[0].Id,
                    Nume = Models.ConnectedUser.Nume,
                    Prenume = Models.ConnectedUser.Prenume,
                    Email = Models.ConnectedUser.Email,
                    SumaDePlata = (ushort)NewBilet.Bilet[0].Pret,
                    Tara = Models.ConnectedUser.Tara,
                    Oras = Models.ConnectedUser.Oras,
                    Strada = Models.ConnectedUser.Strada,
                    NrStrada = (ushort)Models.ConnectedUser.NrStrada,
                    NrTelefon = Models.ConnectedUser.NrTelefon,
                    CodPostal = Models.ConnectedUser.CodPostal,
                    PlecareDin = NewBilet.Bilet[0].LocPlecare,
                    Destinatie = NewBilet.Bilet[0].Destinatie,
                    TipBilet = NewBilet.Bilet[0].TipBilet,
                    TipTransport = NewBilet.Bilet[0].Transport,
                });
                Models.ConnectedUser.IsBiletListModified = true;
                Mesager.Received = true;
                Mesager.ReceivedNewFactura = true;
                Navigation.PopToRootAsync();
            }
        }
        private void RadioButton_BagajCalaDefault(object sender, CheckedChangedEventArgs e)
        {
            NewBilet.Bilet[0].Pret = Mesager.oldPret;
            NewBilet.Bilet[0].PretServiciiSuplimentare = 0;
            NewBilet.Bilet[0].DetaliiServiciiBagaje = false;
        }
        private void RadioButton_BagajCala0(object sender, CheckedChangedEventArgs e)
        {
            NewBilet.Bilet[0].Pret = Mesager.oldPret + 35;
            NewBilet.Bilet[0].PretServiciiSuplimentare = 35;
            NewBilet.Bilet[0].DetaliiServiciiBagaje = true;
        }
        private void RadioButton_BagajCala1(object sender, CheckedChangedEventArgs e)
        {
            NewBilet.Bilet[0].Pret = Mesager.oldPret + 50;
            NewBilet.Bilet[0].PretServiciiSuplimentare = 50;
            NewBilet.Bilet[0].DetaliiServiciiBagaje = true;
        }
        private void RadioButton_BagajCala2(object sender, CheckedChangedEventArgs e)
        {
            NewBilet.Bilet[0].Pret = Mesager.oldPret + 100;
            NewBilet.Bilet[0].PretServiciiSuplimentare = 100;
            NewBilet.Bilet[0].DetaliiServiciiBagaje = true;
        }
        private void TipCard_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            TipCardSelected = true;
        }

        private void PlataCard(object sender, EventArgs e)
        {
            platacard++;
        }

        private void Nume_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            if (entry.Text.Length < 4)
            {
                entry.Placeholder = "Trebuie introdus un nume cu cel puțin 3 caractere";
            }
            else
            {
                Mesager.Nume = entry.Text;
                n1 = 1;
            }
            Mesager.Nume = entry.Text;
        }

        private void EntryDetaliiDocumente(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry.Text.Length < 4)
                entry.Placeholder = "Seria și numărul documentului trebuie să aibă cel puțin 3 caractere";
            else
                DetaliiDocumente = true;
        }

        private void EntryNationalitate(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry.Text.Length < 4)
                entry.Placeholder = "Trebuie introdusă o naționalitate cu cel puțin 3 caractere";
            else
                Nationalitate = true;
        }

        private void CIF_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry.Text.Length < 4)
                entry.Placeholder = "Trebuie introdus un număr valid cu cel puțin 3 caractere";
            else
                CIF = true;
        }

        private void RegistrulComertului_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry.Text.Length < 4)
                entry.Placeholder = "Trebuie introdus un număr valid cu cel puțin 3 caractere";
            else
                RegistrulComertului = true;
        }
        private void EntryContPlataTransfer(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry.Text.Length < 4)
                entry.Placeholder = "Trebuie introdus un cont valid";
            else
                ContPlataTransfer = true;
        }

        private void Check_In_Checked(object sender, EventArgs e)
        {
            var button_switch = sender as Switch;
            if(button_switch.IsToggled == true && SwitchWasToggled == false)
            {
                NewBilet.Bilet[0].Pret = Mesager.oldPret + 7;
                NewBilet.Bilet[0].DetaliiServiciiCheckin = true;
                SwitchWasToggled = true;
            }
            else
                if(SwitchWasToggled == true && NewBilet.Bilet[0].Pret > Mesager.oldPret)
                {
                    NewBilet.Bilet[0].Pret = NewBilet.Bilet[0].Pret - 7;
                NewBilet.Bilet[0].DetaliiServiciiCheckin = false;
                SwitchWasToggled = false;
                }

        }

        private void TransferBancarChecked(object sender, CheckedChangedEventArgs e)
        {
            NewBilet.Bilet[0].PlataCard = false;
            NewBilet.Bilet[0].PlataTransfer = true;
        }

        private void CreditCardChecked(object sender, CheckedChangedEventArgs e)
        {
            NewBilet.Bilet[0].PlataCard = true;
            NewBilet.Bilet[0].PlataTransfer = false;
        }

        private void CVV_Info_Button(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CreditCardInfo());
        }

        private void HeaderMsgConectare_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profil());
        }

        private void HeaderMsgInchidere_Clicked(object sender, EventArgs e)
        {
            FrameHeaderMsg.IsVisible = false;
        }

        private void PersoanaFizica_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            NewBilet.Bilet[0].PFizica = true;
            NewBilet.Bilet[0].PJuridica = false;
        }

        private void CNP_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry.Text.Length == 13)
            {
                Mesager.CNP = Convert.ToInt64(entry.Text);
                CNP = 1;
            }
        }

        private void PersoanaJuridica_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            NewBilet.Bilet[0].PFizica = false;
            NewBilet.Bilet[0].PJuridica = true;
        }

        private void DropDownPret(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            if (NewBilet.Bilet[0].DetaliiPret == false)
            {
                NewBilet.Bilet[0].DetaliiPret = true;
                button.Source = "drop_up_icon.png";
            }
            else
            {
                NewBilet.Bilet[0].DetaliiPret = false;
                button.Source = "drop_down_icon.png";
            }
        }

        private void DropDownDetaliiBilet(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            if (NewBilet.Bilet[0].DetaliiBilet == false)
            {
                NewBilet.Bilet[0].DetaliiBilet = true;
                button.Source = "drop_up_icon.png";
            }
            else
            {
                NewBilet.Bilet[0].DetaliiBilet = false;
                button.Source = "drop_down_icon.png";
            }
        }

        private void CheckBox_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            if (checkbox.IsChecked == true)
                CheckboxRezervare = true;
            else
                CheckboxRezervare = false;
        }

        private void Conditii_Rezervare_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Conditii_Rezervare());
        }

        private void Termeni_Conditii_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Termeni_si_Conditii(1));
        }

        private void CheckBox_CheckedChanged_2(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            if (checkbox.IsChecked == true)
                CheckboxTermeniConditii = true;
            else
                CheckboxTermeniConditii = false;
        }

        private void Info_Bagaje_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Info_Bagaje());
        }

        private void ContSwitch_IsChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (Switch)sender;
            if (Models.ConnectedUser.IsNowConnected == true && Models.ConnectedUser.AccountDataIsCompleted == true)
                CheckBoxChecked = true;
            else
            {
                DisplayAlert("Datele contului nu sunt completate.", "Din pacate nu ati completat toate datele din cont sau nu aveti un cont inregistrat in aplicatie. Adaugati datele necesare si reveniti mai tarziu.", "Ok");
                checkbox.IsToggled = false;
            }
        }

        private void Prenume_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            var label = new Label { Text = "" };
            if (entry.Text.Length < 4)
            {
                label.Text = "Trebuie introdus un prenume cu cel putin 3 caractere";
            }
            else
            {
                Mesager.Prenume = entry.Text;
                p1 = 1;
            }
            Mesager.Prenume = entry.Text;
        }

        private void PickerTitlul_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            Mesager.Titlu = (string)picker.SelectedItem;
            titlu = 1;
        }

        private void PickerDocumente_SelectedIndexChanged(object sender, EventArgs e)
        {
            PickerDocumenteSelected = true;
        }
        private void NumeF_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            if (entry.Text.Length < 4)
            {
                entry.Placeholder = "Trebuie introdus un nume cu cel putin 3 caractere";
            }
            else
            {
                Mesager.Names[0] = entry.Text;
                n2 = 1;
            }
        }

        private void PreumeF_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            if (entry.Text.Length < 4)
            {
                entry.Placeholder = "Trebuie introdus un prenume cu cel putin 3 caractere";
            }
            else
            {
                Mesager.Names[1] = entry.Text;
                p2 = 1;
            }
            //Models.ConnectedUser.ListaFacturi.LastOrDefault().Prenume = entry.Text;
        }

        private void TaraF_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            if (entry.Text.Length < 4)
            {
                entry.Placeholder = "Trebuie introdus un cuvant cu cel putin 3 caractere";
            }
            else
            {
                Mesager.Tara = (string)entry.Text;
                tara = 1;
            }
            //Models.ConnectedUser.ListaFacturi.LastOrDefault().Tara = entry.Text;
        }

        private void CodPostalF_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            if (entry.Text.Length < 4)
            {
                entry.Placeholder = "Trebuie introdus un nr. de cel putin 3 cifre";
            }
            else
            {
                Mesager.CodPostal = entry.Text;
                codp = 1;
            }
            //Models.ConnectedUser.ListaFacturi.LastOrDefault().CodPostal = entry.Text;
        }

        private void OrasF_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            if (entry.Text.Length < 4)
            {
                entry.Placeholder = "Trebuie introdus un cuvant cu cel putin 3 caractere";
            }
            else
            {
                Mesager.Oras = entry.Text;
                oras = 1;
            }
            //Models.ConnectedUser.ListaFacturi.LastOrDefault().Oras = entry.Text;
        }

        private void StradaF_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            if (entry.Text.Length < 4)
            {
                entry.Placeholder = "Trebuie introdus un cuvant cu cel putin 3 caractere";
            }
            else
            {
                Mesager.Strada = entry.Text;
                str = 1;
            }
            //Models.ConnectedUser.ListaFacturi.LastOrDefault().Strada = entry.Text;
        }

        private void NrStradaF_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            if (entry.Text.Length == 0)
            {
                entry.Placeholder = "Trebuie introdus un nr.";
            }
            else
            {
                Mesager.NrStrada = Convert.ToUInt16(entry.Text);
                nrstr = 1;
            }
            //Models.ConnectedUser.ListaFacturi.LastOrDefault().NrStrada = Convert.ToUInt16(entry.Text);
        }

        private void NrTelefonF_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            if (entry.Text.Length < 10)
            {
                entry.Placeholder = "Nr. de telefon introdus nu este valid.";
            }
            else
            {
                Mesager.NrTelefon = entry.Text;
                nrtel = 1;
            }
            //Models.ConnectedUser.ListaFacturi.LastOrDefault().NrTelefon = entry.Text;
        }

        private void AdresaEmailF_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            foreach(char ch in entry.Text)
            {
                if (ch == '@')
                {
                    Mesager.Email = entry.Text;
                    email = 1;
                    break;
                }
            }
            //Models.ConnectedUser.ListaFacturi.LastOrDefault().Email = entry.Text;
        }
    }
}