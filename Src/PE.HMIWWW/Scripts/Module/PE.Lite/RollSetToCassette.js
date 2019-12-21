function OpenAssembleCassetteForm(itemId) {
    //OpenWithParameterInPopupWindow('RollsetToCassette', 'AssembleCassetteAndRollsetDialog', itemId, 500, 520, RefreshData);

    OpenInPopupWindow({
        controller: "RollsetToCassette", method: "AssembleCassetteAndRollsetDialog", width: 550, data: { id: itemId }, afterClose: RefreshData
    });
}
function OpenRollsetInfoPopup(itemId) {
    //OpenWithParameterInPopupWindow('RollsetToCassette', 'RollSetInfoPopupDialog', itemId, 400, 940, RefreshData);
    OpenInPopupWindow({
        controller: "RollsetToCassette", method: "RollSetInfoPopupDialog", width: 500, data: { id: itemId }, afterClose: RefreshData
    });
}
function OpenCasstteInfoPopup(itemId) {
    //OpenWithParameterInPopupWindow('RollsetToCassette', 'CassetteInfoPopupDialog', itemId, 1300, 240, RefreshData);
    OpenInPopupWindow({
        controller: "RollsetToCassette", method: "CassetteInfoPopupDialog", width: 1300, data: { id: itemId }, afterClose: RefreshData
    });
}
function EditData(itemId) {
    //OpenWithParameterInPopupWindow('Customer', 'EditCustomerDialog', itemId, 550, 280, RefreshData);
    OpenInPopupWindow({
        controller: "RollsetToCassette", method: "EditCustomerDialog", width: 600, data: { id: itemId }, afterClose: RefreshData
    });
}

function ConfirmRsReadyForMounting(itemId) {
    var functionName = ConfirmRsReadyForMounting2Confirm;
    var action = 'ConfirmRsReadyForMounting';
    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], "", () => { return functionName(itemId, action) });
    //PromptMessage("@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDelete", "@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmRollSetConfirmInfo", function () { return functionName(itemId, action) });
}

function ConfirmRsReadyForMounting2Confirm(itemId, action) {

    var url = serverAddress + "/RollsetToCassette/" + action;
    var data = { Id: itemId };

    AjaxReqestHelper(url, data, RefreshData);
}


function Delete(itemId) {
    var functionName = Delete2Confirm;
    var action = 'DeleteCustomer';
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => { return functionName(itemId, action) });
    //PromptMessage("@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDelete", "@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDeleteInfo", function () { return functionName(itemId, action) });
}

function Delete2Confirm(itemId, action) {

    var url = serverAddress + "/Customer/" + action;
    var data = { Id: itemId };

    AjaxReqestHelper(url, data, RefreshData);
}

function UnloadRollset(itemId) {
    var functionName = UnloadRollset2Confirm;
    var action = 'UnloadRollset';
    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], "", () => { return functionName(itemId, action) });
    //PromptMessage("@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDelete", "@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDeleteInfo", function () { return functionName(itemId, action) });
}

function UnloadRollset2Confirm(itemId, action) {
    var url = serverAddress + "/RollsetToCassette/" + action;
    var data = { Id: itemId };

    AjaxReqestHelper(url, data, RefreshData);
}

function CancelStatus(itemId) {
    var functionName = CancelStatus2Confirm;
    var action = 'CancelPlan';
    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], "", () => { return functionName(itemId, action) });
    //PromptMessage("@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDelete", "@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDeleteInfo", function () { return functionName(itemId, action) });
}

function CancelStatus2Confirm(itemId, action) {
    var url = serverAddress + "/RollsetToCassette/" + action;
    var data = { Id: itemId };

    AjaxReqestHelper(url, data, RefreshData);
}

