using System;
using System.Threading.Tasks;
using App.Filters;
using Handy.Domain.ReminderContext.Commands;
using Handy.Domain.ReminderContext.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Handy.App.Controllers
{
    [Route("api/reminders")]
    [Authorize]
    [ValidationFilter]
    public class ReminderController : AbstractController
    {
        private readonly IMediator _bus;

        public ReminderController(IMediator bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> ShowRemindersList()
        {
            var reminders = await _bus.Send(new ListReminders {AccountId = GetCurrentUserId()});
            return Json(reminders);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> ShowReminder(Guid id)
        {
            var reminder = await _bus.Send(new ShowReminder {AccountId = GetCurrentUserId(), ReminderId = id});
            return Json(reminder);
        }

        [HttpPost]
        public async Task<IActionResult> AddReminder([FromBody] AddReminder command)
        {
            command.AccountId = GetCurrentUserId();
            var reminder = await _bus.Send(command);
            return Json(reminder);
        }

        [HttpPatch("{id}/content")]
        public async Task<IActionResult> ChangeReminder(Guid id, [FromBody] ChangeReminder command)
        {
            command.ReminderId = id;
            var reminder = await _bus.Send(command);
            return Json(reminder);
        }
        
        [HttpPatch("{id}/time")]
        public async Task<IActionResult> ChangeReminderTime(Guid id, [FromBody] ChangeReminderTime command)
        {
            command.ReminderId = id;
            var reminder = await _bus.Send(command);
            return Json(reminder);
        }
        
        [HttpPatch("{id}/enabled")]
        public async Task<IActionResult> SwitchReminder(Guid id)
        {
            var reminder = await _bus.Send(new SwitchReminder{ReminderId = id});
            return Json(reminder);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminder(Guid id)
        {
            await _bus.Send(new DeleteReminder{ReminderId = id});
            return Json(new {message = "Reminder successfully deleted"});
        }
    }
}