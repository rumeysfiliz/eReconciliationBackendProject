using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ITermsandConditionService
    {
        IResult Update(TermsandCondition termsandCondition);
        IDataResult<TermsandCondition> Get();
    }
}
