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
    public partial class Form4 : Form
    {
        SerialAnimowany serialAnimowany;
        public Form4(SerialAnimowany serial)
        {
            this.serialAnimowany = serial;
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //YES
            if(MontazystaOdcinek.MONTAZYSTAODCINEK.Count>0 && GrafikOdcinek.GRAFIKODCINEK.Count>0)
            {
                Form8 f = new Form8(serialAnimowany);
                this.Hide();
                f.ShowDialog();

            }
            else
            {
                Form7 f = new Form7(serialAnimowany);
                this.Hide();
                f.ShowDialog();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //NO
            this.Hide();
            Form5 f = new Form5(serialAnimowany);
            f.ShowDialog();
        }
    }
}
