using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using ZXing;
using System.Drawing;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls.Primitives;

namespace GeneratorEtykiet
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int ID = -1;
        
        public MainWindow(int id)
        {
            ID = id;
            InitializeComponent();
            LoadFieldsFromJSON(ID);
        }

        public MainWindow()
        {
            LoadFieldsFromJSON(ID);
        }

        public void ExportToPng(FrameworkElement element, string filePath)
        {
            
            // Tworzenie RenderTargetBitmap z określonego elementu
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)element.ActualWidth, (int)element.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            renderBitmap.Render(element);
            CMD.Text += "BP";
            // Konwertowanie do formatu PNG
            PngBitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            // Zapisanie do pliku
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                pngEncoder.Save(fileStream);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Tworzenie okna dialogowego do wyboru lokalizacji i nazwy pliku
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pliki PNG (*.png)|*.png";
            saveFileDialog.Title = "Zapisz obrazek jako...";

            // Wyświetlenie okna dialogowego
            if (saveFileDialog.ShowDialog() == true)
            {
                // Jeśli użytkownik wybrał lokalizację i nazwę pliku, eksportuj obrazek
                ExportToPng(imageGrid, saveFileDialog.FileName);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ExportWindow exportWindow = new ExportWindow();
            exportWindow.Show();
        }

        public void LoadFieldsFromJSON(int productID)
        {
            string json = File.ReadAllText("data.json");

            dynamic dane = JsonConvert.DeserializeObject(json);

            PrzypiszDaneDoTextBox("Nazwa", dane[productID]);
                
            string uwagi = dane[productID]["Uwagi"];
            var sections = uwagi.Split(' ');
            var dimensions = sections[0].Split('x');
            var box_dimensions = sections[1].Split('x');
            var weight = sections[2];

            PrzypiszDaneDoTextBox("Opis_PL", dane[productID]);
            PrzypiszDaneDoTextBox("Dimensions", $"wysokość {dimensions[1]}cm, szerokość {dimensions[0]}cm, głębokość {dimensions[2]}cm");
            PrzypiszDaneDoTextBox("Box_X_Dimension", $"{box_dimensions[0]}cm");
            PrzypiszDaneDoTextBox("Box_Y_Dimension", $"{box_dimensions[1]}cm");
            PrzypiszDaneDoTextBox("Box_Z_Dimension", $"{box_dimensions[2]}cm");
            PrzypiszDaneDoTextBox("Weight", $"{weight}kg");
            PrzypiszDaneDoTextBox("Symbol", dane[productID]);
            AssignBarcodeToImage("Kod_kreskowy", dane[productID]);
        }

        public void LoadFieldsFromJSONAndUpdateTempImage(int productID)
        {
            LoadFieldsFromJSON(productID);
            ExportToPng(imageGrid, "Images/Temp/temp.png");
        }

        private void PrzypiszDaneDoTextBox(string nazwaTextBox, dynamic dane)
        {
            if (dane[nazwaTextBox] != null)
            {
                var textBox = FindName(nazwaTextBox) as TextBox;

                if (textBox != null)
                {
                    textBox.Text = dane[nazwaTextBox].ToString();
                }
                else
                {
                    MessageBox.Show($"Nie znaleziono TextBoxa o nazwie {nazwaTextBox}.");
                }
            }
            else
            {
                MessageBox.Show($"Nie znaleziono danych JSON dla klucza {nazwaTextBox}.");
            }
        }

        private void PrzypiszDaneDoTextBox(string nazwaTextBox, string text)
        {
            var tb = FindName(nazwaTextBox) as TextBox;
            tb.Text = text;
        }

        public Bitmap GenerateBarcode(long number)
        {
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.EAN_13,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 234,
                    Width = 400,
                    PureBarcode = true
                }
            };

            // Konwersja numeru całkowitego na ciąg znaków
            string barcodeText = number.ToString().PadLeft(13, '0'); // Dodanie zer wiodących, aby uzyskać 13 cyfr dla formatu EAN-13

            // Generowanie kodu kreskowego na podstawie ciągu znaków
            Bitmap barcodeBitmap = writer.Write(barcodeText);

            return barcodeBitmap;
        }

        // Funkcja przypisująca wygenerowany kod kreskowy do pola Image w aplikacji WPF
        public void AssignBarcodeToImage(string keyValue, dynamic dane)
        {
            var number = dane[keyValue].ToString();
            // Wygenerowanie kodu kreskowego
            Bitmap barcodeBitmap = GenerateBarcode(long.Parse(number));
            var textBox = FindName("Kod_kreskowy") as TextBox;
            textBox.Text = number;

            // Konwersja kodu kreskowego (Bitmap) na obrazek BitmapImage
            BitmapImage bitmapImage = new BitmapImage();
            using (var memory = new System.IO.MemoryStream())
            {
                barcodeBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memory;
                bitmapImage.EndInit();
            }

            // Przypisanie obrazka do pola Image w aplikacji WPF
            var barcodeImage = (System.Windows.Controls.Image)FindName("BarCode");
            barcodeImage.Source = bitmapImage;
        }

        public void AssignBarcodeToImageFromInputField(long value)
        {
            // Wygenerowanie kodu kreskowego
            Bitmap barcodeBitmap = GenerateBarcode(value);

            // Konwersja kodu kreskowego (Bitmap) na obrazek BitmapImage
            BitmapImage bitmapImage = new BitmapImage();
            using (var memory = new System.IO.MemoryStream())
            {
                barcodeBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memory;
                bitmapImage.EndInit();
            }

            // Przypisanie obrazka do pola Image w aplikacji WPF
            var barcodeImage = (System.Windows.Controls.Image)FindName("BarCode");
            barcodeImage.Source = bitmapImage;
        }

        private void BarCodeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            long value = 0;

            if (!long.TryParse(textBox.Text, out value))
            {
                return;
            }

            AssignBarcodeToImageFromInputField(value);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
