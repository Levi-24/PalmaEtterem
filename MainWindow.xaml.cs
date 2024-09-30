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

namespace PalmaEtterem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            List<Sutemeny> sutemenyek = new List<Sutemeny>();

            InitializeComponent();
            using StreamReader streamReader = new StreamReader(@"../../../src/palma.txt", Encoding.UTF8);
            while (!streamReader.EndOfStream) sutemenyek.Add(new Sutemeny(streamReader.ReadLine()));

            Random rnd = new Random();
            LblAjanlat.Content = sutemenyek[rnd.Next(0, sutemenyek.Count)].Nev;

            LblBestCake.Content = sutemenyek.Count(x => x.Dijazott) + " féle díjnyertes édességből választhat!";

            Sutemeny Legdragabb = sutemenyek.Select(s => s).OrderByDescending(s => s.Ar).First();
            LblDragaNev.Content = Legdragabb.Nev;
            LblDragaAr.Content = Legdragabb.Ar + "/" + Legdragabb.Egyseg;

            Sutemeny Legolcsobb = sutemenyek.Select(s => s).OrderByDescending(s => s.Ar).Last();
            LblOlcsoNev.Content = Legolcsobb.Nev;
            LblOlcsoAr.Content = Legolcsobb.Ar + "/" + Legolcsobb.Egyseg;
        }
    }
}