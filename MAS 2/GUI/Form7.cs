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
    public partial class Form7 : Form
    {
        SerialAnimowany serialAnimowany;
        public Form7(SerialAnimowany serial)
        {
            this.serialAnimowany = serial;
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5(serialAnimowany);
            this.Hide();
            f.ShowDialog();
        }
    }
}
