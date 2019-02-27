using Handy.Domain.AccountContext.Commands;

namespace Handy.App.Services
{
    public interface IAuthService
    {
        string GetToken(LogIn command);
    }
}