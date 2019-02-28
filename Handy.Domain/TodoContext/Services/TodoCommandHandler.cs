using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Handy.Domain.SharedContext.Exceptions;
using Handy.Domain.SharedContext.Services;
using Handy.Domain.TodoContext.Commands;
using Handy.Domain.TodoContext.Entities;
using Handy.Domain.TodoContext.ReadModels;
using MediatR;

namespace Handy.Domain.TodoContext.Services
{
    public class TodoCommandHandler : IRequestHandler<CreateTodoList, TodoListRead>,
                                      IRequestHandler<DeleteTodoList, bool>,
                                      IRequestHandler<AddTodo, TodoRead>,
                                      IRequestHandler<RenameTodo, TodoRead>,
                                      IRequestHandler<CheckTodo, TodoRead>,
                                      IRequestHandler<DeleteTodo, bool>
    {
        private readonly IRepository<TodoList> _todoListRepository;
        private readonly IRepository<Todo> _todoRepository;
        private readonly IMapper _mapper;

        public TodoCommandHandler(IRepository<TodoList> todoListRepository, IRepository<Todo> todoRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _todoRepository = todoRepository;
            _mapper = mapper;
        }
        
        public async Task<TodoListRead> Handle(CreateTodoList command, CancellationToken cancellationToken)
        {
            var todoList = new TodoList(command.AccountId);
            await _todoListRepository.Persist(todoList);
            return _mapper.Map<TodoListRead>(todoList);
        }

        public async Task<TodoRead> Handle(AddTodo command, CancellationToken cancellationToken)
        {
            var todoList = await _todoListRepository.GetById(command.TodoListId);
            if (todoList == null) throw new NotFoundException("Todo list not found");
            
            var todo = new Todo(command.Title);
            todoList.AddTodo(todo);
            
            await _todoListRepository.Update(todoList);
            return _mapper.Map<TodoRead>(todo);
        }

        public async Task<bool> Handle(DeleteTodoList command, CancellationToken cancellationToken)
        {
            var todoList = await _todoListRepository.GetById(command.TodoListId);
            if (todoList == null) throw new NotFoundException("Todo list not found");
            await _todoListRepository.Delete(todoList);
            return true;
        }

        public async Task<TodoRead> Handle(RenameTodo command, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetById(command.TodoId);
            if (todo == null) throw new NotFoundException("Todo not found");
            
            todo.Rename(command.NewTitle);
            
            await _todoRepository.Update(todo);
            return _mapper.Map<TodoRead>(todo);
        }

        public async Task<TodoRead> Handle(CheckTodo command, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetById(command.TodoId);
            if (todo == null) throw new NotFoundException("Todo not found");
            
            todo.Check();
            
            await _todoRepository.Update(todo);
            return _mapper.Map<TodoRead>(todo);
        }

        public async Task<bool> Handle(DeleteTodo command, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetById(command.TodoId);
            if (todo == null) throw new NotFoundException("Todo not found");
            await _todoRepository.Delete(todo);
            return true;
        }
    }
}