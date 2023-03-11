using BackendApp.Web.Contracts;
using BackendApp.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendApp.Web.Controllers;

[ApiController]
[Route("api/todos")]
public class TodosController : ControllerBase
{
    private readonly AppDbContext _context;

    public TodosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos(CancellationToken cancellationToken)
    {
        var todos = await _context.Todos.ToListAsync(cancellationToken);

        var response = new TodosResponse
        {
            Todos = todos.Select(static todo => new TodoItem { Id = todo.Id, Title = todo.Title })
        };
        
        return Ok(response);
    }
}