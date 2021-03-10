var apiKey;
var stations;

function retrieveAllContracts(){
    // 21cd0d074e546da8f6a413eaad3243855368e4be
    apiKey = document.getElementById("input").value;
    const url = "https://api.jcdecaux.com/vls/v3/contracts?apiKey="+apiKey;
    const xmlHttpRequest = new XMLHttpRequest();
    xmlHttpRequest.open("GET", url, true);
    xmlHttpRequest.setRequestHeader("Accept","application/json");
    xmlHttpRequest.onload = contractsRetrieved;
    xmlHttpRequest.send();
}

function contractsRetrieved(){
    console.log("Retrieved !");
    var response = JSON.parse(this.responseText)
    console.log(response);

    createList(response);
}

function createList(response){
    response.forEach(element => {
        var option = new Option(element.name);
        document.getElementById("list").appendChild(option);
    });
}

function retrieveContractStations(){
    var contract = document.getElementById("contract-list").value;
    const url = "https://api.jcdecaux.com/vls/v1/stations?contract="+contract+"&apiKey="+apiKey;
    const xmlHttpRequest = new XMLHttpRequest();
    xmlHttpRequest.open("GET", url, true);
    xmlHttpRequest.setRequestHeader("Accept","application/json");
    xmlHttpRequest.onload = function(){
        stations = JSON.parse(this.responseText);
        console.log(stations);
    };
    xmlHttpRequest.send();
}

function getDistanceFrom2GpsCoordinates(lat1, lon1, lat2, lon2){
    // Radius of the earth in km
    var earthRadius = 6371;
    var dLat = deg2rad(lat2-lat1);
    var dLon = deg2rad(lon2-lon1);
    var a = 
        Math.sin(dLat/2) * Math.sin(dLat/2) +
        Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
        Math.sin(dLon/2) * Math.sin(dLon/2)
    ;
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
    var d = earthRadius * c // Distance in km
    return d;
}

function deg2rad(deg){
    return deg *(Math.PI/180);
}

function getClosestStation(){
    var targetLat = document.getElementById("lat").value;
    var targetLng = document.getElementById("lon").value;
    stations.sort(function(a,b){
        return getDistanceFrom2GpsCoordinates(targetLat, targetLng, a.position.lat, a.position.lng) - 
            getDistanceFrom2GpsCoordinates(targetLat, targetLng, b.position.lat, b.position.lng);
    })
    console.log(stations[0]);
    console.log(stations[0].position.lat)
    console.log(stations[0].position.lng);
}