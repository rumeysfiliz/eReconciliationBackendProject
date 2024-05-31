using Core.Entities;
using Entities.Concrete;

namespace Entities.Dtos
{
    public class CompanyDto : IDto
    {
        public Company Company { get; set; }
        public int UserId { get; set; }
    }
}
