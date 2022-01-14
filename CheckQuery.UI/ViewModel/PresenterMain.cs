using CheckQuery.Business;
using CheckQuery.Domain;
using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication1.ViewModel
{
    public class PresenterMain : ObservableObject
    {
        private string _file;
        public string File 
        {
            get { return _file; }
            set 
            {
                _file = value;
                RaisePropertyChangedEvent("File");
            }
        } 

        public ICommand FindFileCommand 
        {
            get { return new DelegateCommand(FindFile); }
        }

        public void FindFile() 
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
                string file = this.GetFile();
                if (!string.IsNullOrEmpty(file))
                {
                    CheckQuery.Utils.File = file;
                    string fileName = CheckQuery.Utils.GetFileName();
                    File<AnyTemplate> objFileTemplate = new File<AnyTemplate>(file);
                    string data = objFileTemplate.InsertToAnyTemplate();
                    SaveFile(fileName, data);
                }
            }
            catch (Exception ex) 
            {
                System.Windows.MessageBox.Show(ex.Message, "Keeptrue", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            } 
        }

        private string GetFile() 
        {
            string fileName = string.Empty;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".sql";
            dlg.Filter = "SQL Files (*.sql)|*.sql";
            bool? selected = dlg.ShowDialog();
            if (selected == true)
                fileName = dlg.FileName;
            return fileName;
        }

        private string SaveFile(string fileName, string data)
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.DefaultExt = ".sql";
            sfd.Filter = "SQL Files (*.sql)|*.sql";
            sfd.FileName = string.Concat(fileName, "_CHEKED.sql");
            bool? selected = sfd.ShowDialog();
            if (selected == true)
            {
                fileName = sfd.FileName;
                System.IO.File.WriteAllText(@fileName, data);
            }
            return fileName;
        }
    }
}
