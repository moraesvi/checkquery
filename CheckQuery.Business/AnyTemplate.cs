using CheckQuery.Domain;
using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Business
{
    public class AnyTemplate : ICheckAny
    {
        private ICheckAny _objCheckAnyInstance = null;
        private ICheck _objCheckInstance = null;

        public AnyTemplate() 
        {
            this._objCheckAnyInstance = new Any(new InsertTemplate(), new DeleteTemplate());
            this._objCheckInstance = this._objCheckAnyInstance; 
        }

        public IList<IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>>> FilterKey
        {
            get { return this._objCheckAnyInstance.FilterKey; }
        }

        public IDictionary<TemplateType, string> TemplateData
        {
            get { return this._objCheckAnyInstance.TemplateData; }
        }

        public IFile File
        {
            get { return this._objCheckAnyInstance.File; }
        }

        string ICheck.TemplateData
        {
            get { return this._objCheckInstance.TemplateData; }
        }

        public IFileUtils FileUtils<T>() where T : IFileUtils, new()
        {
            return this._objCheckAnyInstance.FileUtils<T>();
        }

        IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>> ICheck.FilterKey
        {
            get { throw new NotImplementedException(); }
        }
    }
}
