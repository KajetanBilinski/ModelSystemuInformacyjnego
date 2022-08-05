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
    public partial class Form3 : Form
    {
        private Rezyser rezyser;
        private SerialAnimowany serial= null;
        private Dictionary<int, SerialAnimowany> seriale = new Dictionary<int, SerialAnimowany>();
        public Form3(Rezyser rezyser)
        {
            this.rezyser = rezyser;
            InitializeComponent();
            int fill = 0;
            foreach (RezyserProdukcja rezyserProdukcja in this.rezyser.produkcjeRezysera)
            {
                SerialAnimowany serial = SerialAnimowany.SERIALEANIMOWANE.Find(s => s.idProdukcja == rezyserProdukcja.idProdukcja);
                if (serial!=null)
                {
                    listBox1.Items.Add(serial.tytul);
                    seriale.Add(fill, serial);
                    fill++;
                }
            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex>=0)
                serial = seriale[listBox1.SelectedIndex];
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serial != null)
            {
                this.Hide();
                Form4 f = new Form4(serial);
                f.ShowDialog();
            }
        }
    }
}
