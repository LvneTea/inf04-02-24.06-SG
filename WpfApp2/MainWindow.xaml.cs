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
using static WpfApp2.MainWindow;

namespace WpfApp2
{

    public partial class MainWindow : Window
    {
        public class Album
        {
            public string Tytul { get; set; }
            public string Artysta { get; set; }
            public string Utwory { get; set; }
            public int RokWydania { get; set; }
            public int Pobrane { get; set; }
        }
            private List<Album> _albumy = new List<Album>();
            private int _aktualnyAlbumIndex = 0;

        private void OdczytajAlbum(string sciezkaPliku)
        {
            if (!File.Exists(sciezkaPliku))
            {
                MessageBox.Show("Nie znaleziono pliku Data.txt");
                return;
            }

            var lines = File.ReadAllLines(sciezkaPliku);

            for (int i = 0; i + 3 < lines.Length; i += 20)
            {
                if (int.TryParse(lines[i + 3].Trim(), out int rokWydania) &&
                    int.TryParse(lines[i + 4].Trim(), out int pobrane))
                {
                    _albumy.Add(new Album
                    {
                        Tytul = lines[i].Trim(),
                        Artysta = lines[i + 1].Trim(),
                        Utwory = lines[i + 2].Trim(),
                        RokWydania = rokWydania,
                        Pobrane = pobrane
                    });
                }
            }
          }
        private void WyswietlAlbum(int index)
        {
            if (_albumy.Count == 0) return;
            labelTytul.Content = _albumy[index].Tytul;
            labelWykonawca.Content = _albumy[index].Artysta;
            labelUtwory.Content = _albumy[index].Utwory + " utworów";
            labelRok.Content = _albumy[index].RokWydania.ToString();
            labelPobrania.Content = _albumy[index].Pobrane.ToString();

        }

        public MainWindow()
        {
            InitializeComponent();
            OdczytajAlbum("Data.txt");
            if (_albumy.Count > 0)
            {
                WyswietlAlbum(_aktualnyAlbumIndex);
            }
        }

        private void Btn_Pobierz_Click(object sender, RoutedEventArgs e)
        {
            if (_albumy.Count == 0) return;
            _albumy[_aktualnyAlbumIndex].Pobrane++;
            WyswietlAlbum(_aktualnyAlbumIndex);

        }

        private void Btn_Lewy_Click(object sender, RoutedEventArgs e)
        {
            if (_albumy.Count == 0) return;
            _aktualnyAlbumIndex--;
            if (_aktualnyAlbumIndex < 0)
            {
                _aktualnyAlbumIndex = _albumy.Count - 1; 
            }
            WyswietlAlbum(_aktualnyAlbumIndex);
        }

        private void Btn_Prawy_Click(object sender, RoutedEventArgs e)
        {
            if (_albumy.Count == 0) return;
            _aktualnyAlbumIndex++;
            if (_aktualnyAlbumIndex >= _albumy.Count)
            {
                _aktualnyAlbumIndex = 0; 
            }
            WyswietlAlbum(_aktualnyAlbumIndex);

    }
}
}
