using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Proiect_Licenta.Views
{
    class Mesager
    {
        public static string Plecare { get; set; }
        public static string Destinatie { get; set; }
        public static string PlecareEscala { get; set; }
        public static string DestinatieEscala { get; set; }
        public static uint Id { get; set; }    
        public static string DataPlecare { get; set; }
        public static string DataIntoarcere { get; set; }
        public static string DataPlecareEscala { get; set; }
        public static bool Activ { get; set; }
        public static string TipBilet { get; set; }
        public static string Clasa { get; set; }
        public static string Transport { get; set; }
        public static string Pasager { get; set; }
        public static string[] NumOfPasager = new string[5];
        public static ushort Pret { get; set; }
        public static int oldPret { get; set; }
        public static bool Received { get; set; }
        public static bool IsAvion { get; set; }
        public static bool IsTren { get; set; }
        public static string Icon { get; set; }
        public static string Icon2 { get; set; }
        public static string IconCompanieZbor { get; set; }
        public static string Nume { get; set; }
        public static string Prenume { get; set; }
        public static string Titlu { get; set; }
        public static string DataNasterii { get; set; }
        public static int BagajCala { get; set; }
        public static string Tara { get; set; }
        public static string CodPostal { get; set; }
        public static string Oras { get; set; }
        public static string Strada { get; set; }
        public static int NrStrada { get; set; }
        public static string NrTelefon { get; set; }
        public static string Email { get; set; }
        public static string Scaun { get; set; }
        public static string Scaun2 { get; set; }
        public static bool IsIntoarcere { get; set; }
        public static bool IsNowConnected { get; set; }
        public static bool IsFromLogin { get; set; }
        public static bool IsFromCreareCont { get; set; }
        public static bool IsFromOferta { get; set; }
        public static bool IsBiletListUpdated { get; set; }
        public static bool ReceivedNewFactura { get; set; }
        public static string Password { get; set; }
        public static string[] Names = new string[2];
        public static string OraPlecare { get; set; }
        public static string OraIntoarcere { get; set; }
        public static string OraPlecareEscala { get; set; }
        public static string Color { get; set; }
        public static byte Peron { get; set; }
        public static byte Peron2 { get; set; }
        public static string NrTransport { get; set; }
        public static string NrTransportEscala { get; set; }
        public static string NrTransport2 { get; set; }

        public static string QRCodeTextValue { get; set; }
        public static string DurataDrum { get; set; }
        public static string Distance { get; set; }
        public static string DurataDrumEscala { get; set; }
        public static string DurataDrumPanaLaEscala { get; set; }
        public static string DurataEscala { get; set; }
        public static string CompanieZbor { get; set; }
        public static string TipTren { get; set; }
        public static long CNP { get; set; }
    }
}
