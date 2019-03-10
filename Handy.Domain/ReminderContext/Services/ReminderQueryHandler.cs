using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Handy.Domain.ReminderContext.Entities;
using Handy.Domain.ReminderContext.Queries;
using Handy.Domain.ReminderContext.ReadModels;
using Handy.Domain.SharedContext.Exceptions;
using Handy.Domain.SharedContext.Services;
using MediatR;

namespace Handy.Domain.ReminderContext.Services
{
    public class ReminderQueryHandler : IRequestHandler<ListReminders, IEnumerable<ReminderRead>>,
                                        IRequestHandler<ShowReminder, ReminderRead>
    {
        private readonly IRepository<Reminder> _reminderRepository;
        private readonly IMapper _mapper;

        public ReminderQueryHandler(IRepository<Reminder> reminderRepository, IMapper mapper)
        {
            _reminderRepository = reminderRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<ReminderRead>> Handle(ListReminders query, CancellationToken cancellationToken)
        {
            if (!query.Filter.Contains("active") && !query.Filter.Contains("disabled"))
                return _mapper.Map<IEnumerable<ReminderRead>>(new List<object>());
            var remindersList = await _reminderRepository
                .ListByCriteria(
                    x => x.AccountId == query.AccountId 
                         && (x.Enabled == query.Filter.Contains("active") || !x.Enabled == query.Filter.Contains("disabled")),
                    query.Limit, query.Offset);
            return _mapper.Map<IEnumerable<ReminderRead>>(remindersList);
        }

        public async Task<ReminderRead> Handle(ShowReminder query, CancellationToken cancellationToken)
        {
            var reminder =
                await _reminderRepository.GetByCriteria(x => x.Id == query.ReminderId && x.AccountId == query.AccountId);
            if (reminder == null) throw new NotFoundException("Reminder not found");
            return _mapper.Map<ReminderRead>(reminder);
        }
    }

}