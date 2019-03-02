using System.Collections.Generic;
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
        
        public async Task<IEnumerable<ReminderRead>> Handle(ListReminders command, CancellationToken cancellationToken)
        {
            var remindersList = await _reminderRepository.ListByCriteria(x => x.AccountId == command.AccountId);
            return _mapper.Map<IEnumerable<ReminderRead>>(remindersList);
        }

        public async Task<ReminderRead> Handle(ShowReminder command, CancellationToken cancellationToken)
        {
            var reminder =
                await _reminderRepository.GetByCriteria(x => x.Id == command.ReminderId && x.AccountId == command.AccountId);
            if (reminder == null) throw new NotFoundException("Reminder not found");
            return _mapper.Map<ReminderRead>(reminder);
        }
    }

}