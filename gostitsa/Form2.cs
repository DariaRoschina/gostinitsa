using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gostitsa
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "" 
                && textBox3.Text == "")
            {
                MessageBox.Show("Введите данные полностью", "Предупреждение");
            }
            else 
            {
                //добавить гостиницу
                shared.db.insGost(textBox1.Text, long.Parse(textBox2.Text),
                    textBox3.Text);
                ActiveForm.Close();
                shared.form1.Form1_Load(null, null);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
