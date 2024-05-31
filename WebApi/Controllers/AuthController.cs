using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]  //burası bizim asıl kayıt kısmımız
        public IActionResult Register(UserAndCompanyRegisterDto userAndCompanyRegister) //bir user bir de company bilgisi göndereceğiz.
        {
            var userExists = _authService.UserExists(userAndCompanyRegister.UserForRegister.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);  //Bu kullanıcı varsa bana hata döndürsün manasında
            }
            var companyExists = _authService.CompanyExists(userAndCompanyRegister.Company);
            if (!companyExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userAndCompanyRegister.UserForRegister, userAndCompanyRegister.UserForRegister.Password, userAndCompanyRegister.Company);

            var result = _authService.CreateAccessToken(registerResult.Data, registerResult.Data.CompanyId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(registerResult.Message);
        }


        [HttpPost("registerSecondAccount")] //burası da bizim firma içerisinde bir kullanıcı admin paneline giriş yaptıysa oradayken kullanıcı eklemek istediğindeki ekran
        public IActionResult RegisterSecondAccount(UserForRegisterToSecondAccountDto userForRegister)
        {
            var userExists = _authService.UserExists(userForRegister.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);  //Bu kullanıcı varsa bana hata döndürsün manasında
            }
            var registerResult = _authService.RegisterSecondAccount(userForRegister, userForRegister.Password, userForRegister.CompanyId);
            var result = _authService.CreateAccessToken(registerResult.Data, userForRegister.CompanyId);  //0 companyId temsil ediyor.
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(registerResult.Message);
        }
        [HttpPost("Login")]
        public IActionResult Login(UserForLogin userForLogin)
        {
            var userToLogin = _authService.Login(userForLogin);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            if (userToLogin.Data.IsActive)
            {
                var userCompany = _authService.GetCompany(userToLogin.Data.Id).Data;
                var result = _authService.CreateAccessToken(userToLogin.Data, userCompany.CompanyId);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Kullanıcı pasif durumda. Aktif etmek için yöneticinize danışın.");


        }

        [HttpGet("confirmuser")]
        public IActionResult ConfirmUser(string value)
        {
            var user = _authService.GetByMailConfirmValue(value).Data;
            user.MailConfirm = true;
            user.MailConfirmDate = DateTime.Now;
            var result = _authService.Update(user);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result.Message);
        }

        [HttpGet("sendConfirmEmail")]
        public IActionResult SendConfirmEmail(int id)
        {
            var user = _authService.GetById(id).Data;
            var result = _authService.SendConfirmEmail(user);

            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result.Message);
        }
    }
}
