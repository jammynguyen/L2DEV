RegisterMethod(HmiRefreshKeys.Equipment, reloadKendoGrid);

function reloadKendoGrid() {
	let grid = $('#EquipmentGrid').data('kendoGrid');
	grid.dataSource.read();
	grid.refresh();
}

function EditEquipmentPopup(id) {
	OpenInPopupWindow({
		controller: "Equipment", method: "EquipmentEditPopupAsync", width: 600, data: { id: id }, afterClose: reloadKendoGrid
	});
}

function CloneEquipmentPopup(id) {
	OpenInPopupWindow({
		controller: "Equipment", method: "EquipmentClonePopupAsync", width: 600, data: { id: id }, afterClose: reloadKendoGrid
	});
}

function AddNew() {
	OpenInPopupWindow({
		controller: "Equipment", method: "EquipmentCreatePopupAsync", width: 600, afterClose: reloadKendoGrid
	});
}

function Delete(id) {
	PromptMessage(Translations["MESSAGE_deleteConfirm"], "", () => {
		let dataToSend = {
			id: id
		};
		let targetUrl = '/Equipment/DeleteEquipmentAsync';

		AjaxReqestHelper(targetUrl, dataToSend, reloadKendoGrid, function () { console.log('DeleteEquipment - failed'); });
	});
}

//function ShowHistory(id) {
//	let dataToSend = {
//		id: id
//	};
//	openSlideScreen('Equipment', 'ShowEquipmentHistory', dataToSend);
//}

function ShowHistory(id) {
	OpenInPopupWindow({
		controller: "Equipment", method: "ShowEquipmentHistory", width: 1200, data: { id: id }, afterClose: reloadKendoGrid
	});
}