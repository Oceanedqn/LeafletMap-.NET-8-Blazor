using LeafletPoc.Client.ResourceFiles;
using LeafletPoc.Client.Theme;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace LeafletPoc.Components.Layout
{
    public partial class MainLayout
    {
        [Inject] public IStringLocalizer<LeafletResource> Localizer { get; set; } = default!;
        private readonly LeafletTheme _theme = new LeafletTheme();
    }
}