using System;
using System.Threading;
using Handy.Domain.ReminderContext.Commands;
using Handy.Domain.ReminderContext.Entities;
using Handy.Domain.ReminderContext.Services;
using Handy.Domain.SharedContext.Services;
using Moq;
using Xunit;

namespace Handy.Domain.Tests
{
    public class ReminderTest
    {
        [Fact]
        public async void ReminderCreated()
        {
            var mockReminderRepo = new Mock<IRepository<Reminder>>();
            var reminderCmdHandler = new ReminderCommandHandler(mockReminderRepo.Object, TestHelper.GetMockMapper());

            var cmd = new AddReminder
            {
                Content = "wank",
                AccountId = Guid.NewGuid(),
                FireOn = new DateTime(2019, 4, 3, 9, 15, 0)
            };

            var reminder = await reminderCmdHandler.Handle(cmd, CancellationToken.None);
            
            Assert.Equal(cmd.Content, reminder.Content);
            Assert.Equal(cmd.FireOn, reminder.FireOn);
            Assert.True(reminder.Enabled);
            Assert.NotNull(reminder.Created);
        }

        [Fact]
        public async void ReminderRenamed()
        {
            var mockReminderRepo = new Mock<IRepository<Reminder>>();
            var reminder = new Reminder(Guid.NewGuid(), "kokoko", new DateTime(2019, 4, 3, 9, 15, 0));
            mockReminderRepo.Setup(x => x.GetById(reminder.Id)).ReturnsAsync(reminder);
            var reminderCmdHandler = new ReminderCommandHandler(mockReminderRepo.Object, TestHelper.GetMockMapper());

            var cmd = new ChangeReminder
            {
                ReminderId = reminder.Id,
                Content = "qooqareqoo"
            };

            var reminderRead = await reminderCmdHandler.Handle(cmd, CancellationToken.None);
            
            Assert.Equal(cmd.Content, reminder.Content);
            Assert.NotNull(reminder.Modified);
        }

        [Fact]
        public async void ReminderTimeChanged()
        {
            var mockReminderRepo = new Mock<IRepository<Reminder>>();
            var reminder = new Reminder(Guid.NewGuid(), "kokoko", new DateTime(2019, 4, 3, 9, 15, 0));
            mockReminderRepo.Setup(x => x.GetById(reminder.Id)).ReturnsAsync(reminder);
            var reminderCmdHandler = new ReminderCommandHandler(mockReminderRepo.Object, TestHelper.GetMockMapper());
            
            var cmd = new ChangeReminderTime
            {
                ReminderId = reminder.Id,
                FireOn = new DateTime(2057, 8, 2, 14, 2, 1)
            };
            
            var reminderRead = await reminderCmdHandler.Handle(cmd, CancellationToken.None);
            
            Assert.Equal(cmd.FireOn, reminderRead.FireOn);
            Assert.NotNull(reminder.Modified);
        }
        
        [Fact]
        public async void ReminderSwitched()
        {
            var mockReminderRepo = new Mock<IRepository<Reminder>>();
            var reminder = new Reminder(Guid.NewGuid(), "kokoko", new DateTime(2019, 4, 3, 9, 15, 0));
            mockReminderRepo.Setup(x => x.GetById(reminder.Id)).ReturnsAsync(reminder);
            var reminderCmdHandler = new ReminderCommandHandler(mockReminderRepo.Object, TestHelper.GetMockMapper());
            
            var cmd = new SwitchReminder
            {
                ReminderId = reminder.Id,
            };
            
            var reminderRead = await reminderCmdHandler.Handle(cmd, CancellationToken.None);
            
            Assert.False(reminder.Enabled);
            Assert.NotNull(reminder.Modified);
        }
    }
}