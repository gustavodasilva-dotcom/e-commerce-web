function ShowMessageError(message) {
    $('.register-alert').show();
    $('.register-alert p').text(message);
}

function SetReadMore() {
    $('.btn-read-more').text('Read more');
    $('#dot').css('display', 'inline');
    $('#more').css('display', 'none');
}

function DisableButton(buttonID, disable) {
    if (disable) {
        $('#' + buttonID).prop('disabled', true);

        $('#' + buttonID).css('background-color', '#B8C5C6');
        $('#' + buttonID).css('font-weight', 'normal');
    }
}

function SetComboOptions(model, elementID) {
    $.each(model, function (i, item) {
        $('#' + elementID).append(`<option value="${model[i].guidID}">${model[i].name}</option>`);
    });
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