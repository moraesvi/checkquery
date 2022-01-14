using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain
{
    public class FileDataConcat : IFile
    {
        private Guid _id;
        private IFileDataType _objInstance = null;

        public FileDataConcat() 
        {
            this._id = Guid.NewGuid();
        }

        public FileDataConcat(Guid id)
        {
            this._id = id;
        }

        public Guid Id 
        {
            get { return this._id; }
        }

        public string FileName { get; set; }

        public string Type { get; set; }

        public IFileDataType Data 
        {
            get { return _objInstance; } 
        }
    }
}
