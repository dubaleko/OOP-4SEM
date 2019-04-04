using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;


namespace laba2
{
    public partial class Form1 : Form
    {
        List<Student> students = new List<Student>();
        Form2 f = new Form2();
        Form3 sort = new Form3();
        Form4 search = new Form4();
        public string button;
        XmlSerializer xSer = new XmlSerializer(typeof(List<Student>));

        public Form1()
        {
            InitializeComponent();
            using (FileStream topStream = new FileStream("student.xml", FileMode.OpenOrCreate))
            {
                students = (List<Student>)xSer.Deserialize(topStream);
                foreach (var item in students)
                 {
                    listBox1.Items.Add(item.info());
                 }
            }
        }

        private void save(object sender, EventArgs e)
        {
            Adress adress = new Adress(textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text);
            Student student = new Student(textBox1.Text, textBox2.Text, textBox3.Text, numericUpDown1.Value, dateTimePicker1.Text,
                                          checkedListBox1.Text, checkedListBox2.Text, button, comboBox1.Text, adress);
            if (!Validation(this))
            {
                students.Add(student);
                ClearFormControls(this);
                listBox1.Items.Clear();
                foreach (var item in students)
                {
                    listBox1.Items.Add(item.info());
                }
                using (Stream fstream = new FileStream("student.xml", FileMode.Create, FileAccess.Write))
                {
                    xSer.Serialize(fstream, students);
                }
            }
        }

        private void show(object sender, EventArgs e)
        {
            using (FileStream topStream = new FileStream("student.xml", FileMode.OpenOrCreate))
            {
                students = (List<Student>)xSer.Deserialize(topStream);
            }

            mylink: f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                string str = Student.database;
                if (Student.database == "retry")
                    goto mylink;
                if ( str != " ")
                {
                    string[] array = str.Split(' ');
                    Student.database = " ";
                    foreach (Student item in students)
                    {
                        if (array[1] == item.firstname && array[0] == item.secondname && array[2] == item.thirdname)
                        {
                            MessageBox.Show(item.allinfoaboutstudent() , "Информация о студенте");
                            break;
                        }
                    }
                }
            }
        }

        private void remove(object sender, EventArgs e)
        {
            using (FileStream topStream = new FileStream("student.xml", FileMode.OpenOrCreate))
            {
                students = (List<Student>)xSer.Deserialize(topStream);
            }

           link: f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                string str = Student.database;
                if (Student.database == "retry")
                    goto link;
                if (str != " ")
                {
                    string[] array = str.Split(' ');
                    Student.database = " ";
                    foreach (Student item in students)
                    {
                        if (array[1] == item.firstname && array[0] == item.secondname && array[2] == item.thirdname)
                        {
                            students.Remove(item);
                            break;
                        }
                    }
                }
            }
            using (Stream fstream = new FileStream("student.xml" , FileMode.Create, FileAccess.Write))
            {
                xSer.Serialize(fstream, students);
            }
            listBox1.Items.Clear();
            foreach (var item in students)
            {
                listBox1.Items.Add(item.info());
            }
        }

        private void onecheckedelement(object sender, EventArgs e)
        {
            CheckedListBox checkBox = (CheckedListBox)sender;
            if (checkBox.CheckedItems.Count > 1)
            {
                for (int i = 0; i < checkBox.Items.Count; i++)
                    checkBox.SetItemChecked(i, false);
                checkBox.SetItemChecked(checkBox.SelectedIndex, true);
            }
        }

        private void radiobuttonchecked(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            button = radioButton.Text;
        }

        private void textboxnumbers(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)) return;
            else e.Handled = true;

        }

        private void textboxletters(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') return;
            if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar)) return;
            else e.Handled = true;         
        }

        public static void ClearFormControls(Form form)
        {
            foreach (Control control in form.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txtbox = (TextBox)control;
                    txtbox.Text = string.Empty;
                }
                else if (control is CheckedListBox)
                {
                    CheckedListBox chkbox = (CheckedListBox)control;
                    for (int i = 0; i < chkbox.Items.Count; i++)
                        chkbox.SetItemChecked(i, false);
                }
                else if (control is RadioButton)
                {
                    RadioButton rdbtn = (RadioButton)control;
                    rdbtn.Checked = false;
                }
                else if (control is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)control;
                    dtp.Value = DateTime.Now;
                }
                else if (control is NumericUpDown)
                {
                    NumericUpDown nud = (NumericUpDown)control;
                    nud.Value = 0;

                }
                else if (control is ComboBox)
                {
                    ComboBox cmb = (ComboBox)control;
                    cmb.Text = "1";
                }
            }
        }

        public bool Validation(Form form)
        {
            int value = 0;
            bool statusvalidation = false;
            foreach (Control control in form.Controls)
            {
                errorProvider1.SetError(control, string.Empty);
                if (control is TextBox)
                {
                    TextBox txtbox = (TextBox)control;
                    if (String.IsNullOrEmpty(txtbox.Text))
                    {
                        errorProvider1.SetError(txtbox, "Это обязательное поле заполните его");
                        statusvalidation = true;
                    }
                }
                else if (control is CheckedListBox)
                {
                    CheckedListBox chkbox = (CheckedListBox)control;
                    if (chkbox.CheckedItems.Count < 1)
                    {
                        statusvalidation = true;
                        errorProvider1.SetError(chkbox, "Выберите одно из подходящих полей");
                    }

                }
                else if (control is NumericUpDown)
                {
                    NumericUpDown nud = (NumericUpDown)control;
                    if (nud.Value < 18)
                    {
                        statusvalidation = true;
                        errorProvider1.SetError(nud, "Мелковат ты для студента");
                    }
                    if (nud.Value > 55)
                    {
                        errorProvider1.SetError(nud, "Староваты вы для студента");
                        statusvalidation = true;
                    }
                        
                }
                else if (control is ComboBox)
                {
                    ComboBox cmb = (ComboBox)control;
                    if (cmb.Text == "")
                    {
                        statusvalidation = true;
                        errorProvider1.SetError(cmb, "Это обязательное поле заполните его");
                    }
                }
                else if (control is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)control;
                    int yearofbirth = dtp.Value.Year + Convert.ToInt32(numericUpDown1.Value);
                    if (yearofbirth != DateTime.Now.Year)
                        if(yearofbirth == DateTime.Now.Year && dtp.Value.Month > DateTime.Now.Month
                        || yearofbirth == DateTime.Now.Year && dtp.Value.Month == DateTime.Now.Month && dtp.Value.Day > DateTime.Now.Day)
                    {
                        statusvalidation = true;
                        errorProvider1.SetError(dtp, "Несовпадение возраста и года рождения");
                    }
                }
                else if (control is RadioButton)
                {
                    RadioButton rbt = (RadioButton)control;
                    if (rbt.Checked == false) value++;
                    if (value == 4)
                    {
                        statusvalidation = true;
                        errorProvider1.SetError(rbt, "Выберите одно из подходящих полей");
                    }
                }
            }
         return statusvalidation;
        }

        private void оПриложенииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ФИО разработчика : Дубалеко В.В"+'\n'+ "Версия программы: 1.1", "Информация о программе");
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            search.ShowDialog();
        }

        private void сортировкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sort.ShowDialog();
        }
    }
}
