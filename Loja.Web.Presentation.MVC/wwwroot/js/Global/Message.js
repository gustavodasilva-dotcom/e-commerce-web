function ShowMessageDiv(message) {
    $('#div_Message').hide();
    $('#div_Message').show();
    $('.modal-content p').text(message);
}

$('.close').click(function () {
    $('#div_Message').hide();
    $(document.body).css('overflow', 'auto');
});