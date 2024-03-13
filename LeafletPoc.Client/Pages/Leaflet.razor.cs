using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LeafletPoc.Client.Pages
{
    public partial class Leaflet : ComponentBase, IAsyncDisposable
    {
        [Inject] IJSRuntime JSRuntime { get; set; } = default!;
        private IJSObjectReference? module;
        private string? result;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                result = await JSRuntime.InvokeAsync<string>("loadMap");
            }
        }
        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (module is not null)
            {
                await module.DisposeAsync();
            }
        }
    }
}