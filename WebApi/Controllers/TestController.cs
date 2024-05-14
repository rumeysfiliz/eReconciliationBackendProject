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
        public async Task<IActionResult> SendTestEmail() //her zaman denemek için böyle bir þey ekler arkasýna kodda kullanýrým
        {
            await _fluentEmail.To("test@test.com").Subject("Test Subject").Body("Test body").SendAsync();

            return NoContent(); //tamam çalýþýyor þimdi hata aldýðýn metopdu göster biraz yavaþ üzgünüm
        }
    }
}
