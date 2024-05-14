using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    //Burada artık bir veri istediğimde data mesaj ve durumu gelecek ya da sadece data ve durumu gelecek
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool success, string message): base(success, message)
        {
            Data = data;
        }
        public DataResult(T data, bool success): base(success) 
        {
            Data = data;
        }

        public T Data { get; }
    }
}
