using System;
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
            richTextBox1.Clear();
            serchfile(path.SelectedPath);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            test = true;
            richTextBox1.Clear();
            serchfile(path.SelectedPath);
        }

        private void serchfile(string path)
        {
            try
            {
                foreach (string file in Directory.GetDirectories(textBox1.Text))
                    serchfile(file);
                foreach (string file in Directory.GetFiles(textBox1.Text))
                {
                    string newfile = "";
                    string[] text = textBox2.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < text.Length; i++)
                    {
                        string[] str = text[i].Split('=');
                        if (file.IndexOf(str[0]) != -1)
                        {
                            if (newfile == "")
                                newfile = file.Replace(str[0], str[1]);
                            else
                                newfile = newfile.Replace(str[0], str[1]);

                            if (i == text.Length - 1)
                            {
                                if (newfile.IndexOf(textBox3.Text) != -1)
                                    newfile = newfile.Insert(newfile.IndexOf(textBox3.Text) + textBox3.Text.Length, " ");
                                richTextBox1.AppendText(file + " -----> " + newfile + "\n");
                                if (!test)
                                    File.Move(file, newfile);
                            }
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("格式錯誤");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
