using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace LeafletPoc.Client.Dialogs
{
    public partial class InfoDialog : ComponentBase
    {
        [CascadingParameter] public MudDialogInstance? MudDialog { get; set; } = default!;

        void Submit() => MudDialog!.Close(DialogResult.Ok(true));
        void Cancel() => MudDialog!.Cancel();
    }
}