function AddNew() {
    //OpenInPopupWindow('RollsManagement', 'AddRollDialog', 560, 300, RefreshData);

    OpenInPopupWindow({
        controller: "RollsManagement", method: "AddRollDialog", width: 600, afterClose: RefreshData
    });
}
function EditData(itemId) {
    //OpenWithParameterInPopupWindow('RollsManagement', 'EditRollDialog', itemId, 560, 300, RefreshData);

    OpenInPopupWindow({
        controller: "RollsManagement", method: "EditRollDialog", width: 600, data: { id: itemId }, afterClose: RefreshData
    });
}
function ScrapRoll(itemId) {
    //OpenWithParameterInPopupWindow('RollsManagement', 'ScrapRollDialog', itemId, 550, 280, RefreshData);

    OpenInPopupWindow({
        controller: "RollsManagement", method: "ScrapRollDialog", width: 600, data: { id: itemId }, afterClose: RefreshData
    });
}
function OpenRollsetInfoPopup(itemId) {
    OpenWithParameterInPopupWindow('RollsManagement', 'RollSetInfoPopupDialog', itemId, 400, 780, RefreshData);
}

function GoToRollDetails(itemId) {

    let dataToSend = {
        workOrderId: workOrderId
    };
    openSlideScreen('RollsManagement', 'RollSetDetails', dataToSend);
}


function Delete(itemId) {
    var functionName = Delete2Confirm;
    var action = 'DeleteRoll';
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action) });
    //PromptMessage("@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDelete", "@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDeleteInfo", function () { return functionName(itemId, action) });
}

function Delete2Confirm(itemId, action) {

    var url = serverAddress + "/RollsManagement/" + action;
    var data = { Id: itemId };

    AjaxReqestHelper(url, data, RefreshData);
}

RegisterMethod(HmiRefreshKeys.Roll, RefreshData);

function RefreshData() {
    $("#RollsManagementGrid").data("kendoGrid").dataSource.read();
    $("#RollsManagementGrid").data("kendoGrid").refresh();
}