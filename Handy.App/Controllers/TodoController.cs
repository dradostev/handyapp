using System;
using System.Threading.Tasks;
using App.Filters;
using Handy.Domain.TodoContext.Commands;
using Handy.Domain.TodoContext.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Handy.App.Controllers
{
    [Route("api/todos")]
    [Authorize]
    [ValidationFilter]
    public class TodoController : AbstractController
    {
        private readonly IMediator _bus;

        public TodoController(IMediator bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> ShowTodoLists()
        {
            var todoLists = await _bus.Send(new ShowMyTodoLists {AccountId = GetCurrentUserId()});
            return Json(todoLists);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> ShowTodoList(Guid id)
        {
            var todoList = await _bus.Send(new ShowTodoList{AccountId = GetCurrentUserId(), TodoListId = id});
            return Json(todoList);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateTodoList()
        {
            var todoList = await _bus.Send(new CreateTodoList{AccountId = GetCurrentUserId()});
            return Json(todoList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(Guid id)
        {
            await _bus.Send(new DeleteTodoList {TodoListId = id});
            return Json(new {message = "Todo list successfully deleted"});
        }
        
        [HttpPost("{id}/items")]
        public async Task<IActionResult> CreateTodo(Guid id, [FromBody] AddTodo command)
        {
            command.TodoListId = id;
            var todo = await _bus.Send(command);
            return Json(todo);
        }

        [HttpPatch("{listId}/items/{todoId}/title")]
        public async Task<IActionResult> RenameTodo(Guid listId, Guid todoId, [FromBody] RenameTodo command)
        {
            command.TodoId = todoId;
            var todo = await _bus.Send(command);
            return Json(todo);
        }
        
        [HttpPatch("{listId}/items/{todoId}/done")]
        public async Task<IActionResult> CheckTodo(Guid listId, Guid todoId)
        {
            var todo = await _bus.Send(new CheckTodo{TodoId = todoId});
            return Json(todo);
        }
        
        [HttpDelete("{listId}/items/{todoId}")]
        public async Task<IActionResult> DeleteTodo(Guid listId, Guid todoId)
        {
            await _bus.Send(new DeleteTodo{TodoId = todoId});
            return Json(new {message = "Todo successfully deleted"});
        }
    }
}