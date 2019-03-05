using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Handy.Domain.AccountContext.Commands;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.AccountContext.ReadModels;
using Handy.Domain.AccountContext.Services;
using Handy.Domain.SharedContext.Exceptions;
using Handy.Domain.SharedContext.MappingProfiles;
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

            mockAccountRepo
                .Setup(x => x.GetByCriteria(exp => exp.Login == "petooh"))
                .ReturnsAsync((Account) null);
            
            var accountCmdHandler = new AccountCommandHandler(mockAccountRepo.Object, TestHelper.GetMockMapper());

            var cmd = new RegisterAccount
            {
                Login = "petooh",
                Password = "123"
            };
            
            var account = await accountCmdHandler.Handle(cmd, CancellationToken.None);
            
            Assert.Equal(cmd.Login, account.Login);
            Assert.Equal(account.Login, account.ScreenName);
            Assert.NotNull(account.Registered);
        }

        [Fact]
        public async void AccountDoesntRegisterIfLoginExists()
        {
            var mockAccountRepo = new Mock<IRepository<Account>>();
            
            mockAccountRepo
                .Setup(x => x.GetByCriteria(exp => exp.Login == "kokoko"))
                .ReturnsAsync(new Account("kokoko", "123", 3, 12345));

            var accountCmdHandler = new AccountCommandHandler(mockAccountRepo.Object, TestHelper.GetMockMapper());

            var cmd = new RegisterAccount
            {
                Login = "kokoko",
                Password = "123"
            };
            
            Assert.ThrowsAsync<ConflictException>(() => accountCmdHandler.Handle(cmd, CancellationToken.None));
        }

    }
}