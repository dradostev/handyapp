using System;
using System.Threading;
using Handy.Domain.SharedContext.Services;
using Handy.Domain.TodoContext.Commands;
using Handy.Domain.TodoContext.Entities;
using Handy.Domain.TodoContext.Services;
using Moq;
using Xunit;

namespace Handy.Domain.Tests
{
    public class TodoTest
    {
        [Fact]
        public async void TodoListCreates()
        {
            var mockTodoListRepo = new Mock<IRepository<TodoList>>();
            var mockTodoRepo = new Mock<IRepository<Todo>>();
            var todoCmdHandler = new TodoCommandHandler(mockTodoListRepo.Object, mockTodoRepo.Object, TestHelper.GetMockMapper());

            var cmd = new CreateTodoList {AccountId = Guid.NewGuid()};

            var todoList = await todoCmdHandler.Handle(cmd, CancellationToken.None);
            
            Assert.NotNull(todoList.Id);
            Assert.Empty(todoList.Todos);
            Assert.NotNull(todoList.Created);
        }

        [Fact]
        public async void TodoAdded()
        {
            var mockTodoListRepo = new Mock<IRepository<TodoList>>();
            var mockTodoRepo = new Mock<IRepository<Todo>>();
            var todoList = new TodoList(Guid.NewGuid());
            mockTodoListRepo.Setup(x => x.GetById(todoList.Id)).ReturnsAsync(todoList);
            
            var todoCmdHandler = new TodoCommandHandler(mockTodoListRepo.Object, mockTodoRepo.Object, TestHelper.GetMockMapper());

            var cmd = new AddTodo
            {
                Title = "fap",
                TodoListId = todoList.Id
            };

            var todoItem = await todoCmdHandler.Handle(cmd, CancellationToken.None);
            
            Assert.NotNull(todoItem.Id);
            Assert.NotNull(todoItem.Created);
            Assert.False(todoItem.Done);
            Assert.Equal(cmd.Title, todoItem.Title);
            Assert.NotNull(todoList.Modified);
        }

        [Fact]
        public async void TodoRenamed()
        {
            var mockTodoListRepo = new Mock<IRepository<TodoList>>();
            var mockTodoRepo = new Mock<IRepository<Todo>>();
            
            var todo = new Todo("fap");
            var todoList = new TodoList(Guid.NewGuid());
            todoList.AddTodo(todo);

            mockTodoRepo.Setup(x => x.GetById(todo.Id)).ReturnsAsync(todo);

            var todoCmdHandler =
                new TodoCommandHandler(mockTodoListRepo.Object, mockTodoRepo.Object, TestHelper.GetMockMapper());
            
            var cmd = new RenameTodo{TodoId = todo.Id, NewTitle = "wank"};

            var todoRead = await todoCmdHandler.Handle(cmd, CancellationToken.None);
            
            Assert.Equal(cmd.NewTitle, todoRead.Title);
            Assert.NotNull(todoRead.Modified);
        }

        [Fact]
        public async void TodoChecked()
        {
            var mockTodoListRepo = new Mock<IRepository<TodoList>>();
            var mockTodoRepo = new Mock<IRepository<Todo>>();

            var todo = new Todo("fap");
            var todoList = new TodoList(Guid.NewGuid());
            todoList.AddTodo(todo);

            mockTodoRepo.Setup(x => x.GetById(todo.Id)).ReturnsAsync(todo);

            var todoCmdHandler =
                new TodoCommandHandler(mockTodoListRepo.Object, mockTodoRepo.Object, TestHelper.GetMockMapper());
            
            var cmd = new CheckTodo{TodoId = todo.Id};
            
            var todoRead = await todoCmdHandler.Handle(cmd, CancellationToken.None);
            
            Assert.True(todoRead.Done);
            Assert.NotNull(todoRead.Modified);
        }
    }
}