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

function getMap() {
    var key = 'ApC8X7yE7vUrNbXZ72k68l9PoN0on80GyfOVMJKbsdtZuwqDUKy4HH6PKSnL0VrD';
    var centerLocation = new Microsoft.Maps.Location(25.30623, -25.6006);
    window.map = new Microsoft.Maps.Map(document.getElementById("mapDiv"), {
        credentials: key,
        showDashboard: true,
        useInertia: false,
        showMapTypeSelector: true,
        enableSearchLogo: false,
        mapTypeId: Microsoft.Maps.MapTypeId.road,
        zoom: 2,
        center: centerLocation
    });
    $.ajax({
        url: "api/problems/getall?timezone=" + window.timeZone,
        cache: false,
        success: loadProblems,
        error: function () { if (console) console.log('there was a problemo :P'); }
    });
    setInterval(function () {
        console.log('Loading problems by interval');
        $.ajax({
            url: "api/problems/getall?timezone=" + window.timeZone,
            cache: false,
            success: loadProblems,
            error: function () { alert('there was a problemo :P'); }
        });
    }, 120000);
}

function getProblemByCode(code) {
    for (var i = 0; i < window.Problems.length; ++i) {
        if (window.Problems[i].Code == code) {
            return window.Problems[i];
        }
    }
    return null;
}
$(document).ready(function () {
    // Add pushpin
    $(document).on('click', '.problem', function (e) {
        var problem = getProblemByCode(e.currentTarget.id);
        if (problem !== null) {
            console.log('Trying to center map at: ' + problem.Latitude + ", " + problem.Longitude);
            centerMap(problem.Latitude, problem.Longitude);
        }
    });
    $('#filter').change(function (e) {
        console.log('filtering by type ...');
        var type = $('#filter').val();
        renderProblems(type);
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
    for (var i = 0; i < problems.length; ++i) {
        if (type == 0 || problems[i].Type == type) {
            var location = {
                latitude: problems[i].Latitude,
                longitude: problems[i].Longitude
            };
            addProblemDescription(problems[i]);
            addPushPin(window.map, location, problems[i], pushPinClicked);
        }
    }
}
function loadProblems(data, status, xhr) {
    $('#filter').val('0');
    var problems = data.Data;
    window.Problems = problems;
    renderProblems(0);
}
function addProblemDescription(problem) {
    var time = timeStampToTime(problem.ReportedAt * 1000);
    var value = '<div class="problem" id="' + problem.Code + '">';
    value += '<h4>';
    value += window.types[problem.Type];
    value += '</h4>';
    value += '<b>Reporters: </b>' + problem.Importance + '<br>';
    value += '<b>Reported at: </b>' + time.hh + ':' + time.mm;

    $('#problems').append(value);
}
function pushPinClicked(e) {
    if (e.targetType == 'pushpin') {
        if (e.target.Problem) {
            var problem = e.target.Problem;
            var time = timeStampToTime(problem.ReportedAt * 1000);
            $('#model-problem-type').html(window.types[problem.Type]);
            $('#modal-problem-importance').html(problem.Importance);
            $('#modal-problem-reportedat').html(time.hh + ':' + time.mm);
            $('#modal-problem-description').html(problem.Description);
            $('#information').modal('show');
        }
    }
}