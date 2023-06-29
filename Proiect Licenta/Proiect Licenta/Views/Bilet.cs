using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Proiect_Licenta.Views
{
    public class Bilet : INotifyPropertyChanged
    {
        int pret, pret_servicii_suplimentare;
        bool pjuridica, pfizica,platacard,platatransfer, detaliipret, detalii_servicii_checkin, detalii_servicii_bagaje, detalii_bilet;
        public event PropertyChangedEventHandler PropertyChanged;  //Event care se activeaza atunci cand se schimba o proprietate
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) //Functia seteaza noua proprietate
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string LocPlecare { get; set; }
        public string Destinatie { get; set; }
        public string PlecareEscala { get; set; }
        public string DestinatieEscala { get; set; }
        public uint Id { get; set; }
        public uint Id2 { get; set; }
        public string DataPlecare { get; set; }
        public string DataIntoarcere { get; set; }
        public string DataPlecareEscala { get; set; } 
        public bool Activ { get; set; }
        public string TipBilet { get; set; }
        public string Clasa { get; set; }
        public string Transport { get; set; }
        public string Pasager { get; set; }
        public string[] NumOfPasager { get; set; }
        public int Pret {                   // Proprietatea variabilei Pret este diferita fata de restul, get {} returneaza pretul memorat,
            get { return pret; }            // iar set{} seteaza noul pret atunci cand este modificat pretul si actualizeaza lista
            set
            {
                pret = value;
                if (PropertyChanged != null)
                    OnPropertyChanged();
            }
        }
        public bool IsAvion { get; set; }
        public bool IsTren { get; set; }
        public bool IsIntoarcere { get; set; }
        public bool IsEscala { get; set; }
        public string Scaun { get; set; }
        public string Scaun2 { get; set; }
        public string Icon { get; set; }
        public string Icon2 { get; set; }
        public string IconCompanieZbor { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Titlu { get; set; }
        public string DataNasterii { get; set; }
        public int BagajCala { get; set; }
        public string Tara { get; set; }
        public string CodPostal { get; set; }
        public string Oras { get; set; }
        public string Strada { get; set; }
        public int NrStrada { get; set; }
        public string NrTelefon { get; set; }
        public string Email { get; set; }
        public string OraPlecare { get; set; }
        public string OraIntoarcere { get; set; }
        public string OraPlecareEscala { get; set; }
        public string Color { get; set; }
        public byte Peron { get; set; }
        public byte Peron2 { get; set; }
        public string NrTransport { get; set; }
        public string NrTransportEscala { get; set; }
        public string NrTransport2 { get; set; }
        public string QRCodeTextValue { get; set; }
        public string Distance { get; set; }
        public string DurataDrum { get; set; }
        public string DurataDrumEscala { get; set; }
        public string DurataDrumPanaLaEscala { get; set; }
        public string DurataEscala { get; set; }
        public string CompanieZbor { get; set; }
        public string TipTren { get; set; }
        public bool PJuridica {
            get { return pjuridica; }            // iar set{} seteaza noul pret atunci cand este modificat pretul si actualizeaza lista
            set
            {
                pjuridica = value;
                if (PropertyChanged != null)
                    OnPropertyChanged();
            }
        }
        public bool PFizica
        {
            get { return pfizica; }            // iar set{} seteaza noul pret atunci cand este modificat pretul si actualizeaza lista
            set
            {
                pfizica = value;
                if (PropertyChanged != null)
                    OnPropertyChanged();
            }
        }
        public bool PlataCard
        {
            get { return platacard; }            // iar set{} seteaza noul pret atunci cand este modificat pretul si actualizeaza lista
            set
            {
                platacard = value;
                if (PropertyChanged != null)
                    OnPropertyChanged();
            }
        }
        public bool PlataTransfer
        {
            get { return platatransfer; }            // iar set{} seteaza noul pret atunci cand este modificat pretul si actualizeaza lista
            set
            {
                platatransfer = value;
                if (PropertyChanged != null)
                    OnPropertyChanged();
            }
        }
        public bool DetaliiPret
        {
            get { return detaliipret; }            // iar set{} seteaza noul pret atunci cand este modificat pretul si actualizeaza lista
            set
            {
                detaliipret = value;
                if (PropertyChanged != null)
                    OnPropertyChanged();
            }
        }
        public bool DetaliiBilet
        {
            get { return detalii_bilet; }            // iar set{} seteaza noul pret atunci cand este modificat pretul si actualizeaza lista
            set
            {
                detalii_bilet = value;
                if (PropertyChanged != null)
                    OnPropertyChanged();
            }
        }
        public bool DetaliiServiciiCheckin
        {
            get { return detalii_servicii_checkin; }            // iar set{} seteaza noul pret atunci cand este modificat pretul si actualizeaza lista
            set
            {
                detalii_servicii_checkin = value;
                if (PropertyChanged != null)
                    OnPropertyChanged();
            }
        }
        public bool DetaliiServiciiBagaje
        {
            get { return detalii_servicii_bagaje; }            // iar set{} seteaza noul pret atunci cand este modificat pretul si actualizeaza lista
            set
            {
                detalii_servicii_bagaje = value;
                if (PropertyChanged != null)
                    OnPropertyChanged();
            }
        }
        public int TaxaDeServiciu { get; set; }
        public int TaxaDeAeroport { get; set; }
        public int PretServiciiSuplimentare
        {
            get { return pret_servicii_suplimentare; }            // iar set{} seteaza noul pret atunci cand este modificat pretul si actualizeaza lista
            set
            {
                pret_servicii_suplimentare = value;
                if (PropertyChanged != null)
                    OnPropertyChanged();
            }
        }
    }
}
