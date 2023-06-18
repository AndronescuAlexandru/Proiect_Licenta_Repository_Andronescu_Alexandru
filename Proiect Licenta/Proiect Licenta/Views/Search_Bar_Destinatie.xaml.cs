using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proiect_Licenta.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Search_Bar_Destinatie : ContentPage
	{
		string[] locatii =
		{
			"București","Timișoara","Cluj-Napoca",  "Braşov",   "Iaşi", "Constanţa",    "Craiova",  "Galaţi",   "Ploieşti", "Oradea",   "Brăila",   "Arad", "Piteşti",
			"Sibiu",    "Bacău",    "Târgu-Mureş",  "Baia Mare",    "Buzău",    "Botoşani", "Satu Mare",    "Râmnicu Vâlcea",   "Drobeta-Turnu Severin",
			"Tulcea",   "Suceava",  "Piatra Neamţ", "Reşiţa",   "Târgu Jiu",    "Târgovişte",   "Focşani",  "Bistriţa", "Slatina",  "Călăraşi", "Alba Iulia",
			"Giurgiu",  "Deva", "Hunedoara",    "Zalău",    "Sfântu-Gheorghe",  "Bârlad",   "Vaslui",   "Roman",    "Turda",    "Mediaş",   "Slobozia", "Alexandria",
			"Voluntari",    "Miercurea-Ciuc",   "Lugoj",    "Medgidia", "Oneşti",   "Sighetu Marmaţiei",    "Petroşani",    "Mangalia", "Tecuci",
			"Odorheiu Secuiesc",    "Râmnicu Sărat",    "Paşcani",  "Dej",  "Reghin",   "Năvodari", "Câmpina",  "Mioveni",  "Câmpulung",    "Caracal",  "Săcele",
			"Făgăraş",  "Feteşti",  "Sighişoara",   "Borşa",    "Roşiori de Vede",  "Curtea de Argeş",  "Sebeş",    "Turnu Măgurele",   "Caransebeş",   "Dorohoi",
			"Câmpia Turzii",    "Târgu Neamţ",  "Târgu Secuiesc",   "Câmpulung Moldovenesc",    "Buşteni",  "Predeal",  "Azuga",    "Sinaia",   "Timisu de Jos",
			"Timisu de Sus",    "Poiana Tapului",   "Adjud",    "Bragadiru Teleorman",  "Târgu Ocna",   "Eforie Nord",  "Eforie Sud","Bragadiru Teleorman","Iași"

		};
		string[] aeroporturi = { "Arad", "Bacău", "Baia Mare", "București", "Brașov", "Cluj-Napoca", "Constanța", "Craiova", "Iași", "Galați-Brăila",
			"Oradea", "Satu Mare", "Sibiu", "Suceava", "Târgu Mureș", "Timișoara", "Tulcea" };
		string[] gari = {"Arad","Gara Aradul Nou","Bacău","Baia Mare","Brașov","Sinaia","Busteni","Azuga","Predeal","Braila","Brăila","Gara Basarab București",
			"Gara Băneasa București","Gara București Nord","Gara Obor București","Gara Progresul București","Buzău","Gara Buzău Nord","Gara Buzău Sud",
			"Cluj-Napoca","Constanța","Craiova","Focșani","Galați","Iași", "Iași Internațional (Nicolina)","Oradea","Piatra Neamţ","Pitești","Pitești Nord",
			"Ploiești Nord","Ploiești Sud","Ploiești Est","Ploiești Vest","Rădăuți","Râmnicu Vâlcea","Roman","Satu Mare","Sibiu","Slobozia","Gara Suceava (Burdujeni)",
			"Gara Suceava Nord (Ițcani)","Gara Suceava Vest (Șcheia)","Târgu Mureș","Teiuș","Timișoara Nord","Timișoara Vest","Vatra Dornei","Gara Alba Iulia","Gara Alexandria",
		"Gara Alexandria Nord","Gara Băile Herculane","Bistrita Nord","Botoșani","Gara Caracal","Gara Caransebeș","Gara Caransebeș Triaj","Gara Călărași Sud",
		"Câmpia Turzii","Câmpulung","Câmpulung Moldovenesc","Curtea de Argeș","Deva","Drobeta-Turnu Severin","Mangalia","Eforie Nord","Eforie Sud","Fetești","Făgăraș",
		"Giurgiu","Giurgiu Nord","Hunedoara","Miercurea Ciuc","Targoviste","Timisu de Jos","Timisu de Sus","Poiana Tapului","Gara Călăraşi Sud","Gara Călăraşi Nord",
		"Tulcea","Bârlad","Târgu Ocna","Zalău"};
		void SearchButtonPressedDestinatie(object sender, System.EventArgs e)
		{
			SearchBar searchBar = (SearchBar)sender;
			StringComparison ComparisonRule = StringComparison.OrdinalIgnoreCase;
			var cautare = locatii.Where(c => c.Contains(searchBar.Text));
			switch (Mesager.Transport)
			{
				case "Avion":
					{
						cautare = aeroporturi.Where(c => c.IndexOf(searchBar.Text, ComparisonRule) >= 0);
						break;
					}
				case "Tren":
					{
						cautare = gari.Where(c => c.IndexOf(searchBar.Text, ComparisonRule) >= 0);
						break;
					}
				case "Autocar":
					{
						cautare = locatii.Where(c => c.IndexOf(searchBar.Text, ComparisonRule) >= 0);
						break;
					}
			}
			SearchList.ItemsSource = cautare;
			//SearchList.ItemsSource = searchBar.Text;
		}
		void SearchResultsClicked(object sender, System.EventArgs e)
		{
			var button = (Button)sender;
			Mesager.Destinatie = button.Text;
			Navigation.PopAsync();
		}
		public Search_Bar_Destinatie ()
		{
			InitializeComponent ();
		}
	}
}