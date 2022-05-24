using System.Net.Http.Json;
using Weblog.Application.Commands.CreateCategory;
using Weblog.Application.Common.Models;
using Weblog.Application.Queries.GetCategoriesWithPagination;
using Weblog.WebWasm.Interfaces;

namespace Weblog.WebWasm.Services;

public class CategoryHttpClient : ICategoryHttpClient, IDisposable
{
    private readonly HttpClient _client;

    public CategoryHttpClient()
    {
        _client = new HttpClient()
        {
            BaseAddress = new Uri(Settings.ApiBaseAddress)
        };
        //_client.BaseAddress = new Uri(Settings.ApiBaseAddress);
    }

    public async Task<int> CreateAsync(CreateCategoryCommand request, CancellationToken cancellationToken = default)
    {
        var res = await _client.PostAsJsonAsync("api/v1/Categories", request, cancellationToken: cancellationToken);
        return await res.Content.ReadFromJsonAsync<int>();
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public Task<PaginatedList<CategoryDto>> GetWithPaginationAsync(GetCategoriesWithPaginationQuery request, CancellationToken cancellationToken = default)
    {
        return _client.GetFromJsonAsync<PaginatedList<CategoryDto>>(
            $"api/v1/Categories?PageNumber={request.PageNumber}&PageSize={request.PageSize}",
            cancellationToken: cancellationToken);
    }
}