using System;
using Handy.Domain.AccountContext.Services;

namespace Handy.Domain.AccountContext.Entities
{
    public class Account
    {
        public Guid Id { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string ScreenName { get; private set; }
        public DateTime Registered { get; private set; }
        public DateTime Modified { get; private set; }

        private Account()
        {
            
        }
        
        public Account(string login, string password, string screenName = "")
        {
            Id = Guid.NewGuid();
            Login = login;
            Password = PasswordHelper.HashPassword(password);
            ScreenName = string.IsNullOrEmpty(screenName) ? login : screenName;
            Registered = DateTime.Now;
        }
    }
}