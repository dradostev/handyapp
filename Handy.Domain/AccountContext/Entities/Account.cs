using System;
using System.Collections.Generic;
using Handy.Domain.AccountContext.Services;
using Handy.Domain.NoteContext.Entities;
using Handy.Domain.ReminderContext.Entities;

namespace Handy.Domain.AccountContext.Entities
{
    public class Account
    {
        public Guid Id { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string ScreenName { get; private set; }
        public long BotChatId { get; private set; }
        public short TimeZone { get; private set; }
        public DateTime Registered { get; private set; }
        public DateTime Modified { get; private set; }
        
        public List<Note> Notes { get; private set; }
        public List<Reminder> Reminders { get; private set; }
        
        private Account() {}
        
        public Account(string login, string password, short tz, long chatId, string screenName = "")
        {
            Id = Guid.NewGuid();
            Login = login;
            TimeZone = tz;
            BotChatId = chatId;
            Password = PasswordHelper.HashPassword(password);
            ScreenName = string.IsNullOrEmpty(screenName) ? login : screenName;
            Registered = DateTime.Now;
        }

        public void StartBotDialog(long chatId)
        {
            BotChatId = chatId;
            Modified = DateTime.Now;
        }

        public void ChangeTimeZone(short newTz)
        {
            TimeZone = newTz;
            Modified = DateTime.Now;
        }
    }
}