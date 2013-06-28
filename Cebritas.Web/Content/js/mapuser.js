window.onload = function () {
    getMap();
};
/**
 * Maps problem type with an icon
 */
window.icons = {
    '1': 'img/pptraffic.png',
    '2': 'img/ppriot.png',
    '3': 'img/ppparade.png',
    '4': 'img/ppblockade.png',
    '5': 'img/ppaccident.png',
    '6': 'img/ppother.png'
};
window.types = {
  '1': 'Traffic Jam',
  '2': 'Manifiestation',
  '3': 'Parade',
  '4': 'Blockade',
  '5': 'Accident',
  '6': 'Other'
};
window.Problems = [];
window.doneReport = false;

function getMap() {
  var key = 'ApC8X7yE7vUrNbXZ72k68l9PoN0on80GyfOVMJKbsdtZuwqDUKy4HH6PKSnL0VrD';
  var centerLocation = new Microsoft.Maps.Location(-16.504545, -68.130344);
  window.map = new Microsoft.Maps.Map(document.getElementById("mapDiv"),{
    credentials:key,
    showDashboard: true,
    useInertia: false,
    showMapTypeSelector:true,
    enableSearchLogo: false,
    mapTypeId: Microsoft.Maps.MapTypeId.road,
    zoom: 15,
    center: centerLocation
  });
  handleEvents();
}

function handleEvents() {
  Microsoft.Maps.Events.addHandler(window.map, 'dblclick', function(e) {
    if(e.targetType == 'map') {
      e.handled = true;
      if (window.doneReport) { return; }
      var point = new Microsoft.Maps.Point(e.getX(), e.getY());
      var location = e.target.tryPixelToLocation(point);

      var position = (location.latitude + '').substring(0, 8) + ', ' + (location.longitude + '').substring(0, 8);
      $('#position').val(position);
      $('#latitude').val(location.latitude);
      $('#longitude').val(location.longitude);

      window.doneReport = true;
      var type = $('#type').val();

      // Enable remove button
      $('#remove').attr('disabled', false);
      $('#submit-report').attr('disabled', false);

      addPushPin(window.map, location, {Type: type});
    }
  });
}
function getProblemByCode(code) {
  for(var i = 0; i<window.Problems.length; ++i) {
    if (window.Problems[i].Code == code) {
      return window.Problems[i];
    }
  }
  return null;
}
$(document).ready(function(){
  $('#remove').click(function(e) {
    $('#remove').attr('disabled', true);
    $('#submit-report').attr('disabled', true);
    window.map.entities.clear();
    $('#position').val('');
    window.doneReport = false;
  });
});
function centerMap(latitude, longitude) {
  window.map.setView({
    center: new Microsoft.Maps.Location(latitude, longitude)
  });
}
function renderProblems(type) {
  var problems = window.Problems;
  window.map.entities.clear();
  if (window.Problems.length === 0) {
    $('#problems').html('<div class="problem"><h4>No problems :D</h4>');
    return;
  }
  $('#problems').html('');
  var firstTime = true;
  for(var i = 0; i<problems.length; ++i) {
    if (type == 0 || problems[i].Type == type) {
      var location = {
        latitude: problems[i].Latitude,
        longitude: problems[i].Longitude
      };
      if (firstTime) {
        centerMap(location.latitude, location.longitude);
        firstTime = false;
      }
      addProblemDescription(problems[i]);
      addPushPin(window.map, location, problems[i], pushPinClicked);
    }
  }
}
function addProblemDescription(problem) {
  var time = timeStampToTime(problem.ReportedAt*1000);
  var value = '<div class="problem" id="'+ problem.Code + '">';
  value += '<h4>';
  value += window.types[problem.Type];
  value += '</h4>';
  value += '<b>Reporters: </b>' + problem.Importance + '<br>';
  value += '<b>Reported at: </b>' + time.hh + ':' + time.mm;

  $('#problems').append(value);
}
function pushPinClicked(e) {
  if (e.targetType == 'pushpin'){
    alert(e.target.Problem.Type);
  }
}