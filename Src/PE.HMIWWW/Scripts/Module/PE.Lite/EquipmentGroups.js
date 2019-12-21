RegisterMethod(HmiRefreshKeys.EquipmentGroups, reloadKendoGrid);

function reloadKendoGrid() {
	let grid = $('#Groups').data('kendoGrid');
	grid.dataSource.read();
	grid.refresh();
}

function EditEquipmentGroupPopup(id) {
	OpenInPopupWindow({
		controller: "EquipmentGroups", method: "EquipmentGroupsEditPopupAsync", width: 600, data: { id: id }, afterClose: reloadKendoGrid
	});
}

function AddNew() {
	OpenInPopupWindow({
		controller: "EquipmentGroups", method: "EquipmentGroupCreatePopup", width: 600, afterClose: reloadKendoGrid
	});
}

function Delete(id) {
	PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
		let dataToSend = {
			id: id
		};
		let targetUrl = '/EquipmentGroups/DeleteEquipmentGroupAsync';

		AjaxReqestHelper(targetUrl, dataToSend, RefreshData, function () { console.log('DeleteEquipmentGroup - failed'); });
	});
}

RegisterMethod(HmiRefreshKeys.EquipmentGroups, RefreshData);

function RefreshData() {
	if ($("#Groups").length > 0) {
		$("#Groups").data("kendoGrid").dataSource.read();
		$("#Groups").data("kendoGrid").refresh();
	}
}
