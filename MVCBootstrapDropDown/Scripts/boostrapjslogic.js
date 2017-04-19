$(document).ready(function()
{
    $('#CountryList').attr('data-live-search', true);
    
    $('.selectCountry').selectpicker(
        {
            width:'100%',
            title:'-[Choose Country] -',
            style:'[btn-warning]',
            size: 6
        })
});