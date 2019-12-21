RegisterMethod(HmiRefreshKeys.Roll, RefreshData);

function ConfigRollset(itemId, type) {
    //if (type == 3 || type == 4) {
    //    OpenWithParameterInPopupWindow('GrindingTurning', 'ConfigRollSetForKocksDialog', itemId, 450, 620, RefreshData);

    //}
    //else {
    //OpenWithParameterInPopupWindow('GrindingTurning', 'ConfigRollSetDialog', itemId, 450, 540, RefreshData);

    OpenInPopupWindow({
        controller: "GrindingTurning", method: "ConfigRollSetDialog", width: 600, height: 680, data: { id: itemId }, afterClose: RefreshData
    });
    //}

}

function RefreshData() {
    $("#ScheduledRollSetGrid").data("kendoGrid").dataSource.read();
    $("#ScheduledRollSetGrid").data("kendoGrid").refresh();
    $("#PlannedRollSetGrid").data("kendoGrid").dataSource.read();
    $("#PlannedRollSetGrid").data("kendoGrid").refresh();
}

function EditData(itemId) {
    //OpenWithParameterInPopupWindow('GrindingTurning', 'EditRollSetDialog', itemId, 280, 240, RefreshData);

    OpenInPopupWindow({
        controller: "GrindingTurning", method: "EditRollSetDialog", width: 600, data: { id: itemId }, afterClose: RefreshData
    });
}

function AssembleRs(itemId) {
    //OpenWithParameterInPopupWindow('GrindingTurning', 'AssembleRollSetDialog', itemId, 280, 280, RefreshData);

    OpenInPopupWindow({
        controller: "GrindingTurning", method: "AssembleRollSetDialog", width: 600, data: { id: itemId }, afterClose: RefreshData
    });
}

function DisassembleRs(itemId) {
    //OpenWithParameterInPopupWindow('GrindingTurning', 'DisassembleRollSetDialog', itemId, 280, 160, RefreshData);

    OpenInPopupWindow({
        controller: "GrindingTurning", method: "DisassembleRollSetDialog", width: 600, data: { id: itemId }, afterClose: RefreshData
    });
}

function ConfirmRollset(itemId, type) {

    //if (type == 3 || type == 4) {
    //    OpenWithParameterInPopupWindow('GrindingTurning', 'ConfirmRollSetForKocksDialog', itemId, 400, 750, RefreshData);
    //} else {
    //OpenWithParameterInPopupWindow('GrindingTurning', 'ConfirmRollSetDialog', itemId, 400, 560, RefreshData);


    OpenInPopupWindow({
        controller: "GrindingTurning", method: "ConfirmRollSetDialog", width: 600, height: 680, data: { id: itemId }, afterClose: RefreshData
    });
    //}

}

function CancelRollset(itemId) {

    var functionName = CancelRollset2Confirm;
    var action = 'CancelRollset';
    //PromptMessage("@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDelete", "@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmTurningRollSetCancelInfo", function () { return functionName(itemId, action) });
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action) });

}

function CancelRollset2Confirm(itemId, action) {

    var url = serverAddress + "/GrindingTurning/" + action;
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

function OpenTurningInfoPopup(itemId, type) {

    OpenInPopupWindow({
        controller: "GrindingTurning", method: "TurningInfoPopupDialog", width: 600, height: 750, data: { id: itemId }, afterClose: RefreshData
    });
    //if (type == 3 || type == 4) {
    //    OpenWithParameterInPopupWindow('GrindingTurning', 'TurningForKocksInfoPopupDialog', itemId, 400, 820, RefreshData);
    //} else {
     //   OpenWithParameterInPopupWindow('GrindingTurning', 'TurningInfoPopupDialog', itemId, 400, 600, RefreshData);
    //}

}

function OpenTurningForConfirmInfoPopup(itemId, type) {
    OpenInPopupWindow({
        controller: "GrindingTurning", method: "TurningForConfirmInfoPopupDialog", width: 500, height: 750, data: { id: itemId }, afterClose: RefreshData
    });
    //if (type == 3 || type == 4) {
    //    OpenWithParameterInPopupWindow('GrindingTurning', 'TurningForConfirmForKocksInfoPopupDialog', itemId, 400, 750, RefreshData);
    //}
    //else {
    //    OpenWithParameterInPopupWindow('GrindingTurning', 'TurningForConfirmInfoPopupDialog', itemId, 400, 600, RefreshData);
    //}

}

function HistoryRollset(itemId, type) {

    OpenInPopupWindow({
        controller: "GrindingTurning", method: "RollSetHistoryPopupDialog", width: 380, height: 700, data: { id: itemId }, afterClose: RefreshData
    });

    //if (type == 3 || type == 4) {
    //    OpenWithParameterInPopupWindow('GrindingTurning', 'RollSetHistoryForKocksPopupDialog', itemId, 335, 780, RefreshData);
    //}
    //else {
    //    OpenWithParameterInPopupWindow('GrindingTurning', 'RollSetHistoryPopupDialog', itemId, 335, 620, RefreshData);
    //}

}

function refreshhistory(e) {
    var dataItem = this.dataItem(e.item.index()).Value;
    var dataToSent = JSON.stringify({ "id": dataItem });
    $("#formContainer").hide("slow");

    $.ajax({
        traditional: true,
        url: serverAddress + "/GrindingTurning/GetRollSetHistoryById",
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        data: dataToSent,
        success: function (result) {
            $("#formContainer").html(result);

        },
        complete: function (r) {
            $("#formContainer").show("slow");
        },
        error: function (error) {
        }
    });
}

