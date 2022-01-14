using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery
{
    public class Utils
    {
        private static IList<string> _fileDataCollection;

        private static string _fileData;

        private static string _file;

        public static IList<string> FileDataCollection
        {
            get 
            {
                IOGetFileData(_file, true);
                return _fileDataCollection; 
            }
            set 
            { 
                _fileDataCollection = value;
            }
        }

        public static string FileData 
        { 
            get 
            {
                IOGetFileData(_file, false);
                return _fileData; 
            } 
            set 
            { 
                _fileData = value; 
            } 
        }

        public static string File 
        {
            get { return _file; }
            set { _file = value; } 
        }

        public static void IOGetFileData(string file, bool serializeToColletion) 
        {
            try
            {
                IOInstance(file, serializeToColletion);
            }
            catch (Exception ex) 
            {
                throw new Exception(string.Format("Ocorreu um problema ao realizar o acesso do arquivo local: {0}", ex.Message));
            } 
        }

        public static string GetDataByKey(string data, string keyStart, string keyEnd)
        {
            try
            {
                string result = string.Empty;
                result = ProcessData(data, keyStart, keyEnd);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ocorreu um problema inesperado ao realizar o processamento do arquivo.");
            }
        }

        public static IList<string> GetDataByKey(IList<string> data, string keyStart, string keyEnd)
        {
            try
            {
                IList<string> resultCollection = new List<string>();
                string result = string.Empty;
                data.ToList().ForEach(fileLine =>
                {
                    result = ProcessData(fileLine, keyStart, keyEnd);
                    if (result != null)
                        resultCollection.Add(result);
                });
                return resultCollection;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ocorreu um problema inesperado ao realizar o processamento do arquivo.");
            }
        }

        public static T GetInstance<T>(Type type) where T : class
        {
            var instance = Activator.CreateInstance(type);
            return (T)instance;
        }

        private static string StringValueIsValid(string value) 
        {
            if (string.IsNullOrWhiteSpace(value)) 
            {
                throw new InvalidOperationException("O campo está vazio, campo obrigatório.");
            }
            return value;
        }

        private static string ProcessData(string data, string keyStart, string keyEnd)
        {
            string result = string.Empty;
            int start = data.IndexOf(keyStart, StringComparison.OrdinalIgnoreCase);
            int end = data.IndexOf(keyEnd, StringComparison.OrdinalIgnoreCase);
            if (start != -1 && end != -1)
            {
                if (end == 0)
                    result = data.Substring(start, (data.Length - start));
                else
                    result = data.Substring(start, (end - start));
            }
            else
                result = null;
            return result;
        }

        public static string GetFileName() 
        {
            string fileName = Path.GetFileNameWithoutExtension(_file);
            StringValueIsValid(fileName);
            return fileName;
        }

        public static string GetFileType() 
        {
            string fileType = Path.GetExtension(_file);
            StringValueIsValid(fileType);
            return fileType;
        }

        private static string IOInstance(string file, bool serializeToColletion) 
        {
            return IOGetData(file, serializeToColletion);
        }

        private static string IOGetData(string file, bool serializeToColletion)
        {
            string fileData = string.Empty;
            _fileDataCollection = new List<string>();
            StringValueIsValid(file);
            using (StreamReader str = new StreamReader(file, Encoding.GetEncoding("iso-8859-1")))
            {
                if (serializeToColletion)
                {
                    while ((fileData = str.ReadLine()) != null)
                    {
                        _fileDataCollection.Add(fileData);
                    }
                }
                else 
                {
                    fileData = str.ReadToEnd();
                    _fileData = fileData;
                    StringValueIsValid(fileData);
                }
            }
            return fileData;
        }
    }
}
