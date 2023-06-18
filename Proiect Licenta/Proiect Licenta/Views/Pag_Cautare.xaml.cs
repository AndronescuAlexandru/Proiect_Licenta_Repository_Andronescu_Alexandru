using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading;


namespace Proiect_Licenta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pag_Cautare : ContentPage
    {
        /* Declararea variabilelor folosite, Transport, Pasager, Plecare si Destinatie memoreaza 2 valori, 0 si 1;
         ele isi schimba valoarea atunci cand in pickerul corespunzator variabile este aleasa o valoare
         Variabilelel de tip bool ButtonPlecarePressed si ButtonDestinatiePressed isi schimba valoarea in true atunci
         cand butonul pentru selectarea locatiei de plecare este apasat, respectiv butonul pentru destinatie, ele sunt folosite in functia
         OnAppearing() pentru a schimba textul butonului in locatia aleasa numai atunci cand este apasat
         */
        int Pasager = 0;
        public static IDictionary<string, int> NumOfPasageri = new Dictionary<string, int>()
        {
            {"Adult",0 },
            {"Student",0},
            {"Adolescent",0},
            {"Copil", 0},
            {"Infant", 0},

        };

        int Plecare = 0, Destinatie = 0;
        bool ButtonPlecarePressed = false;
        bool ButtonDestinatiePressed = false;
        void ButtonClickedPlecare(object sender, System.EventArgs e) //Seteaza valoarea true pentru ButtonPlecarePressed si deschide pagina de selectare a locatiei de plecare
        {
            ButtonPlecarePressed = true;
            Navigation.PushAsync(new Search_Bar_Locatie());
        }
        void ButtonClickedDestinatie(object sender, System.EventArgs e) //Seteaza valoarea true pentru ButtonDestinatiePressed si deschide pagina de cautare a destinatiei
        {
            ButtonDestinatiePressed = true;
            Navigation.PushAsync(new Search_Bar_Destinatie());
        }
        protected override void OnAppearing() //Functia actualizeaza textul butoanelor pentru selectarea locului de plecare si al destinatie atunci cand pagina de cautare este redeschisa 
        {
            var InternetConnection = Connectivity.NetworkAccess;

            if (Models.ConnectedUser.IsNowConnected != false && InternetConnection == NetworkAccess.Internet && ToolbarItems.Count > 0)
                ToolbarItems.RemoveAt(0);
            else if (Models.ConnectedUser.IsNowConnected == false && InternetConnection == NetworkAccess.Internet && ToolbarItems.Count == 0)
                ToolbarItems.Add(new ToolbarItem("Sign In", "Sign In", () =>
                {
                    Navigation.PushAsync(new Profil());
                }));

            if (ButtonPlecarePressed == true && Mesager.Plecare != " ")
            {
                buton_locatie_plecare.Text = Mesager.Plecare;
                Plecare = 1;
            }

            if (ButtonDestinatiePressed == true && Mesager.Destinatie != " ")
            {
                buton_locatie_destinatie.Text = Mesager.Destinatie;
                Destinatie = 1;
            }

            ButtonPlecarePressed = false;
            ButtonDestinatiePressed = false;
        }
        void OnDatePlecareChanged(object sender, EventArgs e) //Functia seteaza data minima pentru Date picker-ul de intoarcere in data de plecare selectata
        {
            picker_data_intoarcere.MinimumDate = picker_data_plecare.Date;
        }
        public Pag_Cautare(int choice) //Constructorul paginii de cautare
        {
            var InternetConnection = Connectivity.NetworkAccess;

            OnAppearing();
            InitializeComponent();

            if (InternetConnection != NetworkAccess.Internet)
                ToolbarItems.Add(new ToolbarItem("Sunteti offline!", "", () =>
                {
                }));

            picker_data_plecare.MinimumDate = DateTime.Today;               //Seteaza data minima a Date Picker-ului de plecare in data de azi
            picker_data_intoarcere.MinimumDate = picker_data_plecare.Date;  //Seteaza data minima a Date Picker-ului de intoarcere in data de plecare selectata
            picker_bilet.SelectedIndex = 0;

            switch (choice) //Schimbarea titlului paginii in functie de tipul de transport ales
            {
                case 0:
                    {
                        Title = "Căutare Trenuri";
                        buton_cautare.Text = "Căutare trenuri";

                        if (picker_clasa.IsVisible == false || FrameClasa.IsVisible == false || LabelClasa.IsVisible == false)
                        {
                            picker_clasa.IsVisible = true;
                            FrameClasa.IsVisible = true;
                            LabelClasa.IsVisible = true;
                        }

                        picker_transport.SelectedIndex = choice;
                        picker_clasa.Items.Clear();
                        picker_clasa.Items.Add("Intai");
                        picker_clasa.Items.Add("A doua");
                        picker_clasa.SelectedIndex = 0;

                        Mesager.Transport = picker_transport.SelectedItem.ToString();

                        break;
                    }
                case 1:
                    {
                        Title = "Căutare Zboruri";
                        buton_cautare.Text = "Căutare zboruri";

                        if (picker_clasa.IsVisible == false || FrameClasa.IsVisible == false || LabelClasa.IsVisible == false)
                        {
                            picker_clasa.IsVisible = true;
                            FrameClasa.IsVisible = true;
                            LabelClasa.IsVisible = true;
                        }

                        picker_transport.SelectedIndex = choice;
                        picker_clasa.Items.Clear();
                        picker_clasa.Items.Add("Economica");
                        picker_clasa.Items.Add("Economica Premium");
                        picker_clasa.Items.Add("Business");
                        picker_clasa.Items.Add("Intai");
                        picker_clasa.SelectedIndex = 0;

                        Mesager.Transport = picker_transport.SelectedItem.ToString();

                        break;
                    }
                case 2:
                    {
                        Title = "Căutare Autocare";
                        buton_cautare.Text = "Căutare autocare";

                        picker_transport.SelectedIndex = choice;
                        picker_clasa.Items.Clear();
                        picker_clasa.Items.Add("Standard");
                        picker_clasa.SelectedIndex = 0;
                        picker_clasa.IsVisible = false;
                        FrameClasa.IsVisible = false;
                        LabelClasa.IsVisible = false; 

                        Mesager.Transport = picker_transport.SelectedItem.ToString();

                        break;
                    }

            }

            picker_bilet.SelectedIndexChanged += (sender, args) => //Afiseaza sau sterge Date Picker-ul pentru intoarcere in functie de tipul de bilet ales
            {
                switch (picker_bilet.SelectedIndex)
                {
                    default:
                        {
                            picker_data_intoarcere.IsVisible = false;
                            picker_data_intoarcere.IsEnabled = false;
                            //FramePicker_DataIntoarcere.IsVisible = false;
                            //FramePicker_DataIntoarcere.IsEnabled = false;
                            DataColumnDefinition.Width = 250;
                            picker_data_plecare.FontSize = 18;
                            picker_data_plecare.HorizontalOptions = LayoutOptions.End;
                            TitluDataIntoarcere.IsVisible = false;
                            break;
                        }
                    case 1:
                        {
                            picker_data_intoarcere.IsVisible = true;
                            picker_data_intoarcere.IsEnabled = true;
                            DataColumnDefinition.Width = 180;
                            picker_data_plecare.FontSize = 15;
                            picker_data_plecare.HorizontalOptions = LayoutOptions.Center;
                            //FramePicker_DataIntoarcere.IsVisible = true;
                            //FramePicker_DataIntoarcere.IsEnabled = true;
                            TitluDataIntoarcere.IsVisible = true;
                            break;
                        }
                }
            };
            /*picker_tip_pasager.SelectedIndexChanged += (sender, args) =>
            {
                Pasager = 1;
            };*/
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            popupListView.IsVisible = true;
        }

        private void PasageriAnulareStepper(object sender, EventArgs e)
        {
            popupListView.IsVisible = false;
            LabelElementeIncomplete.IsVisible = false;
            if (!(Stepper_Adult.Value > Stepper_Adult.MinValue || Stepper_Student.Value > Stepper_Student.MinValue ||
            Stepper_Adolescent.Value > Stepper_Adolescent.MinValue || Stepper_Copil.Value > Stepper_Copil.MinValue ||
            Stepper_Infant.Value > Stepper_Infant.MinValue))
                Pasager = 0;
        }

        private void PasageriSelectareStepper(object sender, EventArgs e)
        {
            if (Stepper_Adult.Value > Stepper_Adult.MinValue || Stepper_Student.Value > Stepper_Student.MinValue ||
            Stepper_Adolescent.Value > Stepper_Adolescent.MinValue || Stepper_Copil.Value > Stepper_Copil.MinValue ||
            Stepper_Infant.Value > Stepper_Infant.MinValue)
            {
                Pasager = 1;
                NumOfPasageri["Adult"] = Stepper_Adult.Value;
                NumOfPasageri["Student"] = Stepper_Student.Value;
                NumOfPasageri["Adolescent"] = Stepper_Adolescent.Value;
                NumOfPasageri["Copil"] = Stepper_Copil.Value;
                NumOfPasageri["Infant"] = Stepper_Infant.Value;
                popupListView.IsVisible = false;
            }
            else
                LabelElementeIncomplete.IsVisible = true;
        }

        void Cautare(object sender, EventArgs e) //Functia transmite optiunile alese mesagerului si deschide pagina cu rezultatele cautarii
        {
            if (Pasager == 1 && Plecare == 1 && Destinatie == 1)
            {
                Mesager.Transport = picker_transport.SelectedItem.ToString();
                Mesager.Clasa = picker_clasa.SelectedItem.ToString();
                Mesager.TipBilet = picker_bilet.SelectedItem.ToString();
                Mesager.DataPlecare = picker_data_plecare.Date.ToString("dd/MMMM/yyyy");
                Mesager.DataIntoarcere = picker_data_intoarcere.Date.ToString("dd/MMMM/yyyy");
                Mesager.Pret = 0;
                if(picker_bilet.SelectedIndex == 0)
                    Mesager.IsIntoarcere = false;
                else
                    Mesager.IsIntoarcere = true;
                foreach (KeyValuePair<string, int> P in NumOfPasageri)
                    if (P.Value != 0)
                        Mesager.Pasager = P.Key;
                Navigation.PushAsync(new Rezultate_Cautare());
            }
            else
                DisplayAlert("Date incomplete", "Nu ati completat toate datele necesare pentru cautarea biletelor. Va rugam introduceti datele lipsa si incercati din nou.", "Ok");
        }
    }

}