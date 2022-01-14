using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain
{
    public class FileData : IFile
    {
        private Guid _id;
        private IFileDataType objInstance = null;

        public FileData() 
        {
            this._id = Guid.NewGuid();
        }

        public FileData(Guid id)
        {
            this._id = id;
        }

        public Guid Id 
        {
            get { return this._id; }
        }

        public string Type { get; set; }

        public string FileName { get; set; }

        public IFileDataType Data
        {
            get { return this.objInstance; }
        }
    }
}
