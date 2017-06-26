using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SumJest.RCC5lib;

namespace Scrambler
{
    public partial class Decode : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        string path = "";
        byte[] file;

        public Decode(string path)
        {
            this.path = path;
            file = null;
            InitializeComponent();
            try { InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("en-US")); }
            catch (Exception) { }
        }


        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.PasswordChar = default(char);
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            textBox1.PasswordChar = '•';
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label8.ForeColor = (((ushort)GetKeyState(0x90)) & 0xffff) == 0 ? Color.Black : Color.LimeGreen;
            label9.ForeColor = (((ushort)GetKeyState(0x14)) & 0xffff) == 0 ? Color.Black : Color.LimeGreen;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                RCC5 decoder = new RCC5(Encoding.ASCII.GetBytes(textBox1.Text));

                FileStream openfile = File.OpenRead(path);
                byte[] efile = new byte[openfile.Length];
                openfile.Read(efile, 0, efile.Length);
                openfile.Close();
                byte[] dfile = decoder.Decode(efile);
                string temppath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "d" + Path.GetExtension(path);
                File.WriteAllBytes(temppath, dfile);
                string[] lines = File.ReadAllLines(temppath);
                File.Delete(temppath);
                string[] newfile = new string[lines.Length - 1];
                string name = lines[0];
                for (int i = 1; i < newfile.Length; i++)
                {
                    newfile[i - 1] = lines[i];
                }
                File.WriteAllLines(Path.GetDirectoryName(path) + "\\" + name, newfile);
                MessageBox.Show("Файл расшифрован и сохранён по пути: \"" + Path.GetDirectoryName(path) + "\\" + name +  "\"", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Decode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { button1_Click(null, null); }
        }
    }
}
