using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proiect_Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Despre_Aplicatie : ContentPage
    {

        public class Items
        {
            public string ImageURL { get; set; }
            public string Text { get; set; }
        }

        List<Items> Images = new List<Items>();
        public Despre_Aplicatie()
        {
            InitializeComponent();

            Shell.SetBackButtonBehavior(this, new BackButtonBehavior //Seteaza noua comanda pentru butonul de back
            {
                Command = new Command(() =>
                {
                    Navigation.PopToRootAsync();
                }),
            });

            Images.Add(new Items
            {
                ImageURL = "https://images.prismic.io/bounce/82e9d02b-d9e0-4d7e-a468-3ae192a13c7e_adrian-luican-eqq8CjOwCYY-unsplash.jpg?auto=compress,format",
                Text = "Aplicația noastră oferă utilizatorilor o multitudine de oferte avantajoase către cele mai frumoase locații din România.  " +
                "Oferim ajutor și sfaturi utile în planificarea vacanțelor de vis."
            });
            Images.Add(new Items
            {
                ImageURL = "https://images.fineartamerica.com/images/artworkimages/mediumlarge/2/the-beautiful-medieval-city-of-sighisoara-from-transylvania-romania-seen-at-night-george-afostovremea.jpg",
                Text = "Descoperiți unele din cele mai frumoase peisaje ale României și începeți aventuri de neuitat alături de cei dragi" +
                " folosind oricare din mijloacele de transport oferite."
            });
            Images.Add(new Items
            {
                ImageURL = "https://hotel-coandi-arad.booked.net/data/Photos/OriginalPhoto/10656/1065668/1065668374/Hotel-Coandi-Arad-Exterior.JPEG",
                Text = "Suntem implicați permanent în dezvoltarea serviciilor oferite. Iar prin aplicația noastră puteți " +
                "închiria camere de hotel, mașini și asigurări de călătorie prin doar câțiva pași simpli."
            });
            CarouselViewPagDescriere.ItemsSource = Images;
            /*Shell.SetBackButtonBehavior(this, new BackButtonBehavior //Seteaza noua comanda pentru butonul de back
            {
                Command = new Command(() =>
                {
                    Navigation.PopToRootAsync();
                }),
                IconOverride = "back.png"
            });*/
        }
    }
}