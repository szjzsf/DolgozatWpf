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
    }
}
