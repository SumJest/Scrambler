using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrambler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RCC5 encoder = new RCC5(Encoding.ASCII.GetBytes(textBox1.Text));
            RCC5 decoder = new RCC5(Encoding.ASCII.GetBytes(textBox1.Text));

            label1.Text = Encoding.ASCII.GetString(decoder.Decode(encoder.Encode(Encoding.ASCII.GetBytes(textBox2.Text))));
        }
    }
}
