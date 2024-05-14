using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, ContextDb>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user, int companyId)
        {
            using (var context = new ContextDb())
            {
                var result = from operationClaim in context.OperationClaims //burada tüm yetkilerin listesini çekiyorum
                             join userOperationClaim in context.UserOperationClaims //bu kod ile kullanıcı yetkileri table'da bulunan yetkiler ve bir üstteki yetkiler arasında eşleştirme yapıyorum ki, kullanıcı yetkileri table'da yazdığım Id bilgisinin karşısında name bilgisi çekebileyim.
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.CompanyId == companyId && userOperationClaim.UserId == user.Id  //burada kullanıcı ve şirket bilgisine göre kısıt veriyorum
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name,
                             }; //burada da yeni bir OperationClaim nesnesi türetip içine sadece UserOperationClaim kısmında yetki evrdiğim kullanıcı ve yetkileri listesini çekiyorum.
                return result.ToList();
            }
        }
    }
}
