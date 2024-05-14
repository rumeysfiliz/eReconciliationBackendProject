using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public class MethodInterception : MethodInterceptionBaseAttribute
    {
        /*Öncesinde sonrasında, hata sırasında veya başarılıysa çalışma sırasında işlemler eklememiz lazım ki Attribute bu işlemler esnasında ne yapacağını bilsin. */
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, Exception e) { } //hata için bana bir exception versin.
        protected virtual void OnSuccess(IInvocation invocation) { }

        // invocation = çağrı
        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }

            OnAfter(invocation); //tüm işlemlerden sonra invocation dönsün.
        }
    }
}
