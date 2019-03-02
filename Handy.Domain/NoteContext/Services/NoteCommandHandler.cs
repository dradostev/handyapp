using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Handy.Domain.NoteContext.Commands;
using Handy.Domain.NoteContext.Entities;
using Handy.Domain.NoteContext.Events;
using Handy.Domain.NoteContext.ReadModels;
using Handy.Domain.SharedContext.Exceptions;
using Handy.Domain.SharedContext.Services;
using MediatR;

namespace Handy.Domain.NoteContext.Services
{
    public class NoteCommandHandler : IRequestHandler<AddNote, NoteRead>,
                                      IRequestHandler<ModifyNote, NoteRead>,
                                      IRequestHandler<DeleteNote, bool>
    {
        private readonly IRepository<Note> _noteRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _bus;

        public NoteCommandHandler(IRepository<Note> noteRepository, IMapper mapper, IMediator bus)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
            _bus = bus;
        }
        
        public async Task<NoteRead> Handle(AddNote command, CancellationToken cancellationToken)
        {
            var note = new Note(command.AccountId, command.Title, command.Content);
            await _noteRepository.Persist(note);
            await _bus.Publish(new NoteAdded {NoteId = note.Id}, cancellationToken);
            return _mapper.Map<NoteRead>(note);
        }

        public async Task<NoteRead> Handle(ModifyNote command, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetByCriteria(x => x.Id == command.NoteId && x.AccountId == command.AccountId);
            if (note == null) throw new NotFoundException("Note not found");
            
            if (!string.IsNullOrEmpty(command.Title)) note.ChangeTitle(command.Title);
            if (!string.IsNullOrEmpty(command.Content)) note.ChangeContent(command.Content);

            await _noteRepository.Update(note);
            return _mapper.Map<NoteRead>(note);
        }

        public async Task<bool> Handle(DeleteNote command, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetByCriteria(x => x.Id == command.NoteId && x.AccountId == command.AccountId);
            if (note == null) throw new NotFoundException("Note not found");

            await _noteRepository.Delete(note);
            return true;
        }
    }
}