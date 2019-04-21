using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace laba8
{
    /// <summary>
    /// Логика взаимодействия для Refresh.xaml
    /// </summary>
    public partial class Refresh : Window
    {
        string connectionString;
        public Refresh()
        {
            InitializeComponent();
            string path = @"D:\Labs\2 sem\OOP\laba8\laba8\bin\Debug\App.config";
            using (StreamReader sr = new StreamReader(path))
            {
                connectionString = sr.ReadToEnd();
            }
        }
        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    SqlCommand command = connection.CreateCommand();
                    command.Transaction = transaction;

                    command.CommandText = Text.Text;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    MessageBox.Show("Данные обновлены", "Обновление данных", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Text.Clear();
            }
            catch (Exception x)
            {
                MessageBox.Show($"Ошибка: {x.Message}", "Сообщение об ошибке", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = new StartWindow();
            startWindow.Show();
            this.Close();
        }
    }
}
