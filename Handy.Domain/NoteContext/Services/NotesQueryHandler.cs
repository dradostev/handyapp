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
        
        public async Task<NoteRead> Handle(ShowNote query, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetByCriteria(x => x.Id == query.NoteId && x.AccountId == query.AccountId);
            if (note == null) throw new NotFoundException("Note not found");
            return _mapper.Map<NoteRead>(note);
        }

        public async Task<IEnumerable<NoteRead>> Handle(ListNotes query, CancellationToken cancellationToken)
        {
            var notes = await _noteRepository.ListByCriteria(x => x.AccountId == query.AccountId, query.Limit, query.Offset);
            return _mapper.Map<IEnumerable<NoteRead>>(notes);
        }
    }
}