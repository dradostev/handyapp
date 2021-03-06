using System;
using System.Threading.Tasks;
using App.Filters;
using Handy.Bot.Core;
using Handy.Domain.NoteContext.Commands;
using Handy.Domain.NoteContext.Queries;
using Handy.Domain.SharedContext.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Handy.App.Controllers
{
    [Route("api/notes")]
    [Authorize]
    [ValidationFilter]
    public class NotesController : AbstractController
    {
        private readonly IMediator _bus;

        public NotesController(IMediator bus)
        {
            _bus = bus;
        }
        
        [HttpGet]
        public async Task<IActionResult> ListNotes([FromQuery] PaginationQuery query)
        {
            var notes = await _bus.Send(new ListNotes
            {
                AccountId = GetCurrentUserId(),
                Limit = query.Limit,
                Offset = query.Offset
            });
            return Json(notes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ShowNote(Guid id)
        {
            var note = await _bus.Send(new ShowNote {NoteId = id, AccountId = GetCurrentUserId()});
            return Json(note);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] AddNote command)
        {
            command.AccountId = GetCurrentUserId();
            var note = await _bus.Send(command);
            return Json(note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyNote(Guid id, [FromBody] ModifyNote command)
        {
            command.NoteId = id;
            command.AccountId = GetCurrentUserId();
            var note = await _bus.Send(command);
            return Json(note);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            await _bus.Send(new DeleteNote {NoteId = id, AccountId = GetCurrentUserId()});
            return Json(new {message = "Note successfully deleted"});
        }
    }
}