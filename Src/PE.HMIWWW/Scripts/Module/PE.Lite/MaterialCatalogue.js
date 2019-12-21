RegisterMethod(HmiRefreshKeys.MaterialCatalogue, RefreshData);

function RefreshData() {
    reloadKendoGrid();
}

function EditMaterialCataloguePopup(id) {
    OpenInPopupWindow({
        controller: "BilletCatalogue", method: "EditMaterialCataloguePopup", width: 600, data: { id: id }, afterClose: reloadKendoGrid
    })
}

function CreateMaterialCataloguePopup(id) {
    OpenInPopupWindow({
        controller: "BilletCatalogue", method: "CreateMaterialCataloguePopup", width: 600, data: { }, afterClose: reloadKendoGrid
    })
}

function reloadKendoGrid() {
    let grid = $('#MaterialCatalogueList').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();
}

//function GoToDetails(Id, popupTitle) {
//    let dataToSend = {
//        id: Id
//    };
//    openSlideScreen('BilletCatalogue', 'ElementDetails', dataToSend);
//}


function Delete(itemId) {
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
        let dataToSend = {
            itemId: itemId
        };
        let targetUrl = '/BilletCatalogue/DeleteMaterialCatalogue';

        AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('deleteMaterialCatalogue - failed'); });
    });
}