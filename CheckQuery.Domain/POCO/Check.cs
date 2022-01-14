using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain.POCO
{
    public class Check<T> : ICheck, ICheckAny where T : ICheck, ICheckAny, new()
    {
        private ICheck _objCheckInstance = null;
        private ICheckAny _objCheckAnyInstance = null;
        private IFile _objFileInstance = null;

        public Check()
        {
            this._objCheckAnyInstance = new T();
            this._objCheckInstance = this._objCheckAnyInstance;
        }

        public Check(IFile obj) 
        {
            this._objCheckAnyInstance = new T();
            this._objCheckInstance = this._objCheckAnyInstance;
            this._objFileInstance = obj;
        }

        public IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>> FilterKey
        {
            get { return this._objCheckInstance.FilterKey; }
        }

        string ICheck.TemplateData
        {
            get { return this._objCheckInstance.TemplateData; }
        }

        public IFile File
        {
            get { return this._objFileInstance; }
        }

        public IDictionary<TemplateType, string> TemplateData
        {
            get { return this._objCheckAnyInstance.TemplateData; }
        }

        public IFileUtils FileUtils<T>()
            where T : IFileUtils, new()
        {
            IFileUtils fileUtils = new T();
            return fileUtils;
        }

        IList<IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>>> ICheckAny.FilterKey
        {
            get { throw new NotImplementedException(); }
        }
    }
}
