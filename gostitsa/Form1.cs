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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(shared.db.getListGostinits(true).ToArray());
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Добавить гостиницу")
            {
                if (MessageBox.Show("Вы администратор?", "Проверка прав доступа",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Hand,
                     MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    new Form2().Show();
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            shared.form3.Show();
            shared.form3.label21.Text=comboBox1.Text;
            shared.form3.label14.Text = comboBox1.Text;
            shared.form3.label20.Text = comboBox1.Text;
            shared.form3.label29.Text = comboBox1.Text;
            shared.form3.label3.Text = comboBox1.Text;

        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            

        }

    }
}
