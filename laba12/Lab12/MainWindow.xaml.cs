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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab12
{
    public partial class MainWindow : Window
    {
        Boss boss;
        Boss oldBoss;

        public MainWindow()
        {
            InitializeComponent();
            InitHouse();
        }

        private void InitHouse()
        {
            roofComboBox.SelectedItem = roofComboBox.Items[0];
            wallComboBox.SelectedItem = wallComboBox.Items[0];
            windowComboBox.SelectedItem = windowComboBox.Items[0];
            doorComboBox.SelectedItem = doorComboBox.Items[0];
        }

        private void OnCreateHouse(object sender, RoutedEventArgs e)
        {
            HouseBuilder builder = new HouseBuilder();
            HouseDirector directorBuilder = new HouseDirector(builder);

            ComboBoxItem roof = (ComboBoxItem)roofComboBox.SelectedItem;
            ComboBoxItem wall = (ComboBoxItem)wallComboBox.SelectedItem;
            ComboBoxItem window = (ComboBoxItem)windowComboBox.SelectedItem;
            ComboBoxItem door = (ComboBoxItem)doorComboBox.SelectedItem;

            Monster.Text = directorBuilder.CreateHouse(roof.Content.ToString(), wall.Content.ToString(), window.Content.ToString(), door.Content.ToString());
        }

        private void OnCreateBoss(object sender, RoutedEventArgs e)
        {
            try
            {
                boss = Boss.GetInstance();
                if (ReferenceEquals(boss, oldBoss))
                {
                    MessageBox.Show("Босс уже создан!");
                    return;
                }
                Image image = new Image
                {
                    Height = 50,
                    Width = 50,
                    Source = new BitmapImage(new Uri(@"D:\Labs\2 sem\OOP\laba12\Lab12\Resources\boss.png"))
                };
                oldBoss = boss;
                flat.Children.Add(image);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnCloneBoss(object sender, RoutedEventArgs e)
        {
            try
            {
                var newBoss = boss.DeepClone();
                cloneTextBox.Text += "Босс клонирован\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnFactoryOpen(object sender, RoutedEventArgs e)
        {
            FactoryDialog factoryDialog = new FactoryDialog(this);
            factoryDialog.Show();
        }
    }
}
