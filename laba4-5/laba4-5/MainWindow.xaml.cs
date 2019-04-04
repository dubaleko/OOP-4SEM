using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.Win32;


namespace laba_4_5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        #region New Save Save_as Open Close
        string mystr = "";

        private void clear()
        {
            Bold.IsChecked = false;
            Italic.IsChecked = false;
            Underline.IsChecked = false;
            FontFamily.SelectedIndex = 0;
            Size.SelectedIndex = 0;
            colorpicker.SelectedColor = Colors.Black;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            TextRange richtxt = new TextRange(RichBox.Document.ContentStart, RichBox.Document.ContentEnd);
            if (richtxt.Text != "" && richtxt.Text != "\r\n")
            {
                MessageBoxResult result = MessageBox.Show(
                  "Хотите ли вы сохранить изменения в файле?",
                  "Сообщение",
                  MessageBoxButton.YesNo,
                  MessageBoxImage.Information
                  );
                if (result == MessageBoxResult.Yes)
                    Save_Click(sender, e);
                clear();
                richtxt.Text = "";
                mystr = "Nameless";
                this.Title = "Nameless";
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(mystr))
            {
                TextRange richtxt = new TextRange(RichBox.Document.ContentStart, RichBox.Document.ContentEnd);
                using (StreamWriter sw = new StreamWriter(mystr, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(richtxt.Text);
                }
            }
            else Save_as_Click(sender, e);
        }
        private void Save_as_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Textfiles|*.txt|Allfiles|*.*";
            Nullable<bool> dialog = fileDialog.ShowDialog();
            TextRange richtxt = new TextRange(RichBox.Document.ContentStart, RichBox.Document.ContentEnd);

            if (dialog == true)
            {
                using (StreamWriter sw = new StreamWriter(fileDialog.FileName, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(richtxt.Text);
                    string str = fileDialog.FileName.Split('\\').Last();
                    mystr = fileDialog.FileName;
                    this.Title = str;
                }
            }
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Textfiles|*.txt|Allfiles|*.*";
            Nullable<bool> dialog = fileDialog.ShowDialog();
            TextRange richtxt = new TextRange(RichBox.Document.ContentStart, RichBox.Document.ContentEnd);
            if (dialog == true)
            {
                using (StreamReader sr = new StreamReader(fileDialog.FileName, System.Text.Encoding.Default))
                {
                    richtxt.Text = "";
                    richtxt.Text += sr.ReadToEnd();
                    mystr = fileDialog.FileName;
                    string str = fileDialog.FileName.Split('\\').Last();
                    this.Title = str;
                }
            }
            clear();
        }
        private void Close_Click(object sender, RoutedEventArgs e) { this.Close(); }
        #endregion

        #region Style
        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Bold.IsChecked)
                RichBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            else
                RichBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
        }
        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Underline.IsChecked)
                RichBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
                RichBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
        }

        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Italic.IsChecked)
                RichBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            else
                RichBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null && RichBox != null)
            {
               string fontFamily = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
               RichBox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, new FontFamily(fontFamily));
            }
        }

        private void Size_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if(comboBox != null && RichBox != null)
            {
                double size = Convert.ToDouble(((ComboBoxItem)comboBox.SelectedItem).Content);
                RichBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, size);
            }
        }

        private void Colorpicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            if (RichBox != null)
            {
                Brush fontColor = new SolidColorBrush(colorpicker.SelectedColor.Value);
                RichBox.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, fontColor);
                
            }
        }

        #endregion

        #region ChangeLanguage

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            if (menuItem.Name == "rus")
            {
                this.Resources = new ResourceDictionary()
                {
                    Source = new Uri(String.Format("Resources/lang.ru-RU.xaml"), UriKind.Relative)
                };
                Edit.Width = 120;
            }
            else
            {
                this.Resources = new ResourceDictionary()
                {
                    Source = new Uri(String.Format("Resources/lang.xaml"), UriKind.Relative)
                };
            }
        }
        #endregion

        #region Drag and Drop
        private void File_Drag(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            e.Handled = true;
        }

        private void File_Drop(object sender, DragEventArgs e)
        {
            try
            {
                string[] filename = (string[])e.Data.GetData(DataFormats.FileDrop, true);
                FileStream fileStream = new FileStream(filename[0], FileMode.Open);
                clear();
                this.Title = Path.GetFileName(filename[0]);
                TextRange range = new TextRange(RichBox.Document.ContentStart, RichBox.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
                e.Handled = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        #endregion

        #region ChangeStyle

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            var uri = new Uri( menuItem.Name+ ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        #endregion
    }
}
    
