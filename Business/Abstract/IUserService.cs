using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user, int companyId); //Kullanıcıya ait yetkileri getirecek.
        void Add(User user);
        void Update(User user);
        User GetByMail(string email); //Mail ile kullanıcıyı getirecek.
        User GetById(int id);
        User GetByMailConfirmValue(string value);
    }
}
