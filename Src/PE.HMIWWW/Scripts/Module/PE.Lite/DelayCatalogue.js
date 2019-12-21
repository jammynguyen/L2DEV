RegisterMethod(HmiRefreshKeys.DelayCatalogue, RefreshData);

function RefreshData() {
    reloadKendoGrid();
}

function AddDelayCataloguePopup() {
    OpenInPopupWindow({
        controller: "DelaysCatalogue", method: "DelayCatalogueAddPopup", width: 600, data: {  }, afterClose: reloadKendoGrid
    });
}

function EditDelayCataloguePopup(id) {
        OpenInPopupWindow({
            controller: "DelaysCatalogue", method: "DelayCatalogueEditPopup", width: 600, data: { delayCatalogueId: id }, afterClose: reloadKendoGrid
        });
    }

function reloadKendoGrid() {
    let grid = $('#DelayCatalogueList').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();
}

function Delete(itemId) {
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
        let dataToSend = {
            itemId: itemId
        };
        let targetUrl = '/DelaysCatalogue/DeleteDelay';

        AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('deleteDelayCatalogue - failed'); });
    });
}