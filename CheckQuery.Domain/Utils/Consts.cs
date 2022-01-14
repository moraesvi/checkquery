using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain
{
    public class Consts
    {
        public static readonly string INSERT_TEMPLATE_IF_NOT_EXISTS = GetTemplate(TemplateType.Insert);

        public static readonly string DELETE_TEMPLATE_IF_EXISTS = GetTemplate(TemplateType.Delete);

        public static readonly string ANY_TEMPLATE = GetTemplate(TemplateType.Any);

        #region GetTemplate

        public static string GetTemplate(TemplateType templateType)
        {
            StringBuilder sbTemplate = new StringBuilder();
            StringBuilder sbAnyTemplate = new StringBuilder();
            sbAnyTemplate.AppendLine("IF {0} (SELECT TOP 1 1 FROM {1} WHERE {2}) ");
            sbAnyTemplate.AppendLine("BEGIN ");
            sbAnyTemplate.AppendLine("     {3}");
            sbAnyTemplate.AppendLine("END");
            sbAnyTemplate.AppendLine("");
            sbAnyTemplate.AppendLine("GO\n");
            switch (templateType)
            {
                case TemplateType.Insert :
                    sbTemplate.AppendLine("IF NOT EXISTS (SELECT TOP 1 1 FROM {0} WHERE {1}) ");
                    sbTemplate.AppendLine("BEGIN ");
                    sbTemplate.AppendLine("     {2}");
                    sbTemplate.AppendLine("END");
                    sbTemplate.AppendLine("");
                    sbTemplate.AppendLine("GO\n");
                    return sbTemplate.ToString();
                case TemplateType.Delete:
                    sbTemplate.AppendLine("IF EXISTS (SELECT TOP 1 1 FROM {0} WHERE {1}) ");
                    sbTemplate.AppendLine("BEGIN ");
                    sbTemplate.AppendLine("     {2}");
                    sbTemplate.AppendLine("END");
                    sbTemplate.AppendLine("");
                    sbTemplate.AppendLine("GO\n");
                    return sbTemplate.ToString();
                case TemplateType.Any:
                    return sbAnyTemplate.ToString();
                default:
                    return sbAnyTemplate.ToString();
            }
        }

        #endregion
    }
}
