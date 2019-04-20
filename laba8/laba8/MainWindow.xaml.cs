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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace laba8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString;
        public MainWindow()
        {
            InitializeComponent();
            string path = @"D:\Labs\2 sem\OOP\laba8\laba8\bin\Debug\App.config";
            using (StreamReader sr = new StreamReader(path))
            {
              connectionString = sr.ReadToEnd();
            }
        }

        private void clear()
        {
            DataPicker.Text = null;
            foreach (var item in grid.Children)
            {
                TextBox nolabe = item as TextBox;
                if (nolabe != null)
                {
                    nolabe.Text = null;
                }
                ComboBox combo = item as ComboBox;
                if(combo != null)
                {
                    combo.Text = null;
                }
            }
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    SqlCommand command = connection.CreateCommand();
                    command.Transaction = transaction;

                    command.CommandText = "INSERT INTO Student (ФИО, Возраст, Дата_Рождения,Специальность,Пол,Курс,Группа) VALUES ('" + FIO.Text + "'," + Convert.ToInt32(Age.Text) + ",'" + Convert.ToDateTime(DataPicker.Text) + "', '" + Specialization.Text + "', '" + Gender.Text + "', " + Convert.ToInt32(Course.Text) + "," + Convert.ToInt32(Group.Text) + ")";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO Adress(Полное_Имя_Жильца,Город,Улица,Индекс,Дом ,Квартира) VALUES ('" + FIO.Text + "', '" + City.Text + "', '" + Street.Text + "', '" + Index.Text + "', " + Convert.ToInt32(Home.Text) + ", " + Convert.ToInt32(Flat.Text) + ")";
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    MessageBox.Show("Данные добавлены", "Добавление данных", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                clear();
            }
            catch (Exception x)
            {
                MessageBox.Show($"Ошибка: {x.Message}","Сообщение об ошибке", MessageBoxButton.OK , MessageBoxImage.Error);
            }
        }

        private void UpdateData_Click(object sender , RoutedEventArgs e)
        {
           try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "update Adress set  Город='" + City.Text + "', Улица='" + Street.Text + "' , Индекс='" + Index.Text + "', Дом = " + Convert.ToInt32(Home.Text) + " , Квартира = " + Convert.ToInt32(Flat.Text) + " where Полное_Имя_Жильца = '" + FIO.Text + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "update Student set Возраст = " + Convert.ToInt32(Age.Text) + ", Дата_рождения = '" + Convert.ToDateTime(DataPicker.Text) + "', Специальность = '" + Specialization.Text + "', Пол = '" + Gender.Text + "', Курс = " + Convert.ToInt32(Course.Text) + ", Группа = " + Convert.ToInt32(Group.Text) + " where ФИО = '" + FIO.Text + "'";
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    MessageBox.Show("Данные обновлены", "Обновление данных", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                clear();
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
