using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Lab7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void setColorButton_Click(object sender, RoutedEventArgs e)
        {
            mainWind.Background = new SolidColorBrush(colorPicker.Color);
        }

        private void ColorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            tt.Text += "sender: " + sender.ToString() + "\n";
            tt.Text += "source: " + e.Source.ToString() + "\n\n";
          
        }
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Это команда на основе RoutedUICommand");
        }
    }
}
