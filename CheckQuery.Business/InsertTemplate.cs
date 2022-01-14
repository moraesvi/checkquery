using CheckQuery.Business.Utils;
using CheckQuery.Domain;
using CheckQuery.Domain.Interfaces;
using CheckQuery.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Business
{
    public class InsertTemplate : ICheckAny
    {
        private ICheck _objInsertInstance = null;

        public InsertTemplate() 
        {
            this._objInsertInstance = new Insert();
        }

        public IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>> FilterKey
        {
            get { return this._objInsertInstance.FilterKey; }
        }

        public IFile File
        {
            get { return this._objInsertInstance.File; }
        }

        public string TemplateData
        {
            get { return this._objInsertInstance.TemplateData; }
        }

        IList<IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>>> ICheckAny.FilterKey
        {
            get { throw new NotImplementedException(); }
        }

        IDictionary<TemplateType, string> ICheckAny.TemplateData
        {
            get { throw new NotImplementedException(); }
        }

        public IFileUtils FileUtils<T>() where T : IFileUtils, new()
        {
            IFileUtils objFileUtils = new T();
            return objFileUtils;
        }
    }
}
