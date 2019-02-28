using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Handy.Domain.TodoContext.Commands
{
    public class DeleteTodo : IRequest<bool>
    {
        [Required]
        public Guid TodoId { get; set; }
    }
}