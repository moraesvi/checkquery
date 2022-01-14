using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain.Interfaces
{
    public interface ICheck
    {
        IDictionary<TemplateType, IDictionary<ProcessGetDataID, IFilterKey>> FilterKey { get; }

        IFile File { get; }

        string TemplateData { get; }

        IFileUtils FileUtils<T>() where T : IFileUtils, new();
    }
}
