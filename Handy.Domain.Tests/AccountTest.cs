using System.Threading;
using Handy.Domain.AccountContext.Commands;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.AccountContext.Services;
using Handy.Domain.SharedContext.Services;
using Moq;
using Xunit;

namespace Handy.Domain.Tests
{
    public class AccountTest
    {
        [Fact]
        public async void AccountRegisters()
        {
            var mockAccountRepo = new Mock<IRepository<Account>>();
            var accountCmdHandler = new AccountCommandHandler(mockAccountRepo.Object);

            var cmd = new RegisterAccount
            {
                Login = "petooh",
                Password = "123"
            };
            
            var account = await accountCmdHandler.Handle(cmd, CancellationToken.None);
            
            Assert.Equal(cmd.Login, account.Login);
            Assert.Equal("a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", account.Password);
            Assert.Equal(account.Login, account.ScreenName);
            Assert.NotNull(account.Registered);
        }
    }
}