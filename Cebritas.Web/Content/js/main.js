(function ($) {
    $('.dropdown-toggle').dropdown();
    // Fix input element click problem
    $('.dropdown input, .dropdown label, .dropdown').click(function (e) {
        e.stopPropagation();
    });

    $('.metro-item').click(function (e) {
        $('.metro-item').removeClass('is-selected');
        $(e.currentTarget).addClass('is-selected');
    });
})(jQuery);