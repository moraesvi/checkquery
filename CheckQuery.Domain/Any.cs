using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain
{
    public class Any : ICheckAny
    {
        private IList<IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>>> _lstIFilterKey;
        private IDictionary<TemplateType, string> _dctTemplateData;
        private IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>> _dctIFilterKey;

        public Any()
        {
            ICheck objInsert = new Insert();
            ICheck objDelete = new Delete();
            IDictionary<ProcessGetDataID, IFilterKey> dctFilter = new Dictionary<ProcessGetDataID, IFilterKey>();
            IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>> templateDataIFilterKey = new Dictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>>();
            IDictionary<TemplateType, string> templateData = new Dictionary<TemplateType, string>();
            this._lstIFilterKey = new List<IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>>>();
            this._dctTemplateData = new Dictionary<TemplateType, string>();
            _dctIFilterKey = new Dictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>>();
            IFilterKey objFilterInsert = new FilterKey("INSERT", "");
            IFilterKey objFilterDelete = new FilterKey("DELETE", "");
            IDictionary<ProcessGetDataID, IFilterKey> insertFilterDataIFilterKey = objInsert.FilterKey
                                                                                            .Where(key => key.Key == TemplateType.Insert)
                                                                                            .FirstOrDefault()
                                                                                            .Value;
            IDictionary<ProcessGetDataID, IFilterKey> deleteFilterDataIFilterKey = objDelete.FilterKey
                                                                                            .Where(key => key.Key == TemplateType.Delete)
                                                                                            .FirstOrDefault()
                                                                                            .Value;
            templateDataIFilterKey.Add(TemplateType.Insert, insertFilterDataIFilterKey);
            templateDataIFilterKey.Add(TemplateType.Delete, deleteFilterDataIFilterKey);
            templateData.Add(TemplateType.Insert, objInsert.TemplateData);
            templateData.Add(TemplateType.Delete, objDelete.TemplateData);
            dctFilter.Add(ProcessGetDataID.CaseOfInsert, objFilterInsert);
            dctFilter.Add(ProcessGetDataID.CaseOfDelete, objFilterDelete);
            this._lstIFilterKey.Add(templateDataIFilterKey);
            this._dctTemplateData.Add(TemplateType.Insert, objInsert.TemplateData);
            this._dctTemplateData.Add(TemplateType.Delete, objDelete.TemplateData);
            this._dctIFilterKey.Add(TemplateType.Any, dctFilter);
        }

        public Any(ICheck insert, ICheck delete) 
        {
            IDictionary<ProcessGetDataID, IFilterKey> dctFilter = new Dictionary<ProcessGetDataID, IFilterKey>();
            IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>> templateDataIFilterKey = new Dictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>>();
            IDictionary<TemplateType, string> templateData = new Dictionary<TemplateType, string>();
            this._lstIFilterKey = new List<IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>>>();
            this._dctTemplateData = new Dictionary<TemplateType, string>();
            _dctIFilterKey = new Dictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>>();
            IFilterKey objFilterInsert = new FilterKey("INSERT", "");
            IFilterKey objFilterDelete = new FilterKey("DELETE", "");
            IDictionary<ProcessGetDataID, IFilterKey> insertFilterDataIFilterKey = insert.FilterKey
                                                                                          .Where(key => key.Key == TemplateType.Insert)
                                                                                          .FirstOrDefault()
                                                                                          .Value;
            IDictionary<ProcessGetDataID, IFilterKey> deleteFilterDataIFilterKey = delete.FilterKey
                                                                                          .Where(key => key.Key == TemplateType.Delete)
                                                                                          .FirstOrDefault()
                                                                                          .Value;
            templateDataIFilterKey.Add(TemplateType.Insert, insertFilterDataIFilterKey);
            templateDataIFilterKey.Add(TemplateType.Delete, deleteFilterDataIFilterKey);
            templateData.Add(TemplateType.Insert, insert.TemplateData);
            templateData.Add(TemplateType.Delete, delete.TemplateData);
            dctFilter.Add(ProcessGetDataID.CaseOfInsert, objFilterInsert);
            dctFilter.Add(ProcessGetDataID.CaseOfDelete, objFilterDelete);
            this._lstIFilterKey.Add(templateDataIFilterKey);
            this._dctTemplateData.Add(TemplateType.Insert, insert.TemplateData);
            this._dctTemplateData.Add(TemplateType.Delete, delete.TemplateData);
            this._dctIFilterKey.Add(TemplateType.Any, dctFilter);
        }

        public IList<IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>>> FilterKey
        {
            get { return this._lstIFilterKey; }
        }

        public IDictionary<TemplateType, string> TemplateData
        {
            get { return _dctTemplateData; }
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

        IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>> ICheck.FilterKey
        {
            get { return _dctIFilterKey; }
        }

        string ICheck.TemplateData
        {
            get { throw new NotImplementedException(); }
        }
    }
}
