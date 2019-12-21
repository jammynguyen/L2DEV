RegisterMethod(HmiRefreshKeys.Steelgrade, RefreshData);

function RefreshData() {
    reloadKendoGrid();
}

function EditSteelgradePopup(id) {
    OpenInPopupWindow({
        controller: "Steelgrade", method: "SteelgradeEditPopup", width: 1250, data: { steelgradeId: id }, afterClose: reloadKendoGrid
    });
}

function CreateSteelgradePopup() {
    OpenInPopupWindow({
        controller: "Steelgrade", method: "SteelgradeCreatePopup", width: 1250, data: { }, afterClose: reloadKendoGrid
    });
}

function reloadKendoGrid() {
    let grid = $('#SteelgradeList').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();
}

function GoToDetails(Id) {
    let dataToSend = {
        Id: Id
    };
    openSlideScreen('Steelgrade', 'ElementDetails', dataToSend);
}

function Delete(itemId) {
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
        let dataToSend = {
            itemId: itemId
        };
        let targetUrl = '/Steelgrade/DeleteSteelgrade';

        AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('deleteSteelgrade - failed'); });
    });
}
