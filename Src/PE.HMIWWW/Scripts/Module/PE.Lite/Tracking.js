let CurrentElement;
let CurrentElementWO;
let CurrentElementH;
let CurrentElementP;

function onElementSelect(e) {
    $('#TrackingDetails').addClass('loading-overlay');
    var grid = e.sender;
    var selectedRow = grid.select();
    var selectedItem = grid.dataItem(selectedRow);
    var dataToSend = {
        DimRawMaterialKey: selectedItem.DimRawMaterialKey,
        workOrderId: selectedItem.DimWorkOrderKey,
        heatId: selectedItem.DimHeatKey,
        productId: selectedItem.DimProductKey
    };

    CurrentElement = {
        DimRawMaterialKey: selectedItem.DimRawMaterialKey
    };

    CurrentElementWO = {
        workOrderId: selectedItem.DimWorkOrderKey
    };

    CurrentElementH = {
        heatId: selectedItem.DimHeatKey
    };

    var url = "/Tracking/ElementDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
    $('#TrackingDetails').removeClass('loading-overlay');
    $('#TrackingDetails').html(partialView);
    setTabStripsStates();
}

function GoToDetails(DimRawMaterialKey, DimWorkOrderKey, DimHeatKey) {
    let dataToSend = {
        DimRawMaterialKey: DimRawMaterialKey,
        workOrderId: DimWorkOrderKey,
        heatId: DimHeatKey,
    };
    openSlideScreen('Tracking', 'ElementDetails', dataToSend);
}