(function () {
    var tileUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
    var tileAttribution = 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>';

    // Global export
    window.deliveryMap = {
        showOrUpdate: function (elementId, markers) {
            console.log('showOrUpdate called with elementId:', elementId);
            console.log('Markers:', markers);

            var elem = document.getElementById(elementId);
            if (!elem) {
                console.error('No element with ID ' + elementId);
                throw new Error('No element with ID ' + elementId);
            }

            console.log('Element found:', elem);

            // Initialize map if needed
            if (!elem.map) {
                console.log('Creating new map');
                elem.map = L.map(elementId, {
                    center: [markers[0].y, markers[0].x],
                    zoom: 10
                });
                elem.map.addedMarkers = [];
                L.tileLayer(tileUrl, { attribution: tileAttribution }).addTo(elem.map);
            }

            var map = elem.map;
            if (map.addedMarkers.length !== markers.length) {
                console.log('Updating markers');
                // Markers have changed, so reset
                map.addedMarkers.forEach(marker => marker.removeFrom(map));
                map.addedMarkers = markers.map(m => {
                    console.log('Adding marker at:', m.y, m.x);
                    return L.marker([m.y, m.x]).bindPopup(m.description).addTo(map);
                });

                // Auto-fit the view
                var markersGroup = new L.featureGroup(map.addedMarkers);
                map.fitBounds(markersGroup.getBounds().pad(0.3));

                // Show applicable popups
                markers.forEach((marker, index) => {
                    if (marker.showPopup) {
                        map.addedMarkers[index].openPopup();
                    }
                });

                console.log('Map updated successfully');
            } else {
                console.log('Animating marker move');
                // Same number of markers, so update positions
                markers.forEach((marker, index) => {
                    animateMarkerMove(
                        map.addedMarkers[index].setPopupContent(marker.description),
                        marker,
                        4000);
                });
            }
        }
    };

    function animateMarkerMove(marker, coords, durationMs) {
        if (marker.existingAnimation) {
            cancelAnimationFrame(marker.existingAnimation.callbackHandle);
        }

        marker.existingAnimation = {
            startTime: new Date(),
            durationMs: durationMs,
            startCoords: { x: marker.getLatLng().lng, y: marker.getLatLng().lat },
            endCoords: coords,
            callbackHandle: window.requestAnimationFrame(() => animateMarkerMoveFrame(marker))
        };
    }

    function animateMarkerMoveFrame(marker) {
        var anim = marker.existingAnimation;
        var proportionCompleted = (new Date().valueOf() - anim.startTime.valueOf()) / anim.durationMs;
        var coordsNow = {
            x: anim.startCoords.x + (anim.endCoords.x - anim.startCoords.x) * proportionCompleted,
            y: anim.startCoords.y + (anim.endCoords.y - anim.startCoords.y) * proportionCompleted
        };

        marker.setLatLng([coordsNow.y, coordsNow.x]);

        if (proportionCompleted < 1) {
            marker.existingAnimation.callbackHandle = window.requestAnimationFrame(
                () => animateMarkerMoveFrame(marker));
        }
    }
})();