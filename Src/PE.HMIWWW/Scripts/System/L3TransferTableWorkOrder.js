RegisterMethod(HmiRefreshKeys.L3TransferTable, RefreshData);

//THIS METHOD WILL BE CALLED BY SYSTEM (SERVER) IN CASE DATA CHANGE, NAME IS IMPORTANT !!!
function RefreshData() {
    $("#L3TransferTableWorkOrderDetail").data("kendoGrid").dataSource.read();
    $("#L3TransferTableWorkOrderDetail").data("kendoGrid").refresh();

    $("#L3TransferTableGeneral").data("kendoGrid").dataSource.read();
    $("#L3TransferTableGeneral").data("kendoGrid").refresh();
}