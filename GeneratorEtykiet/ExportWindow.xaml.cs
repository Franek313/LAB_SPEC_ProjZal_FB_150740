using Microsoft.Win32;
using Newtonsoft.Json;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using ZXing.OneD;

namespace GeneratorEtykiet
{
    /// <summary>
    /// Interaction logic for ExportWindow.xaml
    /// </summary>
    public partial class ExportWindow : Window
    {
        public ObservableCollection<YourDataRow> Rows { get; set; }
        public dynamic data;
        public int currentRowIndex = 0;

        public ExportWindow()
        {
            InitializeComponent();
            Rows = new ObservableCollection<YourDataRow>();
            dataGrid.ItemsSource = Rows;
            ReadJSONDatabase();
        }

        private void ReadJSONDatabase()
        {
            string json = File.ReadAllText("data.json");
            data = JsonConvert.DeserializeObject(json);
        }

        private void DodajWiersz_Click(object sender, RoutedEventArgs e)
        {
            List<string> productNames = new List<string>();
            for(int i = 0; i < data.Count; i++)
            {
                productNames.Add(data[i]["Opis_PL"].ToString());
            }
            Rows.Add(new YourDataRow(productNames, 0));
        }

        // Klasa reprezentująca dane w wierszu
        public class YourDataRow
        {
            public int ID { get; set; }
            public ObservableCollection<string> ListaElementow { get; set; }
            public string WybranyElement { get; set; }
            public ObservableCollection<string> Sloje { get; set; }
            public string WybranySloj { get; set; }
            public string Ilosc { get; set; }

            public YourDataRow(List<string> opcje, int id)
            {
                ListaElementow = new ObservableCollection<string>(opcje);
                Sloje = new ObservableCollection<string>(new List<string> { "PION", "POZ" });
                WybranyElement = ListaElementow[0];
                WybranySloj = Sloje[0];
                ID = id;
                Ilosc = "1";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        int idEtykiety = 0;
        private void GeneratePNG(int id, int sloj)
        {
            LOGS.Text += "PNG GENERATION\n";
            MainWindow mainWindow = new MainWindow(id);
            DateTime now = DateTime.Now;
            string formattedDate = now.ToString("dd.MM.yyyy");
            mainWindow.Kod.Text = $"{Rows[sloj].WybranySloj} {formattedDate} {idEtykiety}";
            idEtykiety++;
            mainWindow.Show(); 
            mainWindow.ExportToPng(mainWindow.imageGrid, "Images/Temp/temp.png");
            mainWindow.Close();
        }

        public void GeneratePDF(List<Tuple<int,int>> elements)
        {
            var document = new PdfDocument();
            
            try
            {
                var page = new PdfPage();

                for (int i = 0; i < elements.Count; i++)
                {
                    if (i != 0)
                    {
                        if (elements[i] != elements[i - 1])
                        {
                            GeneratePNG(elements[i].Item1, elements[i].Item2);
                        }
                    }
                    else
                    {
                        GeneratePNG(elements[i].Item1, elements[i].Item2);
                    }
                    
                    int j = i%4;
                    
                    if (i % 4 == 0)
                    {
                        page = document.AddPage();
                        page.Size = PdfSharpCore.PageSize.A4;
                    }

                    // Utwórz obiekt XGraphics do rysowania na stronie
                    using (var gfx = XGraphics.FromPdfPage(page))
                    {
                        // Wczytaj obraz z pliku
                        string imagePath = "Images/Temp/temp.png"; // Ścieżka do obrazu
                        XImage image = XImage.FromFile(imagePath);

                        double width = page.Width; // Szerokość obrazu
                        double height = image.PixelHeight * page.Width / image.PixelWidth; // Wysokość obrazu

                        // Określ pozycję i rozmiar obrazu na stronie
                        double x = 0; // Pozycja X
                        double y = j * height; // Pozycja Y

                        // Narysuj obraz na stronie PDF
                        gfx.DrawImage(image, x, y, width, height);
                    }
                }
                    
                
                // Inicjalizuj obiekt SaveFileDialog do zapisu pliku
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == true)
                {
                    // Uzyskaj wybraną ścieżkę do zapisu
                    string filePath = saveFileDialog.FileName;

                    // Zapisz dokument PDF do pliku
                    document.Save(filePath);

                    // Otwórz plik PDF w domyślnej aplikacji do przeglądania plików PDF
                    System.Diagnostics.Process.Start(filePath);
                }
            }
            catch (Exception ex)
            {
                // Obsłuż wyjątek związany z generowaniem pliku PDF
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Zamknij dokument PDF
                document.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<Tuple<int, int>> pairedElements = new List<Tuple<int, int>>();
            //List<int> elements = new List<int>();

            LOGS.Text = "";

            string pattern = @"^(?=.*\d)[\d]+$";

            for(int i = 0; i < Rows.Count; i++)
            {
                bool isValid = Regex.IsMatch(Rows[i].Ilosc, pattern);
                if (!isValid)
                {
                    MessageBox.Show("Podano nieprawidłową wartość pola Ilość!", "UWAGA!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (Rows[i].WybranyElement == null)
                {
                    MessageBox.Show("Nie podano nazwy etykiety!", "UWAGA!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                for (int j = 0; j < int.Parse(Rows[i].Ilosc); j++)
                    pairedElements.Add(new Tuple<int, int>(Rows[i].ID, i));
            }

            if(Rows.Count != 0)
                GeneratePDF(pairedElements);

            LOGS.Text += "START PDF GENERATION\n";
            
        }

        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                // Sprawdź, czy zaznaczony jest jakiś wiersz
                if (dataGrid.SelectedItem != null)
                {
                    // Usuń zaznaczony wiersz
                    Rows.Remove(dataGrid.SelectedItem as YourDataRow);
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            YourDataRow selectedRow = comboBox.DataContext as YourDataRow;

            selectedRow.WybranyElement = comboBox.SelectedItem as string;
            selectedRow.ID = comboBox.SelectedIndex;
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            YourDataRow selectedRow = comboBox.DataContext as YourDataRow;

            selectedRow.WybranySloj = comboBox.SelectedItem as string;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Rows.Remove(dataGrid.SelectedItem as YourDataRow);
            }
        }

        
    }
}
