using Weblog.Application.Common.Mappings;
using Weblog.Domain.Entities;

namespace Weblog.Application.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string Title { get; set; }

    public bool Done { get; set; }
}
