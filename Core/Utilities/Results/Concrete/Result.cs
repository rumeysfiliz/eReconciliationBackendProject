using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        
        //Burada şu işlemi yaptık. Bir işlem gönderdiğimde bana bir sonuç gönder. Sonucumuzda işlemin ve mesajın başarılı olup olmadığı
        //Sonuç ve mesajın ayrı ayrı oluşmasının sebebi ayrı ayrı isteyebiliriz.
        public Result(bool success) 
        {
            Success = success;
        }
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}
