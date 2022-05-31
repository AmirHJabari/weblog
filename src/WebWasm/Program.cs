using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IBlogHttpClient, BlogHttpClient>();
builder.Services.AddScoped<ITagHttpClient, TagHttpClient>();
builder.Services.AddScoped<ICategoryHttpClient, CategoryHttpClient>();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    // config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined; // Good On dark theme
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.NewestOnTop = false;

    config.SnackbarConfiguration.ClearAfterNavigation = true;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.HideTransitionDuration = 200;
    config.SnackbarConfiguration.ShowTransitionDuration = 200;
});

builder.Services.AddValidatorsFromAssemblyContaining<Weblog.Application.Common.Models.Result>(); // just a random type from Application assembly
await builder.Build().RunAsync();
