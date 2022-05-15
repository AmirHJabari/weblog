using Weblog.Application.Common.Mappings;
using Weblog.Domain.Entities;

namespace Weblog.Application.Queries.GetTodoItemsWithPagination;

public class TodoItemBriefDto : IMapFrom<TodoItem>
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string Title { get; set; }

    public bool Done { get; set; }
}
