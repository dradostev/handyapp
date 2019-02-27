using System;
using System.Threading.Tasks;
using App.Filters;
using Handy.App.Services;
using Handy.Domain.AccountContext.Commands;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.AccountContext.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Handy.App.Controllers
{
    [Route("api/account")]
    [ValidationFilter]
    public class AccountController : Controller
    {
        private readonly IMediator _bus;
        private readonly IAuthService _authService;

        public AccountController(IMediator bus, IAuthService authService)
        {
            _bus = bus;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterAccount command)
        {
            var account = await _bus.Send(command);
            return Json(account);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LogIn command)
        {
            var token = await _authService.GetToken(command);
            return Json(new {id = User.Identity.Name, token});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowProfile()
        {
            var account = await _bus.Send(new ShowMyProfile {Login = User.Identity.Name});
            return Json(account);
        }
    }
}