using MAS_2;
using MAS_2.Klasy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            new Database();
          
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0 && textBox1.Text.Length > 0)
            {
                Rezyser r = Rezyser.REZYSEROWIE.Find(r => r.imie.Equals(textBox1.Text) && r.nazwisko.Equals(textBox2.Text));
                Console.WriteLine(r);
                if(r!=null)
                {
                    Form3 f = new Form3(r);
                    this.Hide();
                    f.ShowDialog();
                }
                else
                {
                    Form2 f = new Form2();
                    this.Hide();
                    f.ShowDialog();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
