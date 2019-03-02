using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Handy.Domain.ReminderContext.Commands;
using Handy.Domain.ReminderContext.Entities;
using Handy.Domain.ReminderContext.ReadModels;
using Handy.Domain.SharedContext.Exceptions;
using Handy.Domain.SharedContext.Services;
using MediatR;

namespace Handy.Domain.ReminderContext.Services
{
    public class ReminderCommandHandler : IRequestHandler<AddReminder, ReminderRead>,
                                          IRequestHandler<ChangeReminder, ReminderRead>,
                                          IRequestHandler<ChangeReminderTime, ReminderRead>,
                                          IRequestHandler<SwitchReminder, ReminderRead>,
                                          IRequestHandler<DeleteReminder, bool>
    {
        private readonly IRepository<Reminder> _reminderRepository;
        private readonly IMapper _mapper;

        public ReminderCommandHandler(IRepository<Reminder> reminderRepository, IMapper mapper)
        {
            _reminderRepository = reminderRepository;
            _mapper = mapper;
        }
        
        public async Task<ReminderRead> Handle(AddReminder command, CancellationToken cancellationToken)
        {
            var reminder = new Reminder(command.AccountId, command.Content, command.FireOn);
            await _reminderRepository.Persist(reminder);
            return _mapper.Map<ReminderRead>(reminder);
        }

        public async Task<ReminderRead> Handle(ChangeReminder command, CancellationToken cancellationToken)
        {
            var reminder = await _reminderRepository.GetById(command.ReminderId);
            if (reminder == null) throw new NotFoundException("Reminder not found");
            
            reminder.ChangeContent(command.Content);

            await _reminderRepository.Update(reminder);
            return _mapper.Map<ReminderRead>(reminder);
        }

        public async Task<ReminderRead> Handle(ChangeReminderTime command, CancellationToken cancellationToken)
        {
            var reminder = await _reminderRepository.GetById(command.ReminderId);
            if (reminder == null) throw new NotFoundException("Reminder not found");
            
            reminder.ChangeFireTime(command.FireOn);

            await _reminderRepository.Update(reminder);
            return _mapper.Map<ReminderRead>(reminder);
        }

        public async Task<ReminderRead> Handle(SwitchReminder command, CancellationToken cancellationToken)
        {
            var reminder = await _reminderRepository.GetById(command.ReminderId);
            if (reminder == null) throw new NotFoundException("Reminder not found");
            
            reminder.SwitchEnabled();

            await _reminderRepository.Update(reminder);
            return _mapper.Map<ReminderRead>(reminder);
        }

        public async Task<bool> Handle(DeleteReminder command, CancellationToken cancellationToken)
        {
            var reminder = await _reminderRepository.GetById(command.ReminderId);
            if (reminder == null) throw new NotFoundException("Reminder not found");

            await _reminderRepository.Delete(reminder);
            return true;
        }
    }
}