var Cebritas = {};
(function ($) {
    $(document).ready(function () {

        Cebritas.tableMapping =  {"Email": true,"Name": true,"Country": true,"TimeZone": true, "RoleName": true};
        Cebritas.rowUser = {};
        $.ajax({
            type: 'GET',
            url: 'getusers'
        }).done(usersLoaded);
        $.ajax({
            type: 'GET',
            url: 'getregionalinfo'
        }).done(renderDropdown);
        $('#new-error').bind('close', function (e) {
            e.preventDefault();
            $('#new-error').hide();
        });
        $('#update-error').bind('close', function (e) {
            e.preventDefault();
            $('#update-error').hide();
        });

        $('#users-table').on('click', '.table-row', function(e) {
            showUpdateUserModel(Cebritas.rowUser[e.currentTarget.id]);
        });
        $('#new-user').click(function(e) {
            showNewUserModal();
        });
        $('#create-user-form').submit(createNewUser);
        $('#update-user-form').submit(updateUser);
    });
    function renderDropdown(data, textStatus, xhr) {
        for(var i = 0; i<data.timezones.length; ++i) {
            var item = data.timezones[i];
            var option = '<option value="' + item.Id + '">' + item.Name + "</option>";
            $('#new-timezone').append(option);
            $('#update-timezone').append(option);
        }
        for(var i = 0; i<data.countries.length; ++i) {
            var item = data.countries[i];
            var option = '<option value="' + item.Code + '">' + item.Name + "</option>";
            $('#new-country').append(option);
            $('#update-country').append(option);
        }
    }
    /* Load users in a table */
    function usersLoaded(data, textStatus, xhr) {
        var columns = $('#users-table').find('thead th').length;
        var table = $('#users-table > tbody');
        for (var i = 0; i < data.length; ++i) {
            var temp = {};
            temp[data[i].Code] = data[i];
            Cebritas.rowUser[data[i].Code] = data[i];

            table.append(getRow(data[i], data[i].Code));
        }
    }
    function showNewUserModal() {
        $('#new-user-modal').modal('show');
    }
    function showUpdateUserModel(user) {
        $('#update-code').val(user.Code);
        $('#update-email').html(user.Email);
        $('#update-name').val(user.Name);
        $('#update-information').val(user.Description);
        $('#update-role').get(0).value = user.Role;
        $('#update-country').val(user.Country);
        $('#update-timezone').val(user.TimeZoneId);

        $('#update-user-modal').modal('show');
    }
    function createNewUser(e) {
        if (this.Name.value === '' || this.Email.value === '' || this.Password.value === '' || this.VerifyPassword.value === '') {
            e.preventDefault();
            $(".alert").alert();
            $('#new-error').show('fast');
            return;
        }
        if (this.Password.value !== this.VerifyPassword.value) {
            e.preventDefault();
            $(".alert").alert();
            $('#new-error').show('fast');
            return;
        }
    }
    function updateUser(e) {
        if (this.Name.value === '') {
            e.preventDefault();
            $(".alert").alert();
            $('#update-error').show('fast');
            return;
        }
        if (this.Password.value !== this.VerifyPassword.value) {
            e.preventDefault();
            $(".alert").alert();
            $('#update-error').show('fast');
            return;
        }
    }
})(jQuery);