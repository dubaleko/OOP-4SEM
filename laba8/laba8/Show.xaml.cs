using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;

namespace laba8
{
    /// <summary>
    /// Логика взаимодействия для Show.xaml
    /// </summary>
    public partial class Show : Window
    {
        string connectionString;
        public Show()
        {
            InitializeComponent();
            string path = @"D:\Labs\2 sem\OOP\laba8\laba8\bin\Debug\App.config";
            using (StreamReader sr = new StreamReader(path))
            {
                connectionString = sr.ReadToEnd();
            }
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            datagrid.Items.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from Student , Adress where ФИО = Полное_Имя_Жильца";
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string fio = reader.GetString(0);
                        int age = reader.GetInt32(1);
                        DateTime date = reader.GetDateTime(2);
                        string specialization = reader.GetString(3);
                        string gender = reader.GetString(4);
                        int Course = reader.GetInt32(5);
                        int Group = reader.GetInt32(6);
                        string City = reader.GetString(8);
                        string Street = reader.GetString(9);
                        string Index = reader.GetString(10);
                        int Home = reader.GetInt32(11);
                        int Flat = reader.GetInt32(12);
                        Student student = new Student(fio, age, date, specialization, gender, Course, Group, City ,Street , Index ,Home , Flat);
                        datagrid.Items.Add(student);
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
