function AddNew() {
    //OpenInPopupWindow('RollType', 'AddRollTypeDialog', 550, 400, RefreshData);

    OpenInPopupWindow({
        controller: "RollType", method: "AddRollTypeDialog", width: 600, afterClose: RefreshData
    });
}
function EditData(itemId) {
    //OpenWithParameterInPopupWindow('RollType', 'EditRollTypeDialog', itemId, 550, 400, RefreshData);

    OpenInPopupWindow({
        controller: "RollType", method: "EditRollTypeDialog", width: 600, data: { id: itemId }, afterClose: RefreshData
    });
}

function Delete(itemId) {
    var functionName = Delete2Confirm;
    var action = 'DeleteRollType';
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action) });
    //PromptMessage("@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDelete", "@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDeleteInfo", function () { return functionName(itemId, action) });
}

function Delete2Confirm(itemId, action) {

    var url = serverAddress + "/RollType/" + action;
    var data = { Id: itemId };

    AjaxReqestHelper(url, data, RefreshData);
}

RegisterMethod(HmiRefreshKeys.Roll, RefreshData);

function RefreshData() {
    $("#RollTypesGrid").data("kendoGrid").dataSource.read();
    $("#RollTypesGrid").data("kendoGrid").refresh();
}