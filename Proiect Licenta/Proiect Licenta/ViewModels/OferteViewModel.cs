using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin;
using Xamarin.Forms.Maps;

namespace Proiect_Licenta.ViewModels
{
    internal class OferteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

		readonly string[] locatii =
		{
			"București","Timișoara","Cluj-Napoca",  "Braşov",   "Iași", "Constanţa",    "Craiova",  "Galaţi",   "Ploiești", "Oradea",   "Brăila",   "Arad", "Piteşti",
			"Sibiu",    "Bacău",    "Târgu-Mureş",  "Baia Mare",    "Buzău",    "Botoșani", "Satu Mare",    "Râmnicu Vâlcea",   "Drobeta-Turnu Severin",
			"Tulcea",   "Suceava",  "Piatra Neamț", "Reşiţa",   "Târgu Jiu",    "Târgovişte",   "Focșani",  "Bistrița", "Slatina",  "Călăraşi", "Alba Iulia",
			"Giurgiu",  "Deva", "Hunedoara",    "Zalău",    "Sfântu-Gheorghe",  "Bârlad",   "Vaslui",   "Roman",    "Turda",    "Mediaş",   "Slobozia", "Alexandria",
			"Voluntari",    "Miercurea-Ciuc",   "Lugoj",    "Medgidia", "Oneşti",   "Sighetu Marmaţiei",    "Petroşani",    "Mangalia", "Tecuci",
			"Odorheiu Secuiesc",    "Râmnicu Sărat",    "Paşcani",  "Dej",  "Reghin",   "Năvodari", "Câmpina",  "Mioveni",  "Câmpulung",    "Caracal",  "Săcele",
			"Făgăraş",  "Feteşti",  "Sighişoara",   "Borşa",    "Roşiori de Vede",  "Curtea de Argeş",  "Sebeş",    "Turnu Măgurele",   "Caransebeș",   "Dorohoi",
			"Câmpia Turzii",    "Târgu Neamţ",  "Târgu Secuiesc",   "Câmpulung Moldovenesc",    "Buşteni",  "Predeal",  "Azuga",    "Sinaia",   "Timisu de Jos",
			"Timisu de Sus",    "Poiana Tapului",   "Adjud",    "Bragadiru Teleorman",  "Târgu Ocna",   "Eforie Nord",  "Eforie Sud","Bragadiru Teleorman"

		};
		readonly string[] aeroporturi = { "Arad", "Bacău", "Baia Mare", "București", "Brașov", "Cluj-Napoca", "Constanța", "Craiova", "Iași", "Galați-Brăila",
			"Oradea", "Satu Mare", "Sibiu", "Suceava", "Târgu Mureș", "Timișoara", "Tulcea" };

		readonly string[] gari = {"Arad","Gara Aradul Nou","Bacău","Baia Mare","Brașov","Sinaia","Buşteni","Azuga","Predeal","Brăila","Gara Basarab București",
			"Gara Băneasa București","Gara București Nord","Gara Obor București","Gara Progresul București","Buzău","Gara Buzău Nord","Gara Buzău Sud",
			"Cluj-Napoca","Constanța","Craiova","Focșani","Galați","Iași", "Iași Internațional (Nicolina)","Oradea","Piatra Neamț","Pitești","Pitești Nord",
			"Ploiești Nord","Ploiești Sud","Ploiești Est","Ploiești Vest","Rădăuți","Râmnicu Vâlcea","Roman","Satu Mare","Sibiu","Slobozia","Gara Suceava (Burdujeni)",
			"Gara Suceava Nord (Ițcani)","Gara Suceava Vest (Șcheia)","Târgu Mureș","Teiuș","Timișoara Nord","Timișoara Vest","Vatra Dornei","Gara Alba Iulia","Gara Alexandria",
		"Gara Alexandria Nord","Gara Băile Herculane","Bistrita Nord","Botoșani","Gara Caracal","Gara Caransebeș","Gara Caransebeș Triaj","Gara Călărași Sud",
		"Câmpia Turzii","Câmpulung","Câmpulung Moldovenesc","Curtea de Argeș","Deva","Drobeta-Turnu Severin","Mangalia","Eforie Nord","Eforie Sud","Fetești","Făgăraș",
		"Giurgiu","Giurgiu Nord","Hunedoara","Miercurea-Ciuc","Targoviste","Timisu de Jos","Timisu de Sus","Poiana Tapului","Gara Călăraşi Sud","Gara Călăraşi Nord",
		"Tulcea","Bârlad","Târgu Ocna","Zalău"}; // Lista locatiilor posibile care va fi folosita de functia GenerareOferte

		readonly string[] locatiiOferte = { "București", "Bacău", "Oradea", "Brașov", "Timișoara","Cluj-Napoca","Constanța","Craiova",
			"Iași","Ploiești","Arad","Sebeș","Câmpulung-Muscel","Bistrița","Botoșani","Făgăraș","Buzău","Caransebeș","Mangalia","Miercurea-Ciuc","Deva",
		"Hunedoara","Petroșani","Baia Mare","Sighetu Marmației","Drobeta-Turnu Severin","Sighișoara","Piatra Neamț","Sinaia","Bușteni","Azuga","Predeal"};

		readonly string[] locatiiOferteAeroport = {"București","Bacău", "Oradea", "Brașov", "Timișoara", "Cluj-Napoca", "Constanța", "Craiova", "Iași",
			"Satu Mare", "Sibiu","Satu Mare","Suceava","Târgu Mureș","Tulcea"};


		readonly string[] TransportB = { "Tren", "Avion", "Autocar" }; // Lista tipurilor de transport care va fi folosita de functia GenerareOferte

        readonly string[] ClasaAvion = { "Economica", "Economica Premium", "Intai", "Business" }; // Lista claselor pentru zboruri care va fi folosita de functia GenerareOferte

        readonly string[] ClasaTren = { "Intai", "A doua" }; // Lista claselor pentru trenuri care va fi folosita de functia GenerareOferte

        readonly string[] TipBilet = { "Dus", "Dus-Intors" }; // Tipurile de bilete care vor fi folosite de functia GenerareOferte

		readonly string[] PasagerB = { "Adult", "Student", "Adolescent", "Copil", "Infant", "None" }; // Lista cu tipurile de pasageri posibili pentru bilet care va fi folosita de functia GenerareOferte

		readonly string[] luna = { "Ianuarie", "Februarie", "Martie", "Aprilie", "Mai", "Iunie", "Iulie", "August", "Septembrie", "Octombrie", "Noiembrie", "Decembrie" };

		readonly string[] hour = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };

		readonly string[] minute = { "00", "10", "20", "30", "40", "50" };

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		ObservableCollection<Views.Oferta> _Oferte;
		public ObservableCollection<Views.Oferta> Oferte
		{
			get { return _Oferte; }
			set
			{
				_Oferte = value;
				if (value != null)
					OnPropertyChanged();
			}
		}
		string SetImage(string Destinatie)
		{
			string IMG;
			switch (Destinatie)
			{
				case "București":
					{
						IMG = "https://suitehome.ro/img/slider/slider_6.jpg";
						return IMG;
					}
				case "Bacău":
					{
						IMG = "https://urlaub-in-rumänien.de/wp-content/uploads/2017/02/bacau4-1-1.jpg";
						return IMG;
					}
				case "Oradea":
					{
						IMG = "https://www.oradeainimagini.ro/wp-content/uploads/2014/09/10558910_1536619179899228_146476948_o.jpg";
						return IMG;
					}
				case "Brașov":
					{
						IMG = "https://motel-delorean.ro/lib/hfriys/Motel-Delorean---18-kzkg95n0.jpg";
						return IMG;
					}
				case "Timișoara":
					{
						IMG = "https://simonamocanu.ro/wp-content/uploads/2018/04/DSC02626.jpg";
						return IMG;
					}
				case "Cluj-Napoca":
					{
						IMG = "https://urlaub-in-rumänien.de/wp-content/uploads/2017/06/cluj-1.jpg";
						return IMG;
					}
				case "Constanța":
					{
						IMG = "https://www.crafted-tours-romania.com/images/attractions/large/constanta-casino-1.jpg";
						return IMG;
					}
				case "Craiova":
					{
						IMG = "https://scontent.fotp1-2.fna.fbcdn.net/v/t31.18172-8/24958772_268978183629170_3645193540153655438_o.jpg?stp=dst-jpg_p640x640&_nc_cat=103&ccb=1-7&_nc_sid=8631f5&_nc_ohc=jljCHZ-1oRwAX9REJOQ&_nc_ht=scontent.fotp1-2.fna&oh=00_AfDzZQmSsrmcOr78v3bLziQlEQmRQAFmatu5I2VVVvphyw&oe=644290F9";
						return IMG;
					}
				case "Iași":
					{
						IMG = "https://media.dcnews.ro/image/201812/full/palat_08291200.jpg";
						return IMG;
					}

				case "Ploiești":
					{
						IMG = "https://indextaxi.ro/wp-content/uploads/2015/01/index-taxi-ploiesti.jpg";
						return IMG;
					}
				case "Arad":
					{
						IMG = "https://romaniatourism.com/images/arad/arad-romania-2.jpg";
						return IMG;
                    }
				case "Sebeș":
					{
						IMG = "https://www.imperialtransilvania.com/typo3temp/pics/s_f2a59dd76a.jpg";
						return IMG;
					}
				case "Câmpulung-Muscel":
					{
						IMG = "https://scontent.fotp1-2.fna.fbcdn.net/v/t31.18172-8/17546811_1447809968626238_8566899732691119981_o.jpg?_nc_cat=103&ccb=1-7&_nc_sid=19026a&_nc_ohc=-8EYWpOJ__kAX_4m48Z&_nc_ht=scontent.fotp1-2.fna&oh=00_AfAagYw2P3X4VOghZsQtp-j3YhAwiBl8r0QGU9ZudKhPUg&oe=6447B6ED";
						return IMG;
					}
				case "Bistrita":
					{
						IMG = "https://romaniatourism.com/images/bistrita-romania.jpg";
						return IMG;
					}
				case "Botoșani":
					{
						IMG = "https://marketplace.intelligentcitieschallenge.eu/files/imager/images/city/botosani/393/botosani_101d7b8599c0cc599ee16e9637ccc19e.jpg";
						return IMG;
					}

				case "Făgăraș":
					{
						IMG = "https://blog.minimap.ro/wp-content/uploads/2019/12/obiective-turistice-fagaras.jpg";
						return IMG;
					}
				case "Buzău":
					{
						IMG = "https://urlaub-in-rumänien.de/wp-content/uploads/2017/02/buzau2-1-1.jpg";
						return IMG;
					}
				case "Caransebeș":
					{
						IMG = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9e/Caransebes_Primaria.JPG/800px-Caransebes_Primaria.JPG";
						return IMG;
					}
				case "Mangalia":
					{
						IMG = "https://img.directbooking.ro/getimage.ashx?f=Statiuni&w=600&h=399&file=Statiune_10_Mangalia5e59530a-b702-42b0-bb1b-9155e9a489e7.jpg";
						return IMG;
					}

				case "Miercurea-Ciuc":
					{
						IMG = "https://cdn.britannica.com/70/150870-050-8B8494F5/Miercurea-Ciuc-Romania.jpg";
						return IMG;
					}
				case "Deva":
					{
						IMG = "https://www.forbes.ro/wp-content/uploads/2021/10/102-orase-15-Deva.jpg";
						return IMG;
					}
				case "Hunedoara":
					{
						IMG = "https://incotro.olx.ro/wp-content/uploads/2021/09/orasulhunedoara-scaled.jpg";
						break;
					}
				case "Petroșani":
					{
						IMG = "https://www.replicahd.ro/wp-content/uploads/2021/04/Petrosani.jpg";
						return IMG;
					}
				case "Baia Mare":
					{
						IMG = "https://www.emaramures.ro/wp-content/uploads/2019/10/72655754_10215507782154608_9001841974528966656_o.jpg";
						return IMG;
					}
				case "Sighetu Marmației":
					{
						IMG = "https://i.pinimg.com/originals/d1/1d/b7/d11db7835a0d5082eab367bb55b8a00c.jpg";
						return IMG;
					}

				case "Drobeta-Turnu Severin":
					{
						IMG = "https://www.daibau.ro/showfile.php?id=62730";
						return IMG;
					}
				case "Sighișoara":
					{
						IMG = "https://cunoastetitara.md/wp-content/uploads/2019/05/sighisoara-poza-excursii-in-romania.png";
						return IMG;
					}
				case "Piatra Neamț":
					{
						IMG = "https://urlaub-in-rumänien.de/wp-content/uploads/2017/10/piatra_neamt.jpg";
						return IMG;
					}
				case "Sinaia":
                    {
						IMG = "https://www.mywanderlust.pl/wp-content/uploads/2022/11/sinaia-romania-121.jpg";
						return IMG;
					}
				case "Buşteni":
                    {
						IMG = "https://cdn.economedia.ro/wp-content/uploads/2021/11/busteni-enjoy-tomania.jpg";
						return IMG;
					}
				case "Azuga":
                    {
						IMG = "https://www.turistderomania.ro/tdruplfiles/statiunea-azuga.jpg";
						return IMG;
					}
				case "Predeal":
                    {
						IMG = "https://blog.helloromania.com/wp-content/uploads/2021/07/Predeal-Brasov-1200x750.jpg";
						return IMG;
					}
				case "Satu Mare":
                    {
						IMG = "https://s.iw.ro/gateway/g/ZmlsZVNvdXJjZT1odHRwJTNBJTJGJTJG/c3RvcmFnZTA2dHJhbnNjb2Rlci5yY3Mt/cmRzLnJvJTJGc3RvcmFnZSUyRjIwMjAl/MkYxMSUyRjE4JTJGMTI1MzU3NV8xMjUz/NTc1X3NhdHUtbWFyZS5qcGcmdz03ODAm/aD00NDAmaGFzaD01YzBmNjA4M2M2YzQyM2NjY2E1NjAxZDUwMmZkZjIyMA==.thumb.jpg";
						break;

					}
				case "Suceava":
					{
						IMG = "https://romaniatourism.com/images/suceava.jpg";
						break;
					}
				case "Târgu Mureș":
                    {
						IMG = "https://romaniatourism.com/images/targu-mures/targu-mures-2.jpg";
						break;
                    }
				case "Tulcea":
                    {
						IMG = "https://loyal-travel.com/blog/files/2017/04/Tulcea-port-Danube-Delta_cs.jpg";
						break;
                    }
				case "Sibiu":
                    {
						IMG = "https://www.romaniaforall.it/wp-content/uploads/2021/11/Sibiu.jpg";
						break;
                    }
			}
            IMG = "https://cdn.catine.ro/wp-content/uploads/2022/07/destinatii-de-vis-langa-bucuresti-790x460.jpg";
			return IMG;
		}

		string GenerareTimp(string TipTransport, double Distance, string sep)
		{
			string final_time = " ";
			byte hours, mins;
			double time;
			switch (TipTransport)
			{
				case "Avion":
					{
						time = Distance * 0.18;
						if (time < 60)
							return final_time = Convert.ToInt32(time).ToString();
						hours = Convert.ToByte(time / 60);
						time = time * 10;
						mins = Convert.ToByte(time % 10);
						return final_time = hours.ToString() + sep + mins.ToString();
					}
				case "Tren":
					{
						time = Distance * 0.9;
						if (time < 60)
							return final_time = Convert.ToInt32(time).ToString();
						hours = Convert.ToByte(time / 60);
						time = time * 10;
						mins = Convert.ToByte(time % 10);
						return final_time = hours.ToString() + sep + mins.ToString();
					}
				case "Autocar":
					{
						time = Distance * 1;
						if (time < 60)
							return final_time = Convert.ToInt32(time).ToString();
						hours = Convert.ToByte(time / 60);
						time = time * 10;
						mins = Convert.ToByte(time % 10);
						return final_time = hours.ToString() + sep + mins.ToString();
					}
			}
			return final_time;
		}
		void GenerareOferte(int n) //Functia preia listele de mai sus si genereaza n oferte
		{
			Oferte = new ObservableCollection<Views.Oferta>();
			Random numarR = new Random(); //variabila care memorieaza numere la intamplare
			ushort pret = 1, month, monthIntoarcere, dayP,dayI;
			uint ID;
			bool avion = false, intoarcere = true;
			string LocatiePlecare = " ", LocDestinatie = " ", Transport, ClasaB = " ", icon = "missing.png", iconsecond = "missing.png",scaun, tip;
            string nrTransport = " ", nrTransport2 = " ", scaun2 = " ", durataDrum, color = " ";
            byte line = 0, line2 = 0;

			string[] Pasageri = new string[5];
			DateTime Dplecare = DateTime.Now , Dintoarcere = DateTime.Now, DateAux = DateTime.Now;
			TimeSpan timePlecare = DateTime.Now.TimeOfDay, timeIntoarcere = DateTime.Now.TimeOfDay;
			Position Position1 = new Position(44.48945,26.21734);
			Position Position2 = new Position(46.7667, 23.6);
			Distance Distance1 = Distance.BetweenPositions(Position1, Position2);

			for (uint i = 0; i < n; i++)       //nr de bilete generate
			{
				ID = i;                          // se alege numarul corespunzator pentru ID ul biletului
				Transport = TransportB[numarR.Next(3)]; // se alege la intamplare tipul de transport al biletului
				scaun = numarR.Next(1, 128).ToString() + (Char)numarR.Next(65, 90); // se genereaza un nr de scaun pentru bilet format dintr-un nr 1-128 si o litera A-Z
				month = (ushort)numarR.Next(1, 12);
				nrTransport = (Char)numarR.Next(65, 90) + numarR.Next(1, 256).ToString();
				line = (byte)numarR.Next(1, 32);
				timePlecare = TimeSpan.Parse(hour[numarR.Next(0, 22)] + ":" + minute[numarR.Next(0, 5)]);
				Dplecare = Dplecare.AddHours(timePlecare.Hours);
				Dplecare = Dplecare.AddMinutes(timePlecare.Minutes);

				for (int j = 0; j < 5; j++)
					Pasageri[j] = PasagerB[numarR.Next(0, 6)];
				switch (Transport)
				{
					case "Avion":
						{
							int index = numarR.Next(4);
							ClasaB = ClasaAvion[index];

							avion = true;

							icon = "avion_icon.png";
							iconsecond = "avion_filled_black.png";
							color = "#af6262";

							LocatiePlecare = aeroporturi[numarR.Next(16)]; // se alege la intamplare o locatie pentru plecare					
							LocDestinatie = locatiiOferteAeroport[numarR.Next(0,15)];

							if(CoordonateLocatii.latitude.ContainsKey(LocatiePlecare) && CoordonateLocatii.latitude.ContainsKey(LocDestinatie))
                            {
								Position1 = new Position(CoordonateLocatii.latitude[LocatiePlecare], CoordonateLocatii.longitude[LocatiePlecare]);
								Position2 = new Position(CoordonateLocatii.latitude[LocDestinatie], CoordonateLocatii.longitude[LocDestinatie]);
								Distance1 = Distance.BetweenPositions(Position1, Position2);
							}

							pret = (ushort)(numarR.Next(0, 25) + (pret + (Convert.ToInt32(Distance1.Kilometers) / 10)) * 4 + 5 * (index + 1));
							break;
						}
					case "Tren":
						{
							int index = numarR.Next(2);
							ClasaB = ClasaTren[index];

							avion = false;

							icon = "tren_icon.png";
							iconsecond = "tren_lateral.png";
							color = "#61aed9";

							LocatiePlecare = gari[numarR.Next(40)]; // se alege la intamplare o locatie pentru plecare	
							LocDestinatie = locatiiOferte[numarR.Next(32)];

							if (CoordonateLocatii.latitude.ContainsKey(LocatiePlecare) && CoordonateLocatii.latitude.ContainsKey(LocDestinatie))
							{
								Position1 = new Position(CoordonateLocatii.latitude[LocatiePlecare], CoordonateLocatii.longitude[LocatiePlecare]);
								Position2 = new Position(CoordonateLocatii.latitude[LocDestinatie], CoordonateLocatii.longitude[LocDestinatie]);
								Distance1 = Distance.BetweenPositions(Position1, Position2);
							}

							pret = (ushort)(numarR.Next(0, 15) + (pret + (Convert.ToInt32(Distance1.Kilometers) / 10)) + 15 + 5 * (index + 1));
							break;
						}
					case "Autocar":
						{
							ClasaB = "Standard";

							avion = false;

							icon = "autocar_icon.png";
							iconsecond = "autocar_lateral.png";
							color = "#5e3dc6";

							LocatiePlecare = locatii[numarR.Next(40)]; // se alege la intamplare o locatie pentru plecare
							LocDestinatie = locatiiOferte[numarR.Next(32)];

							if (CoordonateLocatii.latitude.ContainsKey(LocatiePlecare) && CoordonateLocatii.latitude.ContainsKey(LocDestinatie))
							{
								Position1 = new Position(CoordonateLocatii.latitude[LocatiePlecare], CoordonateLocatii.longitude[LocatiePlecare]);
								Position2 = new Position(CoordonateLocatii.latitude[LocDestinatie], CoordonateLocatii.longitude[LocDestinatie]);
								Distance1 = Distance.BetweenPositions(Position1, Position2);
							}

							pret = (ushort)(numarR.Next(0, 10) + (pret + (Convert.ToInt32(Distance1.Kilometers) / 10)) + 5);
							break;
						}
				}

				/*if (CoordonateLocatii.latitude.ContainsKey(LocDestinatie) && CoordonateLocatii.latitude.ContainsKey(LocatiePlecare))
				{
					Position1 = new Position(CoordonateLocatii.latitude[LocatiePlecare], CoordonateLocatii.longitude[LocatiePlecare]);
					Position2 = new Position(CoordonateLocatii.latitude[LocDestinatie], CoordonateLocatii.longitude[LocDestinatie]);
				}*/

				while (LocatiePlecare == LocDestinatie)
				{
					LocatiePlecare = locatii[numarR.Next(30)]; // se alege la intamplare o locatie pentru plecare pana cand este diferita de destinatie
				}
				if (month == 2)
					dayP = (ushort)numarR.Next(1, 28);
				else
					dayP = (ushort)numarR.Next(1, 31);

				if(dayP < DateTime.Now.Day && month < DateTime.Now.Month)
                {
					month = (ushort)numarR.Next(DateTime.Now.Month, 12);
					if (month == 2)
						dayP = (ushort)numarR.Next(DateTime.Now.Day, 28);
					else
						dayP = (ushort)numarR.Next(DateTime.Now.Day, 31);
				}
				Dplecare = Dplecare.AddDays(dayP);
				Dplecare = Dplecare.AddMonths(month);
				monthIntoarcere = (ushort)numarR.Next(1, 12);
				if (monthIntoarcere < month)
					monthIntoarcere = (ushort)numarR.Next(month, 12);
				if (month == 2)
				{
					dayI = (ushort)numarR.Next(1, 28);
					if (dayI <= dayP)
						dayI = (ushort)numarR.Next(dayP, 28);
				}
				else
				{
					dayI = (ushort)numarR.Next(1, 31);
					if (dayI <= dayP)
						dayI = (ushort)numarR.Next(dayP, 31);
				}
				tip = TipBilet[numarR.Next(2)];
				if (tip == "Dus")
				{
					intoarcere = false;
				}
				else
					if (tip == "Dus-Intors")
				{
					intoarcere = true;
					pret = (ushort)(pret + 10);
					nrTransport2 = (Char)numarR.Next(65, 90) + numarR.Next(1, 256).ToString();
					scaun2 = numarR.Next(1, 128).ToString() + (Char)numarR.Next(65, 90);
					line2 = (byte)numarR.Next(1, 32);

					if (Dintoarcere.Day == Dplecare.Day)
					{
						Dintoarcere = Dintoarcere.AddHours(numarR.Next(3, 10));
						Dintoarcere = Dintoarcere.AddMinutes(numarR.Next(1, 59));
					}
					else
					{
						Dintoarcere = Dintoarcere.AddHours(numarR.Next(1, 23));
						Dintoarcere = Dintoarcere.AddMinutes(numarR.Next(1, 59));
					}
				}

				durataDrum = GenerareTimp(Transport, Convert.ToDouble(Distance1.Kilometers), " ore ");

				Oferte.Add(new Views.Oferta                        //in urma alegerii datelor se creeaza o oferta noua
				{
					Id = ID,
					LocPlecare = LocatiePlecare,
					Destinatie = LocDestinatie,
					DataPlecare = Dplecare.ToString("dd/MMMM"),
					DataIntoarcere = Dintoarcere.ToString("dd/MMMM"),
					TipBilet = tip,
					Clasa = ClasaB,
					Transport = Transport,
					Pasager = PasagerB[numarR.Next(0, 4)],
					Pret = pret,
					IsAvion = avion,
					Image = SetImage(LocDestinatie),
					Icon = icon,
					Icon2 = iconsecond,
					Scaun = scaun,
					Scaun2 = scaun2,
					IsIntoarcere = intoarcere,
					NrTransport = nrTransport,
					NrTransport2 = nrTransport2,
					Peron = line,
					Peron2 = line2,
					DurataDrum = durataDrum,
					Distance = Convert.ToInt16(Distance1.Kilometers).ToString(),
					OraPlecare = timePlecare.Hours.ToString() + ":" + timePlecare.Minutes.ToString(),
					OraIntoarcere = timeIntoarcere.ToString(),
					Color = color,
				});
				pret = 1;
			}
		}
        public OferteViewModel(int n)
        {
			GenerareOferte(n);
        }
    }
}
