using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace readWrite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string _path;

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog of = new FolderBrowserDialog();
            DialogResult result = of.ShowDialog();
            _path = of.SelectedPath;
            textBox1.Text = _path;
        }

        private void button2_Click(object sender, EventArgs e)
        {
//            StreamReader sr = new StreamReader(textBox1.Text);
//            richTextBox1.Text = sr.ReadToEnd();
//            sr.Close();
            ProcessDirectory(_path);
           
        }

        public void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory. 
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory. 
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        // Insert logic for processing found files here. 
        public void ProcessFile(string path)
        {
            string correct = path.Replace(_path ,"");
            richTextBox1.AppendText(String.Format("Processed file '{0}'. \n", correct));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(textBox1.Text, true);
            sw.WriteLine(textBox2.Text);
            sw.Close();
        }
    }
}
