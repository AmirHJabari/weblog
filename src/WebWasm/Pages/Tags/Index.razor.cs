using Microsoft.AspNetCore.Components.Web;
using Weblog.Application.Commands.CreateTag;
using Weblog.Application.Common.Models;
using Weblog.Application.Queries.GetTagsWithPagination;

namespace Weblog.WebWasm.Pages.Tags;

public partial class Index : ComponentBase
{
    DialogOptions topCenter = new() { Position = DialogPosition.TopCenter };
    MudForm _mudForm;
    public MudTable<TagDto> tagsTable = new();
    MudTextField<string> _tagNameInput;

    private PaginatedList<TagDto> Tags { get; set; }
    private CreateTagCommand Form { get; set; } = new();
    public List<string> TagNames { get; set; } = new();

    [Inject]
    public CreateTagCommandValidator Validator { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public ITagHttpClient ApiClient { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }

    /// <summary>
    /// Here we simulate getting the paged, filtered and ordered data from the server
    /// </summary>
    private async Task<TableData<TagDto>> ReloadTagsTableData(TableState state)
    {
        Console.WriteLine("Tag is getting filled.");
        Tags = await ApiClient.GetWithPaginationAsync(new() { PageNumber = state.Page+1, PageSize = state.PageSize });

        this.StateHasChanged();

        return new TableData<TagDto>() { TotalItems = Tags.TotalCount, Items = Tags.Items };
    }

    private async Task OnSubmit()
    {
        var errors = await Validator.GetValidationErrorsAsync(Form);
        if (errors is not null)
        {
            Snackbar.Add(string.Join("<br/>", errors), Severity.Error);

            return;
        }
        
        try
        {
            var id = await ApiClient.CreateAsync(Form);
            var listTask = tagsTable.ReloadServerData();

            Form = new();
            await _tagNameInput.FocusAsync();

            await listTask;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("This is the error: " + ex);
        }
    }

    async Task InsertManyClicked()
    {
        var dialog = DialogService.Show<InsertDialog>("Delete Server", new DialogParameters { ["Form"] = Form, ["TagNames"] = TagNames }, topCenter);
        var result = await dialog.Result;
        if (!result.Cancelled && result.Data is 1)
        {
            //In a real world scenario we would reload the data from the source here since we "removed" it in the dialog already.
            foreach (var tag in TagNames)
            {
                try
                {
                    Form.Name = tag;
                    var id = await ApiClient.CreateAsync(Form);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("This is the error: " + ex);
                }
            }

            TagNames.Clear();
            
            var listTask = tagsTable.ReloadServerData();
            Form = new();
            await _tagNameInput.FocusAsync();
            await listTask;

            Snackbar.Add("Inserted Successfuly.", Severity.Success);
        }
    }
}