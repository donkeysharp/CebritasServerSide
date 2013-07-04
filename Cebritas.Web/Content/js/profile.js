(function ($) {
    $(document).ready(function () {
        // alert('profile');
        $('#update-password').submit(function (e) {
            var oldPassword = $('#new-password').val().trim();
            var newPassword = $('#new-password').val().trim();
            var verifyPassword = $('#verify-password').val().trim();
            if (oldPassword.length === 0 || newPassword.length === 0 || verifyPassword.length === 0) {
                e.preventDefault();
                $(".alert").alert();
                $('#error-password').show('fast');
            }
            if (newPassword !== verifyPassword) {
                e.preventDefault();
                $(".alert").alert();
                $('#error-password').show('fast');
            }
        });
        $('#error-password').bind('close', function (e) {
            e.preventDefault();
            $('#error-password').hide();
        });
    });
})(jQuery);