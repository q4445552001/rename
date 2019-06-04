using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace rename
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog path = new FolderBrowserDialog();
        bool test = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (path.ShowDialog() == DialogResult.OK)
                textBox1.Text = path.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            test = false;
            serchfile(path.SelectedPath);
        }

        private void serchfile(string path)
        {
            foreach (string file in Directory.GetDirectories(path))
                serchfile(file);
            foreach (string file in Directory.GetFiles(path))
            {
                string newfile = "";
                string[] text = textBox2.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < text.Length; i++)
                    if (file.IndexOf(text[i][0]) != -1)
                    {
                        if (newfile == "")
                            newfile = file.Replace(text[i][0], text[i][2]);
                        else
                            newfile = newfile.Replace(text[i][0], text[i][2]);

                        if (i == text.Length - 1)
                        {
                            newfile = newfile.Insert(file.IndexOf(text[i][0]) + 1, " ");
                            richTextBox1.AppendText(file + " " + newfile + "\n");
                            if(!test)
                                File.Move(file, newfile);
                        }
                    }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            test = true;
            serchfile(path.SelectedPath);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
