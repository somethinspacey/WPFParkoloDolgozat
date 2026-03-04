using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFParkoloDolgozat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void CalcButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ErkezesOraBox.Text) || string.IsNullOrWhiteSpace(ErkezesPercBox.Text) || string.IsNullOrWhiteSpace(TavozasOraBox.Text) || string.IsNullOrWhiteSpace(TavozasPercBox.Text))
            {
                MessageBox.Show("Hiba: üres mezők");
                return;
            }
            if (!int.TryParse(ErkezesOraBox.Text, out _) || !int.TryParse(ErkezesPercBox.Text, out _) || !int.TryParse(TavozasOraBox.Text, out _) || !int.TryParse(TavozasPercBox.Text, out _))
            {
                MessageBox.Show("Hiba: csak szám lehhet bemeneti érték");
                return;
            }
            if (int.Parse(ErkezesOraBox.Text) > 23 || int.Parse(ErkezesOraBox.Text) < 0 || int.Parse(TavozasOraBox.Text) > 23 || int.Parse(TavozasOraBox.Text) < 0)
            {
                MessageBox.Show("Hiba: az óra mező(k) értéke 0 és 23 között legyen");
                return;
            }

            if (int.Parse(ErkezesPercBox.Text) > 59 || int.Parse(ErkezesPercBox.Text) < 0 ||
                int.Parse(TavozasPercBox.Text) > 59 || int.Parse(TavozasPercBox.Text) < 0)
            {
                MessageBox.Show("Hiba: a perc mező(k)értéke 0 es 59 között legyen");
                return;
            }

            if (string.IsNullOrWhiteSpace(zonabox.Text))
            {
                MessageBox.Show("Hiba: adjon meg egy zónát");
                return;
            }
            bool roundup = RoundUpCheck.IsChecked.Value;
            bool potdij = PotdijCheck.IsChecked.Value;
            int fizetendo = 0;
            int arrivalminutes = ((int.Parse(ErkezesOraBox.Text)) * 60 + (int.Parse(ErkezesPercBox.Text)));
            int leaveminutes = ((int.Parse(TavozasOraBox.Text)) * 60 + (int.Parse(TavozasPercBox.Text)));
            int minutestopay = leaveminutes - arrivalminutes;
            int roundedtohours = 0;
            string zone = zonabox.Text;
            int zonadij = 0;
            if (minutestopay < 0)
            {
                MessageBox.Show("Hiba: távozási idő nem lehet hamarabb mint az érkezési idő");
                return;
            }
            switch (zone)
            {
                case "A zóna":
                    fizetendo = (minutestopay - 10) * 600 / 60;
                    break;
                case "B zóna":
                    fizetendo = (minutestopay - 10) * 450 / 60;
                    break;
                case "C zóna":
                    fizetendo = (minutestopay - 10) * 300 / 60;
                    break;
            }

            if (roundup)
            {
                roundedtohours = (int)Math.Ceiling((minutestopay - 10) / 60.0);
                switch (zone)
                {
                    case "A zóna":
                        fizetendo = roundedtohours * 600;
                        break;
                    case "B zóna":
                        fizetendo = roundedtohours * 450;
                        break;
                    case "C zóna":
                        fizetendo = roundedtohours * 300;
                        break;
                }

            }
            if (potdij)
            {
                fizetendo += 5000;
            }

            switch (zone)
            {
                case "A zóna":
                    zonadij = 600;
                    break;
                case "B zóna":
                    zonadij = 450;
                    break;
                case "C zóna":
                    zonadij = 300;
                    break;
            }
            if(fizetendo < 0) { fizetendo = 0; }
            FizetendoShort.Text = Convert.ToString(fizetendo);
            if (roundup) { FizetendoDetailed.Text = $"Parkolási idő: {roundedtohours} óra \nZóna: {zone}\nÓradíj: {zonadij}Ft\nPótdíj: {potdij}\nKedvezmény: 10 perc\nVégösszeg: {fizetendo}Ft"; }
            else { FizetendoDetailed.Text = $"Parkolási idő: {minutestopay} perc \nZóna: {zone}\nÓradíj: {zonadij}Ft\nPótdíj: {potdij}\nKedvezmény: 10 perc\nVégösszeg: {fizetendo}Ft"; }
            
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ErkezesOraBox.Text = string.Empty;
            TavozasOraBox.Text = string.Empty;
            ErkezesPercBox.Text = string.Empty;
            TavozasPercBox.Text = string.Empty;
            zonabox.Text = string.Empty;
            PotdijCheck.IsChecked = false;
            RoundUpCheck.IsChecked = false;
        }

        private void Potdij_Checked(object sender, RoutedEventArgs e)
        {
        }//i know this doesnt do shit

        private void RoundUp_Checked(object sender, RoutedEventArgs e)
        {

        }//this too
        //but its needed otherwise the bastard wont build and error out
        //ps: it might have fixed itself but im not removing it
        //its like my personal load bearing coconut png
        //i know the story behing it is prooobably fake but its still funny
    }
}