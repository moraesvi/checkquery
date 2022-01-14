using CheckQuery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckQuery.Domain.POCO
{
    public class File<T> : IFile where T : IFile, new()
    {
        private IFile _objInstance = null;
        private IFileDataType _objFileDataTypeInstance = null;

        public File(IFileDataType objFileDataType) 
        {
            this._objInstance = new T();
            this._objFileDataTypeInstance = objFileDataType;
        }

        public Guid Id
        {
            get { return this._objInstance.Id; }
        }

        public string Type
        {
            get
            {
                return this._objInstance.Type;
            }
            set
            {
                this._objInstance.Type = value;
            }
        }

        public string FileName
        {
            get
            {
                 return this._objInstance.FileName;
            }
            set
            {
                this._objInstance.FileName = value;
            }
        }

        public IFileDataType Data
        {
            get { return this._objFileDataTypeInstance; }
        }
    }
}
