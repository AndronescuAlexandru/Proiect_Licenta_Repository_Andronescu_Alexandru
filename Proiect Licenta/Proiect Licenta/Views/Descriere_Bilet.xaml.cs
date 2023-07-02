using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Proiect_Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Descriere_Bilet : ContentPage
    {
        //ViewModels.BiletViewModel Bilet = new ViewModels.BiletViewModel();
        List<Bilet> Bilet2 = new List<Bilet>();

        public Descriere_Bilet()
        {
            InitializeComponent();

            Bilet2.Add(new Bilet
            {
                Id = Views.Mesager.Id,
                Id2 = Views.Mesager.Id + 1,
                LocPlecare = Views.Mesager.Plecare,
                Destinatie = Views.Mesager.Destinatie,
                DataPlecare = Views.Mesager.DataPlecare,
                DataIntoarcere = Views.Mesager.DataIntoarcere,
                //DataPlecareEscala = Views.Mesager.DataPlecareEscala.ToString(),
                Activ = true,
                TipBilet = Views.Mesager.TipBilet,
                Clasa = Views.Mesager.Clasa,
                Transport = Views.Mesager.Transport,
                Pasager = Views.Mesager.Pasager,
                Pret = Views.Mesager.Pret,
                IsAvion = Views.Mesager.IsAvion,
                IsTren = Views.Mesager.IsTren,
                Icon = Views.Mesager.Icon,
                Nume = Views.Mesager.Nume,
                Prenume = Views.Mesager.Prenume,
                //Tara = Views.Mesager.Tara,
                Titlu = Views.Mesager.Titlu,
                //NrStrada = Views.Mesager.NrStrada,
                //NrTelefon = Views.Mesager.NrTelefon,
                //DataNasterii = Views.Mesager.DataNasterii,
                //BagajCala = Views.Mesager.BagajCala,
                //Email = Views.Mesager.Email,
                //Oras = Views.Mesager.Oras,
                //CodPostal = Views.Mesager.CodPostal,
                //Strada = Views.Mesager.Strada,
                Icon2 = Views.Mesager.Icon2,
                IsIntoarcere = Views.Mesager.IsIntoarcere,
                //IconCompanieZbor = Views.Mesager.IconCompanieZbor,
                OraPlecare = Views.Mesager.OraPlecare,
                OraIntoarcere = Views.Mesager.OraIntoarcere,
                Color = Views.Mesager.Color,
                Peron = Views.Mesager.Peron,
                Peron2 = Views.Mesager.Peron2,
                NrTransport = Views.Mesager.NrTransport,
                QRCodeTextValue = Views.Mesager.QRCodeTextValue,
                NrTransport2 = Views.Mesager.NrTransport2,
                Scaun = Views.Mesager.Scaun,
                Scaun2 = Views.Mesager.Scaun2,
                //DurataDrum = Views.Mesager.DurataDrum,
                //Distance = Views.Mesager.Distance,
                //DurataDrumEscala = Views.Mesager.DurataDrumEscala,
                //DurataDrumPanaLaEscala = Views.Mesager.DurataDrumPanaLaEscala,
                //DestinatieEscala = Views.Mesager.Destinatie,
                //DurataEscala = Views.Mesager.DurataEscala,
                //NrTransportEscala = Views.Mesager.NrTransportEscala,
                //OraPlecareEscala = Views.Mesager.OraPlecareEscala,
                //PlecareEscala = Views.Mesager.PlecareEscala,
                //CompanieZbor = Views.Mesager.CompanieZbor,
                //TipTren = Views.Mesager.TipTren,
            });

            switch (Bilet2[0].Transport)
            {
                case "Avion":
                    {
                        Bilet2[0].Color = "#d24c4c";
                        break;
                    }
                case "Tren":
                    {
                        Bilet2[0].Color = "#61aed9";
                        break;
                    }
                case "Autocar":
                    {
                        Bilet2[0].Color = "#5e3dc6";
                        break;
                    }
            }

            ListaDescriereBilet.ItemsSource = Bilet2;

          /*   switch (Bilet.Bilet[0].Transport)
             {
                 case "Avion":
                     {
                         Bilet.Bilet[0].Color = "#d24c4c";
                         break;
                     }
                 case "Tren":
                     {
                         Bilet.Bilet[0].Color = "#61aed9";
                         break;
                     }
                 case "Autocar":
                     {
                         Bilet.Bilet[0].Color = "#5e3dc6";
                         break;
                     }
             }

             if(Bilet.Bilet[0].TipBilet == "Dus-Intors")
                 Bilet.Bilet[0].IsIntoarcere = true;
             else Bilet.Bilet[0].IsIntoarcere = false;

             BindingContext = Bilet;*/


            Shell.SetBackButtonBehavior(this, new BackButtonBehavior //Seteaza noua comanda pentru butonul de back
            {
                Command = new Command(() =>
                {
                    Navigation.PopAsync();
                }),
            });
        }
        
    }
}