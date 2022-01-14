using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain
{
    public class Line : ILine
    {
        public long Id { get; set; }
        public string Value { get; set; }
    }
}
