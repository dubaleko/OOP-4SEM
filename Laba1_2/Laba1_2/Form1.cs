using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba1_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        class Student
        {
            public string name { get; set;}
            public int age { get; set; }

            public Student(string name,  int age)
            {
                this.age = age;
                this.name = name;
            }
        }
        string[] Names =        {"Никита", "Сергей", "Володя", "Евгений",
                                "Саша", "Илья", "Валентин", "Артем",
                                "Максим", "Миша" , "Игорь" ,"Василий" , "Андрей",
                                "Вадим",  "Леша",   "Полина", "Лиза", "Катя", "Юля",
                                 "Даша", "Таня", "Настя" , "Вика", "Вероника",
                                 "Егор", "Нина", "Адам", "Витя", "Ольга"};
        List<Student> stud = new List<Student>();
        bool proverka;

        private void button1_Click(object sender, EventArgs e)
        {

            stud.Clear();
            try
            {
                int number = Convert.ToInt32(textBox1.Text);
                Random rand = new Random();
                textBox2.Text = "";
                proverka = true;
                for (int i = 0; i < number; i++)
                {
                    int index = rand.Next(Names.Length);
                    Student student = new Student(Names[index], rand.Next(17, 25));
                    stud.Add(student);
                }
                foreach (Student disk in stud)
                {
                    textBox2.Text += " Имя " + disk.name + "\t" + " Возраст " + disk.age + Environment.NewLine;
                }
            }
            catch
            {
                proverka = false;
                textBox2.Text = "Проверьте корректность введенных данных";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            var selectedstud = from t in stud orderby t.age select t;
            foreach (Student s in selectedstud)
            {
                textBox3.Text += " Имя " + s.name + "\t" + "Возраст" + s.age + Environment.NewLine;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            var select = from t in stud orderby t.age descending select t;
            foreach (Student s in select)
            {
                textBox3.Text += " Имя " + s.name + "\t" + "Возраст" + s.age + Environment.NewLine;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (proverka)
            {
                textBox5.Text = "";
                var select = from t in stud orderby t.age descending select t;
                textBox5.Text += "Максимальный возвраст: " + select.First().age.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (proverka)
            {
                textBox4.Text = "";
                var select = from t in stud orderby t.age select t;
                textBox4.Text += "Минимальный возвраст: " + select.First().age.ToString();
            }
        }

    }
}
