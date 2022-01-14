using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain.Interfaces
{
    public interface ITxtFile
    {
        string Data { get; set; }

        IList<string> DataCollection { get; set; }
    }
}
