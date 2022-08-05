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
    public partial class Form8 : Form
    {
        SerialAnimowany serialAnimowany;
        List<Montazysta> montazysci;
        List<Grafik> graficy;
        public Form8(SerialAnimowany serial)
        {
            this.serialAnimowany = serial;
            this.graficy = new List<Grafik>();
            this.montazysci = new List<Montazysta>();
            InitializeComponent();
            
            foreach(Montazysta montazysta in Montazysta.MONTAZYSCI)
            {
                string[] lista = { montazysta.imie, montazysta.nazwisko, "Montażysta" };
                ListViewItem lvs = new ListViewItem(lista);     
                listView1.Items.Add(lvs);
            }
            foreach (Grafik grafik in Grafik.GRAFICY)
            {
                string[] lista = { grafik.imie, grafik.nazwisko, "Grafik" };
                ListViewItem lvs = new ListViewItem(lista);
                listView1.Items.Add(lvs); 
            }
        }


     

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.CheckedItems.Count; i++)
            {
                var imie = listView1.CheckedItems[i].SubItems[0].Text;
                var nazwisko = listView1.CheckedItems[i].SubItems[1].Text;
                var stanowisko = listView1.CheckedItems[i].SubItems[2].Text;

                if(stanowisko.Equals("Montażysta"))
                {
                    Montazysta m = Montazysta.MONTAZYSCI.Find(m => m.imie.Equals(imie) && m.nazwisko.Equals(nazwisko));
                    montazysci.Add(m);
                }
                else
                {
                    Grafik g = Grafik.GRAFICY.Find(g => g.imie.Equals(imie) && g.nazwisko.Equals(nazwisko));
                    graficy.Add(g);
                }
            }
            this.Hide();
            Form5 f = new Form5(serialAnimowany, montazysci, graficy);
            f.ShowDialog();

            
        }
    }
}
