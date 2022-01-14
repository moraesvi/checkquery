using CheckQuery.Business.POCO;
using CheckQuery.Business.Utils;
using CheckQuery.Domain;
using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Business
{
    public class File<T> where T : ICheck, ICheckAny, new()
    {
        private ICheckAny _objCheckAnyInstance = null;
        private ICheck _objCheckInstance = null;
        private FileReader _objFileReader = null;
        private QueryUtils _objQueryUtils = null;
        private IList<string> _files;
        private string _file;

        public File(string file)
        {
            this._objFileReader = new FileReader(new CheckQuery.Domain.POCO.File<FileData>(new TxtFile()));
            this._objQueryUtils = new QueryUtils(this._objFileReader);
            IFile objFile = this._objFileReader.ReadData(file);
            this._objCheckAnyInstance = new Template<T>(objFile);
            this._objCheckInstance = this._objCheckAnyInstance; 
        }

        public File(IList<string> files)
        {
            throw new NotImplementedException("Em construção");
        }

        public string GetFile 
        {
            get { return this._file; }
            set { this._file = value; }
        }

        public IList<string> Files
        {
            get { return this._files; }
            set { this._files = value; }
        }

        public string InsertToAnyTemplate()
        {
            try
            {
                string resultConcat = string.Empty;
                this._objQueryUtils.TemplateCollection = this._objCheckAnyInstance.TemplateData;
                resultConcat = _objQueryUtils.ConcatToAnyTemplate(this._objCheckAnyInstance, new List<ILine>(), new Line());
                return resultConcat;
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

        public IFile InsertToAnyTemplate<T, X>()
            where T : IFile, new()
            where X : IFileDataType, new()
        {
            try
            {
                IFile file = null;
                this._objQueryUtils.TemplateCollection = this._objCheckAnyInstance.TemplateData;
                file = _objQueryUtils.ConcatToAnyTemplate<T, X>(this._objCheckAnyInstance, new List<ILine>(), new Line());
                return file;
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

        public string InsertToTemplate()
        {
            try
            {
                string resultConcat = string.Empty;
                this._objQueryUtils.Template = this._objCheckInstance.TemplateData;
                resultConcat = _objQueryUtils.ConcatToTemplate(this._objCheckAnyInstance, new List<ILine>(), new Line());
                return resultConcat;
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

        public IFile InsertToTemplate<T, X>()
                             where T : IFile, new()
                             where X : IFileDataType, new()
        {
            try
            {
                IFile file = null;
                this._objQueryUtils.Template = this._objCheckInstance.TemplateData;
                file = _objQueryUtils.ConcatToTemplate<T, X>(this._objCheckInstance, new List<ILine>(), new Line());
                return file;
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
