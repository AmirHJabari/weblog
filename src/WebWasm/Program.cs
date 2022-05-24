using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Weblog.Application.Commands.CreateTag;
using Weblog.WebWasm;
using Weblog.WebWasm.Interfaces;
using Weblog.WebWasm.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IBlogHttpClient, BlogHttpClient>();
builder.Services.AddScoped<ITagHttpClient, TagHttpClient>();
builder.Services.AddScoped<ICategoryHttpClient, CategoryHttpClient>();

builder.Services.AddValidatorsFromAssemblyContaining<Weblog.Application.Common.Models.Result>(); // just a random type from Application assembly
await builder.Build().RunAsync();
