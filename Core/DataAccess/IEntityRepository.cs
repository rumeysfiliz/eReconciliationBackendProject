using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()  //Bunun açıklaması diyoruz ki IEntityRepository bir tane T al bu T de class olmalı, IEntity ile implant edilmeli yani IEntity buna eklenmiş olmalı. Aynı zamanda da new lenebilir olsun. Yani ben buraya interfacesler gönderemeyim onlar newlenemez ama classlar newlenebilir. 
                                                                          //Artık biz bunu bir Generic yapıda oluşturmaya başladık.
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> GetList(Expression<Func<T,bool>>filter = null); //Burada biz ben bir sorgu yapabilirim bu sorgum aynı zamanda null da olabilir. İki durumda da bunu çalıştır dedik.
        T Get(Expression<Func<T, bool>> filter);  //update işleminde tek bir id ye ait kaydı getirmek için
    }
}
