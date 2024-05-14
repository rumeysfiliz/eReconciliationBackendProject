using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    //Bu Attribute classlar ve methodlarda kullanılsın. Birden fazla kullanılabilsin. Inherited edilmiş yapılarda da kullanılabilsin.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)] 
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        //Bunun amacı şidmi biz buraya Validation için Attributelarımızı koymaya başladıktan sonra bunlara sıra vermek isteyebiliriz. Onun için sıra vermek adına
        public int Priority { get; set; }
        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
