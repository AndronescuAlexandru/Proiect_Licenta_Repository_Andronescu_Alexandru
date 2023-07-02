using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proiect_Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Facturile_Mele : ContentPage
    {
        List <Models.Factura> Facturi = new List<Models.Factura> ();
        MemoryStream stream = new MemoryStream();

        void CreatePDFfile()
        {
            System.Threading.Thread.Sleep(3000);
            PdfDocument document = new PdfDocument();

            //Add page to the PDF document
            PdfPage page = document.Pages.Add();
            //Create graphics instance
            PdfGraphics graphics = page.Graphics;
            Stream imageStream = null;

            //Captures the XAML page as image and returns the image in memory stream
            imageStream = new MemoryStream(DependencyService.Get<ISave>().CaptureAsync().Result);
            //Load the image in PdfBitmap
            PdfBitmap image = new PdfBitmap(imageStream);

            //Draw the image to the page
            graphics.DrawImage(image, 0, 0, page.GetClientSize().Width, page.GetClientSize().Height);


            //Save the document into memory stream
            
            document.Save(stream);

            stream.Position = 0;
        }

        public void SendEmail(string EmailAdress)
        {
            try
            {
                MailMessage messageEmail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                messageEmail.From = new MailAddress("proiect_licenta_esky_travel@yahoo.com");
                messageEmail.To.Add(new MailAddress(EmailAdress));
                messageEmail.Subject = "Detaliile de facturare ale biletului dvs. rezervat";
                messageEmail.IsBodyHtml = true;
                messageEmail.Body = "<h1> Biletul dvs. a fost rezervat cu succes.</h1 <h2>Aveti mai jos atasat un document PDF cu detaliile facturii.</h2> ";
                System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                messageEmail.Attachments.Add(new Attachment(stream, "Factura_Bilet.pdf",ct.ToString()));
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.mail.yahoo.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("proiect_licenta_esky_travel@yahoo.com", "tirjvtdltbcswopt");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(messageEmail);
            }
            catch (Exception)
            {

            }
        }

        protected override void OnAppearing()
        {
            if (Models.ConnectedUser.ListaFacturi.Count > 0 && Facturi.Count == 0 && Mesager.ReceivedNewFactura == false)
                Facturi.AddRange (Models.ConnectedUser.ListaFacturi);
            if (Mesager.ReceivedNewFactura == true && Models.ConnectedUser.IsNowConnected == true)
                Facturi.Add(new Models.Factura
                {
                    Nume = Models.ConnectedUser.ListaFacturi.LastOrDefault().Nume,
                    Prenume = Models.ConnectedUser.ListaFacturi.LastOrDefault().Prenume,
                    Email = Models.ConnectedUser.ListaFacturi.LastOrDefault().Email,
                    IdBilet = Models.ConnectedUser.ListaFacturi.LastOrDefault().IdBilet,
                    SumaDePlata = Models.ConnectedUser.ListaFacturi.LastOrDefault().SumaDePlata,
                    Tara = Models.ConnectedUser.ListaFacturi.LastOrDefault().Tara,
                    Oras = Models.ConnectedUser.ListaFacturi.LastOrDefault().Oras,
                    Strada = Models.ConnectedUser.ListaFacturi.LastOrDefault().Strada,
                    NrStrada = Models.ConnectedUser.ListaFacturi.LastOrDefault().NrStrada,
                    NrTelefon = Models.ConnectedUser.ListaFacturi.LastOrDefault().NrTelefon,
                    CodPostal = Models.ConnectedUser.ListaFacturi.LastOrDefault().CodPostal,
                    PlecareDin = Models.ConnectedUser.ListaFacturi.LastOrDefault().PlecareDin,
                    Destinatie = Models.ConnectedUser.ListaFacturi.LastOrDefault().Destinatie,
                    Id = Models.ConnectedUser.ListaFacturi.LastOrDefault().Id,
                    IsMadeWithoutAccount = false,
                });
            else
                if(Mesager.ReceivedNewFactura == true && Models.ConnectedUser.IsNowConnected == false)
            {
                Random random = new Random();
                Facturi.Add(new Models.Factura
                {
                    Nume = Mesager.Nume,
                    Prenume = Mesager.Prenume,
                    Email = Mesager.Email,
                    IdBilet = Mesager.Id,
                    SumaDePlata = Mesager.Pret,
                    Tara = Mesager.Tara,
                    Oras = Mesager.Oras,
                    Strada = Mesager.Strada,
                    NrStrada = (ushort)Mesager.NrStrada,
                    NrTelefon = Mesager.NrTelefon,
                    CodPostal = Mesager.CodPostal,
                    PlecareDin = Mesager.Plecare,
                    Destinatie = Mesager.Destinatie,
                    Id = (ushort)random.Next(1, 1000),
                    IsMadeWithoutAccount = true,
                });
                System.Threading.Thread secondThread = new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.Sleep(2000);
                    CreatePDFfile();
                    SendEmail(Facturi[0].Email);

                });
                secondThread.Start();

            }

            Mesager.ReceivedNewFactura = false;
        }
        public Facturile_Mele()
        { 
            InitializeComponent();
            CollectionViewFacturi.ItemsSource = Facturi;
            OnAppearing();
            //SendEmail(Facturi[0].Email);
        }

        private void SavePDF(object sender, EventArgs e)
        {
            string DetaliiNumePDF = Facturi[0].Id + "_" + Facturi[0].Nume + "_" + Facturi[0].Prenume;
            Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Factura_Bilet_" + DetaliiNumePDF, "application/pdf", stream);
            stream.Flush();
            stream.SetLength(0);
            stream.Close();
            Navigation.PopToRootAsync();
        }
    }
}