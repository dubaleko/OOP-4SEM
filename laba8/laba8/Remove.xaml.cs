using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public Remove()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FIO.Text))
                MessageBox.Show($"Ошибка: Заполните поле", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=OOP;Integrated Security=True";
            string sqlExpression = "DELETE FROM Adress where Полное_Имя_Жильца = '" + FIO.Text + "' DELETE FROM Student where ФИО='" + FIO.Text+"'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int ss = command.ExecuteNonQuery();
                MessageBox.Show(ss.ToString());
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
