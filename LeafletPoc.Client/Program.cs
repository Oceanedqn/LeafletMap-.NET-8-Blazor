using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();
builder.Services.AddLocalization();

builder.Services.AddSingleton<IDialogService, DialogService>();

await builder.Build().RunAsync();
