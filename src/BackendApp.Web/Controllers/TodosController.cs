using BackendApp.Web.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BackendApp.Web.Controllers;

[ApiController]
[Route("api/todos")]
public class TodosController : ControllerBase
{
    private static readonly IEnumerable<TodoItem> Todos = new[]
    {
        new TodoItem { Id = 1, Title = "First" },
        new TodoItem { Id = 2, Title = "Second" },
        new TodoItem { Id = 3, Title = "Third" }
    };

    [HttpGet]
    public IActionResult GetTodos()
    {
        return Ok(new TodosResponse
        {
            Todos = Todos
        });
    }
}