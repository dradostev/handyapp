using System.ComponentModel.DataAnnotations;
using Handy.Domain.AccountContext.Entities;
using MediatR;

namespace Handy.Domain.AccountContext.Commands
{
    public class LogInViaTelegram : IRequest<Account>
    {
        [Required]
        public string Login { get; set; }
        
        [Required]
        public int ChatId { get; set; }
        
        public string ScreenName { get; set; }
    }
}