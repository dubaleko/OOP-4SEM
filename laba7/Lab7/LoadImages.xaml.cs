using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Lab7
{
    /// <summary>
    /// Логика взаимодействия для LoadImages.xaml
    /// </summary>
    public partial class LoadImages : UserControl
    {
        public LoadImages()
        {
            InitializeComponent();
        }
        public static DependencyProperty FileNameProperty;
        static LoadImages()
        {
            FileNameProperty = DependencyProperty.Register("FileName", typeof(string), typeof(LoadImages),
                new FrameworkPropertyMetadata("Sourse", new PropertyChangedCallback(OnFileNameChanged)));
        }
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        private static void OnFileNameChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LoadImages ldi = (LoadImages)sender;
            string str = (string)e.NewValue;
            ldi.IBCImage.Source = new BitmapImage(new Uri(str));
            ldi.IBCTextBox.Text = str;
            
        }

        private void FBCButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            if (openFileDlg.ShowDialog() == true)
            this.FileName = openFileDlg.FileName;
        }

      
    }
}
