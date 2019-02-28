using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Handy.Domain.SharedContext.Exceptions;
using Handy.Domain.SharedContext.Services;
using Handy.Domain.TodoContext.Entities;
using Handy.Domain.TodoContext.Queries;
using Handy.Domain.TodoContext.ReadModels;
using MediatR;

namespace Handy.Domain.TodoContext.Services
{
    public class TodoQueryHandler : IRequestHandler<ShowMyTodoLists, IEnumerable<TodoListRead>>,
                                    IRequestHandler<ShowTodoList, TodoListRead>
    {
        private readonly IRepository<TodoList> _todoListRepository;
        private readonly IMapper _mapper;

        public TodoQueryHandler(IRepository<TodoList> todoListRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<TodoListRead>> Handle(ShowMyTodoLists command, CancellationToken cancellationToken)
        {
            var todoLists = await _todoListRepository.ListByCriteria(x => x.AccountId == command.AccountId);
            return _mapper.Map<IEnumerable<TodoListRead>>(todoLists);
        }

        public async Task<TodoListRead> Handle(ShowTodoList command, CancellationToken cancellationToken)
        {
            var todoList =
                await _todoListRepository.GetByCriteria(x =>
                    x.Id == command.TodoListId && x.AccountId == command.AccountId);
            if (todoList == null) throw new NotFoundException("Todo list not found");
            return _mapper.Map<TodoListRead>(todoList);
        }
    }
}