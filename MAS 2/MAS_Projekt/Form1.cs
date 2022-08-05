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

namespace MAS_Projekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            new Database();
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>0 && textBox2.Text.Length > 0)
            {
                Rezyser r = Rezyser.REZYSEROWIE.Find(r=>r.imie.Equals(textBox1.Text) && r.nazwisko.Equals(textBox2.Text));
                if(r==null)
                {
                    this.Hide();
                    new Form3().ShowDialog();
                }
                else
                {
                    this.Hide();
                    new Form2().ShowDialog();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
