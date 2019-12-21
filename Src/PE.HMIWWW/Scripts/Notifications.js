var requestStatusBar;

function InitNotification () {

    requestStatusBar = new $.peekABar();

    HmiHub.client.System2HmiNotification = function (notification) {

        try {
            RefreshData();
        }
        catch (ex) {
            console.trace(ex);
        }

        if (notification.NotificationTarget === 1) {
            if (notification.NotificationType == 1) {
                SuccessNotification(notification.NotivicationText);
            }
            else
                ErrorNotification(notification.NotivicationText);
        }

    };

}





//ERROR MESSAGES
function WarningMessage(message) {
	swal({
	    title: Translations["warning"], text: message, type: "warning", confirmButtonText: "OK"
	});
}
function ErrorMessage(message) {
	swal({
	    title: Translations["error"], text: message, type: "error", confirmButtonText: "OK"
	});
}
function InfoMessage(message) {
	swal({
	    title: Translations["info"], text: message, type: "info", confirmButtonText: "OK"
	});
}
function SuccessMessage(message) {
	swal({
	    title: Translations["success"], text: message, type: "success", timer: 2000, confirmButtonText: "OK"
	});
}
function PromptMessage(question, information, functionToRun) {
  swal({
    title: question,
    text: information,
    type: "warning",
    showCancelButton: true,
    confirmButtonColor: "#f2a900",
    confirmButtonText: Translations["yes"],
    cancelButtonText: Translations["cancel"],
    closeOnConfirm: true,
    closeOnCancel: true
  }, functionToRun);

	//http://t4t5.github.io/sweetalert/
	//swal({ title: "An input!", text: "Write something interesting:", type: "input", showCancelButton: true, closeOnConfirm: false, animation: "slide-from-top", inputPlaceholder: "Write something" }, function (inputValue) { if (inputValue === false) return false; if (inputValue === "") { swal.showInputError("You need to write something!"); return false } swal("Nice!", "You wrote: " + inputValue, "success"); });
}
function AlarmMessage(message, alarmType, confirmation, functionToRun) {

    if (confirmation)
    {
        if (alarmType == 3)
            alarmTypeText = "error";
        else if (alarmType == 2)
            alarmTypeText = "warning";
        else if (alarmType == 1)
            alarmTypeText = "info";

        swal({ title: Translations[("alarmtype_" + alarmType)], text: message, type: alarmTypeText, showCancelButton: true, confirmButtonColor: "#f2a900", confirmButtonText: Translations["alarmConfirm"], cancelButtonText: Translations["close"], closeOnConfirm: true }, functionToRun);
    }
    else
    {
        if (alarmType == 3)
            ErrorMessage(message);
        else if (alarmType == 2)
            WarningMessage(message);
        else if (alarmType == 1)
            InfoMessage(message);
    }

}
//END OF ERROR MESSAGES



//NOTIFICATION MESSAGES

function ErrorNotification(messageText) {
	var notification = $("#notification").data("kendoNotification");
	notification.show({
	    title: Translations["error"],
		message: messageText
	}, "error");
}
function SuccessNotification(messageText) {
	var notification = $("#notification").data("kendoNotification");
	notification.show({
	    title: Translations["success"],
		message: messageText
	}, "success");
}

function PositiveResultNotification() {
        SuccessNotification(Translations["operationSuccessful"]);
}

function NegativeResultNotification() {
  ErrorNotification(Translations["operationFailed"]);
}

//END OF NOTIFICATION MESSAGES

//REQUEST STATUS MESSAGES

function RequestStarted() {
	requestStatusBar.show({
		html: '<div><img src="../Content/System/Img/ajax-loader.gif" /></div>',
		backgroundColor: '#f2a900'
	});
}
function RequestFinished() {
	requestStatusBar.hide();
}


//END OF REQUEST STATUS MESSAGES


