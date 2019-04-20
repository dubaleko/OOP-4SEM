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
    /// Логика взаимодействия для Remove.xaml
    /// </summary>
    public partial class Remove : Window
    {
        string connectionString;
        public Remove()
        {
            InitializeComponent();
            string path = @"D:\Labs\2 sem\OOP\laba8\laba8\bin\Debug\App.config";
            using (StreamReader sr = new StreamReader(path))
            {
                connectionString = sr.ReadToEnd();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FIO.Text))
                MessageBox.Show($"Ошибка: Заполните поле", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                command.CommandText="DELETE FROM Adress where Полное_Имя_Жильца = '" + FIO.Text + "'";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM Student where ФИО='" + FIO.Text + "'";
                command.ExecuteNonQuery();

                transaction.Commit();
                MessageBox.Show("Данные удалены", "Удаление данных", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            FIO.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = new StartWindow();
            startWindow.Show();
            this.Close();
        }
    }
}
