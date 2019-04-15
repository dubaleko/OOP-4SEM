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

        private void clear()
        {
            Bold.IsChecked = false;
            Italic.IsChecked = false;
            Underline.IsChecked = false;
            FontFamily.SelectedIndex = 0;
            Size.SelectedIndex = 0;
            colorpicker.SelectedColor = Colors.Black;

            RichBox.Selection.ApplyPropertyValue(TextBlock.FontFamilyProperty, new FontFamily("Times New Roman"));
            Bold_Click(null, null);
            Underline_Click(null, null);
            Italic_Click(null, null);
            Size_SelectionChanged(null, null);
            Colorpicker_SelectedColorChanged(null, null);
        }

        private static Dictionary<string, string> FilesText { get; set; } = new Dictionary<string, string>() { ["nameless"] = string.Empty };
        private static string CurrentTabName { get; set; } = "nameless";
        private static string bufFileName = string.Empty;

        private string GetText()
        {
            string content = new TextRange(RichBox.Document.ContentStart, RichBox.Document.ContentEnd).Text;
            return content == string.Empty ? string.Empty : content.Substring(0, content.Length - 2);
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            clear();
            bufFileName = string.Empty;
            CreateNewTab("nameless", string.Empty, false);
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TabItem tab = Tabs.SelectedItem as TabItem;

                if (tab.Tag.ToString() == true.ToString())
                {
                    File.Delete(bufFileName);
                    string content = GetText();
                    FilesText[bufFileName] = content;
                    File.WriteAllBytes(bufFileName, Encoding.UTF8.GetBytes(content));
                }
                else
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text file (*.txt)|*.txt|HTML file (*.html)|*.html";
                    string content = GetText();
                    RichBox.Background = Brushes.White;
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        File.WriteAllBytes(saveFileDialog.FileName, Encoding.UTF8.GetBytes(content));
                        tab.Tag = true.ToString();
                        bufFileName = saveFileDialog.FileName;

                        Label header = new Label() { Content = saveFileDialog.SafeFileName, Margin = new Thickness(0, -5, 0, 0) };
                        header.MouseLeftButtonDown += Tab_Click;
                        header.MouseRightButtonDown += CloseTab_Click;

                        tab.Header = header;
                        CurrentTabName = saveFileDialog.SafeFileName;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text file (*.txt)|*.txt|HTML file (*.html)|*.html";
                if (openFileDialog.ShowDialog() == true)
                {
                    byte[] content = File.ReadAllBytes(openFileDialog.FileName);
                    string strContent = Encoding.UTF8.GetString(content);
                    bufFileName = openFileDialog.FileName;
                    clear();
                    CreateNewTab(openFileDialog.SafeFileName, strContent, true);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private Visibility CheckTabCount()
        {
            if (Tabs.Items.Count == 0)
            {
                RichBox.IsEnabled = false;
                return Visibility.Hidden;
            }
            else
            {
                RichBox.IsEnabled = true;
                return Visibility.Visible;
            }
        }

        private void CreateNewTab(string tabName, string content, bool isOpened)
        {
            ToNewTab(content);
            if (tabName == "nameless")
                tabName += Tabs.Items.Count.ToString();
            Label header = new Label()
            {
                Content = tabName,
                Margin = new Thickness(0, -3, 0, 0),
                Style = (Style)FindResource("Label")
            };
            header.MouseLeftButtonDown += Tab_Click;
            header.MouseRightButtonDown += CloseTab_Click;
            TabItem newTab = new TabItem()
            {
                Tag = isOpened.ToString(),
                Header = header,
                LayoutTransform = new RotateTransform(-90),
                IsSelected = true,
                Style = (Style)FindResource("Tabs")
            };
            CurrentTabName = tabName;
            FilesText.Add(tabName, content);
            Tabs.Items.Add(newTab);
            Tabs.Visibility = CheckTabCount();
        }

        private void ToNewTab(string content)
        {
            if (CurrentTabName != string.Empty)
                FilesText[CurrentTabName] = GetText();
            RichBox.Document.Blocks.Clear();
            RichBox.Document.Blocks.Add(new Paragraph(new Run(content)));
        }

        private void Tab_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string newTabName = ((Label)sender).Content.ToString();
            if (CurrentTabName == newTabName)
                return;
            ToNewTab(FilesText[newTabName]);
            CurrentTabName = newTabName;

        }

        private void CloseTab_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FilesText.Remove(((Label)sender).Content.ToString());
            Tabs.Items.Remove(((Label)sender).Parent);
            Tabs.Visibility = CheckTabCount();
            RichBox.Document.Blocks.Clear();
            CurrentTabName = string.Empty;
            if (Tabs.Items.Count != 0)
            {
                TabItem currentTab = Tabs.SelectedItem as TabItem;
                CurrentTabName = ((Label)currentTab.Header).Content.ToString();
                RichBox.Document.Blocks.Add(new Paragraph(new Run(FilesText[CurrentTabName])));
            }
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
                Edit.Width = 60;
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
    
