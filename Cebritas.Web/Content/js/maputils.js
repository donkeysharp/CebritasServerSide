function timeStampToTime(stamp) {
  var date = new Date(stamp);
  var result = {hh: date.getHours(), mm: date.getMinutes()};
  return result;
}
function drawCircle(location, radius) {
  var R = 6371;
  var MM = Microsoft.Maps;
  var backgroundColor = new Microsoft.Maps.Color(155, 100, 255, 0);
  var borderColor = new Microsoft.Maps.Color(255, 200, 255, 0);
  var lat = (location.latitude * Math.PI) / 180;
  var lon = (location.longitude * Math.PI) / 180;
  var d = parseFloat(radius) / R;
  var circlePoints = new Array();

  for (x = 0; x <= 360; x += 5) {
    var p2 = new MM.Location(0, 0);
    brng = x * Math.PI / 180;
    p2.latitude = Math.asin(Math.sin(lat) * Math.cos(d) + Math.cos(lat) * Math.sin(d) * Math.cos(brng));
    p2.longitude = ((lon + Math.atan2(Math.sin(brng) * Math.sin(d) * Math.cos(lat),
                     Math.cos(d) - Math.sin(lat) * Math.sin(p2.latitude))) * 180) / Math.PI;
    p2.latitude = (p2.latitude * 180) / Math.PI;
    circlePoints.push(p2);
  }

  var polygon = new MM.Polygon(circlePoints, { fillColor: backgroundColor, strokeColor: borderColor, strokeThickness: 1 });
  return polygon;
}
function addPushPin(map, location, info, clickCallback) {
  var polygon = drawCircle(location, 0.078);
  var icon = window.icons[info.Type];
  var options = { icon: window.virtualPath + icon, width: 30, height: 50 };
  map.entities.push(polygon);
  var pushpin= new Microsoft.Maps.Pushpin(location, options);
  pushpin.Problem = info;
  Microsoft.Maps.Events.addHandler(pushpin, 'click', clickCallback);
  map.entities.push(pushpin);
}