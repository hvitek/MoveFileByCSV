using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyFileByCSV
{
    public partial class Form1 : Form
    {
        public string pathCsv;
        public string pathFolder;
        List<string> listAdres = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            vyberSoubor();
            MessageBox.Show(pathCsv);
        }

        private void vyberSoubor()
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Vybrat soubor";
            theDialog.Filter = "CSV files|*.csv";
            theDialog.InitialDirectory = @"C:\";


            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                pathCsv = theDialog.FileName;
               
            }
        }
        private void vybratSlozku()
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    pathFolder = folderBrowserDialog.SelectedPath;

                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            vybratSlozku();
            MessageBox.Show(pathFolder);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(pathCsv != "" && pathFolder != "")
            {
                createList();
                copyFiles();
            }
            else
            {
                MessageBox.Show("Není zadána cesta");
            }
            
        }

        private void copyFiles()
        {
            foreach (string adresa in listAdres.Skip(1))
            {
                FileInfo fi = new FileInfo(adresa);
                string jmenoSouboru = fi.Name;
                fi.MoveTo(pathFolder + @"\" + jmenoSouboru);
            }
            MessageBox.Show("Hotovo");
        }

        private void createList()
        {
            listAdres.Clear();
            
            
            using (StreamReader r = new StreamReader(pathCsv))
            {  
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    listAdres.Add(line);
                }
            }
        }
    }
}
