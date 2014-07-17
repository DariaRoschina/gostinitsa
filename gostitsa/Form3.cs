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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = splitContainer1.Size.Height;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = splitContainer1.Size.Height - 100;
        }

        public void tabPage1_Enter(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = splitContainer1.Size.Height;
            dataGridView1.DataSource = shared.db.getTableKlient();
            dataGridView2.DataSource = shared.db.getTableArhivK();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(shared.db.getListVid(true).ToArray());
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            tabControl1.TabIndex = 0;
           // label21.Text = comboBox1.Text;
            
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Добавить вид")
            {
                /* 1. Добавить вид в таблицу вид_клиента
                 * 
                 * 2. создать таблицу с таким названием vid_'id_vida'
                 *   2.1  получить от пользователя список полей и их типы
                 *   2.2
                 *   CREATE TABLE  "VID_1" (
                 *   "ID_KLIENT" NUMBER(3,0) NOT NULL ENABLE, 
                 *   ////
                 *   );
                 *   ALTER TABLE  "VID_1" 
                 *   ADD CONSTRAINT "VID_1_FK" FOREIGN KEY ("ID_KLIENT")
                 *   REFERENCES  "KLIENTS" ("ID_KLIENT") ON DELETE CASCADE ENABLE;
                 */
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            splitContainer2.SplitterDistance = splitContainer2.Size.Height - 100;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            splitContainer2.SplitterDistance = splitContainer2.Size.Height;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          ///del  
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            splitContainer2.SplitterDistance = splitContainer2.Size.Height;
            dataGridView3.DataSource = shared.db.getTablePoselen();
            //dataGridView4.DataSource = shared.db.getTableNumb();
            //comboBox1.Items.Clear();
            //comboBox1.Items.AddRange(shared.db.getListVid(true).ToArray());

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            splitContainer3.SplitterDistance = splitContainer3.Size.Height;
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            splitContainer3.SplitterDistance = splitContainer3.Size.Height;
            dataGridView5.DataSource = shared.db.getTableBronir();
            //dataGridView6.DataSource = shared.db......;

        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            splitContainer4.SplitterDistance = splitContainer4.Size.Height;
            dataGridView7.DataSource = shared.db.getTableVidK();
            dataGridView8.DataSource = shared.db.getTableSkidki();
            dataGridView9.DataSource = shared.db.getTableSkV();
                    }

        private void button13_Click_1(object sender, EventArgs e)
        {
            splitContainer3.SplitterDistance = splitContainer3.Size.Height - 100;

        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            splitContainer3.SplitterDistance = splitContainer3.Size.Height;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            splitContainer4.SplitterDistance = splitContainer4.Size.Height - 100;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            splitContainer4.SplitterDistance = splitContainer4.Size.Height;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            splitContainer5.SplitterDistance = splitContainer5.Size.Height - 100;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            splitContainer5.SplitterDistance = splitContainer5.Size.Height;
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            splitContainer2.SplitterDistance = splitContainer2.Size.Height;
            dataGridView4.DataSource = shared.db.getTableNumb();
        }

        private void comboBox11_TextChanged(object sender, EventArgs e)
        {
            if (comboBox11.Text == "Добавить комфортность")
            {
                if (MessageBox.Show("Вы администратор?", "Проверка прав доступа",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Hand,
                     MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    new Form2().Show();
                }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }
        //добавление данных в таблицу
        private void button5_Click(object sender, EventArgs e)
        {

        }
        //удаление строки и сохранение данных в архиве (копирование из одной таблицы в другую)
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(Convert.ToInt32(textBox10.Text) - 1);
                  
        }
        
        }
}
