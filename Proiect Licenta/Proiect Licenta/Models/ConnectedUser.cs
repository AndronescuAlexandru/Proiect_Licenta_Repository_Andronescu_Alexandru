using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect_Licenta.Models
{
    public static class ConnectedUser
    {
        private static uint id;
        private static string nume;
        private static string prenume;
        private static string email;
        private static string password;
        private static string tara;
        private static string oras;
        private static string strada;
        private static ushort nr_strada;
        private static string username;
        private static string cod_postal;
        private static string nr_telefon;
        private static bool isNowConnected;
        private static bool isEmptyUser;
        private static bool isFromLogin;
        private static bool isFromCreareCont;
        private static bool isPassChanged;
        private static bool isBiletListModified;
        private static bool isAccountEdited;
        private static bool accountDataIsCompleted;
        public static List<Views.Bilet> ListaBilete = new List<Views.Bilet>();
        public static List<Models.Factura> ListaFacturi = new List<Models.Factura>();
        public static uint Id { get { return id; } set { id = value; } }
        public static string Nume { get { return nume; } set { nume = value; } }
        public static string Prenume { get { return prenume; } set { prenume = value; } }
        public static string Email { get { return email; } set { email = value; } }
        public static string Password { get { return password; } set { password = value; } }
        public static string Tara { get { return tara; } set { tara = value; } }
        public static string Oras { get { return oras; } set { oras = value; } }
        public static string Strada { get { return strada; } set { strada = value; } }
        public static ushort NrStrada { get { return nr_strada; } set { nr_strada = value; } }
        public static string Username { get { return username; } set { username = value; } }
        public static string CodPostal { get { return cod_postal; } set { cod_postal = value; } }
        public static string NrTelefon { get { return nr_telefon; } set { nr_telefon = value; } }
        public static bool IsNowConnected { get { return isNowConnected; } set { isNowConnected = value; } }
        public static bool IsEmptyUser { get { return isEmptyUser; } set { isEmptyUser = value; } }
        public static bool IsFromLogin { get { return isFromLogin; } set { isFromLogin = value; } }
        public static bool IsFromCreareCont { get { return isFromCreareCont; } set { isFromCreareCont = value; } }
        public static bool IsPassChanged { get { return isPassChanged; } set { isPassChanged = value; } }
        public static bool IsBiletListModified { get { return isBiletListModified; } set { isBiletListModified = value; } }
        public static bool IsAccountEdited { get { return isAccountEdited; } set { isAccountEdited = value; } }
        public static bool AccountDataIsCompleted { get { return accountDataIsCompleted; } set { accountDataIsCompleted = value; } }
        static ConnectedUser()
        {
            id = 0;
            nume = " ";
            prenume = " ";
            password = "";
            email = "default@test.com";
            tara = "Romania";
            oras = " ";
            strada = " ";
            nr_strada = 0;
            nr_telefon = " ";
            username = " ";
            cod_postal = " ";
            isNowConnected = false;
            isEmptyUser = true;
            isFromLogin = false;
            isFromCreareCont = false;
            isPassChanged = false;
            isBiletListModified = false;
            isAccountEdited = false;
            accountDataIsCompleted = false;
        }
    }
}
