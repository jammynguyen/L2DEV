function AddNew() {
    //OpenInPopupWindow('RollsetManagement', 'AddRollSetDialog', 280, 220, RefreshData);
    OpenInPopupWindow({
        controller: "RollSetManagement", method: "AddRollSetDialog", width: 400, afterClose: RefreshData
    });
}
function EditData(itemId) {
    //OpenWithParameterInPopupWindow('RollsetManagement', 'EditRollSetDialog', itemId, 270, 220, RefreshData);
    OpenInPopupWindow({
        controller: "RollSetManagement", method: "EditRollSetDialog", width: 400, data: { id: itemId }, afterClose: RefreshData
    });
}
function AssembleRs(itemId) {
    //OpenWithTwoParametersInPopupWindow('RollsetManagement', 'AssembleRollSetDialog', itemId, param, 280, 390, RefreshData);
    OpenInPopupWindow({
        controller: "RollSetManagement", method: "AssembleRollSetDialog", width: 600, data: { id: itemId}, afterClose: RefreshData
    });
}
function DisassembleRs(itemId) {
    //OpenWithParameterInPopupWindow('RollsetManagement', 'DisassembleRollSetDialog', itemId, 280, 160, RefreshData);
    OpenInPopupWindow({
        controller: "RollSetManagement", method: "DisassembleRollSetDialog", width: 600, data: { id: itemId }, afterClose: RefreshData
    });
}
function ConfirmAssembleRS(itemId) {
    var functionName = ConfirmAssembleRS2Confirm;
    var action = 'ConfirmAssembleRollSet';
    //PromptMessage("@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDelete", "@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmRollSetConfirmInfo", function () { return functionName(itemId, action) });
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action) });
}

function OpenRollsetHistoryInfoPopup(itemId, type) {

    //if (type == 3 || type == 4) {
    //    OpenWithParameterInPopupWindow('GrindingTurning', 'RollSetHistoryForKocksPopupDialog', itemId, 335, 780, RefreshData);
    //}
    //else {
    //    OpenWithParameterInPopupWindow('GrindingTurning', 'RollSetHistoryPopupDialog', itemId, 335, 620, RefreshData);
    //}
    OpenInPopupWindow({
        controller: "GrindingTurning", method: "RollSetHistoryPopupDialog", width: 350, height: 700, data: { id: itemId }, afterClose: RefreshData
    });
}

function OpenTurningInfoPopup(itemId, type) {

    OpenInPopupWindow({
        controller: "GrindingTurning", method: "TurningInfoPopupDialog", width: 600, height: 750, data: { id: itemId }, afterClose: RefreshData
    });
}

function ConfirmAssembleRS2Confirm(itemId, action) {

    var url = serverAddress + "/RollsetManagement/" + action;
    var data = { Id: itemId };

    AjaxReqestHelper(url, data, RefreshData);
}

function CancelAssembleRS(itemId) {
    var functionName = CancelAssembleRS2Confirm;
    var action = 'CancelAssembleRollSet';
    PromptMessage("@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDelete", "@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmRollSetCancelInfo", function () { return functionName(itemId, action) });
}

function CancelAssembleRS2Confirm(itemId, action) {

    var url = serverAddress + "/RollsetManagement/" + action;
    var data = { Id: itemId };

    AjaxReqestHelper(url, data, RefreshData);
}

function Delete(itemId) {
    var functionName = Delete2Confirm;
    var action = 'DeleteRollset';
    //PromptMessage("@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDelete", "@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDeleteInfo", function () { return functionName(itemId, action) });
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action) });
}

function Delete2Confirm(itemId, action) {

    var url = serverAddress + "/RollsetManagement/" + action;
    var data = { Id: itemId };

    AjaxReqestHelper(url, data, RefreshData);
}

// 2 functions for refresh list of rolls
function findRoll() {
    //alert($("#Id").val());
    return {
        RollSetId: $("#Id").val()
    }
}

function onChange() {

    //$("#UpperRollId").data("kendoDropDownList").dataSource.read();
    //$('#UpperRollId').data('kendoDropDownList').refresh();
    //$("#BottomRollId").data("kendoDropDownList").dataSource.read();
    //$('#BottomRollId').data('kendoDropDownList').refresh();
    //$("#ThirdRollId").data("kendoDropDownList").dataSource.read();
    //$('#ThirdRollId').data('kendoDropDownList').refresh();

};
RegisterMethod(HmiRefreshKeys.Roll, RefreshData);
function RefreshData() {
    $("#RollSetManagementGrid").data("kendoGrid").dataSource.read();
    $("#RollSetManagementGrid").data("kendoGrid").refresh();
}
function GetRollSet(Id) {
    $.ajax({
        url: serverAddress + "/RollSetManagement/GetEmptyRollList",
        type: 'POST',
        data: 'id=' + Id,
        success: function (result) {
            $('#furnace-body-' + Id).html(result);
        },
        complete: function (r) {
            //when connection with server over
        },
        error: function (error) {
            CoreHandleError(error.status, error.statusText, "AjaxAssembleRoll", true, null, null);
        }
    });
}