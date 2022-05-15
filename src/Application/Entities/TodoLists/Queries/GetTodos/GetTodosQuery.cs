using AutoMapper;
using AutoMapper.QueryableExtensions;
using Weblog.Application.Common.Interfaces;
using Weblog.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Weblog.Application.Common.Mappings;

namespace Weblog.Application.Queries.GetTodos;

public class GetTodosQuery : IRequest<TodosVm>
{
}

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                .ToList(),

            Lists = await _context.TodoLists
                .AsNoTracking()
                .OrderBy(t => t.Title)
                .ProjectToListAsync<TodoListDto>(_mapper.ConfigurationProvider, cancellationToken)
        };
    }
}
