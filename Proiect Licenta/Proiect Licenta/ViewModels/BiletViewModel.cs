using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin;

namespace Proiect_Licenta.ViewModels
{
	internal class BiletViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		ObservableCollection<Views.Bilet> _Bilet;
		public ObservableCollection<Views.Bilet> Bilet
		{
			get { return _Bilet; }
			set
			{
				_Bilet = value;
				if (value != null)
					OnPropertyChanged();
			}
		}
		public BiletViewModel()
		{
			Bilet = new ObservableCollection<Views.Bilet>();
			Bilet.Add(new Views.Bilet
			{
				Id = Views.Mesager.Id,
				Id2 = Views.Mesager.Id + 1,
				LocPlecare = Views.Mesager.Plecare,
				Destinatie = Views.Mesager.Destinatie,
				DataPlecare = Views.Mesager.DataPlecare,
				DataIntoarcere = Views.Mesager.DataIntoarcere,
				DataPlecareEscala = Views.Mesager.DataPlecareEscala,
				Activ = true,
				TipBilet = Views.Mesager.TipBilet,
				Clasa = Views.Mesager.Clasa,
				Transport = Views.Mesager.Transport,
				Pasager = Views.Mesager.Pasager,
				Pret = Views.Mesager.Pret,
				IsAvion = Views.Mesager.IsAvion,
				IsTren = Views.Mesager.IsTren,
				Icon = Views.Mesager.Icon,
				Nume = Views.Mesager.Nume,
				Prenume = Views.Mesager.Prenume,
				Tara = Views.Mesager.Tara,
				Titlu = Views.Mesager.Titlu,
				NrStrada = Views.Mesager.NrStrada,
				NrTelefon = Views.Mesager.NrTelefon,
				DataNasterii = Views.Mesager.DataNasterii,
				BagajCala = Views.Mesager.BagajCala,
				Email = Views.Mesager.Email,
				Oras = Views.Mesager.Oras,
				CodPostal = Views.Mesager.CodPostal,
				Strada = Views.Mesager.Strada,
				Icon2 = Views.Mesager.Icon2,
				IsIntoarcere = Views.Mesager.IsIntoarcere,
				IconCompanieZbor = Views.Mesager.IconCompanieZbor,
				OraPlecare = Views.Mesager.OraPlecare,
				OraIntoarcere = Views.Mesager.OraIntoarcere,
				Color = Views.Mesager.Color,
				Peron = Views.Mesager.Peron,
				Peron2 = Views.Mesager.Peron2,
				NrTransport = Views.Mesager.NrTransport,
				QRCodeTextValue = Views.Mesager.QRCodeTextValue,
				NrTransport2 = Views.Mesager.NrTransport2,
				Scaun = Views.Mesager.Scaun,
				Scaun2 = Views.Mesager.Scaun2,
				DurataDrum = Views.Mesager.DurataDrum,
				Distance = Views.Mesager.Distance,
				DurataDrumEscala = Views.Mesager.DurataDrumEscala,
				DurataDrumPanaLaEscala = Views.Mesager.DurataDrumPanaLaEscala,
				DestinatieEscala = Views.Mesager.Destinatie,
				DurataEscala = Views.Mesager.DurataEscala,
				NrTransportEscala = Views.Mesager.NrTransportEscala,
				OraPlecareEscala = Views.Mesager.OraPlecareEscala,
				PlecareEscala = Views.Mesager.PlecareEscala,
				CompanieZbor = Views.Mesager.CompanieZbor,
				TipTren = Views.Mesager.TipTren,
				PJuridica = false,
				PFizica = true,
				PlataCard = true,
				PlataTransfer = false,
				DetaliiPret = false,
				DetaliiBilet = false,
				DetaliiServiciiCheckin = false,
				DetaliiServiciiBagaje = false,
				TaxaDeServiciu = Convert.ToInt32(Views.Mesager.Pret * 0.3),
				TaxaDeAeroport = Convert.ToInt32(Views.Mesager.Pret * 0.2),
				PretServiciiSuplimentare = 0,
			});
		}
	}
}
