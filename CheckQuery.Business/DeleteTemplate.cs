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
    public class DeleteTemplate : ICheckAny
    {
        private ICheck _objDeleteInstance = null;

        public DeleteTemplate() 
        {
            this._objDeleteInstance = new Delete();
        }

        public IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>> FilterKey
        {
            get { return this._objDeleteInstance.FilterKey; }
        }

        public IFile File
        {
            get { return this._objDeleteInstance.File; }
        }

        public string TemplateData
        {
            get { return this._objDeleteInstance.TemplateData; }
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
