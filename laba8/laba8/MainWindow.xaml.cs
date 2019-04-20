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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace laba8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=OOP;Integrated Security=True";

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            string sqlExpression = "INSERT INTO Student (ФИО, Возраст, Дата_Рождения,Специальность,Пол,Курс,Группа) VALUES ('" + FIO.Text + "'," + Convert.ToInt32(Age.Text) + ",'" + Convert.ToDateTime(DataPicker.Text) + "', '" + Specialization.Text + "', '" + Gender.Text + "', " + Convert.ToInt32(Course.Text) + "," + Convert.ToInt32(Group.Text) + ")" +
                "INSERT INTO Adress(Полное_Имя_Жильца,Город,Улица,Индекс,Дом ,Квартира) VALUES ('"+FIO.Text+"', '"+City.Text+"', '"+Street.Text+"', '"+Index.Text+"', "+Convert.ToInt32(Home.Text)+", "+Convert.ToInt32(Flat.Text)+")";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
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
                string sqlExpression = "update Adress set  Город='"+City.Text+"', Улица='"+Street.Text+"' , Индекс='"+Index.Text+"', Дом = "+ Convert.ToInt32(Home.Text) + " , Квартира = "+ Convert.ToInt32(Flat.Text) + " where Полное_Имя_Жильца = '"+FIO.Text+"'" +
                    "update Student set Возраст = "+ Convert.ToInt32(Age.Text) + ", Дата_рождения = '" + Convert.ToDateTime(DataPicker.Text) + "', Специальность = '" + Specialization.Text + "', Пол = '"+ Gender.Text+ "', Курс = " + Convert.ToInt32(Course.Text) + ", Группа = " + Convert.ToInt32(Group.Text) + " where ФИО = '" + FIO.Text+"'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
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
