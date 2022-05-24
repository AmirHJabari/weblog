using System.Net.Http.Json;
using Weblog.Application.Commands.CreateTag;
using Weblog.Application.Common.Models;
using Weblog.Application.Queries.GetTagsWithPagination;
using Weblog.WebWasm.Interfaces;

namespace Weblog.WebWasm.Services;

public class TagHttpClient : ITagHttpClient, IDisposable
{
    private readonly HttpClient _client;

    public TagHttpClient()
    {
        _client = new HttpClient()
        {
            BaseAddress = new Uri(Settings.ApiBaseAddress)
        };
        //_client.BaseAddress = new Uri(Settings.ApiBaseAddress);
    }

    public async Task<int> CreateAsync(CreateTagCommand request, CancellationToken cancellationToken = default)
    {
        var res = await _client.PostAsJsonAsync("api/v1/Tags", request, cancellationToken: cancellationToken);
        return await res.Content.ReadFromJsonAsync<int>();
    }

    public Task<PaginatedList<TagDto>> GetWithPaginationAsync(GetTagsWithPaginationQuery request, CancellationToken cancellationToken = default)
    {
        return _client.GetFromJsonAsync<PaginatedList<TagDto>>(
            $"api/v1/Tags?PageNumber={request.PageNumber}&PageSize={request.PageSize}",
            cancellationToken: cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}