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
using System.Text.RegularExpressions;

namespace laba2
{
    public partial class Form4 : Form
    {
        List<Student> list = new List<Student>();
        XmlSerializer xSer = new XmlSerializer(typeof(List<Student>));
        int putbutton = 0;
        bool flag = true;

        public Form4()
        {
            InitializeComponent();  
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if(putbutton == 0)
                FormClosing += new FormClosingEventHandler(Form_FormClosing);
            putbutton++;

            List<Student> students = new List<Student>();
            List<Student> buflist = new List<Student>();
            bool myflag = true;
            using (FileStream topStream = new FileStream("student.xml", FileMode.OpenOrCreate))
            {
                students = (List<Student>)xSer.Deserialize(topStream);
            }
            
            errorProvider1.Clear();
            listBox1.Items.Clear();
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)
            {
                errorProvider1.SetError(label1, "Выберите по какому параметру хотите выполнить поиск");
                myflag = false;
            }
            else
            {
                if (textBox2.Text == "" && checkBox2.Checked)
                {
                    myflag = false;
                    errorProvider1.SetError(textBox2, "Введите тест по которому хотите произвести поиск");
                }
                if (textBox3.Text == "" && checkBox3.Checked)
                {
                    myflag = false;
                    errorProvider1.SetError(textBox3, "Введите цифру по которой хотите произвести поиск");
                }
                if (textBox1.Text == "" && textBox4.Text == "" && textBox5.Text == "" && checkBox1.Checked)
                {
                    myflag = false;
                    errorProvider1.SetError(textBox5, "Введите тест по которому хотите произвести поиск");
                }
            }

            if (myflag)
            {
                if (checkBox2.Checked)
                {
                    Regex regex = new Regex("^" + textBox2.Text, RegexOptions.IgnoreCase);
                    foreach (var item in students)
                    {
                        string s = item.specialization;
                        MatchCollection matches = regex.Matches(s);
                        if (matches.Count > 0)
                            buflist.Add(item);

                    }
                }
                if (checkBox3.Checked)
                {
                    Regex regex = new Regex(textBox3.Text, RegexOptions.IgnoreCase);
                    checkbuf(buflist, students);
                    foreach (var item in students)
                    {
                        string s = item.course;
                        MatchCollection matches = regex.Matches(s);
                        if (matches.Count > 0)
                            buflist.Add(item);
                    }
                }

                if (checkBox1.Checked)
                {
                    if (textBox1.Text != "")
                    {
                        Regex regex = new Regex("^" + textBox1.Text, RegexOptions.IgnoreCase);
                        checkbuf(buflist, students);
                        foreach (var item in students)
                        {
                            string s = item.firstname;
                            MatchCollection matches = regex.Matches(s);
                            if (matches.Count > 0)
                                buflist.Add(item);
                        }
                    }
                    if (textBox4.Text != "")
                    {
                        Regex regex1 = new Regex("^" + textBox4.Text, RegexOptions.IgnoreCase);
                        checkbuf(buflist, students);
                        foreach (var item in students)
                        {
                            string s = item.secondname;
                            MatchCollection matches = regex1.Matches(s);
                            if (matches.Count > 0)
                                buflist.Add(item);
                        }
                    }
                    if (textBox5.Text != "")
                    {
                        Regex regex2 = new Regex("^" + textBox5.Text, RegexOptions.IgnoreCase);
                        checkbuf(buflist, students);
                        foreach (var item in students)
                        {
                            string s = item.thirdname;
                            MatchCollection matches = regex2.Matches(s);
                            if (matches.Count > 0)
                                buflist.Add(item);
                        }
                    }
                }
            }
            if (buflist.Count != 0)
            {
                if (!flag)
                {
                    FormClosing += new FormClosingEventHandler(Form_FormClosing);
                    bool flag = true;
                }
                foreach (var item in buflist)
                {
                    listBox1.Items.Add(item.info());
                    list.Add(item);
                }
            }
            else
            {
                flag = false;
                listBox1.Items.Add("По вашему запросу ничего не найдено");
                FormClosing -= new FormClosingEventHandler(Form_FormClosing);
            }
        }

        private void checkbuf(List<Student> buflist , List<Student> students )
        {
            if (buflist.Count != 0)
            {
                students.Clear();
                foreach (var item in buflist)
                {
                    students.Add(item);
                }
                buflist.Clear();
            }
        }

        private void checkBoxCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            if (checkbox.Checked)
            {
                switch(checkbox.Text)
                {
                    case "ФИО":
                        textBox1.ReadOnly = textBox4.ReadOnly = textBox5.ReadOnly = false;
                        break;
                    case "Специальность":
                        textBox2.ReadOnly = false;
                        break;
                    case "Курс":
                        textBox3.ReadOnly = false;
                        break;
                }
            }
            if (!checkbox.Checked)
            {
                switch (checkbox.Text)
                {
                    case "ФИО":
                        textBox1.ReadOnly = textBox4.ReadOnly = textBox5.ReadOnly = true;
                        textBox1.Text = textBox4.Text = textBox5.Text = "";
                        break;
                    case "Специальность":
                        textBox2.ReadOnly = true;
                        textBox2.Text = "";
                        break;
                    case "Курс":
                        textBox3.ReadOnly = true;
                        textBox3.Text = "";
                        break;
                }
            }
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

        private void Form_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(
              "Хотите ли вы сохранить список найденных элементов?",
              "Сообщение",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Information,
              MessageBoxDefaultButton.Button2,
              MessageBoxOptions.DefaultDesktopOnly
              ) == DialogResult.Yes)
            {
                using (Stream fstream = new FileStream("search.xml", FileMode.Create, FileAccess.Write))
                {
                    xSer.Serialize(fstream, list);
                }
            }
        }
        
        private void Close_Form(Object sender,  FormClosingEventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txtbox = (TextBox)control;
                    txtbox.Text = string.Empty;
                }
                else if (control is CheckBox)
                {
                    CheckBox chkbox = (CheckBox)control;
                    for (int i = 0; i < 3; i++)
                        chkbox.Checked = false;
                }
            }
            listBox1.Items.Clear();
            putbutton = 0;
            FormClosing -= new FormClosingEventHandler(Form_FormClosing);
            errorProvider1.Clear();
        }
    }
}
