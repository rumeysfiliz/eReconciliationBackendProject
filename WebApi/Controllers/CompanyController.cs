using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("getcompanyList")]

        public IActionResult GetCompanyList()
        {
            var result = _companyService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


        [HttpGet("getCompany")]
        public IActionResult GetById(int id)
        {
            var result = _companyService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("addCompanyAndUserCompany")]
        public IActionResult AddCompanyAndUserCompany(CompanyDto companyDto)
        {
            var result = _companyService.AddCompanyAndUserCompany(companyDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        //[HttpPost("addCompany")]
        //public IActionResult AddCompany(Company company, int userId)
        //{
        //    var result = _companyService.Add(company);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result.Message);
        //}
        [HttpPost("updateCompany")]
        public IActionResult UpdateCompany(Company company)
        {
            var result = _companyService.Update(company);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
