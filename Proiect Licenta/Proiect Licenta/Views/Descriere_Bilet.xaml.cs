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
        ViewModels.BiletViewModel Bilet = new ViewModels.BiletViewModel();

        public Descriere_Bilet()
        {
            InitializeComponent();

            switch (Bilet.Bilet[0].Transport)
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
            
            BindingContext = Bilet;

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