var popupFormSave = false;
var currentPopup;

//POPUP WINDOW
var popupIsColsed = true;
function OnPopupShow() {
  popupFormSave = false;
  var allForms = $('form');
  $.each(allForms, function (key, value) {
    var val = $.data(value, 'validator');
    if (val !== undefined) {
      var settngs = val.settings;
      settngs.onfocusout = function (element) { $(element).valid(); };
    }
  });

  $.each(allForms, function (key, value) {
    var val = $.data(value, 'validator');
    if (val !== undefined) {
      var settngs = val.settings;

      var oldErrorFunction = settngs.errorPlacement;
      settngs.errorPlacement = function (error, inputElement) {
        $(inputElement).closest('.fieldset').addClass('fieldset-validation-error');
        oldErrorFunction(error, inputElement);

        $('.field-validation-error').each(function () {

          var fieldName = $(this).attr('data-valmsg-for');
          $("#" + fieldName).attr('title', $(this).text());

          $(this).hide();
        }
        );
      };
    }
  });
}

function popupShowFunction() {
  OnPopupShow();
}
function popupError(e) {
  PeErrorHandler(e);
}
function OpenUrlInPopupWindow(popupUrl, windowWidth, windowHeight, dataToSend, popupAfterCloseFunction) {
  $.ajax({
    type: 'GET',
    dataType: 'html',
    data: dataToSend,
    url: popupUrl,
    success: function (result) {
      var options =
      {
        modal: 0,
        type: 'html',
        afterOpen: popupShowFunction,
        afterClose: function () { if (popupFormSave) popupAfterCloseFunction },
        beforeClose: function () { popupIsColsed = true; },
        error: popupError,
        content: result,
        width: windowWidth,
        height: windowHeight ? windowHeight : 'auto',
        preloaderContent: '<img src="../Content/System/Img/ajax-loader-white-bg.gif" class="preloader">'
      };
      if (popupIsColsed) {
        popupIsColsed = false;
        currentPopup = new $.Popup(options);
        currentPopup.open();
      }
    },
    error: PePopupErrorHandler
  });
}


function JsonPostToOpenUrlInPopupWindow(popupUrl, windowWidth, windowHeight, dataToSend, popupAfterCloseFunction) {
  $.ajax({
    type: 'POST',
    dataType: "html",
    data: dataToSend,
    url: popupUrl,
    contentType: "application/json; charset=utf-8",
    success: function (result) {
      var options =
      {
        modal: 0,
        type: 'html',
        afterOpen: popupShowFunction,
        afterClose: popupAfterCloseFunction,
        beforeClose: function () { popupIsColsed = true; },
        error: popupError,
        content: result,
        width: windowWidth,
        height: windowHeight ? windowHeight : 'auto',
        preloaderContent: '<img src="../Content/System/Img/ajax-loader-white-bg.gif" class="preloader">'
      };
      if (popupIsColsed) {
        popupIsColsed = false;
        currentPopup = new $.Popup(options);
        currentPopup.open();
      }
    },
    error: PePopupErrorHandler
  });
}


function OpenInPopupWindow(config) {
  var popupUrl = serverAddress + '//' + config.controller + '//' + config.method;
  $.ajax({
    type: 'GET',
    dataType: 'html',
    data: (config.data != null ? config.data : {}),
    url: popupUrl,
    success: function (result) {

      var icon = '<img src="/Content/Functions/Big/' + (config.icon != null ? config.icon : "edit-white") + '.png"/>';
      var options =
      {
        modal: 0,
        type: 'html',
        afterOpen: popupShowFunction,
        afterClose: config.afterClose !== null ? config.afterClose : undefined,
        beforeClose: function () { popupIsColsed = true; },
        error: popupError,
        content: result,
        width: config.width !== null ? config.width : 400,
        height: config.height !== null ? config.height : 'auto',
        preloaderContent: '<img src="../Content/System/Img/ajax-loader-white-bg.gif" class="preloader">'
      };
      if (popupIsColsed) {
        popupIsColsed = false;
        currentPopup = new $.Popup(options);
        currentPopup.open();
        $('.popup-header-img:not(.info-header-img)').append(icon);

        $('.popup-input').removeClass('k-textbox');


      }
    },
    error: PePopupErrorHandler
  });
}

function ClosePopup() {
  if (!popupIsColsed) {
    currentPopup.close();
    popupIsColsed = true;
  }
}

function OnFormBegin(e) {
  RequestStarted();
}
function OnFormSuccess(e) {
  RequestFinished();
  ClosePopup();
  PositiveResultNotification();
  ModuleWarningHandler(e);
  popupFormSave = true;
  //try {
  //    RefreshData();
  //}
  //catch (ex) {
  //    console.error(ex);
  //}
}
function OnFormSuccessAddOrDelete(e) {

  RequestFinished();
  if (e === "Save") {
    RequestFinished();
    ClosePopup();
  }
  PositiveResultNotification();

  //try {
  //    RefreshData();
  //}
  //catch (ex) {
  //    console.error(ex);
  //}
}
function OnFormError(e) {
  RequestFinished();
  //$("#header").css('background', '#ce0037').css('color', '#fff');
  setPopupFooterErrorColor();
  PeErrorHandler(e);
  $('.formButtonSubmit').attr("disabled", false);

}

function ModuleWarningHandler(data) {
  if (data.ModuleWarningMessage !== null) {
    var message = "";
    var cnt = 0;
    var moduleWarningMessage = data.ModuleWarningMessage;
    do {
      var warningMessages = moduleWarningMessage.WarningMessage;
      message += moduleWarningMessage.ModuleName + "\n";
      warningMessages.forEach(function (w) {
        message += w.PreparedMessage + "\n";
      });
      if (moduleWarningMessage.InnerModuleWarningMessage !== null && moduleWarningMessage.WarningMessage !== null && message !== "") {
        moduleWarningMessage = moduleWarningMessage.InnerModuleWarningMessage;
      } else {
        break;
      }
      cnt++;
    } while (cnt < 10);
    WarningMessage(message);
  }
}

//END OF POPUP WINDOW