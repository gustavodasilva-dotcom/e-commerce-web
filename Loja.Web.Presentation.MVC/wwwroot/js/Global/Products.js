$(function () {

    const stars = document.querySelectorAll('.star');

    stars.forEach((star, i) => {
        star.onclick = function () {
            let currentStar = i + 1;

            let rate = SaveProductRating(currentStar);

            stars.forEach((star, j) => {
                if (currentStar >= j + 1) {
                    star.innerHTML = '&#9733';
                } else {
                    star.innerHTML = '&#9734';
                }

                if (rate != null) {
                    $('.total-rating').text(rate.totalRatings);
                }
            });
        }
    });
});

function SetProductRating(rate) {

    if (rate != null) {

        const stars = document.querySelectorAll('.star');

        stars.forEach((star, i) => {
            let currentRate = i + 1;

            if (Math.round(rate.rating) >= currentRate) {
                star.innerHTML = '&#9733';
            } else {
                star.innerHTML = '&#9734';
            }
        });

        $('.total-rating').text(rate.totalRatings);
    }
}

function SaveProductRating(rate) {

    let rating = null;

    let model = {};

    model.Rating = rate;
    model.ProductGuid = $('#product-guid').val();

    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        data: { model: model },
        url: "/Products/SaveProductRating",
        success: function (result) {
            if (result.Code == 1) {
                rating = result.Rating;

                if (result.RedirectToHome)
                    window.location.href = '/Home/Index';
            }
            else {
                if (result.RedirectToLogin) {
                    window.location.href = '/Accounts/Login';
                } else if (result.RedirectToHome) {
                    window.location.href = '/Home/Index';
                } else {
                    ShowModal(result.Message);
                    return;
                }
            }
        }
    });

    return rating;
};

function GetProductByID(guid) {

    let products = null;

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        data: { guid: guid },
        url: "/Products/Get",
        success: function (result) {
            if (result.Code == 1) {
                products = result.Products;

                if (result.RedirectToHome)
                    window.location.href = '/Home/Index';
            }
            else {
                if (result.RedirectToLogin) {
                    window.location.href = '/Accounts/Login';
                } else if (result.RedirectToHome) {
                    window.location.href = '/Home/Index';
                } else {
                    ShowModal(result.Message);
                    return;
                }
            }
        }
    });

    return products;
}

function GetProducts() {

    let products = null;

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Products/GetAll",
        success: function (result) {
            if (result.Code == 1) {
                products = result.Products;

                if (result.RedirectToHome)
                    window.location.href = '/Home/Index';
            }
            else {
                if (result.RedirectToLogin) {
                    window.location.href = '/Accounts/Login';
                } else if (result.RedirectToHome) {
                    window.location.href = '/Home/Index';
                } else {
                    ShowModal(result.Message);
                    return;
                }
            }
        }
    });

    return products;
}

function GetMostSolds() {

    let products = null;

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Products/GetMostSolds",
        success: function (result) {
            if (result.Code == 1) {
                products = result.Products;

                if (result.RedirectToHome)
                    window.location.href = '/Home/Index';
            }
            else {
                if (result.RedirectToLogin) {
                    window.location.href = '/Accounts/Login';
                } else if (result.RedirectToHome) {
                    window.location.href = '/Home/Index';
                } else {
                    ShowModal(result.Message);
                    return;
                }
            }
        }
    });

    return products;
}