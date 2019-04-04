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
using System.Xml.Linq;

namespace laba2
{
    public partial class Form3 : Form
    {
        int putbutton = 0;
        string str;
        bool flag = false;
        XDocument doc = new XDocument();
        XElement array = new XElement("ArrayOfStudents");

        public Form3()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (checkedListBox1.CheckedItems.Count < 1)
            {
                errorProvider1.SetError(checkedListBox1, "Это обязательное поле заполните его");
                goto link;
            }
            str = checkedListBox1.Text;
            errorProvider1.Clear();
            switch (str)
            {
              case "Фамилия":
                    str = "firstname";
                    break;
              case "Возраст":
                    str = "age";
                    break;
              case "Курс":
                    str = "course";
                    break;
              case "Специальность":
                    str = "specialization";
                    break;
            }
            if (putbutton == 0)
                FormClosing += new FormClosingEventHandler(Form3_FormClosing);
            putbutton++;
            XDocument xdoc = XDocument.Load("student.xml");
            var items = from xe in xdoc.Element("ArrayOfStudent").Elements("Student")
                        orderby (string)xe.Element(str)
                        select xe;
           foreach (var item in items)
           {
             array.Add(item);
             listBox1.Items.Add(item.Element("firstname").Value + " " + item.Element("secondname").Value + " " + item.Element("thirdname").Value);
           }
            link: str = " ";
            if (listBox1.Items.Count < 1)
            {
                FormClosing -= new FormClosingEventHandler(Form3_FormClosing);
                flag = true;
            }
           else
            {
                if (flag)
                {
                    FormClosing += new FormClosingEventHandler(Form3_FormClosing);
                    flag = false;
                }
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

        private void Form3_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if ( MessageBox.Show(
              "Хотите ли вы сохранить отсортированный список?",
              "Сообщение",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Information,
              MessageBoxDefaultButton.Button2,
              MessageBoxOptions.DefaultDesktopOnly
              ) == DialogResult.Yes )
            {
                doc.Add(array);
                doc.Save("sortig.xml");
            }

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, false);
            listBox1.Items.Clear();
            putbutton = 0;
            FormClosing -= new FormClosingEventHandler(Form3_FormClosing);
        }

        private void Close_Form(Object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, false);
            listBox1.Items.Clear();
            putbutton = 0;
            FormClosing -= new FormClosingEventHandler(Form3_FormClosing);
            errorProvider1.Clear();
        }
    }
}
