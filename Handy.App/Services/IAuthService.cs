using System.Threading.Tasks;
using Handy.Domain.AccountContext.Commands;

namespace Handy.App.Services
{
    public interface IAuthService
    {
        Task<string> GetToken(LogIn command);
    }
}