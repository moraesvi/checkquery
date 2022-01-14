using CheckQuery.Business.Utils;
using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Business.POCO
{
    public class Template<T> : ICheck, ICheckAny where T : ICheck, ICheckAny, new()
    {
        private ICheck _objCheckInstance = null;
        private ICheckAny _objCheckAnyInstance = null;
        private FileReader _objReaderInstance = null;
        private IFile _objFileInstance = null;

        public Template(IFile objFile, string file) 
        {
            this._objCheckAnyInstance = new T();
            this._objCheckInstance = this._objCheckAnyInstance;
            this._objReaderInstance = new FileReader(objFile);
            this._objFileInstance = this._objReaderInstance.ReadData(file);
        }

        public Template(IFile objFile)
        {
            this._objCheckAnyInstance = new T();
            this._objCheckInstance = this._objCheckAnyInstance;
            this._objFileInstance = objFile;
        }

        public IDictionary<Domain.TemplateType, IDictionary<Domain.ProcessGetDataID, IFilterKey>> FilterKey
        {
            get { return this._objCheckInstance.FilterKey; }
        }

        public IFile File
        {
            get { return this._objFileInstance; }
        }

        public string TemplateData
        {
            get { return this._objCheckInstance.TemplateData; }
        }

        IList<IDictionary<Domain.TemplateType, IDictionary<Domain.ProcessGetDataID, IFilterKey>>> ICheckAny.FilterKey
        {
            get { return this._objCheckAnyInstance.FilterKey; }
        }

        IDictionary<Domain.TemplateType, string> ICheckAny.TemplateData
        {
            get { return this._objCheckAnyInstance.TemplateData; }
        }

        public IFileUtils FileUtils<T>() where T : IFileUtils, new()
        {
            IFileUtils objFileUtils = new T();
            return objFileUtils;
        }
    }
}
