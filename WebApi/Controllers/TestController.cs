using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IFluentEmail _fluentEmail;

        public TestController(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        [HttpGet]
        public async Task<IActionResult> SendTestEmail() //her zaman denemek i�in b�yle bir �ey ekler arkas�na kodda kullan�r�m
        {
            await _fluentEmail.To("test@test.com").Subject("Test Subject").Body("Test body").SendAsync();

            return NoContent(); //tamam �al���yor �imdi hata ald���n metopdu g�ster biraz yava� �zg�n�m
        }
    }
}
