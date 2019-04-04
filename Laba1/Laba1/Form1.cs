using System;
using System.Windows.Forms;

namespace Laba1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double Value = 0, buf = 0;
        string sign = "", equal = "";

        public void signer(string str)
        {
            if (str == "+") Value = Value + buf;
            if (str == "-") Value = Value - buf;
            if (str == "*") Value = Value * buf;
            if (str == "/") Value = Value / buf;
        }

        private void buttonnumber_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || equal == "=") textBox1.Clear();
            equal = "";
            Button button = (Button)sender;
            if (button.Text != ",") textBox1.Text = textBox1.Text + button.Text;
            else  if (!textBox1.Text.Contains(",")) textBox1.Text = textBox1.Text + button.Text;
        }

        private void operator_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if(textBox1.Text != "") buf = Convert.ToDouble(textBox1.Text);
            if (button.Text == "=")
            {
                signer(sign);
                equal = "=";
                textBox1.Text = Value.ToString();
            }
            else if (button.Text == "+" || button.Text == "-" || button.Text == "*" || button.Text == "/")
            {
                if (equal == "=") buf = 0;
                if (Value != 0) signer(button.Text);
                else Value = buf;
                sign = button.Text;
                textBox1.Text = "";    
            }
            if (button.Text == "C")
            {
                textBox1.Text = sign = "";
                Value = buf = 0;
            }
            if (button.Text == "<-")
                if (textBox1.Text.Length > 0) textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            if (button.Text == "+/-")
                if (textBox1.Text != "") textBox1.Text = (-Convert.ToDouble(textBox1.Text)).ToString();
        }
    }
}