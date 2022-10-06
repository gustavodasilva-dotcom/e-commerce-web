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

function parseFloatToBackEnd(value) {

    value = parseFloat(value);
    value = '' + value;
    return value.replace('.', ',');
}

$('.btn-read-more').click(function () {

    if ($('.btn-read-more').text() === 'Read more') {

        $('.btn-read-more').text('Read less');
        $('#dots').css('display', 'none');
        $('#more').css('display', 'inline');
    } else {

        $('.btn-read-more').text('Read more');
        $('#dots').css('display', 'inline');
        $('#more').css('display', 'none');
    }
});

$('.profile').click(function () {

    if ($('.menu').css('visibility') == 'hidden') {

        $('.menu').css('visibility', 'visible');
        $('.menu').css('opacity', '1');
    } else {

        $('.menu').css('visibility', 'hidden');
        $('.menu').css('opacity', '0');
    }
});