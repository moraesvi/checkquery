using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain
{
    public enum TemplateType
    {
        Delete,
        Insert,
        Any
    }

    public enum ProcessGetDataID 
    {
        TableName,
        FieldOfWhereClausule,
        ValueOfWhereClausule,
        CaseOfInsert,
        CaseOfDelete,
        CaseNotFound
    }
}
