using System;
using System.ComponentModel.DataAnnotations;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.AccountContext.ReadModels;
using MediatR;

namespace Handy.Domain.AccountContext.Queries
{
    public class ShowMyProfile : IRequest<UserProfile>
    {
        [Required]
        public Guid Id { get; set; }
    }
}