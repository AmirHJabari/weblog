using Microsoft.AspNetCore.Components.Web;
using Weblog.Application.Commands.CreateTag;
using Weblog.Application.Common.Models;
using Weblog.Application.Queries.GetTagsWithPagination;
namespace Weblog.WebWasm.Pages.Tags;

public partial class Index
{
    MudForm _mudForm;
    MudTextField<string> _tagNameInput;

    private PaginatedList<TagDto> Tags { get; set; }
    private CreateTagCommand Form { get; set; } = new();

    [Inject]
    public CreateTagCommandValidator Validator { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public ITagHttpClient ApiClient { get; set; }

    private async Task OnSubmit()
    {
        var errors = await Validator.GetValidationErrorsAsync(Form);
        if (errors is not null)
        {
            Snackbar.Add(string.Join("\n", errors), Severity.Error);

            return;
        }
        
        try
        {
            var id = await ApiClient.CreateAsync(Form);
            var listTask = UpdateListAsync();

            Form = new();
            await _tagNameInput.FocusAsync();

            await listTask;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("This is the error: " + ex);
        }
    }

    private async Task UpdateListAsync()
    {
        Tags = await ApiClient.GetWithPaginationAsync(new() { PageNumber = 1, PageSize = 10 });
    }
}