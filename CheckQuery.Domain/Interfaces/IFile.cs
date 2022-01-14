using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain.Interfaces
{
    public interface IFile
    {
        Guid Id { get;  }

        string Type { get; set; }

        string FileName { get; set; }

        IFileDataType Data { get; }
    }
}
