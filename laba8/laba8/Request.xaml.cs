using System.IO;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System;

namespace laba8
{
    /// <summary>
    /// Логика взаимодействия для Request.xaml
    /// </summary>
    public partial class Request : Window
    {
        string connectionString;
        public Request()
        {
            InitializeComponent();
            string path = @"D:\Labs\2 sem\OOP\laba8\laba8\bin\Debug\App.config";
            using (StreamReader sr = new StreamReader(path))
            {
                connectionString = sr.ReadToEnd();
            }
        }

        private void dorequest_Click(object sender, RoutedEventArgs e)
        {
            datagrid.Items.Clear();
            datagrid.Columns.Clear();
            if (Request1.IsChecked == true)
            {
                datagrid.Columns.Add(new DataGridTextColumn() { Header = "ФИО", Binding = new Binding("FIO") });
                datagrid.Columns.Add(new DataGridTextColumn() { Header = "Возраст", Binding = new Binding("Age") });
                datagrid.Columns.Add(new DataGridTextColumn() { Header = "Дата Рождения", Binding = new Binding("date") });
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "select ФИО , Возраст ,Дата_рождения from Student";
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string FIO = reader.GetString(0);
                            int age = reader.GetInt32(1);
                            DateTime data = reader.GetDateTime(2);
                            Student student = new Student(FIO, age, data);
                            datagrid.Items.Add(student);
                        }
                    }

                }
            }
            if (Request2.IsChecked == true)
            {
                datagrid.Columns.Add(new DataGridTextColumn() { Header = "Полное имя Жильца", Binding = new Binding("FIO") });
                datagrid.Columns.Add(new DataGridTextColumn() { Header = "Город", Binding = new Binding("City") });
                datagrid.Columns.Add(new DataGridTextColumn() { Header = "Улица", Binding = new Binding("Street") });
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "select  Полное_Имя_Жильца, Город, Улица from Adress";
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string FIO = reader.GetString(0);
                            string City = reader.GetString(1);
                            string Street = reader.GetString(2);

                            Student student = new Student(FIO, City, Street);
                            datagrid.Items.Add(student);
                        }
                    }

                }
            }
            if (Request3.IsChecked == true)
            {
                datagrid.Columns.Add(new DataGridTextColumn() { Header = "ФИО", Binding = new Binding("FIO") });
                datagrid.Columns.Add(new DataGridTextColumn() { Header = "Специальность", Binding = new Binding("specialization") });
                datagrid.Columns.Add(new DataGridTextColumn() { Header = "Курс", Binding = new Binding("Course") });
                datagrid.Columns.Add(new DataGridTextColumn() { Header = "Группа", Binding = new Binding("Group") });
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "select  ФИО, Специальность, Курс, Группа from Student";
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string FIO = reader.GetString(0);
                            string specialization = reader.GetString(1);
                            int course = reader.GetInt32(2);
                            int group = reader.GetInt32(3);
                            Student student = new Student(FIO,specialization,course,group);
                            datagrid.Items.Add(student);
                        }
                    }

                }
            }
        }

        private void Comeback_Click(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = new StartWindow();
            startWindow.Show();
            this.Close();
        }
    }
}
