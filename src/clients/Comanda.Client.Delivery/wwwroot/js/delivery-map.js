// Delivery Map JavaScript Functions
let map = null;
let markersLayer = null;
let currentLocationMarker = null;
let currentLocationCircle = null;

window.initDeliveryMap = function(elementId, centerLat, centerLng, markers) {
    console.log('initDeliveryMap called', { elementId, centerLat, centerLng, markerCount: markers?.length });
    
    // Check if map already exists and remove it
    if (map) {
        console.log('Removing existing map');
        map.remove();
        map = null;
        markersLayer = null;
    }

    // Get the container element
    const container = document.getElementById(elementId);
    if (!container) {
        console.error('Map container not found:', elementId);
        return false;
    }

    // Initialize the map
    try {
        map = L.map(elementId, {
            zoomControl: true,
            scrollWheelZoom: true
        }).setView([centerLat, centerLng], 13);

        console.log('Map initialized successfully');

        // Add OpenStreetMap tiles (same as Leaflet.js homepage)
        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
            maxZoom: 19
        }).addTo(map);

        // Create markers layer
        markersLayer = L.layerGroup().addTo(map);
        console.log('Markers layer created');

        // Force size calculation after a short delay
        setTimeout(() => {
            if (map) {
                map.invalidateSize();
                console.log('Map size invalidated');
            }
        }, 100);

        // Add markers
        if (markers && markers.length > 0) {
            addMarkersToMap(markers);
            
            // Fit bounds to show all markers
            const bounds = L.latLngBounds(markers.map(m => [m.lat, m.lng]));
            map.fitBounds(bounds, { padding: [50, 50] });
            console.log('Markers added and bounds fitted');
        }

        return true;
    } catch (error) {
        console.error('Error initializing map:', error);
        return false;
    }
};

window.updateDeliveryMarkers = function(markers) {
    if (!markersLayer) return;

    // Clear existing markers
    markersLayer.clearLayers();

    // Validate markers is an array
    if (!markers || !Array.isArray(markers)) {
        console.warn('updateDeliveryMarkers: markers is not an array', typeof markers);
        return;
    }

    // Add new markers
    addMarkersToMap(markers);
};

window.focusOnLocation = function(lat, lng) {
    if (!map) return;
    map.setView([lat, lng], 16);
};

window.updateCurrentLocation = function(lat, lng, accuracy) {
    if (!map) return;

    console.log('Updating current location:', lat, lng, 'accuracy:', accuracy);

    // Remove existing current location marker and circle
    if (currentLocationMarker) {
        currentLocationMarker.remove();
    }
    if (currentLocationCircle) {
        currentLocationCircle.remove();
    }

    // Create accuracy circle (shows GPS accuracy)
    currentLocationCircle = L.circle([lat, lng], {
        radius: accuracy,
        color: '#2196F3',
        fillColor: '#2196F3',
        fillOpacity: 0.15,
        weight: 1,
        opacity: 0.3
    }).addTo(map);

    // Create current position marker (blue dot)
    const currentLocationIcon = L.divIcon({
        className: 'current-location-marker',
        html: '<div style="background: #2196F3; border: 3px solid white; border-radius: 50%; width: 16px; height: 16px; box-shadow: 0 2px 6px rgba(0,0,0,0.4);"></div>',
        iconSize: [16, 16],
        iconAnchor: [8, 8]
    });

    currentLocationMarker = L.marker([lat, lng], { 
        icon: currentLocationIcon,
        zIndexOffset: 1000 // Ensure it's on top
    }).addTo(map);

    console.log('Current location marker updated');
};

function addMarkersToMap(markers) {
    if (!markers || !markersLayer) {
        console.warn('addMarkersToMap: markers or markersLayer is null', { markers: !!markers, markersLayer: !!markersLayer });
        return;
    }
    
    // Ensure markers is an array
    if (!Array.isArray(markers)) {
        console.error('addMarkersToMap: markers is not an array', typeof markers, markers);
        return;
    }

    console.log('Adding', markers.length, 'markers to map');

    markers.forEach(function(marker, index) {
        console.log('Adding marker', index, marker);
        
        // Create a custom div icon for the number
        const numberIcon = L.divIcon({
            className: 'delivery-marker-number',
            html: '<div style="background: #4CAF50; color: white; border-radius: 50%; width: 36px; height: 36px; display: flex; align-items: center; justify-content: center; font-weight: bold; font-size: 14px; border: 2px solid #2E7D32; box-shadow: 0 2px 4px rgba(0,0,0,0.3);">' + marker.items + '</div>',
            iconSize: [36, 36],
            iconAnchor: [18, 18]
        });

        const markerObj = L.marker([marker.lat, marker.lng], { icon: numberIcon });

        // Build popup content
        let popupContent = '<div style="min-width: 150px;">';
        popupContent += '<strong>Order #' + marker.orderId.substring(0, 8) + '</strong><br>';
        popupContent += '<span>' + marker.items + ' item(s)</span><br>';
        if (marker.locationName) {
            popupContent += '<strong>' + marker.locationName + '</strong><br>';
        }
        if (marker.address) {
            popupContent += '<span style="color: #666;">' + marker.address + '</span>';
        }
        popupContent += '</div>';

        markerObj.bindPopup(popupContent);
        markersLayer.addLayer(markerObj);
        console.log('Marker', index, 'added successfully');
    });
    
    console.log('All markers added. Total in layer:', markersLayer.getLayers().length);
}







