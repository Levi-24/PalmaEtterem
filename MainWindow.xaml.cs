using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PalmaEtterem
{
    public partial class MainWindow : Window
    {
        List<Sutemeny> sutemenyek = new List<Sutemeny>();
        public MainWindow()
        {

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

            using StreamWriter streamWriter = new StreamWriter(@"../../../src/lista.txt", false, Encoding.UTF8);
            var write = sutemenyek.GroupBy(s => s.Nev).Select(s => s.First());
            foreach (var item in write) streamWriter.WriteLine(item.Nev + " " + item.Tipus + ";");

            using StreamWriter streamWriter2 = new StreamWriter(@"../../../src/stat.txt", false, Encoding.UTF8);
            var write2 = sutemenyek.GroupBy(s => s.Tipus).Select(s => new { Tipus = s.Key, Db = s.Count() });
            foreach (var item in write2) streamWriter2.WriteLine(item.Tipus + "-" + item.Db);
        }

        private void BtnArajanlat_Click(object sender, RoutedEventArgs e)
        {
            var arajanlat = sutemenyek.Where(s => s.Tipus == TbxTipus.Text);

            if (arajanlat.Any())
            {
                using StreamWriter streamWriter = new StreamWriter(@"../../../src/ajanlat.txt", false, Encoding.UTF8);
                foreach (var item in arajanlat) streamWriter.WriteLine(item.Nev + " " + item.Ar + " " + item.Egyseg);

                MessageBox.Show($"Az ajánlatokat tartalmazó fájl elkészült! \n{arajanlat.Count()}db süteményt találtunk melyek átlagára: {Math.Round(arajanlat.Average(s => s.Ar))}Ft");
            }
            else MessageBox.Show("Nincs ilyen típusú desszertünk. Kérjük, válasszon mást!");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using StreamWriter streamWriter = new StreamWriter(@"../../../src/cuki.txt", true, Encoding.UTF8);
                bool? dij = TbxDij.IsChecked;
                string suti = TbxNev.Text.ToString() + ";" + TbxFelvetelTipus.Text.ToString() + ";" + dij.ToString() + ";" + TbxAr.Text.ToString() + ";" + TbxEgyseg.Text.ToString() + ";";
                sutemenyek.Add(new Sutemeny(suti));
                streamWriter.WriteLine(suti);
                MessageBox.Show("Sikeresen hozzáadva!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}");
            }
        }
    }
}  