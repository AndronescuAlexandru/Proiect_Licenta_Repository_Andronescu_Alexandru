using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect_Licenta.Models
{
    public class Factura
    {
        public uint Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public uint IdBilet { get; set; }
        public ushort SumaDePlata { get; set; }
        public string Tara { get; set; }
        public string Oras { get; set; }
        public string Strada { get; set; }
        public ushort NrStrada { get; set; }
        public string CodPostal { get; set; }
        public string NrTelefon { get; set; }
        public string PlecareDin { get; set; }
        public string Destinatie { get; set; }
        public string TipTransport { get; set; }
        public string TipBilet { get; set; }
        public bool IsMadeWithoutAccount { get; set; }
        public long CNP { get; set; }
    }
}
