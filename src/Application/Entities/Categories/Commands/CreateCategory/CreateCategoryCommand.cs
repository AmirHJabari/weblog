using Weblog.Application.Common.Interfaces;
using Weblog.Domain.Entities;
using Weblog.Domain.Events;
using MediatR;
using Weblog.Domain.Enums;

namespace Weblog.Application.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<int>
{
    public string Description { get; set; }

    public string UrlName { get; set; }

    public string Name { get; set; }
}

internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category()
        {
            Name = request.Name,
            UrlName = request.UrlName,
            Description = request.Description
        };

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
