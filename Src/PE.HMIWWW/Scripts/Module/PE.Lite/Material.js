let CurrentElement;

var columns = ["LastUpdateTs", "IsDummy",
    "IsAssigned", "WorkOrderName", "HeatName"];

var button_array = $('.arrow-categories');

function onElementSelect(e) {
    hideCategories();

    if ($('.k-i-arrow-left').length) {
        button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
        $('.more').show(100);
        $('.less').hide(100);
    }

    $('#MaterialDetails').addClass('loading-overlay');
    let grid = e.sender;
    let selectedRow = grid.select();
    let selectedItem = grid.dataItem(selectedRow);
    let dataToSend = {
        MaterialId: selectedItem.MaterialId
    };

    CurrentElement = {
        MaterialId: selectedItem.MaterialId
    };

    let url = "/Material/ElementDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
    $('#MaterialDetails').removeClass('loading-overlay');
    $('#MaterialDetails').html(partialView);
}

function selectRow() {
    var grid = $('#SearchGrid').data("kendoGrid");
    var gridData = grid.dataSource.view();
    var id = getUrlParameter('materialId');
    //if (id != null) {
    for (var i = 0; i < gridData.length; i++) {
        var currentItem = gridData[i];
        if (currentItem.MaterialId === id) {
            var currenRow = grid.table.find("tr[data-uid='" + currentItem.uid + "']");
            grid.select(currenRow);
            break;
        }
    }
    //}
}

function EditWorkOrder() {
    try {
        OpenInPopupWindow({
            controller: "WorkOrder", method: "WorkOrderEditPopup", width: 1250, data: { id: CurrentElement.MaterialId, byMaterial: true }, afterClose: reloadKendoGrid
        });
        PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_MaterialScrap"], scrapMaterial);
    } catch (e) {
        if (e instanceof TypeError) {
            WarningMessage(Translations["MESSAGE_SelectMaterial"]);
        }

    }
}

function dataBoundHandler() {
    selectRowAfterBack(this);
}

function reloadKendoGrid() {
    let grid = $('#SearchGrid').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();
}