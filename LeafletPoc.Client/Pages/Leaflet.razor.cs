using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LeafletPoc.Client.Pages
{
    public partial class Leaflet : ComponentBase
    {
        [Inject] IJSRuntime JSRuntime { get; set; } = default!;
        private string? _result;
        private string? _locationMessage;
        private Position _location = new Position { Latitude = 0, Longitude = 0 };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await RequestLocationPermission();
                Console.WriteLine(_locationMessage);
                _result = await JSRuntime.InvokeAsync<string>("loadMap", _location.Latitude, _location.Longitude);
            }
        }


        private async Task RequestLocationPermission()
        {
            try
            {
                _location = await JSRuntime.InvokeAsync<Position>("requestLocationPermission");
                _locationMessage = $"Latitude: {_location.Latitude}, Longitude: {_location.Longitude}";
            }
            catch (Exception ex)
            {
                _locationMessage = $"Erreur lors de la récupération de la position : {ex.Message}";
            }
        }
    }

    public class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}