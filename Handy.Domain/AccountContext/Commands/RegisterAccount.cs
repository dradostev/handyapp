using System.ComponentModel.DataAnnotations;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.AccountContext.ReadModels;
using MediatR;

namespace Handy.Domain.AccountContext.Commands
{
    public class RegisterAccount : IRequest<UserProfile>
    {
        [Required]
        public string Login { get; set; }
        
        [Required, MinLength(3)]
        public string Password { get; set; }
        
        public string ScreenName { get; set; }
    }
}