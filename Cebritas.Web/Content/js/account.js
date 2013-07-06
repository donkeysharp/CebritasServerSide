var Cebritas = {};
(function ($) {
    $(document).ready(function () {
        Cebritas.tableMapping =  {"Email": true,"Name": true,"Country": true,"TimeZone": true};
        Cebritas.rowUser = {};
        $.ajax({
            type: 'GET',
            url: 'getusers'
        }).done(usersLoaded);
        $('#users').on('click', '.table-row', function(e) {
            displayUser(Cebritas.rowUser[e.currentTarget.id]);
        });
    });
    /* Load users in a table */
    function usersLoaded(data, textStatus, xhr) {
        var columns = $('#users').find('thead th').length;
        var table = $('#users > tbody');
        for (var i = 0; i < data.length; ++i) {
            var temp = {};
            temp[data[i].Code] = data[i];
            Cebritas.rowUser[data[i].Code] = data[i];

            table.append(getRow(data[i], data[i].Code));
        }
    }
    function displayUser(user) {
        $('#new-user').modal('show');
    }
})(jQuery);