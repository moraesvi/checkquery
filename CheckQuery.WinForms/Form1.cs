using CheckQuery.Business;
using CheckQuery.Domain;
using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckQuery.WinForms
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
                string file = string.Empty;
                IFile fileResult;
                string fileName = string.Empty;
                this.openFileDialog1.DefaultExt = "sql";
                this.openFileDialog1.Filter = "sql files (*.sql)|*.sql";
                DialogResult result = this.openFileDialog1.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    file = this.openFileDialog1.FileName;
                    File<InsertTemplate> objFile = new File<InsertTemplate>(file);
                    fileResult = objFile.InsertToTemplate<FileDataConcat, TxtFile>();
                    if (fileResult != null)
                    {
                        MessageBox.Show("Processado com sucesso.", "Keeptrue", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                    this.saveFileDialog1.FileName = string.Concat(fileResult.FileName, "_", "Cheked");
                    DialogResult resultFileSaveDialog = this.saveFileDialog1.ShowDialog();
                    if (resultFileSaveDialog == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = this.saveFileDialog1.FileName + ".sql";
                        System.IO.File.WriteAllText(@fileName, fileResult.Data.Data);
                        MessageBox.Show("Arquivo gerado com sucesso.", "Keeptrue", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Keeptrue", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
