namespace styczen2023
{
    public class Wybor
    {
        public string WyborName { get; set; }
    }

    public partial class MainPage : ContentPage
    {
        public List<Wybor> Wybors { get; set; }
        public string selectedWybor = "";
        public string wygenerowanehaslo = "";
        string[] hasla = {
                "abcdefghijklmnopqrstuvwxyz",
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "0123456789",
                "!@#$%^&*()_+-="
            };
        public MainPage()
        {
            InitializeComponent();
            loadWybor();
            pickeritemsadd();
        }
        private void loadWybor()
        {
            Wybors = new List<Wybor>
        {
            new Wybor { WyborName = "Kierownik" },
            new Wybor { WyborName = "Starszy programista" },
            new Wybor { WyborName = "Młodszy programista" },
            new Wybor { WyborName = "Tester" }
        };
        }
        private void pickeritemsadd()
        {
            foreach (var wybor in Wybors)
            {
                picker.Items.Add(wybor.WyborName);
            }
        }
        private void OnEventIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                selectedWybor = Wybors[selectedIndex].WyborName;
            }
        }
        public void generacjahasla()
        {
            wygenerowanehaslo = "";
            int iloscznakowwhasle = Convert.ToInt32(znakientry.Text);
            string[] wygenerowanehaslotablica = new string[iloscznakowwhasle];
            Random r = new Random();
            int wielkaliteraindex = -1;
            int cyfraindex = -1;
            int znakspecjalnyindex = -1;
            if (maleiwielkieliteryCheckbox.IsChecked == true && iloscznakowwhasle > 0)
            {
                wielkaliteraindex = r.Next(0, iloscznakowwhasle);
                while (wielkaliteraindex == cyfraindex || wielkaliteraindex == znakspecjalnyindex)
                {
                    wielkaliteraindex = r.Next(0, iloscznakowwhasle);
                }
                wygenerowanehaslotablica[wielkaliteraindex] = Convert.ToString(hasla[1][r.Next(0, hasla[1].Length - 1)]);
            }
            if (cyfryCheckbox.IsChecked == true && iloscznakowwhasle > 1)
            {
                cyfraindex = r.Next(0, iloscznakowwhasle);
                while (cyfraindex == wielkaliteraindex || cyfraindex == znakspecjalnyindex)
                {
                    cyfraindex = r.Next(0, iloscznakowwhasle);
                }
                wygenerowanehaslotablica[cyfraindex] = Convert.ToString(hasla[2][r.Next(0, hasla[2].Length - 1)]);
            }
            if (znakispecjalneCheckbox.IsChecked == true && iloscznakowwhasle > 2)
            {
                znakspecjalnyindex = r.Next(0, iloscznakowwhasle);
                while (znakspecjalnyindex == cyfraindex || znakspecjalnyindex == wielkaliteraindex)
                {
                    znakspecjalnyindex = r.Next(0, iloscznakowwhasle);
                }
                wygenerowanehaslotablica[znakspecjalnyindex] = Convert.ToString(hasla[3][r.Next(0, hasla[3].Length - 1)]);

            }
            for (int i = 0; i < iloscznakowwhasle; i++)
            {
                if (wygenerowanehaslotablica[i] == null)
                {
                    wygenerowanehaslo += hasla[0][r.Next(0, hasla[0].Length - 1)];
                }
                else
                {
                    wygenerowanehaslo += wygenerowanehaslotablica[i];
                }
            }

        }

        private async void Generujhaslo(object sender, EventArgs e)
        {
            generacjahasla();
            await DisplayAlert("", $"{wygenerowanehaslo}", "OK");
        }

        private async void Zatwierdzprzycisk(object sender, EventArgs e)
        {
            if(wygenerowanehaslo == "")
            {
                wygenerowanehaslo = "Brak hasła";
            }
            await DisplayAlert("", $"Dane pracownika: {imieentry.Text} {nazwiskoentry.Text} {selectedWybor} Hasło: {wygenerowanehaslo}", "OK");
        }
    }

}
