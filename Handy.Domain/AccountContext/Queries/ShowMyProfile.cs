using System;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.AccountContext.Entities;
using MediatR;

namespace Handy.Domain.AccountContext.Queries
{
    public class ShowMyProfile : IRequest<Account>
    {
        [Required]
        public Guid Id { get; set; }
    }
}