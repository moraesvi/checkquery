using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain.POCO
{
    public class FileUtils<T> : IFileUtils where T : IFileUtils, new()
    {
        IFileUtils _objInstanceFileUtils = null;

        public FileUtils() 
        {
            this._objInstanceFileUtils = new T();
        }

        public IList<ILine> FieldOfWhereClausule<T>(string fileLine, IList<ILine> lstLine, ILine line, IFilterKey filterData) 
            where T : ICheck, new()
        {
            return this._objInstanceFileUtils.FieldOfWhereClausule<T>(fileLine, lstLine, line, filterData);
        }

        public IList<ILine> ValueOfWhereClausule<T>(string fileLine, IList<ILine> lstLine, ILine line, IFilterKey filterData) 
            where T : ICheck, new()
        {
            return this._objInstanceFileUtils.ValueOfWhereClausule<T>(fileLine, lstLine, line, filterData);
        }
    }
}
