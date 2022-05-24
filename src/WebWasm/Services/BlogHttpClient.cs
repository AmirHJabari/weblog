using System.Net.Http.Json;
using Weblog.Application.Commands.CreateBlog;
using Weblog.Application.Common.Models;
using Weblog.Application.Queries.GetBlogsWithPagination;
using Weblog.WebWasm.Interfaces;

namespace Weblog.WebWasm.Services;

public class BlogHttpClient : IBlogHttpClient, IDisposable
{
    private readonly HttpClient _client;

    public BlogHttpClient()
    {
        _client = new HttpClient()
        {
            BaseAddress = new Uri(Settings.ApiBaseAddress)
        };
        //_client.BaseAddress = new Uri(Settings.ApiBaseAddress);
    }

    public async Task<int> CreateAsync(CreateBlogCommand request, CancellationToken cancellationToken = default)
    {
        var res = await _client.PostAsJsonAsync("api/v1/Blogs", request, cancellationToken: cancellationToken);
        return await res.Content.ReadFromJsonAsync<int>();
    }

    public Task<PaginatedList<BlogBriefDto>> GetWithPaginationAsync(GetBlogsWithPaginationQuery request, CancellationToken cancellationToken = default)
    {
        return _client.GetFromJsonAsync<PaginatedList<BlogBriefDto>>(
            $"api/v1/Blogs?ExcludeNonPublic={request.ExcludeNonPublic}&PageNumber={request.PageNumber}&PageSize={request.PageSize}",
            cancellationToken: cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}