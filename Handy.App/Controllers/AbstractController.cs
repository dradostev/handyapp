using System;
using Microsoft.AspNetCore.Mvc;

namespace Handy.App.Controllers
{
    public abstract class AbstractController : Controller
    {
        protected Guid GetCurrentUserId()
        {
            return Guid.Parse(User.Identity.Name);
        }
    }
}