RegisterMethod(HmiRefreshKeys.DefectCatalogue, RefreshData);

function RefreshData() {
    reloadKendoGrid();
}

function AddDefectCataloguePopup() {
        OpenInPopupWindow({
            controller: "DefectCatalogue", method: "DefectCatalogueAddPopup", width: 600, data: {}, afterClose: reloadKendoGrid
        });
    }

function EditDefectCataloguePopup(id) {
        OpenInPopupWindow({
            controller: "DefectCatalogue", method: "DefectCatalogueEditPopup", width: 600, data: { id: id }, afterClose: reloadKendoGrid
        });
    }

function reloadKendoGrid() {
    let grid = $('#DefectCatalogueList').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();
}

function Delete(itemId) {
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
        let dataToSend = {
            itemId: itemId
        };
        let targetUrl = '/DefectCatalogue/DeleteDefect';

        AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('deleteDefect - failed'); });
    });
}