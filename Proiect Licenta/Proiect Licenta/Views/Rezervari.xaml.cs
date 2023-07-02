using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proiect_Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Rezervari : TabbedPage
    {
        public Profil Calling { get; set; }
        List<Bilet> RezervareT = new List<Bilet>();
        List<Bilet> RezervareC = new List<Bilet>();
        private string email = Models.ConnectedUser.Email;
        protected override void OnAppearing() //Functia actualizeaza pagina biletelor rezervate din cont
        {
            var InternetConnection = Connectivity.NetworkAccess;

            if (Models.ConnectedUser.IsNowConnected != false && InternetConnection == NetworkAccess.Internet && ToolbarItems.Count > 0)
                ToolbarItems.RemoveAt(0);
            else if (Models.ConnectedUser.IsNowConnected == false && InternetConnection == NetworkAccess.Internet && ToolbarItems.Count == 0)
                ToolbarItems.Add(new ToolbarItem("Sign In", "Sign In", () =>
                {
                    Navigation.PushAsync(new Profil());
                }));

            if (Models.ConnectedUser.Email != email)
            {
                RezervareC.Clear();
                RezervareT.Clear();
                email = Models.ConnectedUser.Email;
            }
            if(Models.ConnectedUser.ListaBilete.Count > 0 && RezervareC.Count == 0 && RezervareT.Count == 0 && Mesager.Received == false)
            {

                foreach(Bilet b in Models.ConnectedUser.ListaBilete)
                {
                    switch(b.Transport)
                    {
                        case "Avion":
                            {
                                b.Color = "#d24c4c";
                                break;
                            }
                        case "Tren":
                            {
                                b.Color = "#61aed9";
                                break;
                            }
                        case "Autocar":
                            {
                                b.Color = "#5e3dc6";
                                break;
                            }
                    }
                    if(b.Activ == true)
                        RezervareC.Add(new Bilet
                        {
                            Id = b.Id,
                            Clasa = b.Clasa,
                            TipBilet = b.TipBilet,
                            Transport = b.Transport,
                            Pasager = b.Pasager,
                            Pret = b.Pret,
                            DataPlecare = b.DataPlecare,
                            DataIntoarcere = b.DataIntoarcere,
                            Activ = b.Activ,
                            Destinatie = b.Destinatie,
                            LocPlecare = b.LocPlecare,
                            Icon = b.Icon,
                            Icon2 = b.Icon2,
                            IsIntoarcere = b.IsIntoarcere,
                            Scaun = b.Scaun,
                            Scaun2 = b.Scaun2,
                            Nume = b.Nume,
                            Prenume = b.Prenume,
                            Color = b.Color,
                            Peron = b.Peron,
                            Peron2 = b.Peron2,
                            NrTransport = b.NrTransport,
                            NrTransport2 = b.NrTransport2,
                            QRCodeTextValue = b.QRCodeTextValue,
                        });
                    RezervareT.Add(new Bilet
                    {
                        Id = b.Id,
                        Clasa = b.Clasa,
                        TipBilet = b.TipBilet,
                        Transport = b.Transport,
                        Pasager = b.Pasager,
                        Pret = b.Pret,
                        DataPlecare = b.DataPlecare,
                        DataIntoarcere = b.DataIntoarcere,
                        Activ = b.Activ,
                        Destinatie = b.Destinatie,
                        LocPlecare = b.LocPlecare,
                        Icon = b.Icon,
                        Icon2 = b.Icon2,
                        IsIntoarcere = b.IsIntoarcere,
                        Scaun = b.Scaun,
                        Scaun2 = b.Scaun2,
                        Nume = b.Nume,
                        Prenume = b.Prenume,
                        Color = b.Color,
                        Peron = b.Peron,
                        Peron2 = b.Peron2,
                        NrTransport = b.NrTransport,
                        NrTransport2 = b.NrTransport2,
                        QRCodeTextValue = b.QRCodeTextValue,
                    });
                }
            }
            if(Mesager.Received == true)
            {
                RezervareC.Add(new Bilet
                {
                    Id = Models.ConnectedUser.ListaBilete.LastOrDefault().Id,
                    Clasa = Models.ConnectedUser.ListaBilete.LastOrDefault().Clasa,
                    TipBilet = Models.ConnectedUser.ListaBilete.LastOrDefault().TipBilet,
                    Transport = Models.ConnectedUser.ListaBilete.LastOrDefault().Transport,
                    Pasager = Models.ConnectedUser.ListaBilete.LastOrDefault().Pasager,
                    Pret = Models.ConnectedUser.ListaBilete.LastOrDefault().Pret,
                    DataPlecare = Models.ConnectedUser.ListaBilete.LastOrDefault().DataPlecare,
                    DataIntoarcere = Models.ConnectedUser.ListaBilete.LastOrDefault().DataIntoarcere,
                    Activ = Models.ConnectedUser.ListaBilete.LastOrDefault().Activ,
                    Destinatie = Models.ConnectedUser.ListaBilete.LastOrDefault().Destinatie,
                    LocPlecare = Models.ConnectedUser.ListaBilete.LastOrDefault().LocPlecare,
                    Icon = Models.ConnectedUser.ListaBilete.LastOrDefault().Icon,
                    Icon2 = Models.ConnectedUser.ListaBilete.LastOrDefault().Icon2,
                    IsIntoarcere = Models.ConnectedUser.ListaBilete.LastOrDefault().IsIntoarcere,
                    Scaun = Models.ConnectedUser.ListaBilete.LastOrDefault().Scaun,
                    Scaun2 = Models.ConnectedUser.ListaBilete.LastOrDefault().Scaun2,
                    Nume = Models.ConnectedUser.ListaBilete.LastOrDefault().Nume,
                    Prenume = Models.ConnectedUser.ListaBilete.LastOrDefault().Prenume,
                    Color = Models.ConnectedUser.ListaBilete.LastOrDefault().Color,
                    QRCodeTextValue = Models.ConnectedUser.ListaBilete.LastOrDefault().QRCodeTextValue,
                    NrTransport = Models.ConnectedUser.ListaBilete.LastOrDefault().NrTransport,
                    NrTransport2 = Models.ConnectedUser.ListaBilete.LastOrDefault().NrTransport2,
                    Peron = Models.ConnectedUser.ListaBilete.LastOrDefault().Peron,
                    Peron2 = Models.ConnectedUser.ListaBilete.LastOrDefault().Peron2,
                });
                RezervareT.Add(new Bilet
                {
                    Id = Models.ConnectedUser.ListaBilete.LastOrDefault().Id,
                    Clasa = Models.ConnectedUser.ListaBilete.LastOrDefault().Clasa,
                    TipBilet = Models.ConnectedUser.ListaBilete.LastOrDefault().TipBilet,
                    Transport = Models.ConnectedUser.ListaBilete.LastOrDefault().Transport,
                    Pasager = Models.ConnectedUser.ListaBilete.LastOrDefault().Pasager,
                    Pret = Models.ConnectedUser.ListaBilete.LastOrDefault().Pret,
                    DataPlecare = Models.ConnectedUser.ListaBilete.LastOrDefault().DataPlecare,
                    DataIntoarcere = Models.ConnectedUser.ListaBilete.LastOrDefault().DataIntoarcere,
                    Activ = Models.ConnectedUser.ListaBilete.LastOrDefault().Activ,
                    Destinatie = Models.ConnectedUser.ListaBilete.LastOrDefault().Destinatie,
                    LocPlecare = Models.ConnectedUser.ListaBilete.LastOrDefault().LocPlecare,
                    Icon = Models.ConnectedUser.ListaBilete.LastOrDefault().Icon,
                    Icon2 = Models.ConnectedUser.ListaBilete.LastOrDefault().Icon2,
                    IsIntoarcere = Models.ConnectedUser.ListaBilete.LastOrDefault().IsIntoarcere,
                    Scaun = Models.ConnectedUser.ListaBilete.LastOrDefault().Scaun,
                    Scaun2 = Models.ConnectedUser.ListaBilete.LastOrDefault().Scaun2,
                    Nume = Models.ConnectedUser.ListaBilete.LastOrDefault().Nume,
                    Prenume = Models.ConnectedUser.ListaBilete.LastOrDefault().Prenume,
                    Color = Models.ConnectedUser.ListaBilete.LastOrDefault().Color,
                    QRCodeTextValue = Models.ConnectedUser.ListaBilete.LastOrDefault().QRCodeTextValue,
                    NrTransport = Models.ConnectedUser.ListaBilete.LastOrDefault().NrTransport,
                    NrTransport2 = Models.ConnectedUser.ListaBilete.LastOrDefault().NrTransport2,
                    Peron = Models.ConnectedUser.ListaBilete.LastOrDefault().Peron,
                    Peron2 = Models.ConnectedUser.ListaBilete.LastOrDefault().Peron2,
                });
            }
            Mesager.Received = false;
        }
        public string SetIconImage()
        {
            switch (Mesager.Transport)
            {
                case "Avion":
                    {
                        return "avion_icon.png";
                    }
                case "Tren":
                    {
                        return "tren_icon.png";
                    }
                case "Autocar":
                    {
                        return "autocar_icon.png";
                    }
            }
            return "missing.png";
        }
        async void Detalii(object sender, EventArgs e)
        {
            var button = sender as Button;
            var bilet = button.BindingContext as Bilet;

            Mesager.Id = bilet.Id;
            Mesager.Pret = (ushort)bilet.Pret;
            Mesager.oldPret = bilet.Pret;
            Mesager.TipBilet = bilet.TipBilet;
            Mesager.Transport = bilet.Transport;
            Mesager.Pasager = bilet.Pasager;
            Mesager.Activ = bilet.Activ;
            Mesager.DataPlecare = bilet.DataPlecare;
            Mesager.DataIntoarcere = bilet.DataIntoarcere;
            Mesager.Plecare = bilet.LocPlecare;
            Mesager.Destinatie = bilet.Destinatie;
            Mesager.Clasa = bilet.Clasa;
            Mesager.IsAvion = bilet.IsAvion;
            Mesager.Icon = bilet.Icon;
            Mesager.Icon2 = bilet.Icon2;
            Mesager.Scaun = bilet.Scaun;
            if(bilet.TipBilet == "Dus-Intors")
                Mesager.IsIntoarcere = true;
            else
                Mesager.IsIntoarcere = false;
            Mesager.Peron = bilet.Peron;
            Mesager.NrTransport = bilet.NrTransport;
            Mesager.NrTransport2 = bilet.NrTransport2;
            Mesager.Peron2 = bilet.Peron2;
            Mesager.Scaun2 = bilet.Scaun2;
            Mesager.QRCodeTextValue = bilet.QRCodeTextValue;
            Mesager.Nume = bilet.Nume;
            Mesager.Prenume = bilet.Prenume;
            Mesager.Tara = bilet.Tara;
            Mesager.Titlu = bilet.Titlu;
            Mesager.NrStrada = bilet.NrStrada;
			Mesager.NrTelefon = bilet.NrTelefon;
            Mesager.DataNasterii = bilet.DataNasterii;
            Mesager.BagajCala = bilet.BagajCala;
            Mesager.Email = bilet.Email;
            Mesager.Oras = bilet.Oras;
            Mesager.CodPostal = bilet.CodPostal;
            Mesager.Strada = bilet.Strada;
            Mesager.IconCompanieZbor = bilet.IconCompanieZbor;
            Mesager.OraPlecare = bilet.OraPlecare;
            Mesager.OraIntoarcere = bilet.OraIntoarcere;
            Mesager.Color = bilet.Color;
            Mesager.DurataDrum = bilet?.DurataDrum;
            Mesager.Distance = bilet.Distance;
            Mesager.DurataDrumEscala = bilet.DurataDrumEscala;
            Mesager.DurataDrumPanaLaEscala = bilet.DurataDrumPanaLaEscala;
            Mesager.DestinatieEscala = bilet.DestinatieEscala;
            Mesager.DurataEscala = bilet.DurataEscala;
            Mesager.NrTransportEscala = bilet.NrTransportEscala;
            Mesager.OraPlecareEscala = bilet.OraPlecareEscala;
            Mesager.PlecareEscala = bilet.PlecareEscala;
            Mesager.CompanieZbor = bilet.CompanieZbor;
            Mesager.TipTren = bilet.TipTren;

            await Navigation.PushAsync(new Descriere_Bilet());
        }
        public Rezervari()
        {
            var InternetConnection = Connectivity.NetworkAccess;

            InitializeComponent();
            OnAppearing();

            ListaRezervariTotale.ItemsSource = RezervareT;
            ListaRezervariCurente.ItemsSource = RezervareC;
            SelectedTabColor = Color.FromHex("#248cd9");
            UnselectedTabColor = Color.Black;

            /*addRezervare(1, "Bucuresti","Cluj-Napoca", "13/02/2023", "13/04/2023",true,"Dus","Economica","Avion","Adult",240);
            addRezervare(2, "Bucuresti", "Brasov", "13/03/2023", "13/04/2023", true,"Dus", "Economica", "Avion","Adolescent",99);
            addRezervare(3, "Giurgiu", "Alexandria", "18/02/2023", "13/04/2023",true,"Dus", "Economica", "Avion","Copil",50);
            addRezervare(4, "Buzau","Ploiesti", "11/02/2023", "13/04/2023", true,"Dus", "Economica", "Avion","Adult",189);*/

            if (InternetConnection != NetworkAccess.Internet)
                ToolbarItems.Add(new ToolbarItem("Sunteti offline!", "", () =>
                {
                }));

        }
        void rezervari_curente(object sender, EventArgs e) //Functia afiseaza lista biletelor active
        {
            ListaRezervariTotale.IsVisible = false;
            ListaRezervariTotale.IsEnabled = false;
            ListaRezervariCurente.IsVisible = true;
            ListaRezervariCurente.IsEnabled = true;  
        }
        void rezervari_totale(object sender, EventArgs e) //Functia afiseaza lista biletelor totale
        {
            ListaRezervariTotale.IsVisible = true;
            ListaRezervariTotale.IsEnabled = true;
            ListaRezervariCurente.IsVisible = false;
            ListaRezervariCurente.IsEnabled = false;        
        }
    }
}