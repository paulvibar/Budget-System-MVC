$(document).ready(function () {
    // Enable Live Search.  
    $('.dropdown').attr('data-live-search', true);

    $('.dropdown').selectpicker(
        {
            width: '100%',
            style: 'btn-warning',
            size: 6
        });
});  