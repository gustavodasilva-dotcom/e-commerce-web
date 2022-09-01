﻿$('#logout-button').click(function () {
    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        url: "/Accounts/Logout",
        success: function (result) {
            if (result.Code == 1) {
                window.location.href = '/Home/Index';
            }
            else {
                alert(result.Message);
            }
        }
    });
});

function ShowMessageError(message) {
    $('.register-alert').show();
    $('.register-alert p').text(message);
}

function SetReadMore() {
    $('.btn-read-more').text('Read more');
    $('#dot').css('display', 'inline');
    $('#more').css('display', 'none');
}

$('.btn-read-more').click(function () {
    if ($('.btn-read-more').text() === 'Read more') {
        $('.btn-read-more').text('Read less');
        $('#dot').css('display', 'none');
        $('#more').css('display', 'inline');
    } else {
        $('.btn-read-more').text('Read more');
        $('#dot').css('display', 'inline');
        $('#more').css('display', 'none');
    }
});