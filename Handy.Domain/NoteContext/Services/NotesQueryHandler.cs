using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Handy.Domain.NoteContext.Entities;
using Handy.Domain.NoteContext.Queries;
using Handy.Domain.NoteContext.ReadModels;
using Handy.Domain.SharedContext.Exceptions;
using Handy.Domain.SharedContext.Services;
using MediatR;

namespace Handy.Domain.NoteContext.Services
{
    public class NotesQueryHandler : IRequestHandler<ShowNote, NoteRead>,
                                     IRequestHandler<ListNotes, IEnumerable<NoteRead>>
    {
        private readonly IRepository<Note> _noteRepository;
        private readonly IMapper _mapper;

        public NotesQueryHandler(IRepository<Note> noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }
        
        public async Task<NoteRead> Handle(ShowNote command, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetByCriteria(x => x.Id == command.NoteId && x.AccountId == command.AccountId);
            if (note == null) throw new NotFoundException("Note not found");
            return _mapper.Map<NoteRead>(note);
        }

        public async Task<IEnumerable<NoteRead>> Handle(ListNotes command, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.ListByCriteria(x => x.AccountId == command.AccountId);
            return _mapper.Map<IEnumerable<NoteRead>>(note);
        }
    }
}