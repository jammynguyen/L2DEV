function DelayEditPopup(id) {
        OpenInPopupWindow({
            controller: "Delays", method: "DelayEditPopup", width: 600, data: { delayId: id }, afterClose: reloadKendoGrid
        });
}

function DelayDividePopup(id) {
    OpenInPopupWindow({
        controller: "Delays", method: "DelayDividePopup", width: 600, data: { delayId: id }, afterClose: reloadKendoGrid
    });
}

    function reloadKendoGrid() {
        let grid = $('#DelayList').data('kendoGrid');
        grid.dataSource.read();
        grid.refresh();
    }