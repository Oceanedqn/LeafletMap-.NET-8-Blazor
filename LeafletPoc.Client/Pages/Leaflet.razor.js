/* eslint-disable indent */
let map;

window.requestLocationPermission = function () {
    return new Promise((resolve, reject) => {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            function (position) {
                resolve({ latitude: position.coords.latitude, longitude: position.coords.longitude });
            },
            function (error) {
                switch (error.code) {
                    case error.PERMISSION_DENIED:
                        console.error("L'utilisateur a refusé la demande de géolocalisation.");
                        break;
                    case error.POSITION_UNAVAILABLE:
                        console.error("L'information de localisation n'est pas disponible.");
                        break;
                    case error.TIMEOUT:
                        console.error('La demande de géolocalisation a expiré.');
                        break;
                    case error.UNKNOWN_ERROR:
                        console.error("Une erreur inconnue s'est produite.");
                        break;
                }
            }
        );
        } else {
            reject("La géolocalisation n'est pas prise en charge par ce navigateur.");
        }
    });
};

// Loads and displays the map. Also displays a marker at the user's position.
function loadMap (latitude, longitude) {
    map = L.map('map', { center: [latitude, longitude], zoom: 16 });
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 19 }).addTo(map);

    L.marker([latitude, longitude]).addTo(map)
        .bindPopup('You are here')
        .openPopup();
}

// Displays the various markers and defines the dialog opening function.
function displayMarkers(locations, dotNetObject) {
    const restaurantIcon = L.icon({
        iconUrl: 'img/restaurantIcon.png',
        iconAnchor: [22, 94], // point of the icon which will correspond to marker's location
        popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
    });
    const pubIcon = L.icon({
        iconUrl: 'img/pubIcon.png',
        iconAnchor: [22, 94], // point of the icon which will correspond to marker's location
        shadowAnchor: [4, 62],  // the same for the shadow
        popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
    });
    const markers = new L.MarkerClusterGroup({
        iconCreateFunction: function (cluster) {
            let markers = cluster.getAllChildMarkers();
            let html = '<div class="circle">' + markers.length + '</div>';
            return L.divIcon({ html: html, className: 'mycluster', iconSize: L.point(32, 32) });
        }
    });
    let typeIcon;

    locations.forEach(location => {
        if (location.type == 0) {
            typeIcon = restaurantIcon;
        } else {
            typeIcon = pubIcon;
        }
        markers.addLayer(L.marker([location.latitude, location.longitude], { icon: typeIcon }).addTo(map)
                .on('click', function (e) {
                    dotNetObject.invokeMethodAsync('OpenDetailDialog', location)
                }));
    });

    map.addLayer(markers);
}
