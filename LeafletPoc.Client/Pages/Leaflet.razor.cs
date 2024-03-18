using LeafletPoc.Client.Dialogs;
using LeafletPoc.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MudBlazor;

namespace LeafletPoc.Client.Pages
{
    public partial class Leaflet : ComponentBase
    {
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;

        private string? _result;
        private string? _locationMessage;
        private Location _location = new Location { Latitude = 0, Longitude = 0 };

        private List<Location> _locations = new List<Location>();




        protected override void OnInitialized()
        {
            GetLocations();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await RequestLocationPermissionAsync();

                var dotNetReference = DotNetObjectReference.Create(this);

                _result = await JSRuntime.InvokeAsync<string>("loadMap", _location.Latitude, _location.Longitude);
                await JSRuntime.InvokeVoidAsync("displayMarkers", _locations, dotNetReference);
            }
        }


        /// <summary>
        /// Simulates the function to recover the different locations
        /// </summary>
        private void GetLocations()
        {
            _locations = new List<Location>()
            {
                new Location {Id = 0, Name = "les pissenlits", Latitude = 48.688347, Longitude = 6.1819333, Type = LocationType.Restaurant },
                new Location {Id = 1, Name = "Vins et Tartines", Latitude = 48.688347, Longitude = 6.1819333, Type = LocationType.Poissonerie},
                new Location {Id = 2, Name = "Café du commerce", Latitude = 48.6931448, Longitude = 6.1825616, Type = LocationType.Bar},
                new Location {Id = 3, Name = "La table de stan", Latitude = 48.6937738, Longitude= 6.1809019, Type = LocationType.Restaurant},
            };
        }


        /// <summary>
        /// Asks the user to agree to share his position and places a marker on the map
        /// </summary>
        /// <returns>Return its current location</returns>
        private async Task RequestLocationPermissionAsync()
        {
            try
            {
                _location = await JSRuntime.InvokeAsync<Location>("requestLocationPermission");
                _locationMessage = $"Latitude: {_location.Latitude}, Longitude: {_location.Longitude}";
            }
            catch (Exception ex)
            {
                _locationMessage = $"Erreur lors de la récupération de la position : {ex.Message}";
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
    }
}