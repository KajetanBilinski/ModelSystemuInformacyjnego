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
    public partial class Form5 : Form
    {
        SerialAnimowany serial;
        List<Montazysta> montazysci;
        List<Grafik> graficy;
        public Form5(SerialAnimowany serialAnimowany)
        {
            this.serial = serialAnimowany;
            InitializeComponent();
        }
        public Form5(SerialAnimowany serialAnimowany,List<Montazysta> montazysci,List<Grafik> graficy)
        {
            this.serial = serialAnimowany;
            this.montazysci=montazysci;
            this.graficy = graficy;
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(textBox1.Text.Length>0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                if(!SerialAnimowany.Odcinek.ODCINKI.Exists(o=>o.tytul.Equals(textBox1.Text)))
                {
                    int numer = trackBar1.Value;
                    string tytul = textBox1.Text;
                    string opisK = textBox2.Text;
                    string opisD = textBox3.Text;
                    Database.conn.Open();
                    SerialAnimowany.Odcinek odcinek = new SerialAnimowany.Odcinek(numer, tytul, opisK, opisD, serial, false);
                    this.serial.dodajOdcinek(odcinek);
                    if(montazysci!=null && montazysci.Count > 0)
                    {
                        foreach(Montazysta montazysta in montazysci)
                        {
                            montazysta.dodajMontazystaOdcinek(new MontazystaOdcinek(montazysta, odcinek, false));
                        }
                    }
                    if(graficy!=null && graficy.Count>0)
                    {
                        foreach (Grafik grafik in graficy)
                        {
                            grafik.dodajGrafikOdcinek(new GrafikOdcinek(grafik, odcinek, false));
                        }
                    }
                    Database.conn.Close();
                    this.Hide();
                    Form6 f = new Form6();
                    f.ShowDialog();
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //Długi opis
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //Krótki opis
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //tytul
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
