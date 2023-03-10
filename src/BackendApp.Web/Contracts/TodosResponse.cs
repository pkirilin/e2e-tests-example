namespace BackendApp.Web.Contracts;

public class TodosResponse
{
    public IEnumerable<TodoItem> Todos { get; init; } = Array.Empty<TodoItem>();
}