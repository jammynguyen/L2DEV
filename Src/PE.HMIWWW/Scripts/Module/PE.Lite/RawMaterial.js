let CurrentElement;
let materialsToAssign;

var columns = ["CreatedTs", "LastUpdateTs"];
var button_array = $('.arrow-categories');

RegisterMethod(HmiRefreshKeys.RawMaterialDetails, RefreshData);

//THIS METHOD WILL BE CALLED BY SYSTEM (SERVER) IN CASE DATA CHANGE, NAME IS IMPORTANT !!!
function RefreshData() {
    let url = "/RawMaterial/ElementDetails";
    AjaxReqestHelperSilentWithoutDataType(url, CurrentElement.Id, setElementDetailsPartialView);
    $("#SearchGrid").data("kendoGrid").dataSource.read();
    $("#SearchGrid").data("kendoGrid").refresh();
}


function onElementSelect(e) {
    hideCategories();

    if ($('.k-i-arrow-left').length) {
        button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
        $('.more').show(100);
        $('.less').hide(100);
    }

    $('#RawMaterialDetails').addClass('loading-overlay');
    let grid = e.sender;
    let selectedRow = grid.select();
    let selectedItem = grid.dataItem(selectedRow);
    let dataToSend = {
        RawMaterialId: selectedItem.Id
    };

    CurrentElement = {
        RawMaterialId: selectedItem.Id,
        RawMaterialName: selectedItem.RawMaterialName
    };

    let url = "/RawMaterial/ElementDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
    $('#RawMaterialDetails').html(partialView);
    $('#RawMaterialDetails').removeClass('loading-overlay');
}

function colorEmptyL3MaterialRow() {
    var grid = $("#SearchGrid").data("kendoGrid");
    var data = grid.dataSource.data();
    $.each(data, function (i, row) {
        if (row.MaterialName == null) {
            $('tr[data-uid="' + row.Id + '"] ').css({ "background-color": "#f95554", "color": "#fff" });
        }
    });
}

function onMaterialToAssignSelect(e) {
    $('#RawMaterialDetails').addClass('loading-overlay');

    let grid = e.sender;
    let selectedRow = grid.select();
    let selectedItem = grid.dataItem(selectedRow);
    materialsToAssign = {
        RawMaterialId: CurrentElement.RawMaterialId,
        L3MaterialId: selectedItem.MaterialId,
    };

    let assignMaterials = function (callback) {
        if (callback === false) {
            $('#RawMaterialDetails').removeClass('loading-overlay');
        } else {
            let url = "/RawMaterial/AssignMaterials";
            AjaxReqestHelperSilentWithoutDataType(url, materialsToAssign, setElementDetailsPartialView);
            $("#SearchGrid").data("kendoGrid").dataSource.read();
            $("#SearchGrid").data("kendoGrid").refresh();
        }
    };

    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_MaterialAssign"], assignMaterials);
}

function onUnassignBtnEvent() {
    $('#RawMaterialDetails').addClass('loading-overlay');

    let unassignMaterial = function (callback) {
        if (callback === false) {
            $('#RawMaterialDetails').removeClass('loading-overlay');
        } else {
            let url = "/RawMaterial/UnassignMaterial";
            dataToSend = {
                RawMaterialId: CurrentElement.RawMaterialId
            };
            AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
            $("#SearchGrid").data("kendoGrid").dataSource.read();
            $("#SearchGrid").data("kendoGrid").refresh();
        }
    };

    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_MaterialUnassign"], unassignMaterial);

}

function onScrapBtnEvent() {
    $('#RawMaterialDetails').addClass('loading-overlay');

    let scrapMaterial = function (callback) {
        if (callback === false) {
            $('#RawMaterialDetails').removeClass('loading-overlay');
        } else {
            let url = "/RawMaterial/MarkMaterialAsScrap";
            dataToSend = {
                RawMaterialId: CurrentElement.RawMaterialId
            };
            AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
            $("#SearchGrid").data("kendoGrid").dataSource.read();
            $("#SearchGrid").data("kendoGrid").refresh();
        }
    };

    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_MaterialScrap"], scrapMaterial);

}

function GoToMeasurement(MeasurementId) {
    let dataToSend = {
        measurementId: MeasurementId
    };
    openSlideScreen('RawMaterial', 'MeasurementDetails', dataToSend);
}

function GoToHistory(RawMaterialStepId) {
    let dataToSend = {
        rawMaterialStepId: RawMaterialStepId
    };
    openSlideScreen('RawMaterial', 'HistoryDetails', dataToSend);
}