using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain
{
    public class Delete : ICheck
    {
        IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>> _dctIFilterKey = null;

        public Delete() 
        {
            _dctIFilterKey = new Dictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>>();
            IDictionary<ProcessGetDataID, IFilterKey> dctFlterData = new Dictionary<ProcessGetDataID, IFilterKey>();
            IFilterKey objTableName = new FilterKey("FROM", "WHERE");
            IFilterKey objFilterField = new FilterKey("WHERE", "");
            IFilterKey objFilterValue = new FilterKey("WHERE", "");
            dctFlterData.Add(ProcessGetDataID.TableName, objTableName);
            dctFlterData.Add(ProcessGetDataID.FieldOfWhereClausule, objFilterField);
            dctFlterData.Add(ProcessGetDataID.ValueOfWhereClausule, objFilterValue);
            _dctIFilterKey.Add(TemplateType.Delete, dctFlterData);
        }

        public string TemplateData
        {
            get { return Utils.GetTemplateData(TemplateType.Delete); }
        }

        public IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>> FilterKey
        {
            get { return this._dctIFilterKey; }
        }

        public IFile File
        {
            get { return this.File; }
        }

        public IFileUtils FileUtils<T>() where T : IFileUtils, new()
        {
            IFileUtils fileUtils = new T();
            return fileUtils;
        }
    }
}
