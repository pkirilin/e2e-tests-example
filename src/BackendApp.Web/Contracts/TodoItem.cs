using System.Diagnostics.CodeAnalysis;

namespace BackendApp.Web.Contracts;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class TodoItem
{
    public int Id { get; init; }
    public string? Title { get; init; }
}