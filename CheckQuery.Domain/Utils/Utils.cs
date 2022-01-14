using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain
{
    public class Utils
    {
        public static T GetInstance<T>(Type type) where T : class
        {
            var instance = Activator.CreateInstance(type);
            return (T)instance;
        }

        public static string GetTemplateData(TemplateType templateType)
        {
            switch (templateType)
            {
                case TemplateType.Insert:
                    return Consts.INSERT_TEMPLATE_IF_NOT_EXISTS;
                case TemplateType.Delete:
                    return Consts.DELETE_TEMPLATE_IF_EXISTS;
                case TemplateType.Any:
                    return Consts.ANY_TEMPLATE;
                default:
                    return Consts.ANY_TEMPLATE;
            }
        }

        public static string GetDataByKey(string data, string keyStart, string keyEnd)
        {
            try
            {
                string result = string.Empty;
                result = ProcessData(data, keyStart, keyEnd);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ocorreu um problema inesperado ao realizar o processamento do arquivo.");
            }
        }

        public static IList<string> GetDataByKey(IList<string> data, string keyStart, string keyEnd)
        {
            try
            {
                IList<string> resultCollection = new List<string>();
                string result = string.Empty;
                data.ToList().ForEach(fileLine =>
                {
                    result = ProcessData(fileLine, keyStart, keyEnd);
                    if (result != null)
                        resultCollection.Add(result);
                });
                return resultCollection;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ocorreu um problema inesperado ao realizar o processamento do arquivo.");
            }
        }

        private static string ProcessData(string data, string keyStart, string keyEnd)
        {
            string result = string.Empty;
            int start = data.IndexOf(keyStart, StringComparison.OrdinalIgnoreCase);
            int end = data.IndexOf(keyEnd, StringComparison.OrdinalIgnoreCase);
            if (start != -1 && end != -1)
            {
                if (end == 0)
                    result = data.Substring(start, (data.Length - start));
                else
                    result = data.Substring(start, (end - start));
            }
            else
                result = null;
            return result;
        }
    }
}
