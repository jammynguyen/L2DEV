var target;

//$(window).bind("load", function () {
//    target = new EventTarget();
//});
function InitRefreshHandler() {

  try {
    HmiHub.client.System2HmiRefresh = function (refreshData) {
      console.log("refresh: " + refreshData.RefreshKey);
      FireRefreshEvent(refreshData.RefreshKey);
    };
  }
  catch (err) {
    console.log(err);
  }
}



function EventTarget() {
  this._listeners = {};
}

EventTarget.prototype = {

  constructor: EventTarget,


  fire: function (refreshKey) {
    //if (typeof parameter == "string") {
    //    parameter = { refreshKey: parameter };
    //}


    if (!refreshKey) {  //falsy
      console.log("Event object missing 'type' property.");
      throw new Error("Event object missing 'type' property.");
    }

    if (this._listeners[refreshKey] instanceof Array) {
      var listeners = this._listeners[refreshKey];

      if (listeners.length > 0) {
        console.log("SYS REFRESH. Key: " + refreshKey + " No of listeners: " + listeners.length);
      }

      for (var i = 0, len = listeners.length; i < len; i++) {
        try {
          listeners[i].call(this, refreshKey);
        }
        catch (ex) {
          console.log("Exception during triggering refresh event with key: " + refreshKey + " : " + ex);
        }
      }
    }
  },
  addListener: function (refreshKey, listener) {
    if (typeof this._listeners[refreshKey] === "undefined") {
      this._listeners[refreshKey] = [];
    }

    this._listeners[refreshKey].push(listener);

    //target.fire(refreshKey);

  },
};


function PrepareRefreshEvent() {
  target = new EventTarget();
}
function RegisterMethod(refreshKey, methodToCall) {
  if (target === undefined)
    PrepareRefreshEvent();
  target.addListener(refreshKey, methodToCall);
}
function FireRefreshEvent(refreshKey) {
  if (target === undefined)
    PrepareRefreshEvent();
  target.fire(refreshKey);
}