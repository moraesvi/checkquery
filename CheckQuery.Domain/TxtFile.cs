using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain
{
    public class TxtFile : IFileDataType
    {
        private IList<string> _dataCollection;
        public TxtFile() 
        {
            _dataCollection = new List<string>();
        }

        public string Data { get; set; }

        public IList<string> DataCollection 
        {
            get { return this._dataCollection; }
            set { this._dataCollection = value; }
        }
    }
}
