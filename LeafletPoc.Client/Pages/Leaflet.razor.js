function loadMap(raw) {
    let map = L.map('map').setView({ lon: 26.097133, lat: 44.446165 }, 16);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 19 }).addTo(map);
    return "";
}