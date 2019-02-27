using System;

namespace Handy.Domain.AccountContext.ReadModels
{
    public class UserProfile
    {
        public Guid Id { get; private set; }
        public string Login { get; private set; }
        public string ScreenName { get; private set; }
        public DateTime Registered { get; private set; }
    }
}