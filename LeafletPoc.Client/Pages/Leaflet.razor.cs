using LeafletPoc.Client.Dialogs;
using LeafletPoc.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace LeafletPoc.Client.Pages
{
    public partial class Leaflet : ComponentBase
    {
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;

        private Location _location = new Location { Latitude = 0, Longitude = 0 };
        private List<Locations> _locations = new List<Locations>();
        private DotNetObjectReference<Leaflet> _dotNetReference = default!;
        private Locations _lastClickedLocation = default!;

        protected override void OnInitialized()
        {
            GetLocations();
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _dotNetReference = DotNetObjectReference.Create(this);
                await RequestLocationPermissionAsync();
                await JSRuntime.InvokeVoidAsync("loadMap", _location.Latitude, _location.Longitude);
                await DisplayMarkersAsync(_locations.Find(x => x.LocationType == LocationType.Restaurant)!);
            }
        }


        /// <summary>
        /// Simulates the function to recover the different locations
        /// Retrieve the list of types available for list creation
        /// Creation of different lists according to type
        /// Filling lists with data
        /// </summary>
        private void GetLocations()
        {
            List<Location> locations = new List<Location>()
            {
                new Location {Id = 0, Name = "les pissenlits", Latitude = 48.688347, Longitude = 6.1819333, Type = LocationType.Poissonerie },
                new Location {Id = 1, Name = "Vins et Tartines", Latitude = 48.678347, Longitude = 6.1819333, Type = LocationType.Restaurant},
                new Location {Id = 2, Name = "Café du commerce", Latitude = 48.6931448, Longitude = 6.1825616, Type = LocationType.Bar},
                new Location {Id = 3, Name = "La table de stan", Latitude = 48.6937738, Longitude= 6.1809019, Type = LocationType.Restaurant},
            };

            // Retrieve the list of types available for list creation
            IEnumerable<LocationType> typesToAdd = locations.Select(location => location.Type)
                .Where(type => !_locations.Exists(l => l.LocationType == type))
                .Distinct();


            // Creation of different lists according to type
            foreach (LocationType type in typesToAdd)
            {
                _locations.Add(new Locations() { LocationType = type, Color = GetRandomColor(), Name = GetNameByType(type) });
            }

            // Filling lists with data
            foreach (Location location in locations)
            {
                _locations.Find(x => x.LocationType == location.Type)!.LocationsList.Add(location);
            }
        }


        /// <summary>
        /// Asks the user to agree to share his position.
        /// </summary>
        /// <returns>Return its current location</returns>
        private async Task RequestLocationPermissionAsync()
        {
            _location = await JSRuntime.InvokeAsync<Location>("requestLocationPermission");
        }


        /// <summary>
        /// Displays all markers in the list.
        /// </summary>
        /// <param name="locations"></param>
        /// <returns></returns>
        private async Task  DisplayMarkersAsync(Locations locations)
        {
            _lastClickedLocation = locations;
            await JSRuntime.InvokeVoidAsync("displayMarkers", locations, _dotNetReference);
            StateHasChanged();
        }


        /// <summary>
        /// Get random color
        /// </summary>
        /// <returns></returns>
        private string GetRandomColor()
        {
            Random rand = new Random();

            int r = rand.Next(256);
            int g = rand.Next(256);
            int b = rand.Next(256);

            return $"#{r:X2}{g:X2}{b:X2}";

        }


        /// <summary>
        /// Get name by type location
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetNameByType(LocationType type)
        {
            if(type == LocationType.Bar)
            {
                return "Bar";
            }
            else if(type == LocationType.Restaurant)
            {
                return "Restaurant";
            }
            else
            {
                return "Poissonerie";
            }
        }


        /// <summary>
        /// Method invoked from the js file. Opens the dialog
        /// </summary>
        /// <param name="location"></param>
        [JSInvokable("OpenDetailDialog")]
        public void OpenDetailDialog(Location location)
        {
            DialogOptions options = new DialogOptions { CloseButton = true };
            DialogService.Show<InfoDialog>(location.Name, options);
        }


        public string GetButtonStyle(Locations locations)
        {
            string witheColor = "";
            if(IsDarkColor(locations.Color))
            {
                witheColor = "color : white;";
            }

            return locations == _lastClickedLocation ? $"background-color:{locations.Color}; {witheColor}" : $"border: 2px solid {locations.Color};";
        }


        /// <summary>
        /// Checks if the color is dark or not.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool IsDarkColor(string color)
        {
            // Convertir la couleur hexadécimale en couleur RGB
            System.Drawing.Color c = System.Drawing.ColorTranslator.FromHtml(color);

            // Calculer la luminosité de la couleur
            double luminance = (0.299 * c.R + 0.587 * c.G + 0.114 * c.B) / 255;

            // Si la luminosité est inférieure à 0.5, la couleur est considérée comme sombre
            return luminance < 0.5;
        }
    }
}