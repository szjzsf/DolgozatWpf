using System.IO;
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

namespace DolgozatWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class Dolgozat
    {
        public string Nev { get; set; }

        public int Eletkor { get; set; }
        public int Pontszam { get; set; }
        public Dolgozat(string nev, int eletkor, int pontszam)
        {
            Nev = nev;
            Eletkor = eletkor;
            Pontszam = pontszam;
        }

    }

    public partial class MainWindow : Window
    {
        public List<Dolgozat> dolgozatok = new List<Dolgozat>();
        public MainWindow()
        {
            InitializeComponent();
            var path = "C:\\Users\\Szalonna József\\source\\repos\\DolgozatWpf\\DolgozatWpf\\dolgozatok.txt";
            var sorok = File.ReadAllLines(path).Skip(1);
            foreach (string s in sorok)
            {
                string[] darabok = s.Split(';');
                string neve = darabok[0];
                int eletkora = int.Parse(darabok[1]);
                int pontszama = int.Parse(darabok[2]);
                dolgozatok.Add(new Dolgozat(neve, eletkora, pontszama));
            }
            dataGrid.ItemsSource = dolgozatok;
        }

        private void hozzaadas(object sender, RoutedEventArgs e)
        {
            int eletkora;
            int pontszama;
            if(nev.Text.Length>0 && int.TryParse(eletkor.Text,out eletkora) 
                && eletkora > 6
                && int.TryParse(pontszam.Text, out pontszama)
                && pontszama>=0 && pontszama<=100)
            {
                dolgozatok.Add(new Dolgozat(nev.Text, eletkora, pontszama));
                dataGrid.ItemsSource = dolgozatok;
                dataGrid.Items.Refresh();
            }

            else
            {
                MessageBox.Show("Nem megfelelő adatok a beviteli mezőkben!");
            }
        }

        private void mentes(object sender, RoutedEventArgs e)
        {
            if (dolgozatok == null)
            {
                MessageBox.Show("Nincsenek dolgozatok!", "hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string fajlba = "";
            foreach (var d in dolgozatok)
            {
                fajlba += d.Nev + ";" + d.Eletkor + ";" + d.Pontszam + "\n";
            }

            try
            {
                File.WriteAllText("dolgozatok.txt", fajlba);
                MessageBox.Show("Sikeres mentés!", "Infó", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show("Nem megfelelő argumentum", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ioe)
            {
                MessageBox.Show("Hibás fájlnév vagy elérési út", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
