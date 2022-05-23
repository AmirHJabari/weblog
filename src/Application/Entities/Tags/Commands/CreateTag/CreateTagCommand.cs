using Weblog.Application.Common.Interfaces;
using Weblog.Domain.Entities;
using Weblog.Domain.Events;
using MediatR;
using Weblog.Domain.Enums;

namespace Weblog.Application.Commands.CreateTag;

public class CreateTagCommand : IRequest<int>
{
    public string Name { get; set; }
}

internal class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        request.Name = request.Name.ToLower();
        var entity = new Tag()
        {
            Name = request.Name
        };

        await _context.Tags.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
