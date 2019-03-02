using System;
using System.Collections.Generic;
using Handy.Domain.AccountContext.Services;
using Handy.Domain.NoteContext.Entities;
using Handy.Domain.ReminderContext.Entities;
using Handy.Domain.TodoContext.Entities;

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
        
        public List<Note> Notes { get; private set; }
        public List<TodoList> TodoLists { get; private set; }
        public List<Reminder> Reminders { get; private set; }
        
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