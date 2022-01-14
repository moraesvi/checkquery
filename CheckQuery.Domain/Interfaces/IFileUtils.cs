using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain.Interfaces
{
    public interface IFileUtils
    {
        IList<ILine> FieldOfWhereClausule<T>(string fileLine, IList<ILine> lstLine, ILine line, IFilterKey filterData)
            where T : ICheck, new();

        IList<ILine> ValueOfWhereClausule<T>(string fileLine, IList<ILine> lstLine, ILine line, IFilterKey filterData)
            where T : ICheck, new();
    }
}
