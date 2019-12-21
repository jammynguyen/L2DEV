let CurrentElement;

var columns = ["ProductCatalogueName", "SteelgradeCode", "HeatName",
    "ToBeCompletedBefore", "ProductionEnd", "TargetOrderWeight", "MaterialsAssigned",
    "ProductsNumber", "ProductsWeight", "MetallicYield"];

var button_array = $('.arrow-categories');

function onElementSelect(e) {
    hideCategories();

    if ($('.k-i-arrow-left').length) {
        button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
        $('.more').show(100);
        $('.less').hide(100);
    }

    $('#WorkOrderDetails').addClass('loading-overlay');
    var grid = e.sender;
    var selectedRow = grid.select();
    var selectedItem = grid.dataItem(selectedRow);
    var dataToSend = {
        WorkOrderId: selectedItem.WorkOrderId
    };

    CurrentElement = {
        WorkOrderId: selectedItem.WorkOrderId
    };

    var url = "/WorkOrder/ElementDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function onElementReload() {

    var dataToSend = {
        WorkOrderId: CurrentElement.WorkOrderId
    };

    var url = "/WorkOrder/ElementDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
}

function setElementDetailsPartialView(partialView) {
    $('#WorkOrderDetails').removeClass('loading-overlay');
    $('#WorkOrderDetails').html(partialView);

    $("#chart").data("kendoChart").options.series[0].data[0].visible = false;
    let i;
    for (i = 0; i < $("#chart").data("kendoChart").options.series[0].data.length; i++) {
        if ($("#chart").data("kendoChart").options.series[0].data[i].value === 0 ||
            $("#chart").data("kendoChart").options.series[0].data[i].value === null) {
            $("#chart").data("kendoChart").options.series[0].data[i].visible = false;
            $("#chart").data("kendoChart").options.series[0].data[i].visibleInLegend = false;
            $("#chart").data("kendoChart").options.series[0].overlay.gradient = false;
        }
    }
}

function GoToMaterial(materialId) {
    let dataToSend = {
        MaterialId: materialId
    };
    openSlideScreen('Material', 'ElementDetails', dataToSend);
}

function AddNew() {
    OpenInPopupWindow({
        controller: "WorkOrder", method: "WorkOrderCreatePopup", width: 1250, afterClose: reloadKendoGrid
    });
}

function EditWorkOrder() {
    try {
        OpenInPopupWindow({
            controller: "WorkOrder", method: "WorkOrderEditPopup", width: 1250, data: { id: CurrentElement.WorkOrderId, byMaterial: false }, afterClose: onElementReload
        });
    } catch (e) {
        if (e instanceof TypeError) {
            WarningMessage(Translations["MESSAGE_SelectMaterial"]);
        }

    }
}

//function AddNewHeat() {
//    ClosePopup();
//    let grid = $('#FKHeatIdRef');
//    grid.hide();
//    OpenInPopupWindow({
//        controller: "Heat", method: "HeatCreatePopup", width: 1250, afterClose: reloadKendoGrid
//    });
//}

function reloadKendoGrid() {
    let grid = $('#SearchGrid').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();

    onElementReload();

    //let materials = $('#MaterialGrading').data('kendoGrid');
    //materials.dataSource.read();
    //materials.refresh();

    //let workorderbody = $('#WorkOrderBody').data('kendoTabStrip');
    //workorderbody.reload();
    //workorderbody.refresh();
    
}

function Delete() {
    $('#WorkOrderDetails').addClass('loading-overlay');

    let currentOrder = function (callback) {
        if (callback === false) {
            $('#WorkOrderDetails').removeClass('loading-overlay');
        } else {
            let url = "/WorkOrder/DeleteWorkOrder";
            dataToSend = {
                workOrderId: CurrentElement.WorkOrderId
            };
            AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);
            $("#SearchGrid").data("kendoGrid").dataSource.read();
            $("#SearchGrid").data("kendoGrid").refresh();
        }
    };
    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], Translations["MESSAGE_MaterialScrap"], currentOrder);
}

function onAdditionalData() {
    return {
        text: $("#FKHeatIdRef").val()
    };
}

//heat autocomplete autohide
$(function () {

    $('#error').css("display", "none");
    $("#form").kendoValidator().data("kendoValidator");

    $('.k-autocomplete').css("width", "150px");

    $('.k-autocomplete input').keydown(function () {
        $('.k-autocomplete').animate({
            width: 400
        }, 200, function () {
            // Animation complete.
        });
    })

    $('.k-autocomplete input').focusout(function () {
        $('.k-autocomplete').animate({
            width: 150
        }, 400, function () {
            // Animation complete.
        });
    })


});

function displayMessage() {
    if (document.getElementById('SingleMaterialWeight').value === "") {
        document.getElementById('SingleMaterialWeight').value = 1000;
    }
    var validator = $("#form").kendoValidator().data("kendoValidator");

    if (validator.validate()) {
        $('#error').css("display", "none");
    } else {
        $('#error').css("display", "block");
        $('#popup-footer')
            .css('display', 'block')
            .css('background-color', 'rgb(206, 0, 55)');
    }
};

function setMaterialsWeight() {
    const Weight = document.getElementById("TargetOrderWeight").value;
    const Number = document.getElementById("MaterialsNumber").value;
    if (Weight === "" || Number === "") {
        document.getElementById('SingleMaterialWeight').value = "";
    }
    else {
        document.getElementById('SingleMaterialWeight').value = Weight / Number;
    }
}

$(function () {

    $('#error').css("display", "none");
    $("#form").kendoValidator().data("kendoValidator");

});


function ShowProductCatalogueDetailsAfterSelect() {
    var productCatalogId = $("#FKProductCatalogueId")[0].value;

    if (productCatalogId !== "") {
        var data = AjaxGetDataHelper(URL("WorkOrder", "GetProductCatalogDetails"), { productCatalogId: productCatalogId });
        $(".product-details").show();
        $("#Thickness").text(data.Thickness);
        $("#Width").text(data.Width);
        $("#Weight").text(data.Weight);
        $("#Steelgrade").text(data.Steelgrade);
        $("#Shape").text(data.Shape);
    }
    else {
        $(".product-details").hide();
    }
}

function CalculateMaterialsWeight() {
    var heat = $("#FKHeatIdRef")[0].value;
    var materialNumber = $("#MaterialsNumber")[0].value;

    if (heat !== "" && materialNumber !== "") {
        var data = AjaxGetDataHelper(URL("WorkOrder", "GetMaterialWeight"), { heatName: heat });
        $(".material-weight").show();
        $("#material-weight-value").text(parseFloat(data * materialNumber).toFixed(3));
    }
    else {
        $(".material-weight").hide();
    }
}

function disableInputs() {
    var wostatus = $("#WorkOrderStatus").data("kendoDropDownList").value();
    if (wostatus > 2) {
        $("#WorkOrderName").attr("readonly", true);
        $("#TargetOrderWeight").data("kendoNumericTextBox").readonly();
        $("#ToBeCompletedBefore").data("kendoDatePicker").readonly();
        $("#IsTestOrder").attr("readonly", true).attr("hidden", true).click(function () { return false; });
        $("#FKProductCatalogueId").data("kendoDropDownList").readonly();
        $("#FKHeatIdRef").data("kendoAutoComplete").readonly();
        $("#MaterialsNumber").data("kendoNumericTextBox").readonly();
        $("#ExtraLabelInformation").attr("readonly", true);
        $("#NextAggregate").attr("readonly", true);
        $("#OperationCode").attr("readonly", true);
        $("#QualityPolicy").attr("readonly", true);
        $("#FKCustomerId").data("kendoDropDownList").readonly();
        $("#FKReheatingGroupId").data("kendoDropDownList").readonly();
    }
}
