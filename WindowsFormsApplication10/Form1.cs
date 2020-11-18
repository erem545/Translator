using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "txt (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = File.ReadAllText(openFileDialog.FileName);
                }
            }
            catch
            {
                MessageBox.Show("Недопустимый формат файла");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            int x = 0;
            int z = 0;
            for (int i = 0; i < textBox1.Lines.Length; i++)
            {
                switch (textBox1.Lines[i])
                {
                    case "var":
                        {
                            textBox2.Text += "{\r\n";
                            x++;
                            break;
                        }
                    case "begin":
                        {
                            if (x == 1) x++;
                            else textBox2.Text += "{\r\n";
                            break;
                        }
                    case "end;":
                        {
                            textBox2.Text += "}\r\n";
                            break;
                        }
                    case "end":
                        {
                            textBox2.Text += "}\r\n";
                            break;
                        }
                    case "end.":
                        {
                            textBox2.Text += "}";
                            break;
                        }
                    default:
                        {
                            if ((x == 1) && (textBox1.Lines[i] != ""))
                            {
                                if (textBox1.Lines[i].Contains("integer")) textBox2.Text += "int ";
                                if (textBox1.Lines[i].Contains("byte")) textBox2.Text += "byte ";
                                if (textBox1.Lines[i].Contains("double")) textBox2.Text += "double ";
                                if (textBox1.Lines[i].Contains("string")) textBox2.Text += "string ";
                                if (textBox1.Lines[i].Contains("char")) textBox2.Text += "char ";
                                int j = 0;
                                string s = textBox1.Lines[i];
                                while (s[j] != ':')
                                {
                                    textBox2.Text += s[j];
                                    j++;
                                }
                                textBox2.Text += ";\r\n";
                            }
                            if ((x == 2) && (textBox1.Lines[i] != ""))
                            {
                                bool q = true;
                                string s = textBox1.Lines[i];
                                if (textBox1.Lines[i].Contains(" then"))
                                {
                                    string substr = " then";
                                    int n = s.IndexOf(substr);
                                    s = s.Remove(n, substr.Length);
                                    z = 0;
                                }
                                if (textBox1.Lines[i].Contains(" of"))
                                {
                                    string substr = " of";
                                    int n = s.IndexOf(substr);
                                    s = s.Remove(n, substr.Length);
                                }
                                if (textBox1.Lines[i].Contains(" do"))
                                {
                                    string substr = " do";
                                    int n = s.IndexOf(substr);
                                    s = s.Remove(n, substr.Length);
                                }
                                if (textBox1.Lines[i].Contains("case"))
                                {
                                    string substr = "case";
                                    int n = s.IndexOf(substr);
                                    s = s.Remove(n, substr.Length);
                                    s = "switch" + s;
                                    z = 1;
                                }
                                if (textBox1.Lines[i].Contains("until"))
                                {
                                    string substr = "until";
                                    int n = s.IndexOf(substr);
                                    s = s.Remove(n, substr.Length);
                                    s = "while" + s;
                                    z = 1;
                                }
                                if (s.Contains("repeat"))
                                {
                                    textBox2.Text += "do";
                                    q = false;
                                }
                                if (s.Contains("for"))
                                {
                                    string substr = ":";
                                    int n = s.IndexOf(substr);
                                    s = s.Remove(n, substr.Length);
                                    substr = "for ";
                                    n = s.IndexOf(substr);
                                    s = s.Remove(n, substr.Length);
                                    string p = "";
                                    int j = 0;
                                    while (s[j] != '=')
                                    {
                                        p += s[j];
                                        j++;
                                    }
                                    s = "for (" + s + "; " + p + "++)";
                                    substr = " to ";
                                    string chstr = "; " + p + "<=";
                                    int IndexFirst = s.IndexOf(substr);
                                    s = s.Remove(IndexFirst, substr.Length).Insert(IndexFirst, chstr);

                                    textBox2.Text += s;
                                    q = false;
                                }
                                if (q) if ((s.Contains(":")) && (!(s.Contains(";"))))
                                {
                                    textBox2.Text += "case " + s;
                                    q = false;
                                }
                                if ((s.Contains("else")) && (z == 1))
                                {
                                    textBox2.Text += "default:";
                                    q = false;
                                }
                                if (q) for (int j = 0; j < s.Length; j++)
                                {
                                    if (s[j] != ':') textBox2.Text += s[j];
                                }
                                textBox2.Text += "\r\n";
                            }
                            break;
                        }
                }
            }
        }
    }
}
