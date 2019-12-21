RegisterMethod(HmiRefreshKeys.Roll, RefreshData);

function AddNew() {
    //OpenInPopupWindow('ActualStandConfiguration', 'AddStandConfigurationDialog', 320, 340, RefreshData);
    OpenInPopupWindow({
        controller: "ActualStandConfiguration", method: "AddStandConfigurationDialog", width: 320, afterClose: RefreshData
    });
}
function EditData(itemId) {
    //OpenWithParameterInPopupWindow('ActualStandConfiguration', 'EditStandConfigurationDialog', itemId, 280, 270, RefreshData);

    OpenInPopupWindow({
        controller: "ActualStandConfiguration", method: "EditStandConfigurationDialog", width: 500, data: { id: itemId }, afterClose: RefreshData
    });

}
function OpenRollsetInfoPopup(itemId, type) {
    //if (type == 3 || type == 4) {
    //    OpenWithParameterInPopupWindow('GrindingTurning', 'TurningForKocksInfoPopupDialog', itemId, 400, 750, RefreshData);
    //} else {
    //    OpenWithParameterInPopupWindow('GrindingTurning', 'TurningInfoPopupDialog', itemId, 400, 600, RefreshData);
    //}
    OpenInPopupWindow({
        controller: "GrindingTurning", method: "TurningInfoPopupDialog", width: 400, top: 100, data: { id: itemId }, afterClose: RefreshData
    });
}
function MountCassette(itemId) {
    //OpenWithParameterInPopupWindow('ActualStandConfiguration', 'MountCassetteDialog', itemId, 280, 340, RefreshData);
    OpenInPopupWindow({
        controller: "ActualStandConfiguration", method: "MountCassetteDialog", width: 500, data: { id: itemId }, afterClose: RefreshData
    });
}

function MountEmptyCassette(itemId) {
    //OpenWithParameterInPopupWindow('ActualStandConfiguration', 'MountCassetteDialog', itemId, 280, 340, RefreshData);
    OpenInPopupWindow({
        controller: "ActualStandConfiguration", method: "MountEmptyCassetteDialog", width: 500, data: { id: itemId }, afterClose: RefreshData
    });
}

function DismountCassette(itemId) {
    //OpenWithParameterInPopupWindow('ActualStandConfiguration', 'DismountCassetteDialog', itemId, 280, 320, RefreshData);
    OpenInPopupWindow({
        controller: "ActualStandConfiguration", method: "DismountCassetteDialog", width: 500, data: { id: itemId }, afterClose: RefreshData
    });
}
function SwapCassettes(itemId) {
    //OpenWithParameterInPopupWindow('ActualStandConfiguration', 'SwapCassetteDialog', itemId, 480, 540, RefreshData);
    OpenInPopupWindow({
        controller: "ActualStandConfiguration", method: "SwapCassetteDialog", width: 300, data: { id: itemId }, afterClose: RefreshData
    });
}
// for mounting rollset to  empty cassette whichone is in Stand but only for swapRollSet
function MountRollSet(itemId, positionId) {
    //OpenWithTwoParametersInPopupWindow('ActualStandConfiguration', 'MountRollSetDialog', itemId, null, 540, 240, RefreshData);
    OpenInPopupWindow({
        controller: "ActualStandConfiguration", method: "MountRollSetDialog", width: 540, data: { id: itemId , param: positionId}, afterClose: RefreshData
    });
}
// for mounting rollset to  empty cassette whichone is in Stand - popup to Mounted rollset
function OpenAssembleCassetteForm(itemId) {
    //OpenWithParameterInPopupWindow('ActualStandConfiguration', 'AssembleCassetteAndRollsetDialog', itemId, 500, 520, RefreshData);
    OpenInPopupWindow({
        controller: "ActualStandConfiguration", method: "AssembleCassetteAndRollsetDialog", width: 500, data: { id: itemId }, afterClose: RefreshData
    });
}
function DismountRollSet(itemId, positionId) {
    //OpenWithTwoParametersInPopupWindow('ActualStandConfiguration', 'DismountRollSetDialog', itemId, positionId, 540, 240, RefreshData);
    OpenInPopupWindow({
        controller: "ActualStandConfiguration", method: "DismountRollSetDialog", width: 540, data: { id: itemId, param: positionId}, afterClose: RefreshData
    });
}
function SwapRollSet(itemId, positionId) {
    //OpenWithTwoParametersInPopupWindow('ActualStandConfiguration', 'SwapRollSetDialog', itemId, positionId, 540, 300, RefreshData);
    OpenInPopupWindow({
        controller: "ActualStandConfiguration", method: "SwapRollSetDialog", width: 540, data: { id: itemId, param: positionId }, afterClose: RefreshData
    });
}
function PassChangeGroove(itemId, type) {
    //if (type == 3 || type == 4) {
    //    OpenWithParameterInPopupWindow('ActualStandConfiguration', 'PassChangeGrooveForKocksDialog', itemId, 400, 730, RefreshData);
    //}
    //else {
    //    OpenWithParameterInPopupWindow('ActualStandConfiguration', 'PassChangeGrooveDialog', itemId, 400, 640, RefreshData);
    //}

    OpenInPopupWindow({
        controller: "ActualStandConfiguration", method: "PassChangeGrooveDialog", width: 400, height: 820, top: 100, data: { id: itemId }, afterClose: RefreshData
    });

}
function OpenCasstteInfoPopup(itemId) {
    //OpenWithParameterInPopupWindow('ActualStandConfiguration', 'CassetteInfoPopupDialog', itemId, 1300, 300, RefreshData);
    OpenInPopupWindow({
        controller: "ActualStandConfiguration", method: "CassetteInfoPopupDialog", width: 1300, data: { id: itemId }, afterClose: RefreshData
    });
}
function SwapStands(itemId) {
    //OpenWithParameterInPopupWindow('ActualStandConfiguration', 'SwapStandDialog', itemId, 320, 220, RefreshData);
    OpenInPopupWindow({
        controller: "ActualStandConfiguration", method: "SwapStandDialog", width: 340, data: { id: itemId }, afterClose: RefreshData
    });
}

function Delete(itemId) {
    var functionName = Delete2Confirm;
    var action = 'DeleteStandConfiguration';
    PromptMessage(Translations["MESSAGE_ConfirmationMsg"], "", () => { return functionName(itemId, action) });
    //PromptMessage("@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDelete", "@PE.HMIWWW.Core.Resources.VM_Resources.GLOB_ConfirmDeleteInfo", function () { return functionName(itemId, action) });
}

function Delete2Confirm(itemId, action) {

    var url = serverAddress + "/ActualStandConfiguration/" + action;
    var data = { Id: itemId };

    AjaxReqestHelper(url, data, RefreshData);
}
// for mounting rollset to  empty cassette whichone is in Stand


function StylingAfterCompare(param) {
    if (param > 100) {
        return "background: #e61c1c;text-align: right;color:white;";  //red
    }
    else if (param > 90) {
        return "background: #ef8438;text-align: right;color:white;";  //amber
    }
    else if (param > 80) {
        return "background: #d5b500; text-align: right;color:white;"; //yellow
    }
    else {
        return "text-align: right;";
    }
}

function RefreshData() {
    $("#ActualStandConfiguration").data("kendoGrid").dataSource.read();
    $("#ActualStandConfiguration").data("kendoGrid").refresh();
    $("#PassChangeActualData").data("kendoGrid").dataSource.read();
    $("#PassChangeActualData").data("kendoGrid").refresh();
}