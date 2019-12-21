//function EditDefectCataloguePopup(id) {
//        OpenInPopupWindow({
//            controller: "DefectCatalogue", method: "DefectCatalogueEditPopup", width: 600, data: { id: id }, afterClose: reloadKendoGrid
//        });
//    }

//    function reloadKendoGrid() {
//        let grid = $('#DefectCatalogueList').data('kendoGrid');
//        grid.dataSource.read();
//        grid.refresh();
//    }

var CurrentElement = Html.Raw(Json.Encode(model));

function GoToMaterial(materialId) {
    let dataToSend = {
        MaterialId: materialId
    };
    openSlideScreen('Material', 'ElementDetails', dataToSend);
}

function onSelect(e) {
    let urlWithParameter = "/Products/GetProductsLabelWithRawMaterialNameAsync?rawMaterialName=" + e;
        $('#labelPicture').first().attr("src", urlWithParameter);
    
}