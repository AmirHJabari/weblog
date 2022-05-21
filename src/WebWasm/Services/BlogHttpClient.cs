using System.Net.Http.Json;
using Weblog.Application.Common.Models;
using Weblog.Application.Queries.GetBlogsWithPagination;
using Weblog.WebWasm.Interfaces;

namespace Weblog.WebWasm.Services;

public class BlogHttpClient : IBlogHttpClient
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

    public Task<PaginatedList<BlogBriefDto>> GetWithPaginationAsync(GetBlogsWithPaginationQuery request)
    {
        return _client.GetFromJsonAsync<PaginatedList<BlogBriefDto>>(
            $"api/v1/Blogs?ExcludeNonPublic={request.ExcludeNonPublic}&PageNumber={request.PageNumber}&PageSize={request.PageSize}");
    }
}