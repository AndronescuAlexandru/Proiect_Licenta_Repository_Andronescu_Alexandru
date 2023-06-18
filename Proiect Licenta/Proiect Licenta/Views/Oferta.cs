using System;
using System.Collections.Generic;
using System.Text;

namespace Proiect_Licenta.Views
{
    internal class Oferta
    {
        public string LocPlecare { get; set; }
        public string Destinatie { get; set; }
        public string PlecareEscala { get; set; }
        public string DestinatieEscala { get; set; }
        public uint Id { get; set; }
        public uint Id2 { get; set; }
        public string DataPlecare { get; set; }
        public string DataIntoarcere { get; set; }
        public DateTime DataPlecareEscala { get; set; }
        public string TipBilet { get; set; }
        public string Clasa { get; set; }
        public string Transport { get; set; }
        public string Pasager { get; set; }
        public ushort Pret { get; set; }
        public bool IsAvion { get; set; }
        public bool IsIntoarcere { get; set; }
        public bool IsEscala { get; set; }
        public string Scaun { get; set; }
        public string Scaun2 { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public string Icon2 { get; set; }
        public string OraPlecare { get; set; }
        public string OraIntoarcere { get; set; }
        public string OraPlecareEscala { get; set; }
        public byte Peron { get; set; }
        public byte Peron2 { get; set; }
        public string NrTransport { get; set; }
        public string NrTransportEscala { get; set; }
        public string NrTransport2 { get; set; }
        public string Distance { get; set; }
        public string DurataDrum { get; set; }
        public string Color { get; set; }
    }
}
