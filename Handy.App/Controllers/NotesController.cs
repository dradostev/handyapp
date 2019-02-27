using System;
using System.Threading.Tasks;
using Handy.Domain.NoteContext.Commands;
using Handy.Domain.NoteContext.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Handy.App.Controllers
{
    [Route("api/notes")]
    [Authorize]
    public class NotesController : AbstractController
    {
        private readonly IMediator _bus;

        public NotesController(IMediator bus)
        {
            _bus = bus;
        }
        
        [HttpGet]
        public async Task<IActionResult> ListNotes(Guid id)
        {
            var notes = await _bus.Send(new ListNotes {AccountId = GetCurrentUserId()});
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