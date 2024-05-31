using Core.Entities;

namespace Entities.Concrete
{
    public class TermsandCondition : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}