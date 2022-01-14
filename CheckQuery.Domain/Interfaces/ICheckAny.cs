using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain.Interfaces
{
    public interface ICheckAny : ICheck
    {
        IList<IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>>> FilterKey { get; }

        IDictionary<TemplateType, string> TemplateData { get; }

        IFileUtils FileUtils<T>() where T : IFileUtils, new();
    }
}
