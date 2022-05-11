using Weblog.Application.TodoLists.Queries.ExportTodos;

namespace Weblog.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
