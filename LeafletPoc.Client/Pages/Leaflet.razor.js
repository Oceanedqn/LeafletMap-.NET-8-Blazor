// Requests location of the current user.
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

// Loads and displays the map.
function loadMap(latitude, longitude) {

    const osm = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '© OpenStreetMap'
    });

    const osmHOT = L.tileLayer('https://{s}.tile.openstreetmap.fr/hot/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '© OpenStreetMap contributors, Tiles style by Humanitarian OpenStreetMap Team hosted by OpenStreetMap France'
    });


    map = L.map('map', {
        center: [latitude, longitude],
        zoom: 14,
        layers: [osm, osmHOT]
    });

    const baseMaps = {
        "OpenStreetMap": osm,
        "OpenStreetMap.HOT": osmHOT
    };

    L.control.layers(baseMaps).addTo(map);
}

// Displays the various markers and defines the dialog opening function.
function displayMarkers(locations, dotNetObject) {
    removeAllMarkers();

    const restaurantIcon = L.icon({
        iconUrl: 'img/restaurantIcon.png'
    });

    const pubIcon = L.icon({
        iconUrl: 'img/pubIcon.png'
    });


    let markers = L.markerClusterGroup({
        iconCreateFunction: function (cluster) {
            let markers = cluster.getAllChildMarkers();
            let html = '<div class="circle">' + markers.length + '</div>';
            return L.divIcon({ html: html, className: 'mycluster', iconSize: L.point(32, 32) });
        }
    });

    locations.forEach(location => {
        let iconType;
        if (location.type === 0) {
            iconType = restaurantIcon;
        } else {
            iconType = pubIcon;
        }

        markers.addLayer(L.marker([location.latitude, location.longitude], { icon: iconType }).on('click', function (e) {
            dotNetObject.invokeMethodAsync('OpenDetailDialog', location);
        }));
    });
    map.addLayer(markers);
}


// Deletes all markers in the map
function removeAllMarkers() {
    map.eachLayer(function (layer) {
        if (layer instanceof L.Marker || layer instanceof L.MarkerClusterGroup) {
            map.removeLayer(layer);
        }
    });
}
