let CurrentElement;

var columns = ["ShiftKey", "LastUpdateTs", "IsDummy",
    "IsAssigned", "ShiftKey", "Weight", "WorkOrderName"];
var button_array = $('.arrow-categories');

function onElementSelect(e) {
    hideCategories();

    if ($('.k-i-arrow-left').length) {
        button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
        $('.more').show(100);
        $('.less').hide(100);
    }

    $('#ProductsDetails').addClass('loading-overlay');
    let grid = e.sender;
    let selectedRow = grid.select();
    let selectedItem = grid.dataItem(selectedRow);
    let dataToSend = {
        ProductId: selectedItem.ProductId
    };

    CurrentElement = {
        ProductId: selectedItem.ProductId
    };

    let url = "/Products/ElementDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function onSelect(e) {
    if ($(e.item).find(".k-link").text() === "Label") {
        let urlWithParameter = "/Products/GetProductsLabel?ProductId=" + CurrentElement.ProductId;
        $('#labelPicture').first().attr("src", urlWithParameter);
    }
}

function GoToMaterial(materialId) {
    let dataToSend = {
        MaterialId: materialId
    };
    openSlideScreen('Material', 'ElementDetails', dataToSend);
}


function setElementDetailsPartialView(partialView) {
    $('#ProductsDetails').removeClass('loading-overlay');
    $('#ProductsDetails').html(partialView);
}

function selectRow() {
    const grid = $('#SearchGrid').data("kendoGrid");
    let gridData = grid.dataSource.view();
    let id = getUrlParameter('productId');
    //if (id != null) {
    for (let i = 0; i < gridData.length; i++) {
        let currentItem = gridData[i];
        if (currentItem.ProductId === id) {
            let currenRow = grid.table.find("tr[data-uid='" + currentItem.uid + "']");
            grid.select(currenRow);
            break;
        }
    }
    //}
}

