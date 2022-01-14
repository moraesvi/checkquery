using CheckQuery.Business.POCO;
using CheckQuery.Domain;
using CheckQuery.Domain.Interfaces;
using CheckQuery.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Business.Utils
{
    public class QueryUtils
    {
        private string _template;
        private IDictionary<TemplateType, string> _dctTemplate;
        private FileReader _objFileReader = null;

        public QueryUtils(FileReader obj)
        {
            this._objFileReader = obj;
        }

        public string Template
        {
            get { return this._template; }
            set { this._template = value; }
        }

        public IDictionary<TemplateType, string> TemplateCollection
        {
            get { return this._dctTemplate; }
            set { this._dctTemplate = value; }
        }

        //public IList<Template<A>> ConcatToTemplate<A, T, X>(Template<A> objTemplate, ICheckAny checkData, IList<ILine> lstLine, ILine line)
        //    where T : IFile, new()
        //    where X : IFileDataType, new()
        //    where A : ICheck, new()
        //{
        //    throw new NotImplementedException("Em construção");
        //}

        public IFile ConcatToAnyTemplate<T, X>(ICheckAny checkAny, IList<ILine> lstLine, ILine line)
            where T : IFile, new()
            where X : IFileDataType, new()
        {
            StringBuilder sbResult = new StringBuilder();
            IFile fileData = checkAny.File;
            IFileDataType dataTypeValue = fileData.Data;
            IFile objFileDataConcat = new CheckQuery.Domain.POCO.File<T>(new X());
            Parallel.For(0, dataTypeValue.DataCollection.Count,
                new ParallelOptions() { MaxDegreeOfParallelism = 3 },
                index =>
                {
                    var file = dataTypeValue.DataCollection;
                    lock (fileData)
                    {
                        try
                        {
                            TemplateType templateType = TemplateType.Any;
                            ICheck check = checkAny;
                            string fileLine = file[index];
                            IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>> dctIFilterKey = check.FilterKey;
                            IDictionary<ProcessGetDataID, IFilterKey> filterKey = this.GetProcessTemplate(fileLine, ref templateType, dctIFilterKey, checkAny.FilterKey);
                            bool isValid = ValidateExec(fileLine, templateType);
                            if (isValid)
                            {
                                string resultData = this.ConcatDataSerialized<ICheckAny>(checkAny, lstLine, line, fileData, objFileDataConcat, templateType, filterKey, fileLine);
                                sbResult.Append(resultData);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException(string.Concat("Ocorreu um erro inesperado\n ", ex.Message));
                        }
                    }
                });
            objFileDataConcat.Data.Data = sbResult.ToString();
            return objFileDataConcat;
        }

        public string ConcatToAnyTemplate(ICheckAny checkAny, IList<ILine> lstLine, ILine line)
        {
            StringBuilder sbResult = new StringBuilder();
            IFile fileData = checkAny.File;
            IFileDataType dataTypeValue = fileData.Data;
            Parallel.For(0, dataTypeValue.DataCollection.Count,
                new ParallelOptions() { MaxDegreeOfParallelism = 3 },
                index =>
                {
                    var file = dataTypeValue.DataCollection;
                    lock (fileData)
                    {
                        try
                        {
                            TemplateType templateType = TemplateType.Any;
                            ICheck check = checkAny;
                            string fileLine = file[index];
                            IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>> dctIFilterKey = check.FilterKey;
                            IDictionary<ProcessGetDataID, IFilterKey> filterKey = this.GetProcessTemplate(fileLine, ref templateType, dctIFilterKey, checkAny.FilterKey);
                            bool isValid = ValidateExec(fileLine, templateType);
                            if (isValid)
                            {
                                string resultData = this.ConcatData(lstLine, line, fileLine, filterKey, templateType);
                                sbResult.Append(resultData);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException(string.Concat("Ocorreu um erro inesperado\n ", ex.Message));
                        }
                    }
                });
            return sbResult.ToString();
        }

        public IFile ConcatToTemplate<T, X>(ICheck checkData, IList<ILine> lstLine, ILine line)
            where T : IFile, new()
            where X : IFileDataType, new()
        {
            StringBuilder sbResult = new StringBuilder();
            IFile fileData = checkData.File;
            IFileDataType dataTypeValue = fileData.Data;
            IFile objFileDataConcat = new CheckQuery.Domain.POCO.File<T>(new X());
            Parallel.For(0, dataTypeValue.DataCollection.Count,
                new ParallelOptions() { MaxDegreeOfParallelism = 3 },
                indexDataType =>
                {
                    var file = dataTypeValue.DataCollection;
                    lock (fileData)
                    {
                        try
                        {
                            string fileLine = file[indexDataType];
                            TemplateType templateType = checkData.FilterKey
                                                                 .Keys
                                                                 .FirstOrDefault();
                            bool isValid = ValidateExec(fileLine, templateType);
                            if (isValid)
                            {
                                string resultData = this.ConcatDataSerialized<ICheck>(checkData, templateType, lstLine, line, fileData, objFileDataConcat, fileLine);
                                sbResult.Append(resultData);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException(string.Concat("Ocorreu um erro inesperado\n ", ex.Message));
                        }
                    }
                });
            objFileDataConcat.Data.Data = sbResult.ToString();
            return objFileDataConcat;
        }

        public string ConcatToTemplate(ICheck checkData, IList<ILine> lstLine, ILine line)
        {
            StringBuilder sbResult = new StringBuilder();
            IFile fileData = checkData.File;
            IFileDataType dataTypeValue = fileData.Data;
            Parallel.For(0, dataTypeValue.DataCollection.Count,
                new ParallelOptions() { MaxDegreeOfParallelism = 3 },
                indexDataType =>
                {
                    var file = dataTypeValue.DataCollection;
                    lock (fileData)
                    {
                        try
                        {
                            string fileLine = file[indexDataType];
                            TemplateType templateType = checkData.FilterKey
                                                                 .Keys
                                                                 .FirstOrDefault();
                            bool isValid = ValidateExec(fileLine, templateType);
                            if (isValid)
                            {
                                string resultData = this.ConcatData(checkData, templateType, lstLine, line, fileLine);
                                sbResult.Append(resultData);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException(string.Concat("Ocorreu um erro inesperado\n ", ex.Message));
                        }
                    }
                });
            return sbResult.ToString();
        }

        #region ConcatData

        private string ConcatDataSerialized<T>(T checkData, TemplateType templateType, IList<ILine> lstLine, ILine line, IFile fileData, IFile objFileDataConcat, string fileLine)
            where T : ICheck
        {
            IDictionary<ProcessGetDataID, IFilterKey> filterKey = checkData.FilterKey
                                                                           .Where(key => key.Key == templateType)
                                                                           .FirstOrDefault()
                                                                           .Value;
            string tableName = this.GetTableName(fileLine, filterKey);
            string resultData = this.ProcessData(lstLine, line, filterKey, tableName, fileLine);
            objFileDataConcat.FileName = fileData.FileName;
            objFileDataConcat.Type = fileData.Type;
            objFileDataConcat.Data.DataCollection.Add(resultData);
            return resultData;
        }

        private string ConcatDataSerialized<T>(T checkAny, IList<ILine> lstLine, ILine line, IFile fileData, IFile objFileDataConcat, TemplateType templateType, IDictionary<ProcessGetDataID, IFilterKey> filterKey, string fileLine)
            where T : ICheckAny
        {
            string tableName = this.GetTableName(fileLine, filterKey);
            string resultData = this.ProcessData(lstLine, line, filterKey, templateType, tableName, fileLine);
            objFileDataConcat.FileName = fileData.FileName;
            objFileDataConcat.Type = fileData.Type;
            objFileDataConcat.Data.DataCollection.Add(resultData);
            return resultData;
        }

        private string ConcatData(IList<ILine> lstLine, ILine line, string fileLine, IDictionary<ProcessGetDataID, IFilterKey> filterKey, TemplateType templateType)
        {
            ProcessGetDataID processId = filterKey.FirstOrDefault()
                                                  .Key;
            string tableName = this.GetTableName(fileLine, filterKey);
            string resultData = this.ProcessData(lstLine, line, filterKey, templateType, tableName, fileLine);
            return resultData;
        }

        private string ConcatData(ICheck checkData, TemplateType templateType, IList<ILine> lstLine, ILine line, string fileLine)
        {
            IDictionary<ProcessGetDataID, IFilterKey> filterKey = checkData.FilterKey
                                                                           .Where(key => key.Key == templateType)
                                                                           .FirstOrDefault()
                                                                           .Value;
            string tableName = this.GetTableName(fileLine, filterKey);
            string resultData = this.ProcessData(lstLine, line, filterKey, tableName, fileLine);
            return resultData;
        }

        #endregion

        #region ProcessData

        private string ProcessData(IList<ILine> lstLine, ILine line, IDictionary<ProcessGetDataID, IFilterKey> filterKey, string tableName, string fileLine)
        {
            IList<ILine> whereFields = this.GetFieldOfWhereClausule(fileLine, filterKey, lstLine, line);
            IList<ILine> whereValues = this.GetValueOfWhereClausule(fileLine, filterKey, lstLine, line);
            string queryResultConcatenated = this.ProcessConcatOfTemplate(fileLine, tableName, whereFields, whereValues);
            return queryResultConcatenated;
        }

        private string ProcessData(IList<ILine> lstLine, ILine line, IDictionary<ProcessGetDataID, IFilterKey> filterKey, TemplateType templateType, string tableName, string fileLine)
        {
            IList<ILine> whereFields = this.GetFieldOfWhereClausule(fileLine, filterKey, lstLine, line);
            IList<ILine> whereValues = this.GetValueOfWhereClausule(fileLine, filterKey, lstLine, line);
            string queryResultConcatenated = this.ProcessConcatOfTemplate(fileLine, tableName, templateType, whereFields, whereValues);
            return queryResultConcatenated;
        }

        #endregion

        #region ValidateExec

        private bool ValidateExec(string fileLine, TemplateType fileType)
        {
            try
            {
                string result = string.Empty;
                string tableName = string.Empty;
                string startKey = fileType.ToString();
                string endKey = "";
                result = this._objFileReader.GetFieldByKey(fileLine, startKey, endKey);
                if (string.IsNullOrEmpty(result))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um problema ao concatenar os dados do arquivo.\n{0}", ex.Message));
            }
        }

        #endregion

        #region GetTemplate

        private IDictionary<ProcessGetDataID, IFilterKey> GetProcessTemplate(string fileLine, ref TemplateType templateType, IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>> filterKey, IList<IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>>> filters) 
        {
            IDictionary<ProcessGetDataID, IFilterKey> filterOfInsertCase = filterKey.Where(key => key.Key == TemplateType.Insert)
                                                                                    .FirstOrDefault()
                                                                                    .Value;
            IDictionary<ProcessGetDataID, IFilterKey> filterOfDeleteCase = filterKey.Where(key => key.Key == TemplateType.Delete)
                                                                                    .FirstOrDefault()
                                                                                    .Value;
            IFilterKey insertFilter = filterOfInsertCase.Where(key => key.Key == ProcessGetDataID.CaseOfInsert)
                                                        .FirstOrDefault()
                                                        .Value;
            IFilterKey deleteFilter = filterOfInsertCase.Where(key => key.Key == ProcessGetDataID.CaseOfDelete)
                                                        .FirstOrDefault()
                                                        .Value;
            string resultCaseOfInsert = string.Empty;
            string resultCaseOfDelete = string.Empty;
            string tableName = string.Empty;
            string insertStartKey = insertFilter.StartKey;
            string insertEndKey = insertFilter.EndKey;
            string deleteStartKey = deleteFilter.StartKey;
            string deleteEndKey = deleteFilter.EndKey;
            resultCaseOfInsert = this._objFileReader.GetFieldByKey(fileLine, insertStartKey, insertEndKey);
            resultCaseOfDelete = this._objFileReader.GetFieldByKey(fileLine, deleteStartKey, deleteEndKey);
            if (!string.IsNullOrEmpty(resultCaseOfInsert))
            {
                templateType = TemplateType.Insert;
                return filterOfInsertCase;
            }
            else if (!string.IsNullOrEmpty(resultCaseOfDelete))
            {
                templateType = TemplateType.Delete;
                return filterOfDeleteCase;
            }
            return null;
        }

        #endregion

        #region GetTableName

        private string GetTableName(string fileLine, IDictionary<ProcessGetDataID, IFilterKey> filterKey)
        {
            try
            {
                IFilterKey filter = filterKey.Where(key => key.Key == ProcessGetDataID.TableName)
                                             .FirstOrDefault()
                                             .Value;
                string tableName = this.TableName(fileLine, filter);
                return tableName;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um problema ao concatenar os dados do arquivo.\n{0}", ex.Message));
            }
        }

        private string GetTableName(string fileLine, IFilterKey filterKey)
        {
            try
            {
                string tableName = this.TableName(fileLine, filterKey);
                return tableName;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um problema ao concatenar os dados do arquivo.\n{0}", ex.Message));
            }
        }

        private string TableName(string fileLine, IFilterKey filter)
        {
            string result = string.Empty;
            string tableName = string.Empty;
            string startKey = filter.StartKey;
            string endKey = filter.EndKey;
            result = this._objFileReader.GetFieldByKey(fileLine, startKey, endKey);
            if (string.IsNullOrEmpty(result))
                return string.Empty;
            tableName = result.Split(new char[] { ' ' }, StringSplitOptions.None)[1];
            return tableName;
        }

        #endregion

        #region WhereClausule

        #region GetFieldOfWhereClausule

        private IList<ILine> GetFieldOfWhereClausule(string fileLine, IFilterKey filterKey, IList<ILine> lstLine, ILine line)
        {
            #region Where
            try
            {
                IList<ILine> lstLineResult = CheckQuery.Utils.GetInstance<IList<ILine>>(lstLine.GetType());
                lstLineResult = this.FieldOfWhereClausule(fileLine, lstLine, line, filterKey);
                return lstLineResult;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Ocorreu um problema ao concatenar os dados do arquivo.\n{0}", ex.Message));
            }
            #endregion
        }

        private IList<ILine> GetFieldOfWhereClausule(string fileLine, IDictionary<ProcessGetDataID, IFilterKey> filterKey, IList<ILine> lstLine, ILine line)
        {
            #region Where
            try
            {
                IList<ILine> lstLineResult = CheckQuery.Utils.GetInstance<IList<ILine>>(lstLine.GetType());
                IFilterKey filterData = filterKey.Where(filter => filter.Key == ProcessGetDataID.FieldOfWhereClausule)
                                                 .FirstOrDefault()
                                                 .Value;
                lstLineResult = this.FieldOfWhereClausule(fileLine, lstLine, line, filterData);
                return lstLineResult;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Ocorreu um problema ao concatenar os dados do arquivo.\n{0}", ex.Message));
            }
            #endregion
        }

        private IList<ILine> FieldOfWhereClausule(string fileLine, IList<ILine> lstLine, ILine line, IFilterKey filterData)
        {
            lstLine = CheckQuery.Utils.GetInstance<IList<ILine>>(lstLine.GetType());
            string result = string.Empty;
            string startKey = filterData.StartKey;
            string endKey = filterData.EndKey;
            result = this._objFileReader.GetFieldByKey(fileLine, startKey, endKey);
            string[] whereSplit = result.Split(new char[] { ',' }, StringSplitOptions.None);
            int whereIndex = 1;
            foreach (string value in whereSplit)
            {
                string whereValue = value;
                if (value.IndexOf("ID", StringComparison.OrdinalIgnoreCase) >= 0 && whereIndex == 1)
                    continue;
                if (value.IndexOf("(", StringComparison.OrdinalIgnoreCase) >= 0 || value.IndexOf(")", StringComparison.OrdinalIgnoreCase) >= 0)
                    whereValue = value.Replace("(", "").Replace(")", "");
                line = CheckQuery.Utils.GetInstance<ILine>(line.GetType());
                line.Id = whereIndex;
                line.Value = whereValue.Trim();
                lstLine.Add(line);
                whereIndex++;
            }
            return lstLine;
        }

        #endregion

        #region GetValueOfWhereClausule

        private IList<ILine> GetValueOfWhereClausule(string fileLine, IFilterKey filterKey, IList<ILine> lstLine, ILine line)
        {
            #region Value
            try
            {
                IList<ILine> lstLineResult = CheckQuery.Utils.GetInstance<IList<ILine>>(lstLine.GetType());
                lstLineResult = this.ValueOfWhereClausule(fileLine, lstLine, line, filterKey);
                return lstLineResult;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Ocorreu um problema ao concatenar os dados do arquivo.\n{0}", ex.Message));
            }
            #endregion
        }

        private IList<ILine> GetValueOfWhereClausule(string fileLine, IDictionary<ProcessGetDataID, IFilterKey> filterKey, IList<ILine> lstLine, ILine line)
        {
            #region Value
            try
            {
                IList<ILine> lstLineResult = CheckQuery.Utils.GetInstance<IList<ILine>>(lstLine.GetType());
                IFilterKey filterData = filterKey.Where(filter => filter.Key == ProcessGetDataID.ValueOfWhereClausule)
                                                 .FirstOrDefault()
                                                 .Value;
                lstLineResult = this.ValueOfWhereClausule(fileLine, lstLine, line, filterData);
                return lstLineResult;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Ocorreu um problema ao concatenar os dados do arquivo.\n{0}", ex.Message));
            }
            #endregion
        }

        private IList<ILine> ValueOfWhereClausule(string fileLine, IList<ILine> lstLine, ILine line, IFilterKey filterData)
        {
            lstLine = CheckQuery.Utils.GetInstance<IList<ILine>>(lstLine.GetType());
            string filterValue = string.Empty;
            bool closedQuotes = false;
            int quoteCount = 0;
            string result = string.Empty;
            long valueIndex = 1;
            string startKey = filterData.StartKey;
            string endKey = filterData.EndKey;
            result = this._objFileReader.GetFieldByKey(fileLine, startKey, endKey);
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
                line = CheckQuery.Utils.GetInstance<ILine>(line.GetType());
                line.Id = valueIndex;
                line.Value = string.Concat(sbPieceOfValue.ToString(), filterValue).Trim();
                lstLine.Add(line);
                valueIndex++;
            }
            return lstLine;
        }

        #endregion

        #endregion

        #region ProcessConcatOfTemplate

        private string ProcessConcatOfTemplate(string fileLine, string tableName, IList<ILine> whereFields, IList<ILine> whereValues)
        {
            try
            {
                string whereConcat = string.Empty;
                whereConcat = this.ConcatOfTemplate(fileLine, whereFields, whereValues);
                whereConcat = string.Format(_template, tableName, whereConcat, fileLine);
                return whereConcat;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(string.Format("Ocorreu um problema ao processar o template.\n{0}", ex.Message));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um problema ao processar o template.\n{0}", ex.Message));
            }
        }

        private string ProcessConcatOfTemplate(string fileLine, string tableName, TemplateType templateType, IList<ILine> whereFields, IList<ILine> whereValues)
        {
            try
            {
                string template = _dctTemplate.Where(key => key.Key == templateType)
                                              .FirstOrDefault()
                                              .Value;
                string whereConcat = string.Empty;
                whereConcat = this.ConcatOfTemplate(fileLine, whereFields, whereValues);
                whereConcat = string.Format(template, tableName, whereConcat, fileLine);
                return whereConcat;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(string.Format("Ocorreu um problema ao processar o template.\n{0}", ex.Message));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ocorreu um problema ao processar o template.\n{0}", ex.Message));
            }
        }

        private string ConcatOfTemplate(string fileLine, IList<ILine> whereFields, IList<ILine> whereValues)
        {
            string whereConcat = string.Empty;
            string query = string.Empty;
            StringBuilder whereSB = new StringBuilder();
            StringBuilder sbResult = new StringBuilder();
            bool dataIsValid = (whereFields.Count == whereValues.Count);
            if (dataIsValid)
            {
                int index = 0;
                string where = string.Empty;
                foreach (ILine lineField in whereFields)
                {
                    foreach (ILine lineValue in whereValues)
                    {
                        if (lineField.Id == lineValue.Id)
                        {
                            string whereFormated = string.Concat("RTRIM(LTRIM(", lineField.Value, "))");
                            string valueFormated = string.Concat("'", lineValue.Value, "'");
                            if (index == 0)
                                where = string.Concat(whereFormated, "=", valueFormated);
                            else
                                where = string.Concat(" AND ", whereFormated, "=", valueFormated);
                            whereSB.Append(where);
                        }
                        index++;
                    }
                }
                whereConcat = whereSB.ToString();
            }
            else
            {
                throw new InvalidOperationException(string.Concat("A linha: ", fileLine, "\nEstá com erro de sintaxe, verifique o número de colunas"));
            }
            return whereConcat;
        }

        #endregion
    }
}
