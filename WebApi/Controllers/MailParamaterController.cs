﻿using Business.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailParamaterController : ControllerBase
    {
        private readonly IMailParameterService _mailParameterService;

        public MailParamaterController(IMailParameterService mailParameterService)
        {
            _mailParameterService = mailParameterService;
        }
        [HttpPost("update")]
        public IActionResult Update(MailParameter mailParameter)
        {
        var result = _mailParameterService.Update(mailParameter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
