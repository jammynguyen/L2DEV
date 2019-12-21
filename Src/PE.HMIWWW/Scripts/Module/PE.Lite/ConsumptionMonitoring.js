let CurrentElement;

function onElementSelect(e) {
    $('#MeasurementDetails').addClass('loading-overlay');
    let grid = e.sender;
    let selectedRow = grid.select();
    let selectedItem = grid.dataItem(selectedRow);
    let dataToSend = {
        featureId: selectedItem.FeatureId
    };

    CurrentElement = {
        featureId: selectedItem.FeatureId
    };

    let url = "/ConsumptionMonitoring/GetFeatureDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
    $('#MeasurementDetails').removeClass('loading-overlay');
    $('#MeasurementDetails').html(partialView);
}

function selectRow() {
    const grid = $('#FeaturesSearchGrid').data("kendoGrid");
    let gridData = grid.dataSource.view();
    let id = getUrlParameter('featureId');
    //if (id != null) {
    for (let i = 0; i < gridData.length; i++) {
        let currentItem = gridData[i];
        if (currentItem.ProductId === id) {
            let currentRow = grid.table.find("tr[data-uid='" + currentItem.uid + "']");
            grid.select(currentRow);
            break;
        }
    }
    //}
}
