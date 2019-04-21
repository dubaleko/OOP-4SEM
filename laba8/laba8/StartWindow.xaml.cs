using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace laba8
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Remove remove = new Remove();
            this.Close();
            remove.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Show show = new Show();
            this.Close();
            show.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Request request = new Request();
            this.Close();
            request.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Refresh refresh = new Refresh();
            this.Close();
            refresh.Show();
        }
    }
}
