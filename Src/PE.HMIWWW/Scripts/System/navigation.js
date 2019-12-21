let breadcrumbs = [];
let tmpBreadcrumb;

function openSlideScreen(controller, method = 'Index', parameters = null, title = null) {
  let url = URL(controller, method);
  if ($('#MainScreen > .block-overlay').length <= 0) {
    let overlay = $('<div class="block-overlay"></div>').hide().fadeIn(300);
    $('#MainScreen').prepend(overlay);
    }
    if (title) {
        tmpBreadcrumb = title;
    } else {
        tmpBreadcrumb = controller;
    }
  AjaxReqestHelperSilentWithoutDataType(url, parameters, showSlideScreen, closeSlideScreen);
}

function showSlideScreen(view) {
  view = $(
    '<div id="' + tmpBreadcrumb + '-' + breadcrumbs.length + '" class="container-fluid p-4 detail-overlay-container">' +
    '<div class="breadcrumbs">' + updateBreadCrumbs(tmpBreadcrumb) + '</div>' +
    '<i class="material-icons back-detail-btn" onclick="closeDetailScreen(this)">arrow_forward</i>' +
    '<i class="material-icons close-detail-btn" onclick="closeSlideScreen()">close</i>' +
    view +
    '</div>').hide();/*.fadeIn(1000);*/
  $('#SpecificScreen').append(view);
  view.show('slide', { direction: 'right' }, 1000);
}

function closeSlideScreen() {
  $('.detail-overlay-container').each(function (index, obj) {
    //obj.fadeOut(100, function () {
      $(this).remove();
    //});
  });
  $('.block-overlay').fadeOut(1000, function () {
    $(this).remove();
    tmpBreadcrumb = undefined;
    breadcrumbs = [];
  });
}

function closeDetailScreen(e) {
  $(e).parent().hide('slide', { direction: 'right' }, 1000, function () {
    $(e).parent().remove();
    breadcrumbs.pop();
    if ($('.detail-overlay-container').length <= 0) {
      $('.block-overlay').fadeOut(1000, function () {
        $(this).remove();
        breadcrumbs = [];
        tmpBreadcrumb = undefined;
      });
    }
  });
}

function updateBreadCrumbs(tmpBreadcrumb) {
  breadcrumbs.push({
    name: tmpBreadcrumb,
    orderSeq: breadcrumbs.length
  });
  let breadcrumbsContainer = '';
  $.each(breadcrumbs, function (index, obj) {
    breadcrumbsContainer += '<span id="breadcrumb-' + obj.orderSeq + '" class="breadcrumb-element" data-orderseq="' + obj.orderSeq + '" onClick="goToSlideScreen(this)">' + obj.name + '</span>';
  });
  tmpBreadcrumb = undefined;
  return breadcrumbsContainer;
}

function goToSlideScreen(e) {
  let orderSeq = $(e).data('orderseq');

  $.each(breadcrumbs, function (index, obj) {
    if (obj.orderSeq > orderSeq) {
      $('#' + obj.name + '-'+obj.orderSeq).hide('slide', { direction: 'right' }, 1000, function () {
        $('#' + obj.name + '-' + obj.orderSeq).remove();
      });
      breadcrumbs = breadcrumbs.filter(function (x) {
        return x.orderSeq < obj.orderSeq;
      });
    }
  });
}



/*    |
 *    | 
 *    |   FIRST REVERSE NAVIGATION VERSION
 *    |     COULD BE HELPFULL 
 *   \ /
 *   \/
 * */

var restoreGridsStates = function () {
  $('.k-grid').each(function (i, obj) {
    $(obj).data('kendoGrid').dataSource.read();
  });
};

function loadDataForGrid(e) {

  let grid = e.sender;
  let path = $(location).attr('pathname');
  let gridData = sessionStorage[path];
  if (gridData) {
    let dataArray = JSON.parse(gridData);
    if (typeof dataArray !== 'undefined' && dataArray.length > 0 && grid) {
      // get data for grid
      let data = dataArray.find(x => x.type === 'k-grid' && x.name === $(grid.element).attr('id'));

      if (data && !data.hasBeenSet) {
        dataArray = dataArray.filter(function (obj) {
          return obj.type !== data.type && obj.name !== data.name;
        });
        saveGridState(grid, true);

        grid.setOptions(data.options);

      } else {
        console.log('No data for grid or data has been set alredy');
        return false;
      }
    } else {
      console.log('No data to read for grid');
      return false;
    }
  } else {
    console.log('No data in session storage');
    return false;
  }
  return true;
}

function setTabStripsStates() {
  $('.k-tabstrip').each(function (i, obj) {
    loadTabStripState($(obj).data('kendoTabStrip'));
  });
  //restoreGridsStates();
}

function loadTabStripState(ts) {
  let path = $(location).attr('pathname');
  let dataArray = sessionStorage[path];
  if (dataArray) {
    dataArray = JSON.parse(dataArray);
    if (typeof dataArray !== 'undefined' && dataArray.length > 0 && ts) {
      let data = dataArray.find(x => x.type === 'k-tabStrip' && x.name === $(ts.element).attr('id'));
      if (data && !data.hasBeenSet) {
        ts.select(data.selected);

        dataArray = dataArray.filter(function (obj) {
          return obj.type !== data.type && obj.name !== data.name;
        });

        saveTabStripState(ts, true);
      }
    }
  }
}

function saveGridState(grid, hasBeenSet = false) {
  let path = $(location).attr('pathname');
  //grid = $(this).data('kendoGrid');
  let data = {
    type: 'k-grid',
    name: $(grid.element).attr('id'),
    options: grid.getOptions(),
    selected: grid.dataItem(grid.select()),
    hasBeenSet: hasBeenSet
  };
  saveStateInSessionStorage(path, data);
}

function saveTabStripState(ts = null, hasBeenSet = false) {
  // Init
  let path = $(location).attr('pathname');
  let selectedIndex = $(ts.item).index();
  // Get tab statuses
  //let tabStrip = $(ts.sender.element[0]).attr('id');
  let tabStrip;
  if (ts !== null) {
    tabStrip = $(ts).attr('id');
  } else {
    tabStrip = $(this).attr('id');
  }
  let data = {
    type: 'k-tabStrip',
    name: tabStrip,
    options: '',
    selected: selectedIndex,
    hasBeenSet: hasBeenSet
  };
  saveStateInSessionStorage(path, data);
}

function saveStateInSessionStorage(path, data) {
  try {
    // init
    let dataArray = [];
    // Get session data
    let sessionData = sessionStorage[path];
    // if any session data for this path exist - clear it on the same element
    if (sessionData) {
      console.log('Session on path ' + path + ' exists! Try to override properties...');
      dataArray = JSON.parse(sessionData);
      dataArray = dataArray.filter(function (obj) {
        return obj.type !== data.type && obj.name !== data.name;
      });
    } else {
      console.log('Session on path not exists! Start creating new one.');
    }
    // Add element to array 
    dataArray.push(data);
    // stringify and save in session storage
    sessionStorage[path] = kendo.stringify(dataArray);
  } catch (e) {
    console.log('Error when trying save state of ' + data);
  }
  console.log('saveStateOnSessionStorage() - successful!');
}

function selectRowAfterBack(grid) {
  let path = $(location).attr('pathname');
  let dataArray = sessionStorage[path];
  if (dataArray) {
    dataArray = JSON.parse(dataArray);
    if (typeof dataArray !== 'undefined' && dataArray.length > 0 && grid) {
      let data = dataArray.find(x => x.type === 'k-grid' && x.name === $(grid.element).attr('id'));
      if (data) {
        let gridData = grid.dataSource.data();
        for (let i = 0; i < gridData.length; i++) {
          if (JSON.stringify(gridData[i]) === JSON.stringify(data.selected)) {
            var currenRow = grid.table.find("tr[data-uid='" + gridData[i].uid + "']");
            grid.select(currenRow);
            break;
          }
        }
      }
    }
  }
}

function resetLoadStatusOfAllElements() {
  try {
    let path = $(location).attr('pathname');
    let sessionData = sessionStorage[path];
    let dataArray = JSON.parse(sessionData);

    $.each(dataArray, function (i, obj) {
      obj.hasBeenSet = false;
    });

    sessionStorage[path] = kendo.stringify(dataArray);
  } catch (e) {
    console.log(e);
    return false;
  }

  return true;
}
