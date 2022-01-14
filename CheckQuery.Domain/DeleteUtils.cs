using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain
{
    public class DeleteUtils : IFileUtils
    {
        public IList<ILine> FieldOfWhereClausule<T>(string fileLine, IList<ILine> lstLine, ILine line, IFilterKey filterData) where T : ICheck, new()
        {
            lstLine = Utils.GetInstance<IList<ILine>>(lstLine.GetType());
            string result = string.Empty;
            string startKey = filterData.StartKey;
            string endKey = filterData.EndKey;
            result = Utils.GetDataByKey(fileLine, startKey, endKey);
            string[] whereSplit = result.Split(new char[] { ',' }, StringSplitOptions.None);
            int whereIndex = 1;
            foreach (string value in whereSplit)
            {
                string whereValue = value;
                if (value.IndexOf("ID", StringComparison.OrdinalIgnoreCase) >= 0 && whereIndex == 1)
                    continue;
                if (value.IndexOf("(", StringComparison.OrdinalIgnoreCase) >= 0 || value.IndexOf(")", StringComparison.OrdinalIgnoreCase) >= 0)
                    whereValue = value.Replace("(", "").Replace(")", "");
                line = Utils.GetInstance<ILine>(line.GetType());
                line.Id = whereIndex;
                line.Value = whereValue.Trim();
                lstLine.Add(line);
                whereIndex++;
            }
            return lstLine;
        }

        public IList<ILine> ValueOfWhereClausule<T>(string fileLine, IList<ILine> lstLine, ILine line, IFilterKey filterData) where T : ICheck, new()
        {
            lstLine = Utils.GetInstance<IList<ILine>>(lstLine.GetType());
            string filterValue = string.Empty;
            bool closedQuotes = false;
            int quoteCount = 0;
            string result = string.Empty;
            long valueIndex = 1;
            string startKey = filterData.StartKey;
            string endKey = filterData.EndKey;
            result = Utils.GetDataByKey(fileLine, startKey, endKey);
            StringBuilder sb = new StringBuilder();
            StringBuilder sbPieceOfValue = new StringBuilder();
            string[] split = result.Split(new char[] { ',' }, StringSplitOptions.None);
            foreach (var value in split)
            {
                filterValue = value;
                int singleQuotesCount = value.Where(valor => valor == '\'').Count();
                if (value.IndexOf("NEWID()", StringComparison.OrdinalIgnoreCase) >= 0 && valueIndex == 1)
                    continue;
                if ((singleQuotesCount == 1 || singleQuotesCount == 0) && closedQuotes == false)
                {
                    sbPieceOfValue.Append(value);
                    quoteCount++;
                    if (quoteCount == 1)
                        continue;
                    else if (quoteCount == 2)
                        quoteCount = 0;
                }
                if (singleQuotesCount == 2)
                    closedQuotes = true;
                if (value.IndexOf("'", StringComparison.OrdinalIgnoreCase) >= 0 || value.IndexOf("(", StringComparison.OrdinalIgnoreCase) >= 0 || value.IndexOf(")", StringComparison.OrdinalIgnoreCase) >= 0)
                    filterValue = value.Replace("'", "").Replace("(", "").Replace(")", "");
                line = Utils.GetInstance<ILine>(line.GetType());
                line.Id = valueIndex;
                line.Value = string.Concat(sbPieceOfValue.ToString(), filterValue).Trim();
                lstLine.Add(line);
                valueIndex++;
            }
            return lstLine;
        }
    }
}
