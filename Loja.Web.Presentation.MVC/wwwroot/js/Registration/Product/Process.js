let guidID;
let isEdit = false;

$(document).ready(function () {
    CheckRoute();

    SetMasks();

    GetManufacturers();
    GetSubcategories();
    GetCurrencies();
    GetMeasurements();

    if (isEdit) GetProductDetails(guidID);
});

function CheckRoute() {
    var route = window.location.href;
    var params = new URL(route).searchParams;
    var edit = params.get('edit');
    guidID = params.get('guidID');

    if (edit == null || edit == '') window.location.href = '/Home';
    if (edit !== '0' && edit !== '1') window.location.href = '/Home';
    if (edit === '1' && (guidID == null || guidID == '')) window.location.href = '/Home';

    isEdit = edit == '1' ? true : false;
}

function SetMasks() {
    $('#register-input-price').maskMoney();
    $('#register-input-weight').mask("#0.000", { reverse: true });
    $('#register-input-height').mask("#0.000", { reverse: true });
    $('#register-input-width').mask("#0.000", { reverse: true });
    $('#register-input-length').mask("#0.000", { reverse: true });
}

function GetManufacturers() {
    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Manufacturers/Get",
        success: function (result) {
            if (result.Code == 1) {
                SetComboBoxManufacturers(result.Manufacturers);
            }
            else {
                ShowMessageError(result.Message);
            }
        }
    });
}

function GetSubcategories() {
    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Subcategories/Get",
        success: function (result) {
            if (result.Code == 1) {
                SetComboBoxSubcategories(result.Subcategories);
            }
            else {
                ShowMessageError(result.Message);
            }
        }
    });
}

function GetCurrencies() {
    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Currencies/Get",
        success: function (result) {
            if (result.Code == 1) {
                SetComboBoxCurrencies(result.Currencies);
            }
            else {
                ShowMessageError(result.Message);
            }
        }
    });
}

function GetMeasurements() {
    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Measurements/Get",
        success: function (result) {
            if (result.Code == 1) {
                SetComboBoxMeasurements(result.Measurements);
            }
            else {
                ShowMessageError(result.Message);
            }
        }
    });
}

function GetProductDetails(guidID) {
    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Products/GetDetails",
        data: { productID: guidID },
        success: function (result) {
            if (result.Code == 1) {
                SetDetails(result.Product);
            }
            else {
                ShowMessageError(result.Message);
            }
        }
    });
}

$('.register-btn-submit').click(function () {
    let productModel = {};

    if (isEdit) productModel.GuidID = guidID

    productModel.Name = $('#register-input-name').val();

    productModel.Description = $('#register-input-description').val();

    productModel.Price = $('#register-input-price').val();

    productModel.CurrencyID = parseInt($('#register-select-currencies').val());

    productModel.Discount = parseInt($('#register-input-discount').val());

    productModel.SubcategoryID = parseInt($('#register-select-subcategories').val());

    productModel.ManufacturerID = parseInt($('#register-select-manufacturers').val());

    productModel.Weight = $('#register-input-weight').val();
    productModel.WeightMeasurementTypeID = parseInt($('#register-select-mass-measurements').val());

    productModel.Height = $('#register-input-height').val();
    productModel.HeightMeasurementTypeID = parseInt($('#register-select-height-measurements').val());

    productModel.Width = $('#register-input-width').val();
    productModel.WidthMeasurementTypeID = parseInt($('#register-select-width-measurements').val());

    productModel.Length = $('#register-input-length').val();
    productModel.LengthMeasurementTypeID = parseInt($('#register-select-length-measurements').val());

    productModel.Stock = parseInt($('#register-input-stock').val());

    productModel.IsEdit = isEdit;

    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        url: "/Products/Process",
        data: { model: productModel },
        success: function (result) {
            if (result.Code == 1) {
                window.location.href = '/Products/Details?guidID=' + result.GuidID;
            }
            else {
                ShowMessageError(result.Message);
            }
        }
    });
});

function SetComboBoxManufacturers(manufacturers) {
    $.each(manufacturers, function (i, item) {
        $('#register-select-manufacturers').append(`<option value="${manufacturers[i].id}">${manufacturers[i].name}</option>`);
    });
}

function SetComboBoxSubcategories(subcategories) {
    $.each(subcategories, function (i, item) {
        $('#register-select-subcategories').append(`<option value="${subcategories[i].id}">${subcategories[i].name}</option>`);
    });
}

function SetComboBoxCurrencies(currencies) {
    $.each(currencies, function (i, item) {
        $('#register-select-currencies').append(`<option value="${currencies[i].id}">${currencies[i].name}</option>`);
    });
}

function SetComboBoxMeasurements(measurements) {
    var mass = measurements.filter(function (value) { return value.measurementTypeID == 1 });
    var height = measurements.filter(function (value) { return value.measurementTypeID == 2 });

    $.each(height, function (i, item) {
        $('#register-select-height-measurements').append(`<option value="${height[i].id}">${height[i].name}</option>`);
        $('#register-select-width-measurements').append(`<option value="${height[i].id}">${height[i].name}</option>`);
        $('#register-select-length-measurements').append(`<option value="${height[i].id}">${height[i].name}</option>`);
    });

    $.each(mass, function (i, item) {
        $('#register-select-mass-measurements').append(`<option value="${mass[i].id}">${mass[i].name}</option>`);
    });
}

function SetDetails(product) {
    $('#register-input-name').val(product.name);
    $('#register-input-description').text(product.description);
    $('#register-input-price').val(product.price).trigger('mask.maskMoney');
    $('#register-input-discount').val(product.discount);
    $('#register-input-weight').val(product.weight);
    $('#register-input-height').val(product.height);
    $('#register-input-width').val(product.width);
    $('#register-input-length').val(product.length);
    $('#register-input-stock').val(product.stock);

    $('#register-select-manufacturers').val(product.manufacturerID);
    $('#register-select-subcategories').val(product.subcategoryID);
    $('#register-select-currencies').val(product.currencyID);

    $('#register-select-height-measurements').val(product.heightMeasurementTypeID);
    $('#register-select-width-measurements').val(product.widthMeasurementTypeID);
    $('#register-select-length-measurements').val(product.lengthMeasurementTypeID);
    $('#register-select-mass-measurements').val(product.weightMeasurementTypeID);
}