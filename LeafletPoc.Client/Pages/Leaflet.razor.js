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
        iconUrl: 'img/restaurantIcon.png',
        iconSize: [25, 41], // Taille de l'icône à un zoom de 1
    });

    const pubIcon = L.icon({
        iconUrl: 'img/pubIcon.png',
        iconSize: [25, 41], // Taille de l'icône à un zoom de 1
    });

    let markers = L.markerClusterGroup({
        iconCreateFunction: function (cluster) {
            let markers = cluster.getAllChildMarkers();
            let html = '<div class="circle" style="background-color: ' + locations.color + '!important;">' + markers.length + '</div>';
            return L.divIcon({ html: html, className: 'mycluster', iconSize: L.point(32, 32) });
        }
    });

    locations.locationsList.forEach(location => {
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

    // Select all markers
    let elements = document.querySelectorAll('.leaflet-marker-icon');
    elements.forEach(function (element) {
        // Change color to match with her group
        element.style.filter = 'hue-rotate(' + hexToHueRotate(locations.color) +')';
    });
}


// Deletes all markers in the map
function removeAllMarkers() {
    map.eachLayer(function (layer) {
        if (layer instanceof L.Marker || layer instanceof L.MarkerClusterGroup) {
            map.removeLayer(layer);
        }
    });
}

// Changes color in the icon marker
function hexToHueRotate(hex) {
    // Remove the "#" from the hexadecimal string
    hex = hex.replace("#", "");

    // Convert hexadecimal values into RGB values
    let r = parseInt(hex.substring(0, 2), 16) / 255;
    let g = parseInt(hex.substring(2, 4), 16) / 255;
    let b = parseInt(hex.substring(4, 6), 16) / 255;

    // Convert RGB values to HSL values
    let max = Math.max(r, g, b);
    let min = Math.min(r, g, b);
    let h, s, l = (max + min) / 2;

    if (max == min) {
        h = s = 0; // Achromatique
    } else {
        let d = max - min;
        s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
        switch (max) {
            case r: h = (g - b) / d + (g < b ? 6 : 0); break;
            case g: h = (b - r) / d + 2; break;
            case b: h = (r - g) / d + 4; break;
        }
        h /= 6;
    }

    let blueHue = 211; // hue value to obtain a shade of blue

    // Convert hue value to degrees
    let hueInDegrees = Math.round((h * 340 + blueHue) % 340);

    // Returns hue value for hue-rotate
    return hueInDegrees + "deg";
}
