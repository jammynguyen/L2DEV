

//var selectedDefectsIds = [];

var CurrentElement;

$(document).ready(function () {

    const defects = getDefectsList();
    const qualities = GetProductQualityList();

    if (defects)
        drawDefectsList(defects);

    if (qualities) {
        //const enumObject = JSON.parse(qualities);
        drawEnumList(qualities);
    }

    setCheckBoxByCellClick();


    //let checkboxes = $("input[type='checkbox']");

    //checkboxes.click(function () {
    //    if ($(this).is(':checked')) {
    //        selectedDefectsIds.push(parseInt($(this)[0].id));
    //    } else {
    //        selectedDefectsIds = selectedDefectsIds.filter((el) => { return el != parseInt($(this)[0].id); });
    //    }
});

//$("#get").click(function () {
//    quality = $("#Quality").data("kendoDropDownList");

//});

function drawEnumList(enumObject) {

    const defaultSelectedKey = 0;

    const enumKeys = Object.keys(enumObject);
    const enumValues = Object.values(enumObject);

    const qualitiesForm = document.createElement('form');

    qualitiesForm.setAttribute("id", "qualitiesForm");

    for (let i in enumObject) {
        if (enumObject.hasOwnProperty(i)) {
            if (enumKeys[i] == defaultSelectedKey) {
                qualitiesForm.innerHTML += "<input type='radio' id='quality" + i + "' name='quality' value='" + enumKeys[i] + "' checked> <label class='quality-label' for='quality" + i + "'>" + enumValues[i] + "</label><br>";
            } else {
                qualitiesForm.innerHTML += "<input type='radio' id='quality" + i + "' name='quality' value='" + enumKeys[i] + "'> <label class='quality-label' for='quality" + i + "'>" + enumValues[i] + "</label><br>";
            }
        }
    }

    $('#enumList').append(qualitiesForm);

}


function getSelectedQuality() {
    return parseInt($('input[name=quality]:checked', '#qualitiesForm').val());
}

function getDefectsList(options) {
    //var url = '@Url.Action("GetDefectsList", "ProductQualityMgm")';
    //var result;
    //$.ajax({
    //    url: url,
    //    async: false,
    //    success: function (data) {
    //        if (options !== undefined) {
    //            options.success(data);
    //        }
    //        result = data.Data;
    //    }
    //});

    var data = AjaxGetDataHelper(URL("ProductQualityMgm", "GetDefectsList"));

    return data.Data;
}

function GetProductQualityList(options) {

    let data = AjaxGetDataHelper(URL("ProductQualityMgm", "GetProductQualities"));

    return data;
}

function setQuality(ProductId) {

    //quality = $("#Quality").data("kendoDropDownList");
    //let qualityId = quality.value();

    let qualityId = getSelectedQuality();
    let selectedDefectsIds = [];
    selectedDefectsIds = getSelectedDefects();

    let dataToSend = {
        productId: ProductId,
        quality: qualityId,
        defectIds: selectedDefectsIds
    };

    let targetUrl = '/ProductQualityMgm/AssignQualityAsync';

    AjaxReqestHelper(targetUrl, dataToSend, reloadKendoGrid, function () { console.log('set Product Quality - failed'); });
}

function drawDefectsList(defects) {
    const checkboxListContainer = $('#checkboxList');

    for (let i in defects) {

        let defect = '<div class="defect">' +
            '<div class="code cell">' + defects[i].DefectCatalogueCode + '</div>' +
            '<div class="checkbox cell"><input type="checkbox" id="' + defects[i].DefectCatalogueId + '" ></div>' +
            '</div>';

        checkboxListContainer.append(defect);
    }
}

function setCheckBoxByCellClick() {

    $(".checkbox").click(function () {
        console.log($(this).children());
        let checkbox = $(this).children();
        if (checkbox[0].checked) {
            checkbox.prop('checked', false);
        } else {
            checkbox.prop('checked', true);
        }
    });

    $("input[type='checkbox']").click(function () {
        let checkbox = $(this);
        if (checkbox[0].checked) {
            checkbox.prop('checked', false);
        } else {
            checkbox.prop('checked', true);
        }
    });

}

function getSelectedDefects() {
    const checkboxes = $("input[type='checkbox']");
    const selectedDefectsIds = [];

    checkboxes.each(function () {
        if ($(this)[0].checked)
            selectedDefectsIds.push(parseInt($(this)[0].id));
    });

    return selectedDefectsIds;
}

function reloadKendoGrid() {
    let grid = $('#ProductHistoryGrid').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();
}

//function GoToProductDetails(productId) {

//    CurrentElement = {
//        ProductId: productId
//    };

//    let dataToSend = {
//        ProductId: productId
//    };
//    openSlideScreen('Products', 'ElementDetails', dataToSend);
//}


function onSelect(e) {
    if ($(e.item).find(".k-link").text() === "Label") {
        let urlWithParameter = "/Products/GetProductsLabel?ProductId=" + CurrentElement.ProductId;
        $('#labelPicture').first().attr("src", urlWithParameter);
    }
}

function GoToMaterial(materialId) {
    let dataToSend = {
        MaterialId: materialId
    };
    openSlideScreen('Material', 'ElementDetails', dataToSend);
}