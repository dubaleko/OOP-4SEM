using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void textboxletters(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') return;
            if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar)) return;
            else e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                Student.database = "retry";
            }
            if (!Validation(this))
            {
                Student.database = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text;
                    this.Close();
                    ClearFormControls(this);
            }
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
            }
        }
        public bool Validation(Form form)
        {
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
            }
            return statusvalidation;
        }
    }
}
