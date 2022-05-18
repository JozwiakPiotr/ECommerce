using ECommerce.Models.DTO;
using ECommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
            => _accountService = accountService;

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetById([FromRoute] Guid id)
        {
            var result = await _accountService.GetById(id);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUser loginUser)
        {
            var token = await _accountService.Login(loginUser);
            return Ok(token);
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegisterUser registerUser)
        {
            await _accountService.Register(registerUser);
            return Ok();
        }
    }
}