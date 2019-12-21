RegisterMethod(HmiRefreshKeys.Schedule, RefreshData);

function RefreshData() {
    if ($("#Schedule").length > 0) {
        $("#Schedule").data("kendoGrid").dataSource.read();
        $("#Schedule").data("kendoGrid").refresh();
    }

    if ($("#UnassignedWorkOrders").length > 0) {
        $("#UnassignedWorkOrders").data("kendoGrid").dataSource.read();
        $("#UnassignedWorkOrders").data("kendoGrid").refresh();
    }
}

function addWorkOrderToSchedule(workOrderId) {
    let dataToSend = {
        workOrderId: workOrderId
    };
    let targetUrl = '/Schedule/AddWorkOrderToSchedule';

    AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('addWorkOrderToSchedule - failed'); });
}

function removeItemFromSchedule(scheduleId, workOrderId, orderSeq) {
    PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
        let dataToSend = {
            scheduleId: scheduleId,
            workOrderId: workOrderId,
            orderSeq: orderSeq
        };
        let targetUrl = '/Schedule/RemoveItemFromSchedule';

        AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('removeItemFromSchedule - failed'); });
    });
}

function moveItemInSchedule(scheduleId, workOrderId, direction) {
    let dataToSend = {
        scheduleId: scheduleId,
        workOrderId: workOrderId,
        direction: direction
    };
    let targetUrl = '/Schedule/MoveItemInSchedule';

    AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('MoveItemInSchedule - failed'); });
}


function createTestSchedule(scheduleId) {
    OpenInPopupWindow({
        controller: 'Schedule',
        method: 'CloneSchedulePopup',
        heigth: 830,
        width: 500,
        data: { scheduleId: scheduleId },
        afterClose: RefreshData
    });
}

function GoToWorkOrderDetails(workOrderId) {

    let dataToSend = {
        workOrderId: workOrderId
    };
    openSlideScreen('WorkOrder', 'ElementDetails', dataToSend);
}

function GoToUnasignedWorkOrder() {
    openSlideScreen('Schedule', 'PreparePratialForSchedule');
    //OpenInPopupWindow({
        //controller: "WorkOrder", method: "GetNoScheduledWorkOrderList", width: 600, afterClose: reloadKendoGrid
    //})
}

function EditMaterialCataloguePopup(id) {
    OpenInPopupWindow({
        controller: "BilletCatalogue", method: "EditMaterialCataloguePopup", width: 600, data: { id: id }, afterClose: reloadKendoGrid
    })
}

function reloadKendoGrid() {
    let grid = $('#UnassignedWorkOrders').data('kendoGrid');
    grid.dataSource.read();
    grid.refresh();
}

function ColorRowInTable() {
    $restrictedRows = 0;
    var grid = $("#ScheduleList").data("kendoGrid");
    var gridData = grid.dataSource.view();
    for (var i = 0; i < gridData.length; i++) {
        var currentUid = gridData[i].uid;
        var currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");
        if (gridData[i].IsProducedNow == 0) {
            currenRow.css({ 'background': '#00B5E2' });
            currenRow.css({ 'color': 'white' });
        }
        if (gridData[i].IsProducedNow === 1) {
            currenRow.css({ 'background': '#7A9A01' });
            currenRow.css({ 'color': 'white' });
        }
    }
}