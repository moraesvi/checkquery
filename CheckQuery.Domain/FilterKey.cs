 using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain
{
    public class FilterKey : IFilterKey
    {
        private string _startKey = string.Empty;
        private string _endKey = string.Empty;

        public FilterKey(string startKey, string endKey) 
        {
            this._startKey = startKey;
            this._endKey = endKey;
        }

        public string StartKey 
        {
            get { return this._startKey; }
        }

        public string EndKey 
        {
            get { return this._endKey; } 
        }
    }
}
