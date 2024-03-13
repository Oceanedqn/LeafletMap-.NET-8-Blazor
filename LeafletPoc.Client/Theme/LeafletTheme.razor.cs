using MudBlazor;

namespace LeafletPoc.Client.Theme
{
    public partial class LeafletTheme
    {
        readonly MudTheme Leaflet = new MudTheme()
        {
            Palette = new PaletteLight()
            {
                Primary = "#7392B7",
                Secondary = "#759EB8",
                Success = "#D7FFAB",
                Dark = "#303842",
                Warning = "#87255B",
                Info = "#788cc7"
            },

            Typography = new Typography()
            {
                H2 = new H2()
                {
                    FontSize = "32px",
                    FontWeight = 500,
                    LineHeight = 1.2,
                },
                Body2 = new Body2()
                {
                    FontSize = "16px",
                    FontWeight = 700,
                    LineHeight = 1.5,
                },
                H1 = new H1()
                {
                    FontSize = "40px",
                    FontWeight = 500,
                    LineHeight = 1.2,
                },

            },
            LayoutProperties = new LayoutProperties()
            {
                AppbarHeight = "57px",
            }
        };


        /// <summary>
        /// Apply custom theme, use it in MainLayout
        /// </summary>
        /// <returns></returns>
        public MudTheme UseCustomColor()
        {
            return Leaflet;
        }
    }
}