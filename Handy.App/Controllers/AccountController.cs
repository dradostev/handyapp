using System.Threading.Tasks;
using App.Filters;
using Handy.Domain.AccountContext.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Handy.App.Controllers
{
    [Route("api/account")]
    [ValidationFilter]
    public class AccountController : Controller
    {
        private readonly IMediator _bus;

        public AccountController(IMediator bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterAccount command)
        {
            var account = await _bus.Send(command);
            return Json(account);
        }
    }
}