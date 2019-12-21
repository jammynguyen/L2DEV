var HmiHub;
var serverAddress;
//var currentPopup;
var SignalrConnection;
//var popupFormSave = false;

// PAGE EVENTS
$(window).bind("load", function () {
  //FindAllTelerikGridsAndBindErrorHandler();
});
$(document).ready(function () {

  SignalrConnection = $.connection.hub;
  GetServerAddress();
  SetupSignalRHub();
  HmiHub.client.System2HmiBroadcastShowAlarmPopup = function (alarmData) {
    ShowAlarmPopup(alarmData);
  };


  InitNotification();
  InitRefreshHandler();


  setTimeout(function () {
    SetupSignalR();
  }, 500); // Delay start of signalr


});
// END OF PAGE EVENTS


//HELPER FUNCTIONS
function GetServerAddress() {
  //sf - fix for IE
  if (!window.location.origin) {
    window.location.origin = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
  }
  serverAddress = window.location.origin;
}
function URL(controller, method) {
  return serverAddress + "/" + controller + "/" + method;
}


function printState(state) {
  return ["connecting", "connected", "reconnecting", state, "disconnected"][state];
}

function getQueryVariable(variable) {
  var query = window.location.search.substring(1),
    vars = query.split("&"),
    pair;
  for (var i = 0; i < vars.length; i++) {
    pair = vars[i].split("=");
    if (pair[0] === variable) {
      return unescape(pair[1]);
    }
  }
}
function StartSignalR() {
  if (printState(SignalrConnection.state) === "connected") {
    SignalrConnection.stop();
  } else if (printState(SignalrConnection.state) === "disconnected") {
    var activeTransport = getQueryVariable("transport") || "auto";
    SignalrConnection.start({ transport: activeTransport })
      .done(function () {
        console.log("connection started. Id=" + SignalrConnection.id + ". Transport=" + SignalrConnection.transport.name);
      })
      .fail(function (error) {
        console.log(error);
      });
  }
}
function SetupSignalRHub() {

  SignalrConnection.hub = serverAddress + "/signalr";
  HmiHub = $.connection.HmiHub;
}
function SetupSignalR() {

  SignalrConnection.logging = true;

  SignalrConnection.connectionSlow(function () {
    console.log("SIGNALR - connectionSlow");
  });
  SignalrConnection.disconnected(function () {
    console.log("SIGNALR - disconnected");
    setTimeout(function () {
      $.SignalrConnection.start();
      console.log("SIGNALR reconnecting...");
    }, 3000); // Restart connection after 3 seconds.
  });
  SignalrConnection.error(function (error) {
    console.log(error);
  });
  SignalrConnection.reconnected(function () {
    console.log("SIGNALR - reconnected");
  });
  SignalrConnection.reconnecting(function () {
    console.log("SIGNALR - reconnecting");
  });
  SignalrConnection.starting(function () {
    console.log("SIGNALR - starting");
  });
  SignalrConnection.stateChanged(function (state) {
    console.log("SIGNALR - stateChanged: " + printState(state.oldState) + " => " + printState(state.newState));
  });
  HmiHub.client.hubMessage = function (data) {
    console.log("SIGNALR - hubMessage: " + data);
  };

  var activeTransport = getQueryVariable("transport") || "auto";
  SignalrConnection.start({ transport: activeTransport })
    .done(function () {
      console.log("connection started. Id=" + SignalrConnection.id + ". Transport=" + SignalrConnection.transport.name);
    })
    .fail(function (error) {
      console.log(error);
    });

  $.ajaxSetup({
    cache: false
  });
}

// TELERIK GRID ERROR HANDLING 
function PeErrorHandler(e) {
  if (e.status === 200) {
    //alert("Dear developer, You fucked something up!. Probably object returned in controller is not matching ajax request."); 
    return;
  }
  if (e.status === 500) {
    ErrorMessage(e.responseJSON.Errors);
  }
  else if (e.status === 403) {
    ErrorMessage(e.responseJSON.Data.Errors);
  }

  else {
    ErrorMessage("Internal server error.");
  }
}
function PePopupErrorHandler(e) {
  if (e.status === 200)
    return;
  if (e.status === 500) {
    ErrorMessage(e.statusText);
  }
  else if (e.status === 403) {
    ErrorMessage(e.statusText);
  }

  else {
    ErrorMessage("Internal server error.");
  }
}
function TelerikErrorHandler(e) {

  var returnedObject;
  $('.k-grid').each(function () {
    var grid = $(this).data('kendoGrid');
    if (grid.dataSource === e.sender) {
      returnedObject = grid;
    }
  });
  $('.k-scheduler').each(function () {
    var scheduler = $(this).data('kendoScheduler');
    if (scheduler.dataSource === e.sender) {
      returnedObject = scheduler;
    }
  });
  $('.k-chart').each(function () {
    var chart = $(this).data('kendoChart');
    if (chart.dataSource === e.sender) {
      returnedObject = chart;
    }
  });
  $('.k-widget.k-grid.k-editable').each(function () {
    var grid = $(this).data('kendoGrid');
    if (grid.dataSource === e.sender) {
      returnedObject = grid;
    }
  });
  TelerikError(e, returnedObject, null);
}
function TelerikError(e, source, status) {

  if (e.xhr !== null) {
    if (e.xhr.status === 403) {
      //var grid1 = $("#" +source.element[0].id).data("kendoGrid");
      var grid = $("#" + source.element[0].id)[0];
      grid.children[0].style.display = "none";
      grid.style.padding = "5px";
      grid.style.color = "#ff917e";

      grid.innerHTML = e.xhr.responseJSON.Data.Errors + " !";

    }
    else if (e.xhr.status === 500) {
      //internal server error
      if (e.xhr.responseJSON)
        ErrorMessage(e.xhr.responseJSON.Errors); //changed for grid
      else
        console.log(e.xhr.responseText);
    }
    else {
      //all other
      ErrorMessage(e.xhr.responseJSON.Data.Errors);
    }
  }
  else if (e.errors) {
    var message = "";
    $.each(e.errors, function (key, value) {
      if ('errors' in value) {
        $.each(value.errors, function () {
          message += this + "\n";
        });
      }
    });
    ErrorMessage(message);
  }
  else {
    ErrorMessage("Internal server error.");
  }
}
// END OF TELERIK GRID ERROR HANDLING 
//base page functions
function Refresh() {
  window.location.href = window.location.href;
}
function Back() {
  window.history.back();
}
function Print() {
  window.print();
}
function GotoUrl(url) {
  window.location.assign(serverAddress + "/" + url);
}
// end base page functions

// AJAX HELPER
function AjaxReqestHelper(targetUrl, dataToSend, onSuccessCustomMethod, onErrorCustomMethod) {

  RequestStarted();
  $.ajax({
    type: 'POST',
    dataType: "json",
    url: targetUrl,
    traditional: true,
    data: dataToSend,
    success: function (data) {
      PositiveResultNotification();
      try {
        onSuccessCustomMethod(data);
        ModuleWarningHandler(data);
      }
      catch (ex) { console.log(ex); }
    },
    complete: RequestFinished,
    error: function (data) {
      PeErrorHandler(data);
      try {
        onErrorCustomMethod();
      }
      catch (ex) { console.log(ex); }
    }

  });
}
function AjaxReqestHelperSilent(targetUrl, dataToSend, onSuccessCustomMethod, onErrorCustomMethod) {


  $.ajax({
    type: 'POST',
    dataType: "json",
    url: targetUrl,
    traditional: true,
    data: dataToSend,

    success: function (data) {
      console.log("Request on url: " + targetUrl + " SUCCESSFULL");
      try {
        onSuccessCustomMethod(data);
      }
      catch (ex) { console.log(ex); }
    },
    error: function (data) {
      console.log("ERROR during request on url: " + targetUrl);
      try {
        onErrorCustomMethod();
      }
      catch (ex) { console.log(ex); }
    }
  });
}
function AjaxReqestHelperSilentWithoutDataType(targetUrl, dataToSend, onSuccessCustomMethod, onErrorCustomMethod) {


  $.ajax({
    type: 'POST',
    url: targetUrl,
    traditional: true,
    data: dataToSend,

    success: function (data) {
      console.log("Request on url: " + targetUrl + " SUCCESSFULL");
      try {
        onSuccessCustomMethod(data);
      }
      catch (ex) { console.log(ex); }
    },
    error: function (data) {
      console.log("ERROR during request on url: " + targetUrl);
      try {
        onErrorCustomMethod();
      }
      catch (ex) { console.log(ex); }
    }
  });
}
function AjaxGetDataHelper(targetUrl, dataToSend) {
  var result;
  $.ajax({
    type: 'POST',
    dataType: 'json',
    url: targetUrl,
    async: false,
    data: dataToSend !== null ? dataToSend : undefined,
    success: function (data) {
      // console.log("Request on url: " + targetUrl + " SUCCESSFULL");
      result = data;
    },
    error: function (data) {
      console.log("ERROR during request on url: " + targetUrl);
      //try {
      //  onErrorCustomMethod();
      //}
      //catch (ex) { console.log(ex); }
    }
  });
  return result;
}
//END OF AJAX HELPER


// ALARM HANDLING
function RefreshAlarmGridAndClose() {
  ClosePopup();
  $("#AlarmGrid").data('kendoGrid').dataSource.read();
}
function SubmitAlarmForm(alarmId) {

  url = serverAddress + "/alarm/Confirm";
  data = 'alarmId=' + alarmId;

  AjaxReqestHelper(url, data, RefreshAlarmGridAndClose);
}
function RefreshAlarmGrid() {
  $("#AlarmGrid").data('kendoGrid').dataSource.read();
}
function ConfirmAlarm(alarmId) {

  var url = serverAddress + "/alarm/Confirm";
  var data = 'alarmId=' + alarmId;

  AjaxReqestHelper(url, data, RefreshAlarmGrid);
}
function DetailsAlarm(alarmId) {

  OpenInPopupWindow({
    controller: 'Alarm',
    method: 'DetailsDialog',
    width: 500,
    height: 310,
    data: { id: alarmId }
  });
}
function ShowAlarmPopup(alarmData) {
  AlarmMessage(alarmData.Message, alarmData.AlarmTypeId, alarmData.NeedConfirmation, function () { ConfirmAlarm(alarmData.AlarmId); });
}
//END OF ALARM HANDLING

// colors
var colors = ["f2a900", "6ba4b8", "00b5e2", "cedc00", "97999b", "c6aa76", "5c462b", "7a9a01", "00587c", "8c4799", "500778", "ffcd00", "ce0037", "971b2f", "425563", "e87722"];

function GetColorById(colorId) {
  return "#" + colors[colorId];
}
function GetColorModulo16(elementId) {
  return "#" + colors[elementId % 16];
}
// end of colors

//HMI OPERATION REQUEST ON MODULES
function ModuleOperationRequest(operationId, RefreshData, onSuccessCustomMethod, onErrorCustomMethod) {
  var url = serverAddress + "/home/ModuleOperationRequest";
  var data = { Id: operationId };
  AjaxReqestHelper(url, data, RefreshData, onSuccessCustomMethod, onErrorCustomMethod);
}
//END OF HMI OPERATION REQUEST ON MODULES


// ARRAYS COMPARISON HELPER FUNCTION

if (Array.prototype.equals) {
  console.warn("Overriding existing Array.prototype.equals. Possible causes: New API defines the method, there's a framework conflict or you've got double inclusions in your code.");
}
// attach the .equals method to Array's prototype to call it on any array
Array.prototype.equals = function (array) {
  // if the other array is a falsy value, return
  if (!array) {
    return false;
  }

  // compare lengths - can save a lot of time
  if (this.length !== array.length) {
    return false;
  }

  for (var i = 0, l = this.length; i < l; i++) {
    // Check if we have nested arrays
    if (this[i] instanceof Array && array[i] instanceof Array) {
      // recurse into the nested arrays
      if (!this[i].equals(array[i])) {
        return false;
      }
    }
    else if (this[i] !== array[i]) {
      // Warning - two different object instances will never be equal: {x:20} != {x:20}
      return false;
    }
  }
  return true;
};
// Hide method from for-in loops
Object.defineProperty(Array.prototype, "equals", { enumerable: false });


function setCookie(cname, cvalue, exdays) {
  var d = new Date();
  d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
  var expires = "expires=" + d.toUTCString();
  document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
  var name = cname + "=";
  var ca = document.cookie.split(';');
  for (var i = 0; i < ca.length; i++) {
    var c = ca[i];
    while (c.charAt(0) === ' ') {
      c = c.substring(1);
    }
    if (c.indexOf(name) === 0) {
      return c.substring(name.length, c.length);
    }
  }
  return "";
}


// helper method to conver asp datetime to js datetime object
function toJavaScriptDate(value) {
  let pattern = /Date\(([^)]+)\)/;
  let results = pattern.exec(value);
  let dt = new Date(parseFloat(results[1]));
  return dt;
}

function setPopupFooterErrorColor() {
  $("#popup-footer").css('background', '#ce0037').css('color', '#fff');
}

// Kendo grid helpers
function refreshKendoGrid(gridName) {
    let grid = $(`#${gridName}`).data('kendoGrid');

    if (!grid) {
        throw `There is no Kendo Grid with name '${gridName}'`;
    }

    grid.dataSource.read();
    grid.refresh();
    return true;
}

function getUrlParameter(sParam) {
  var sPageURL = window.location.search.substring(1),
    sURLVariables = sPageURL.split('&'),
    sParameterName,
    i;

  for (i = 0; i < sURLVariables.length; i++) {
    sParameterName = sURLVariables[i].split('=');

    if (sParameterName[0] === sParam) {
      return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
    }
  }
}

