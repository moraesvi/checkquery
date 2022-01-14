using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain.Interfaces
{
    public interface IFilterKey
    {
        string StartKey { get; }

        string EndKey { get; }
    }
}
