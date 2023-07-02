using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

namespace Proiect_Licenta.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]

	public partial class Rezultate_Cautare : ContentPage
	{
		List<Bilet> BiletsT = new List<Bilet>();
		List<Bilet> Bilets = new List<Bilet>();

		SqlConnection sql;

		byte[] hours = new byte[5];
		byte[] mins = new byte[5];
		sbyte[] converter = new sbyte[5];

		string[] locatii =
		{
			"București","Timişoara","Cluj-Napoca",  "Braşov",   "Iaşi", "Constanţa",    "Craiova",  "Galaţi",   "Ploieşti", "Oradea",   "Brăila",   "Arad", "Piteşti",
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

		readonly string[] Locatii = {
				   "București", "Bacau","Oradea","Brașov","Timișoara","Brăila","Galați","Cluj-Napoca","Constanta","Craiova",
			"Iasi","Ploiesti","Arad","Aiud","Blaj","Sebeș","Câmpulung-Muscel","Curtea de Argeș","Onești","Moinești",
			"Bistrita","Botoșani","Dorohoi","Beiuș","Marghita","Salonta","Făgăraș","Codlea","Săcele","Buzău","Râmnicu Sărat",
			"Reșița","Caransebeș","Călărași","Oltenița","Turda","Dej","Câmpia Turzii","Gherla","Mangalia","Medgidia",
			"Sfântu Gheorghe","Târgu Secuiesc","Târgoviște","Moreni","Calafat","Băilești","Tecuci","Giurgiu","Târgu Jiu",
			"Motru","Miercurea Ciuc","Gheorgheni","Odorheiu Secuiesc","Toplița","Deva","Hunedoara","Brad","Lupeni",
			"Orăștie","Petroșani","Vulcan","Slobozia","Fetești","Urziceni","Pașcani","Baia Mare","Sighetu Marmației",
			"Roman","Drobeta-Turnu Severin","Orșova","Sighișoara","Reghin","Târnăveni","Piatra Neamț","Slatina","Caracal",
			"Câmpina","Satu Mare","Carei","Zalău","Sibiu","Mediaș","Suceava","Fălticeni","Rădăuți","Câmpulung Moldovenesc",
			"Vatra Dornei","Alexandria","Roșiori de Vede","Turnu Măgurele","Lugoj","Tulcea","Vaslui","Bârlad","Huși",
			"Focșani","Adjud","Râmnicu Vâlcea","Drăgășani"}; // Lista locatiilor posibile care va fi folosita de functia Generare Bilet
		
		readonly string[] TransportB = { "Tren", "Avion", "Autocar" }; // Lista tipurilor de transport care va fi folosita de functia Generare Bilet
		
		readonly string[] ClasaAvion = { "Economica", "Economica Premium", "Business","Intai" }; // Lista claselor pentru zboruri care va fi folosita de functia Generare Bilet
		
		readonly string[] ClasaTren = { "Intai","A doua"}; // Lista claselor pentru trenuri care va fi folosita de functia Generare Bilet
		
		readonly string[] TipBilet = { "Dus", "Dus-Intors" }; // Tipurile de bilete care vor fi folosite de functia Generare Bilet
		
		readonly string[] PasagerB = { "Adult", "Student", "Adolescent", "Copil", "Infant"}; // Lista cu tipurile de pasageri posibili pentru bilet care va fi folosita de functia Generare Bilet
		
		readonly string[] hour = { "01", "02", "03", "04", "05", "06","07","08","09","10","11","12","13","14","15","16","17","18","19","20","21","22","23" };
		
		readonly string[] minute = { "00", "10", "20", "30", "40", "50" };

		readonly string[] CompaniiAvion = {"Wizz Air","Blue Air","Tarom","Ryanair", "Lufthansa","easyJet","Carpatair","LOT","Air Moldova"};

		readonly string[] TipTren = {"Regio (R)","Inter Regio (IR)","Inter City (IC)", "Inter Regio Night (IRN)" };

		/*Functia GenerareBilet are 3 parametrii; Dplecare preia data plecarii aleasa de catre utilizator,
		 * Dintoarcere preia data de intoarcere daca este aleasa de utilizator si n este numarul dorit de bilete pentru a fi generate,
		 * functia preia listele de mai sus(Locatii,TransportB,ClasaAvion,ClasaTren,TipBilet,PasagerB) si genereaza n bilete*/

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

			sql.Open();
		}

		string GenerareTimp(string TipTransport, string Tip_Tren, double Distance, string sep)
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
						switch(Tip_Tren)
                        {
							case "Regio (R)":
                                {
									time = Distance * 1.53;
									break;
								}
							case "Inter Regio (IR)":
								{
									time = Distance * 0.92;
									break;
								}
							case "Inter City (IC)":
								{
									time = Distance * 0.8;
									break;
								}
							case "Inter Regio Night (IRN)":
								{
									time = Distance * 0.92;
									break;
								}
							default:
								{
									time = Distance * 0.92;
									break;
								}
						}
						
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

		sbyte[] TimeStringToByte(string Time)
        {
			sbyte[] ConvertedTime = new sbyte[5];
			for(int i = 0; i < Time.Length; i++)
            {
				if(Time[i] != ':' || !Char.IsLetter(Time[i]))
					ConvertedTime[i] = Convert.ToSByte(Time[i]);
				else
					ConvertedTime[i] = -1;
            }
			return ConvertedTime;
        }

		string AirlineIcon(string airline)
        {
			switch(airline)
            {
				case "Wizz Air":
                    {
						return "wizz_icon";
                    }
				case "Blue Air":
                    {
						return "blue_air_icon";
					}
				case "Tarom":
                    {
						return "tarom_icon";
					}
				case "Ryanair":
                    {
						return "ryanair_icon";
					}
				case "Lufthansa":
                    {
						return "lufthansa_icon";
					}
				case "easyJet":
                    {
						return "easyjet_icon";
					}
				case "Carpatair":
                    {
						return "carpatair_icon";
					}
				case "LOT":
                    {
						return "lot_icon";

					}
				case "Air Moldova":
                    {
						return "air_moldova_icon";
					}
			}
			return "tarom_icon";
		}

		void PreluareBilete()
        {
			Random numarR = new Random();

			string icon = "avion_icon2.png", iconsecond = "avion_filled_black.png", color = "#d24c4c";
			string companie = " ";
			string iconCompanie = AirlineIcon(companie);

			string queryString = "Select * from dbo.Bilete";
			SqlCommand cmd = new SqlCommand(queryString, sql);
			SqlDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				switch (reader["Tip_Transport"].ToString())
				{
					case "Avion":
						{

							companie = reader["Tip_Tren_Companie_Zbor"].ToString();
							iconCompanie = AirlineIcon(companie);

							icon = "avion_icon2.png";
							iconsecond = "avion_filled_black.png";
							color = "#d24c4c";


							break;
						}
					case "Tren":
						{
							icon = "tren_icon.png";
							iconsecond = "tren_lateral.png";
							color = "#61aed9";// culoare alternativa : #4383b2


							break;
						}
					case "Autocar":
						{
							icon = "autocar_icon.png";
							iconsecond = "autocar_lateral.png";
							color = "#5e3dc6";


							break;
						}
				}
				try
				{
					Bilets.Add(new Bilet                        //in urma alegerii datelor se creeaza un bilet nou
					{
						Id = Convert.ToUInt32(reader["ID"]),
						Id2 = Convert.ToUInt32(reader["ID_Retur"]),
						LocPlecare = reader["Loc_Plecare"].ToString(),
						Destinatie = reader["Destinatie"].ToString(),
						DataPlecare = reader["Data_Plecare"].ToString(),
						DataIntoarcere = reader["Data_Retur"].ToString(),
						DataPlecareEscala = reader["Data_Plecare_Escala"].ToString(),
						Activ = Convert.ToBoolean(reader["Activ"]),
						TipBilet = reader["Tip_Bilet"].ToString(),
						Clasa = reader["Clasa"].ToString(),
						Transport = reader["Tip_Transport"].ToString(),
						Pasager = reader["Tip_Pasager"].ToString(),
						Pret = Convert.ToInt32(reader["Pret"]),
						IsAvion = Convert.ToBoolean(reader["Is_Avion"]),
						IsTren = Convert.ToBoolean(reader["Is_Tren"]),
						IsEscala = Convert.ToBoolean(reader["Has_Escala"]),
						Icon = icon,
						Icon2 = iconsecond,
						IconCompanieZbor = iconCompanie,
						Scaun = reader["Scaun"].ToString(),
						Scaun2 = reader["Scaun_Retur"].ToString(),
						IsIntoarcere = Convert.ToBoolean(reader["Is_Intoarcere"]),
						OraPlecare = reader["Ore_Plecare"].ToString(),
						OraIntoarcere = reader["Ore_Intoarcere"].ToString(),
						Color = color,
						NrTransport = reader["Nr_Transport"].ToString(),
						NrTransport2 = reader["Nr_Transport_Retur"].ToString(),
						Peron = Convert.ToByte(reader["Peron"]),
						Peron2 = Convert.ToByte(reader["Peron_Retur"]),
						Distance = reader["Distanta"].ToString(),
						DurataDrum = reader["Durata_Drum"].ToString(),
						DurataDrumEscala = reader["Durata_Drum_Escala"].ToString(),
						DurataDrumPanaLaEscala = reader["Durata_Drum_Pana_La_Escala"].ToString(),
						DurataEscala = reader["Durata_Escala"].ToString(),
						PlecareEscala = reader["Plecare_Escala"].ToString(),
						DestinatieEscala = reader["Destinatie_Escala"].ToString(),
						OraPlecareEscala = reader["Ora_Plecare_Escala"].ToString(),
						NrTransportEscala = reader["Nr_Transport_Escala"].ToString(),
						CompanieZbor = companie,
						TipTren = reader["Tip_Tren_Companie_Zbor"].ToString(),
					});
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}

			}
			reader.Close();
			sql.Close();
		}

		void GenerareBilet(DateTime Dplecare, DateTime Dintoarcere, int n)
        {
			Random numarR = new Random(); //variabila care memorieaza numere la intamplare
			byte line = 0, line2 = 0, error = 200; 
			
			int pret;
			int ID, ID_Retur = 0;
			
			bool avion = false, intoarcere = false, tren = false;
			
			string Transport, ClasaB = " ", icon = "missing.png", iconsecond = "missing.png",scaun, tip, color = " ",nrTransport = " ", nrTransport2 = " ";
			string scaun2 = " ", durataDrum = " ",durataDrumPanaLaEscala = " ",LocPlecareEscala = " ", durataDrumTotal = " ", DurataEscala = " ", timePlecareEscala = " ";
			string tip_tren_companie = " ", iconCompanie = " ";

			string plecare = " ", destinatie = " ";

			double distantaEscala = 0;
			bool IsEscala = false;

			DateTime DataPlecareEscala = Convert.ToDateTime(Mesager.DataPlecare);
			TimeSpan timePlecare = DateTime.Now.TimeOfDay, timeIntoarcere = DateTime.Now.TimeOfDay;
			Position Position1 = new Position(44.48945, 26.21734);
			Position Position2 = new Position(46.7667, 23.6);
			Distance distance1 = Distance.BetweenPositions(Position1, Position2);


			durataDrum = GenerareTimp(Mesager.Transport, tip_tren_companie, Convert.ToDouble(distance1.Kilometers)," ore ");

			if (Mesager.IsFromOferta)
			{
				tip = Mesager.TipBilet;
				Transport = Mesager.Transport;
			}
			else
			{
				tip = TipBilet[numarR.Next(2)];
				Transport = TransportB[numarR.Next(3)]; // se alege la intamplare tipul de transport al biletului
			}

			if (Transport == "Avion")
				if (numarR.Next(2) == 1)
					IsEscala = true;

			if (CoordonateLocatii.latitude.ContainsKey(Mesager.Plecare) && CoordonateLocatii.latitude.ContainsKey(Mesager.Destinatie) && IsEscala == false)
			{
				Position1 = new Position(CoordonateLocatii.latitude[Mesager.Plecare], CoordonateLocatii.longitude[Mesager.Plecare]);
				Position2 = new Position(CoordonateLocatii.latitude[Mesager.Destinatie], CoordonateLocatii.longitude[Mesager.Destinatie]);
				distance1 = Distance.BetweenPositions(Position1, Position2);
				durataDrum = GenerareTimp(Mesager.Transport, tip_tren_companie, Convert.ToDouble(distance1.Kilometers), " ore ");
			}

			if(CoordonateLocatii.latitude.ContainsKey(Mesager.Plecare) && CoordonateLocatii.latitude.ContainsKey(Mesager.Destinatie) && IsEscala == true)
            {
				LocPlecareEscala = aeroporturi[numarR.Next(0, 17)];
				hours[0] = Convert.ToByte(numarR.Next(0, 6).ToString());
				mins[0] = Convert.ToByte(numarR.Next(0, 59).ToString());
				Position1 = new Position(CoordonateLocatii.latitude[Mesager.Plecare], CoordonateLocatii.longitude[Mesager.Plecare]);
				Position PositionEscala1 = new Position(CoordonateLocatii.latitude[LocPlecareEscala], CoordonateLocatii.longitude[LocPlecareEscala]);
				Position2 = new Position(CoordonateLocatii.latitude[Mesager.Destinatie], CoordonateLocatii.longitude[Mesager.Destinatie]);
				distance1 = Distance.BetweenPositions(Position1, PositionEscala1);
				Distance DistanceEscala = Distance.BetweenPositions(PositionEscala1, Position2);
				distantaEscala = DistanceEscala.Kilometers;
				durataDrumPanaLaEscala = GenerareTimp(Mesager.Transport, tip_tren_companie, Convert.ToDouble(distance1.Kilometers),":");
				DurataEscala = hours[0] + " ore " + mins[0];
				converter = TimeStringToByte(durataDrumPanaLaEscala);
				durataDrumPanaLaEscala = GenerareTimp(Mesager.Transport, tip_tren_companie, Convert.ToDouble(distance1.Kilometers), " ore ");

				//hours[1] = (byte)(Convert.ToByte(timePlecare.Hours) + Convert.ToByte(converter[0]) + Convert.ToByte(hours[0]));
				//mins[1] = (byte)(Convert.ToByte(timePlecare.Minutes) + mins[0]);

				durataDrum = GenerareTimp(Mesager.Transport, tip_tren_companie, Convert.ToDouble(distance1.Kilometers) + distantaEscala + Convert.ToDouble(error), " ore ");			
			}


			for (uint i = 0; i < n; i++)       //nr de bilete generate
            {
				timePlecare = TimeSpan.Parse(hour[numarR.Next(0, 22)] + ":" + minute[numarR.Next(0, 5)]);
				line = (byte)numarR.Next(1, 32);
				nrTransport = (Char)numarR.Next(65, 90) + numarR.Next(1, 256).ToString();	
				scaun = numarR.Next(1, 128).ToString() + (Char)numarR.Next(65, 90); // se genereaza un nr de scaun pentru bilet
				Dplecare = Dplecare.AddDays(numarR.Next(0, 30));
				Dplecare = Dplecare.AddMonths(numarR.Next(0, 12));
				Dplecare = Dplecare.AddHours(timePlecare.Hours);
				Dplecare = Dplecare.AddMinutes(timePlecare.Minutes);
				pret = 1;

				if (IsEscala == true)
                {
					//Urmatoarele 5 randuri sunt folosite pentru a adauga ora si minutul de plecare dupa terminarea escalei
					DataPlecareEscala = DataPlecareEscala.AddHours(timePlecare.Hours);
					DataPlecareEscala = DataPlecareEscala.AddHours(converter[0]);
					DataPlecareEscala = DataPlecareEscala.AddHours(hours[0]);
					DataPlecareEscala = DataPlecareEscala.AddMinutes(timePlecare.Minutes);
					DataPlecareEscala = DataPlecareEscala.AddMinutes(mins[0]);
					timePlecareEscala = DataPlecareEscala.Hour.ToString() + ":" + DataPlecareEscala.Minute.ToString();
					pret = pret - 20;
				}

				/* Daca data de intoarcere nu este aleasa atunci se seteaza tip ul biletului "Dus" si se seteaza flag-ul "intoarcere" cu valoarea false
				 * altfel se seteaza tip-ul biletului "Dus-Intors" si flag-ul "intoarcere" primeste valoarea true */

				ID = numarR.Next(1, 999999);                          // se alege numarul corespunzator pentru ID ul biletului

				if (tip == "Dus")
					intoarcere = false;
				else
				{
					intoarcere = true;

					pret = pret + 10;

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

					nrTransport2 = (Char)numarR.Next(65, 90) + numarR.Next(1, 256).ToString();

					scaun2 = numarR.Next(1, 128).ToString() + (Char)numarR.Next(65, 90);

					line2 = (byte)numarR.Next(1, 32);
					ID_Retur = ID + 1;
				}
	
				switch (Transport)
                {
					case "Avion":
                        {
							int index = numarR.Next(4);
							ClasaB = ClasaAvion[index];

							tip_tren_companie = CompaniiAvion[numarR.Next(0, 9)];
							iconCompanie = AirlineIcon(tip_tren_companie);

							avion = true;
							tren = false;

							icon = "avion_icon2.png";
							iconsecond = "avion_filled_black.png";
							color = "#d24c4c";

							plecare = aeroporturi[numarR.Next(0, aeroporturi.Length)];
							destinatie = aeroporturi[numarR.Next(0, aeroporturi.Length)];

							if (Mesager.IsFromOferta == true)
								while(pret < Mesager.Pret)
									pret = pret + numarR.Next(0, 25) + (pret + (Convert.ToInt32(distance1.Kilometers) / 10)) * 4 + 5 * (index + 1);
							else
								pret = pret + numarR.Next(0, 25) + (pret + (Convert.ToInt32(distance1.Kilometers) / 10)) * 4 + 5 * (index + 1);
							break;
                        }
					case "Tren":
                        {
							int index = numarR.Next(2);
							ClasaB = ClasaTren[index];

							tip_tren_companie = TipTren[numarR.Next(0, 3)];
							durataDrum = GenerareTimp(Mesager.Transport, tip_tren_companie, Convert.ToDouble(distance1.Kilometers), " ore ");

							avion = false;
							tren = true;

							icon = "tren_icon.png";
							iconsecond = "tren_lateral.png";
							color = "#61aed9";// culoare alternativa : #4383b2

							plecare = gari[numarR.Next(0, gari.Length)];
							destinatie = gari[numarR.Next(0, gari.Length)];

							if (Mesager.IsFromOferta == true)
								while (pret < Mesager.Pret)
									pret = pret + numarR.Next(0, 15) +  (pret + (Convert.ToInt32(distance1.Kilometers) / 10) ) + 15 + 5 * (index + 1);
							else
								pret = pret + numarR.Next(0, 15) + (pret + (Convert.ToInt32(distance1.Kilometers) / 10)) + 15 + 5 * (index + 1);
							break;
                        }
					case "Autocar":
                        {
							ClasaB = "Standard";

							avion = false;
							tren = false;

							icon = "autocar_icon.png";
							iconsecond = "autocar_lateral.png";
							color = "#5e3dc6";

							plecare = locatii[numarR.Next(0, locatii.Length)];
							destinatie = locatii[numarR.Next(0, locatii.Length)];

							if (Mesager.IsFromOferta == true)
								while (pret < Mesager.Pret)
									pret = pret + numarR.Next(0,10) + (pret + (Convert.ToInt32(distance1.Kilometers) / 10) ) + 5;
							else
								pret = pret + numarR.Next(0, 10) + (pret + (Convert.ToInt32(distance1.Kilometers) / 10)) + 5;
							break;
                        }
                }

				Bilets.Add(new Bilet                        //in urma alegerii datelor se creeaza un bilet nou
				{
					Id = Convert.ToUInt32(ID),
					Id2 = Convert.ToUInt32(ID_Retur),
					LocPlecare = Mesager.Plecare,
					Destinatie = Mesager.Destinatie,
					DataPlecare = Dplecare.Day.ToString() + "/" + Dplecare.Month.ToString() + "/" + Dplecare.Year.ToString(),
					DataIntoarcere = Dintoarcere.Day.ToString() + "/" + Dintoarcere.Month.ToString() + "/" + Dintoarcere.Year.ToString(),
					DataPlecareEscala = DataPlecareEscala.ToString(),
					Activ = true,
					TipBilet = tip,
					Clasa = ClasaB,
					Transport = Transport,
					Pasager = PasagerB[numarR.Next(0, 4)],
					Pret = pret,
					IsAvion = avion,
					IsTren = tren,
					IsEscala = IsEscala,
					Icon = icon,
					Icon2 = iconsecond,
					IconCompanieZbor = iconCompanie,
					Scaun = scaun,
					Scaun2 = scaun2,
					IsIntoarcere = intoarcere,
					OraPlecare = timePlecare.Hours.ToString() + ":" + timePlecare.Minutes.ToString(),
					OraIntoarcere = timeIntoarcere.ToString(),
					Color = color,
					NrTransport = nrTransport,
					NrTransport2 = nrTransport2,
					Peron = line,
					Peron2 = line2,
					Distance = Convert.ToInt16(distance1.Kilometers).ToString(),
					DurataDrum = durataDrum,
					DurataDrumEscala = GenerareTimp(Mesager.Transport, tip_tren_companie, Convert.ToDouble(distantaEscala), " ore "),
					DurataDrumPanaLaEscala = durataDrumPanaLaEscala,
					DurataEscala = DurataEscala,
					PlecareEscala = LocPlecareEscala,
					DestinatieEscala = Mesager.Destinatie,
					OraPlecareEscala = DataPlecareEscala.Hour.ToString() + ":" + DataPlecareEscala.Minute.ToString(),
					NrTransportEscala = (Char)numarR.Next(65, 90) + numarR.Next(1, 256).ToString(),
					CompanieZbor = tip_tren_companie,
					TipTren = tip_tren_companie,

				});

				Dplecare = Convert.ToDateTime(Mesager.DataPlecare);
				Dintoarcere = Convert.ToDateTime(Mesager.DataIntoarcere);
				DataPlecareEscala = Convert.ToDateTime(Mesager.DataPlecare);
			}
        }
		
		void AdaugareBilet(object sender, EventArgs e)
        {
			var button = (ImageButton)sender;
			var bilet = (Bilet)button.BindingContext;
			Mesager.Id = bilet.Id;
			Mesager.Pret = (ushort)bilet.Pret;
			Mesager.oldPret = bilet.Pret;
			Mesager.TipBilet = bilet.TipBilet;
			Mesager.Transport = bilet.Transport;
			Mesager.Pasager = bilet.Pasager;
			Mesager.Activ = bilet.Activ;
			Mesager.DataPlecare = bilet.DataPlecare;
			Mesager.DataIntoarcere = bilet.DataIntoarcere;
			Mesager.DataPlecareEscala = bilet.DataPlecareEscala;
			Mesager.Plecare = bilet.LocPlecare;
			Mesager.Destinatie = bilet.Destinatie;
			Mesager.Clasa = bilet.Clasa;
			Mesager.IsAvion = bilet.IsAvion;
			Mesager.IsTren = bilet.IsTren;
			Mesager.Icon = bilet.Icon;
			Mesager.Icon2 = bilet.Icon2;
			Mesager.IconCompanieZbor = bilet.IconCompanieZbor;
			Mesager.Scaun = bilet.Scaun;
			Mesager.Scaun2 = bilet.Scaun2;
			Mesager.IsIntoarcere = bilet.IsIntoarcere;
			Mesager.OraPlecare = bilet.OraPlecare;
			Mesager.OraIntoarcere = bilet.OraIntoarcere;
			Mesager.Color = bilet.Color;
			Mesager.Peron = bilet.Peron;
			Mesager.Peron2 = bilet.Peron2;
			Mesager.NrTransport = bilet.NrTransport;
			Mesager.NrTransport2 = bilet.NrTransport2;
			Mesager.PlecareEscala = bilet.PlecareEscala;
			Mesager.DestinatieEscala = bilet.DestinatieEscala;
			Mesager.DurataDrum = bilet.DurataDrum;
			Mesager.DurataDrumEscala = bilet.DurataDrumEscala;
			Mesager.DurataDrumPanaLaEscala = bilet.DurataDrumPanaLaEscala;
			Mesager.DurataEscala = bilet.DurataEscala;
			Mesager.OraPlecareEscala = bilet.OraPlecareEscala;
			Mesager.NrTransportEscala = bilet.NrTransportEscala;
			Mesager.CompanieZbor = bilet.CompanieZbor;
			Mesager.TipTren = bilet.TipTren;
			Navigation.PushAsync(new Adaugare_Bilet());
			

		}
		public string SetHeaderImage()
        {
			switch(Mesager.Transport)
            {
				case "Avion":
                    {
						return "avion.png";
                    }
				case "Tren":
                    {
						return "tren.png";
                    }
				case "Autocar":
                    {
						HeaderImageSource.ScaleY = 2.3;
						HeaderImageSource.ScaleX = 2.1;
						return "autocar.jpeg";
                    }
            }
			return "missing.png";
        }
		void BackButton(object sender, EventArgs e)
        {
			Navigation.PopAsync();
        }
        protected override void OnAppearing()
        {
			LabelColLocPlecare.Text = Mesager.Plecare;
			LabelColDestinatie.Text = Mesager.Destinatie;
			//LabelColDataPlecare.Date = Convert.ToDateTime(Mesager.DataPlecare);
			foreach (Bilet B in BiletsT)
			{
				B.LocPlecare = Mesager.Plecare;
			}
		}

		public Rezultate_Cautare ()
		{
			var InternetConnection = Connectivity.NetworkAccess;
			InitializeComponent ();
			OnAppearing();

			BindingContext = BiletsT;
			ListaBilete.ItemsSource =  BiletsT;
			LabelColDataPlecare.Text = Mesager.DataPlecare;

			if (Mesager.IsIntoarcere == true)
			{
				LabelColDataIntoarcere.Text = Mesager.DataIntoarcere;
				LabelColDataIntoarcere.IsVisible = true;
				LabelColDataPlecare.Margin = new Thickness(-15, 1, 0, 0);
			}	

			HeaderImageSource.Source = SetHeaderImage();

			if (InternetConnection == NetworkAccess.Internet)
			{
				ConnectToDB();
				PreluareBilete();
			}
			else
			{
				if (Models.ConnectedUser.Email == "dev@test.com")
				{
					DisplayAlert("Mod offline", "Nu se poate realiza o conecțiune la internet. Deoarece sunteți conectat la contul de developer, aplicația va genera automat bilete", "Ok");
					GenerareBilet(Convert.ToDateTime(Mesager.DataPlecare),Convert.ToDateTime(Mesager.DataIntoarcere), 100);
				}
				else
				{
					NothingFoundImg.Source = "no_internet.png";
					NoBiletLabel.Text = "Nu exista conectiune la internet.";
				}
			}

			//LabelColDataPlecare.MinimumDate = DateTime.Today;
			//NavigationPage.SetTitleIconImageSource(this, "avion.png");
			switch (Mesager.Transport)
			{
				case "Avion":
					{
						NumeTitle.Text = "Zboruri";
						break;
					}
				case "Tren":
					{
						NumeTitle.Text = "Trenuri";
						break;
					}
				case "Autocar":
					{
						NumeTitle.Text = "Autocare";
						break;
					}

			}

			foreach (Bilet B in Bilets)  //filtru pentru afisarea numai biletelor care corespund alegerilor facute la pagina Pag_Cautare
			{
				if ((B.Clasa == Mesager.Clasa && B.Transport == Mesager.Transport) && (B.Pasager == Mesager.Pasager && B.TipBilet == Mesager.TipBilet)
					&& Mesager.IsIntoarcere == B.IsIntoarcere) // && (Mesager.Plecare == B.LocPlecare && Mesager.Destinatie == B.Destinatie))
						BiletsT.Add(new Bilet
						{
							Id = B.Id,
							LocPlecare = Mesager.Plecare,
							Destinatie = Mesager.Destinatie,
							DataPlecare = Mesager.DataPlecare,
							DataIntoarcere = Mesager.DataIntoarcere,
							DataPlecareEscala = B.DataPlecareEscala,
							Activ = true,
							TipBilet = B.TipBilet,
							Clasa = B.Clasa,
							Transport = B.Transport,
							Pasager = B.Pasager,
							Pret = B.Pret,
							IsAvion = B.IsAvion,
							IsTren = B.IsTren,
							IsEscala = B.IsEscala,
							Icon = B.Icon,
							Icon2 = B.Icon2,
							IconCompanieZbor = B.IconCompanieZbor,
							IsIntoarcere = B.IsIntoarcere,
							Scaun = B.Scaun,
							Scaun2 = B.Scaun2,
							OraPlecare = B.OraPlecare,
							OraIntoarcere = B.OraIntoarcere,
							Color = B.Color,
							NrTransport = B.NrTransport,
							NrTransport2 = B.NrTransport2,
							Peron = B.Peron,
							Peron2 = B.Peron2,
							Distance = B.Distance,
							DurataDrum = B.DurataDrum,
							DurataDrumEscala = B.DurataDrumEscala,
							DurataDrumPanaLaEscala = B.DurataDrumPanaLaEscala,
							DurataEscala = B.DurataEscala,
							PlecareEscala = B.PlecareEscala,
							DestinatieEscala = B.DestinatieEscala,
							OraPlecareEscala = B.OraPlecareEscala,
							NrTransportEscala = B.NrTransportEscala,
							CompanieZbor = B.CompanieZbor,
							TipTren = B.TipTren,
						});
			}

		}

        private void LocPlecare_TextChanged(object sender, EventArgs e)
        {
			var button = (Button)sender;
			Navigation.PushAsync(new Search_Bar_Locatie());
            foreach(Bilet B in BiletsT)
				B.LocPlecare = Mesager.Plecare;
			button.Text = Mesager.Plecare;
		}

        private void Destinatie_TextChanged(object sender, EventArgs e)
        {
			var button = (Button)sender;
			Navigation.PushAsync(new Search_Bar_Destinatie());
			foreach (Bilet B in BiletsT)
				B.Destinatie = Mesager.Destinatie;
			button.Text = Mesager.Destinatie;
		}

        private void DataPlecare_TextChanged(object sender, TextChangedEventArgs e)
        {
			var entry = (DatePicker)sender;
			foreach (Bilet B in BiletsT)
				B.DataPlecare = entry.Date.ToString("dd/MMMM/yyyy");//B.DataPlecare = entry.Date.ToString("dd/MMMM/yyyy");
			Mesager.DataPlecare = entry.Date.ToString("dd/MMMM/yyyy");
		}

        private async void RedirectEscala(object sender, EventArgs e)
        {
			var button = (Button)sender;
			var bilet = (Bilet)button.BindingContext;

			Mesager.Id = bilet.Id;
			Mesager.Pret = (ushort)bilet.Pret;
			Mesager.oldPret = bilet.Pret;
			Mesager.TipBilet = bilet.TipBilet;
			Mesager.Transport = bilet.Transport;
			Mesager.Pasager = bilet.Pasager;
			Mesager.Activ = bilet.Activ;
			Mesager.DataPlecare = bilet.DataPlecare;
			Mesager.DataIntoarcere = bilet.DataIntoarcere;
			Mesager.DataPlecareEscala = bilet.DataPlecareEscala;
			Mesager.Plecare = bilet.LocPlecare;
			Mesager.Destinatie = bilet.Destinatie;
			Mesager.Clasa = bilet.Clasa;
			Mesager.IsAvion = bilet.IsAvion;
			Mesager.Icon = bilet.Icon;
			Mesager.Icon2 = bilet.Icon2;
			Mesager.Scaun = bilet.Scaun;
			Mesager.Scaun2 = bilet.Scaun2;
			Mesager.IsIntoarcere = bilet.IsIntoarcere;
			Mesager.OraPlecare = bilet.OraPlecare;
			Mesager.OraIntoarcere = bilet.OraIntoarcere;
			Mesager.Color = bilet.Color;
			Mesager.Peron = bilet.Peron;
			Mesager.Peron2 = bilet.Peron2;
			Mesager.NrTransport = bilet.NrTransport;
			Mesager.NrTransport2 = bilet.NrTransport2;
			Mesager.PlecareEscala = bilet.PlecareEscala;
			Mesager.DestinatieEscala = bilet.DestinatieEscala;
			Mesager.DurataDrumEscala = bilet.DurataDrumEscala;
			Mesager.DurataDrumPanaLaEscala = bilet.DurataDrumPanaLaEscala;
			Mesager.DurataDrum = bilet.DurataDrum;
			Mesager.DurataEscala = bilet.DurataEscala;
			Mesager.OraPlecareEscala = bilet.OraPlecareEscala;
			Mesager.NrTransportEscala = bilet.NrTransportEscala;

			await Navigation.PushAsync(new Detalii_Escala());
        }
    }
}