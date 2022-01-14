using CheckQuery.Domain;
using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Business.Utils
{
    public class FileReader
    {
        private IFile _objInstance = null;
        public FileReader(IFile obj) 
        {
            _objInstance = obj;
        }

        public IList<IFile> ReadData(IList<string> files)
        {
            try
            {
                IList<IFile> lstFile = new List<IFile>();
                foreach (string file in files)
                {
                    CheckQuery.Utils.File = file;
                    _objInstance.FileName = CheckQuery.Utils.GetFileName();
                    _objInstance.Type = CheckQuery.Utils.GetFileType();

                    #region File Data
                    _objInstance.Data.DataCollection = CheckQuery.Utils.FileDataCollection;
                    #endregion
                    lstFile.Add(_objInstance);
                }
                return lstFile;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IFile ReadData(string file) 
        {
            try 
            {
                CheckQuery.Utils.File = file;
                _objInstance.FileName = CheckQuery.Utils.GetFileName();
                _objInstance.Type = CheckQuery.Utils.GetFileType();

                #region File Data
                _objInstance.Data.DataCollection = CheckQuery.Utils.FileDataCollection;
                #endregion

                return _objInstance;   
            }
            catch(InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetFieldByKey(string data, string keyStart, string keyEnd)
        {
            try
            {
                string dataResult = CheckQuery.Utils.GetDataByKey(data, keyStart, keyEnd);
                return dataResult;
            }
            catch (InvalidOperationException ex) 
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<string> GetFieldByKey(IList<string> data, string keyStart, string keyEnd)
        {
            try
            {
                IList<string> dataResult = CheckQuery.Utils.GetDataByKey(data, keyStart, keyEnd);
                return dataResult;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
