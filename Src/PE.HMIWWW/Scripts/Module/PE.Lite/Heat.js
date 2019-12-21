let CurrentElement;

var columns = ["SteelgradeCode", "SteelGradeDensity", "MaterialCatalogueName", "HeatWeightRef", "IsDummy", "CreatedTs"];
var button_array = $('.arrow-categories');

function onElementSelect(e) {
    hideCategories();

    if ($('.k-i-arrow-left').length) {
        button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
        //button.toggleClass('off on');
        $('.more').show(100);
        $('.less').hide(100);
    }


    $('#HeatDetails').addClass('loading-overlay');
    var grid = e.sender;
    var selectedRow = grid.select();
    var selectedItem = grid.dataItem(selectedRow);
    var dataToSend = {
        HeatId: selectedItem.HeatId
    };

    CurrentElement = {
        HeatId: selectedItem.HeatId
    };

    var url = "/Heat/ElementDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
    saveGridState(this);
}

function setElementDetailsPartialView(partialView) {
    $('#HeatDetails').removeClass('loading-overlay');
    $('#HeatDetails').html(partialView);
    setTabStripsStates();
}

function GoToMaterial(materialId) {
    let dataToSend = {
        MaterialId: materialId
    };
    openSlideScreen('Material', 'ElementDetails', dataToSend);
}

function GoToWorkOrder(workOrderId) {
    let dataToSend = {
        WorkOrderId: workOrderId
    };
    openSlideScreen('WorkOrder', 'ElementDetails', dataToSend);
}

function AddNew() {
    OpenInPopupWindow({
        controller: "Heat", method: "HeatCreatePopup", width: 1250, afterClose: reloadKendoGrid
    });
}

function reloadKendoGrid() {
    let grid = $('#SearchGrid').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();
}

function onAdditionalData() {
    return {
        text: $("#FKMaterialCatalogueId").val()
    };
}