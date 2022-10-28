let guidID;
let isEdit = false;
let imagesIDs = [];

$(document).ready(function () {

    CheckRoute();

    SetMasks();

    GetListManufacturers();
    GetListSubcategories();
    GetListCurrencies();
    GetListMeasurements();

    if (isEdit) {
        let products = GetProductByID(guidID);
        SetDetails(products);
    }

    SetElementsVisibility(isEdit);

});

$('#edit').click(function () {

    let visible = false;

    if (document.querySelector('.register-input').disabled)
        visible = false;
    else
        visible = true;

    SetElementsVisibility(visible);
});

$('.register-btn-submit').click(function () {

    if (imagesIDs.length == 0 && $('input[type="file"]').get(0).files.length > 0) {
        imagesIDs = UploadImages();

        if (imagesIDs.length != 6) {
            ShowModal("There has to be exactly 6 files to upload.");
            return;
        }
    }

    let productModel = {};

    if (isEdit) productModel.GuidID = guidID

    productModel.Name = $('#register-input-name').val();

    productModel.Description = $('#register-input-description').val();

    productModel.Price = parseFloatToBackEnd($('#register-input-price').val());

    productModel.CurrencyGuid = $('#register-select-currencies').val();

    productModel.Discount = $('#register-input-discount').val();

    productModel.SubcategoryGuid = $('#register-select-subcategories').val();

    productModel.ManufacturerGuid = $('#register-select-manufacturers').val();

    productModel.Weight = parseFloatToBackEnd($('#register-input-weight').val());
    productModel.WeightGuid = $('#register-select-mass-measurements').val();

    productModel.Height = parseFloatToBackEnd($('#register-input-height').val());
    productModel.HeightGuid = $('#register-select-height-measurements').val();

    productModel.Width = parseFloatToBackEnd($('#register-input-width').val());
    productModel.WidthGuid = $('#register-select-width-measurements').val();

    productModel.Length = parseFloatToBackEnd($('#register-input-length').val());
    productModel.LengthGuid = $('#register-select-length-measurements').val();

    productModel.Stock = parseInt($('#register-input-stock').val());

    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        url: "/Products/Save",
        data: { model: productModel },
        success: function (result) {
            if (result.Code == 1) {
                if (imagesIDs.length == 0 && $('input[type="file"]').get(0).files.length > 0)
                    InsertProductsImages(result.Products.id, imagesIDs);

                window.location.href = '/Products/Details?guidID=' + result.Products.guidID;
            }
            else {
                ShowModal(result.Message);
            }
        }
    });
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

function GetListManufacturers() {

    let manufacturers = GetManufacturers();

    if (manufacturers != null) SetComboOptions(manufacturers, 'register-select-manufacturers');
}

function GetListSubcategories() {

    let subcategories = GetSubcategories();

    if (subcategories != null) SetComboOptions(subcategories, 'register-select-subcategories');
}

function GetListCurrencies() {

    let currencies = GetCurrencies();

    if (currencies != null) SetComboOptions(currencies, 'register-select-currencies');
}

function GetListMeasurements() {

    let measurements = GetMeasurements();

    if (measurements != null) SetComboBoxMeasurements(measurements);
}

function SetComboBoxMeasurements(measurements) {

    var mass = measurements.filter(function (value) { return value.measurementTypeID == 1 });
    var height = measurements.filter(function (value) { return value.measurementTypeID == 2 });

    $.each(height, function (i, item) {
        $('#register-select-height-measurements').append(`<option value="${height[i].guidID}">${CapitalizeFirstLetter(height[i].name)}</option>`);
        $('#register-select-width-measurements').append(`<option value="${height[i].guidID}">${CapitalizeFirstLetter(height[i].name)}</option>`);
        $('#register-select-length-measurements').append(`<option value="${height[i].guidID}">${CapitalizeFirstLetter(height[i].name)}</option>`);
    });

    $.each(mass, function (i, item) {
        $('#register-select-mass-measurements').append(`<option value="${mass[i].guidID}">${CapitalizeFirstLetter(mass[i].name)}</option>`);
    });
}

function SetElementsVisibility(able) {

    $('#btn-edit').css('display', !able ? 'none' : 'block');

    $('#register-input-name').prop('disabled', able);
    $('#register-input-description').prop('disabled', able);

    $('#register-select-subcategories').prop('disabled', able);
    $('#register-select-manufacturers').prop('disabled', able);
    $('#register-select-currencies').prop('disabled', able);

    $('#register-input-price').prop('disabled', able);
    $('#register-input-discount').prop('disabled', able);

    $('#register-select-mass-measurements').prop('disabled', able);
    $('#register-input-weight').prop('disabled', able);

    $('#register-select-height-measurements').prop('disabled', able);
    $('#register-input-height').prop('disabled', able);

    $('#register-select-width-measurements').prop('disabled', able);
    $('#register-input-width').prop('disabled', able);

    $('#register-select-length-measurements').prop('disabled', able);
    $('#register-input-length').prop('disabled', able);

    $('#register-input-stock').prop('disabled', able);

    $('#register-input-file').prop('disabled', able);

    $('#btn-register-update').css('display', able ? 'none' : 'block');

    if (isEdit) $('#btn-register-update').text('Update');

}

function SetDetails(product) {

    $('#register-input-name').val(product.name);
    $('#register-input-description').text(product.description);
    $('#register-input-price').val(product.price).trigger('mask.maskMoney');
    $('#register-input-discount').val(product.discount);

    if (product.weight != null) {
        $('#register-input-weight').val(product.weight.value);
        $('#register-select-mass-measurements').val(product.weight.guidID);
    }

    if (product.height != null) {
        $('#register-input-height').val(product.height.value);
        $('#register-select-height-measurements').val(product.height.guidID);
    }

    if (product.width != null) {
        $('#register-input-width').val(product.width.value);
        $('#register-select-width-measurements').val(product.width.guidID);
    }

    if (product.length != null) {
        $('#register-input-length').val(product.length.value);
        $('#register-select-length-measurements').val(product.length.guidID);
    }

    $('#register-input-stock').val(product.stock);

    $('#register-select-manufacturers').val(product.manufacturer.guidID);
    $('#register-select-subcategories').val(product.subcategory.guidID);
    $('#register-select-currencies').val(product.currency.guidID);

}