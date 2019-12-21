RegisterMethod(HmiRefreshKeys.ProductCatalogue, RefreshData);

function RefreshData() {
    reloadKendoGrid();
}
function EditProductCataloguePopup(id) {
    OpenInPopupWindow({
        controller: "ProductCatalogue", method: "ProductCatalogueEditPopup", width: 1250, data: { id: id }, afterClose: reloadKendoGrid
    });
}

function CreateProductCataloguePopup(id) {
    OpenInPopupWindow({
        controller: "ProductCatalogue", method: "ProductCatalogueCreatePopup", width: 1250, data: { }, afterClose: reloadKendoGrid
    });
}

function reloadKendoGrid() {
    let grid = $('#ProductCatalogueList').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();
}

function GoToDetails(Id) {
    let dataToSend = {
        id: Id
    };
    openSlideScreen('ProductCatalogue', 'ElementDetails', dataToSend);
}

function Delete(itemId) {
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
        let dataToSend = {
            itemId: itemId
        };
        let targetUrl = '/ProductCatalogue/DeleteProductCatalogue';

        AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('deleteProductCatalogue - failed'); });
    });
}