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
using SumJest.RCC5lib;
using System.IO;

namespace Scrambler
{


    public partial class Encode : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        string path = "";
        bool isMatch = false;

        public Encode(string path)
        {   
            InitializeComponent();
            this.path = path;
            try { InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("en-US")); }
            catch (Exception) { }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
                if (textBox2.Text != "" && textBox1.Text != "")
                {
                    if (textBox1.Text.Equals(textBox2.Text))
                    {
                        label3.ForeColor = Color.Green;
                        label3.Text = "Passwords match";
                        isMatch = true;
                    }
                    else
                    {
                        label3.ForeColor = Color.Red;
                        label3.Text = "Passwords do not match";
                        isMatch = false;
                    }
                }
                else if (textBox2.Text == "" || textBox1.Text == "")
                {
                    label3.Text = "";
                    label3.ForeColor = Color.Black;
                    isMatch = false;
                }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                if (textBox2.Text != "" && textBox1.Text != "")
                {
                    if (textBox1.Text.Equals(textBox2.Text))
                    {
                        label3.ForeColor = Color.Green;
                        label3.Text = "Passwords match";
                        isMatch = true;
                    }
                    else
                    {
                        label3.ForeColor = Color.Red;
                        label3.Text = "Passwords do not match";
                        isMatch = false;
                    }
                }
                else if (textBox2.Text == "" || textBox1.Text == "")
                {
                    label3.Text = "";
                    label3.ForeColor = Color.Black;
                    isMatch = false;
                }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (isMatch)
            {
                try
                {
                    RCC5 encoder = new RCC5(Encoding.ASCII.GetBytes(textBox1.Text));
                    string[] file = File.ReadAllLines(path);
                    string[] newfile = new string[file.Length + 1];
                    newfile[0] = Path.GetFileName(path);
                    for (int i = 1; i < newfile.Length; i++)
                    {
                        newfile[i] = file[i - 1];
                    }
                    string temppath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".scram";
                    File.AppendAllLines(temppath, newfile);
                    FileStream read = File.OpenRead(temppath);
                    byte[] dfile = new byte[read.Length];
                    read.Read(dfile, 0, dfile.Length);
                    read.Close();
                    File.Delete(temppath);
                    byte[] efile = encoder.Encode(dfile);
                    FileStream save = File.Create(temppath);
                    save.Write(efile, 0, efile.Length);
                    save.Close();
                    MessageBox.Show("Файл зашифрован и сохранён по пути: \"" + temppath + "\"");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
                
        }

        private void Encode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { button1_Click(null, null); }
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.PasswordChar = default(char);
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            textBox1.PasswordChar = '•';
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = default(char);
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '•';
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label8.ForeColor = (((ushort)GetKeyState(0x90)) & 0xffff) == 0 ? Color.Black : Color.LimeGreen;
            label9.ForeColor = (((ushort)GetKeyState(0x14)) & 0xffff) == 0 ? Color.Black : Color.LimeGreen;
        }
    }
}
