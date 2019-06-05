using System.Collections.Generic;
using System.Windows;
using laba11.Models;
using laba11.ViewModels;

namespace laba11
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static List<Prod> product = new List<Prod>();
        private void OnStartup(object sender, StartupEventArgs e)
        {
            Prod _1 = new Prod { Count = 22, Name = "Стул", Price = 35 };
            Prod _2 = new Prod { Count = 22, Name = "Кресло", Price = 36 };
            Prod _3 = new Prod { Count = 22, Name = "Стол", Price = 37 };

            product.Add(_1);
            product.Add(_2);
            product.Add(_3);

            MainWindow view = new MainWindow(); // создали View
            MainViewModel viewModel = new laba11.ViewModels.MainViewModel(App.product); // Создали ViewModel
            view.DataContext = viewModel; // положили ViewModel во View в качестве DataContext
            view.Show();
        }
    }
}
