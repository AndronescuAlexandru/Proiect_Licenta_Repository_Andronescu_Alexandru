using System;
using System.Collections.Generic;
using System.Text;
namespace Proiect_Licenta.Models
{
    public class User
    {
        private uint id;
        private string nume;
        private string prenume;
        private string email;
        private string password;
        private string tara;
        private string oras;
        private string strada;
        private ushort nr_strada;
        private string username;
        private string cod_postal;
        private string nr_telefon;
        private bool isNowConnected;
        private bool isEmptyUser;
        private List<Views.Bilet> listaBilete;
        private List<Models.Factura> listaFacturi;
        
        public uint Id { get { return id; } set { id = value; } }
        public string Nume { get { return nume; } set { nume = value; } }
        public string Prenume { get { return prenume; } set { prenume = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Tara { get { return tara; } set { tara = value; } }
        public string Oras { get { return oras; } set { oras = value; } }
        public string Strada { get { return strada; } set { strada = value; } }
        public ushort NrStrada { get { return nr_strada; } set { nr_strada = value; } }
        public string Username { get { return username; } set { username = value; } }
        public string CodPostal { get { return cod_postal; } set { cod_postal = value; } }
        public string NrTelefon { get { return nr_telefon; } set { nr_telefon = value; } }
        public bool IsNowConnected { get { return isNowConnected; } set { isNowConnected = value; } }
        public bool IsEmptyUser { get { return isEmptyUser; } set { isEmptyUser = value; } }
        public List<Views.Bilet> ListaBilete { get { return listaBilete; } set { listaBilete = value; } }
        public List<Models.Factura> ListaFacturi { get { return listaFacturi; } set { listaFacturi = value; } }
        public User()
        {
            id = 0;
            nume = " ";
            prenume = " ";
            email = "default_email@defaul.com";
            password = " ";
            tara = " ";
            oras = " ";
            strada = " ";
            nr_strada = 0;
            username = " ";
            cod_postal = " ";
            nr_telefon = " ";
            isNowConnected = false;
            isEmptyUser = true;
            ListaBilete = new List<Views.Bilet>();
            ListaFacturi = new List<Models.Factura>();
        }
    }
}
