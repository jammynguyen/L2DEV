let CurrentElement;
var lastSelectedRow = null;
var lastUpdatedElementRow = null;
var kendoGrid;

function onElementSelect(e) {

    kendoGrid = $('#SetupSearchGrid').data('kendoGrid');
    lastSelectedRow = kendoGrid.select();

    $('#SetupDetails').addClass('loading-overlay');
    let grid = e.sender;
    let selectedRow = grid.select();
    let selectedItem = grid.dataItem(selectedRow);

    console.log(lastSelectedRow[0].outerHTML);

    if (lastUpdatedElementRow && (lastSelectedRow[0].outerHTML == lastUpdatedElementRow[0].outerHTML)) {
        lastUpdatedElementRow = null;
        return;
    }

    let dataToSend = {
        TelegramId: selectedItem.TelegramId
    };

    CurrentElement = {
        TelegramId: selectedItem.TelegramId
    };

    let url = "/Setup/ElementDetails";
    AjaxReqestHelperSilentWithoutDataType(url, dataToSend, setElementDetailsPartialView);


}

function setElementDetailsPartialView(partialView) {
    $('#SetupDetails').removeClass('loading-overlay');
    $('#SetupDetails').html(partialView);
}
function valueEditor(container, options) {

    switch (options.model.DataType) {
        case 'BOOL':
            let checked = '';
            if (options.model.Value === '1') checked = 'checked';
            $('<input type="checkbox"  id="Value"  ' + checked + '>').appendTo(container);
            break;
        case 'REAL':
            $('<input type="number" step=".1"  id="Value" value=' + options.model.Value + '>').appendTo(container);
            break;
        case 'INT':
        case 'DINT':
            $('<input type="number" step="1"  id="Value"  value=' + options.model.Value + '>').appendTo(container);
            break;
        default:
            $('<input type="text"  id="Value"  value=' + options.model.Value + '>').appendTo(container);
            break;
    }
}
function updateValuesSave(data) {

    lastUpdatedElementRow = kendoGrid.select();

    let value;
    if (data.model.DataType === 'BOOL') {
        if ($('#Value').prop('checked') === true) {
            value = "1";
        }
        else {
            value = "0";
        }
    }
    else
        value = $("#Value").val();
    let dataToSend = {
        ElementId: data.model.ElementId,
        Value: value,
        DataType: data.model.DataType,
        TelegramId: data.model.TelegramId,
        TelegramStructureIndexString: data.model.TelegramStructureIndexString
    };
    AjaxReqestHelper(URL('Setup', 'UpdateValues'), dataToSend, RefreshData, RefreshData);

}

function RefreshData() {

    let tree = $("#TelegramStructureTree").data("kendoTreeList");
    tree.dataSource.read();
    $('.k-command-cell>button').html('<span class="k-icon edit-button-ico"></span>');


    $.when(() => {
        kendoGrid.dataSource.read();
        kendoGrid.refresh();
    }).then(() => {
        kendoGrid.select(lastSelectedRow);
    });
}

function onBound() {
    let tree = $("#TelegramStructureTree").data("kendoTreeList");
    let treeData = tree.dataSource.view();

    for (let i = 0; i < treeData.length; i++) {
        let currentUid = treeData[i].uid;
        if (treeData[i].DataType === "STRUCT") {
            var currenRow = tree.table.find("tr[data-uid='" + currentUid + "']");
            var editButton = $(currenRow).find(".edit-button");
            editButton.hide();
        }
    }
}

function SendTelegram() {
    AjaxReqestHelper(URL('Setup', 'SendTelegram'), { telegramId: CurrentElement.TelegramId }, function () { });
}

function onEdit() {
    $('.k-grid-update').html('<span class="k-icon k-i-check"></span>');
    $('.k-grid-cancel').html('<span class="k-icon k-i-check"></span>');

    $('.k-grid-cancel').on('click', function () {
        $('.k-command-cell>button').html('<span class="k-icon edit-button-ico"></span>');
    });
}

function CreateNewVersion() {
    PromptMessage("New Version", "Create new version of this telegram?", function () { return CreateNewVersionConfirmed(CurrentElement.TelegramId) });

}
function CreateNewVersionConfirmed(telegramId) {
    return AjaxReqestHelper(URL("Setup", "CreateNewVersion"), { telegramId: telegramId }, RefreshData, RefreshData);
}

function DeleteSetup() {
        PromptMessage(Translations["MESSAGE_deleteConfirm"], "?", function () { return DeleteSetupConfirmed(CurrentElement.TelegramId) });

}
function DeleteSetupConfirmed(telegramId) {
    return AjaxReqestHelper(URL("Setup", "DeleteSetup"), { telegramId: telegramId }, RefreshData, RefreshData);
}

$(document).ready(function () {
});

