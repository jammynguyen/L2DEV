
var timer;
$(document).ready(function () {
  ReadData();

  $('.zone-body').on('mouseover', '.raw-material', function () {
    const info_pane = $('.info-pane');
    let foo = ($(this));
    timer = setTimeout(function () {
      let heat = ""; if (foo.attr('data-heat') !== "null") heat = foo.attr('data-heat');
      let steelGrade = ""; if (foo.attr('data-steelgrade') !== "null") steelGrade = foo.attr('data-steelgrade');
      let workOrder = ""; if (foo.attr('data-workorder') !== "null") workOrder = foo.attr('data-workorder');


      $('#lbl_rawname').html(foo.attr('data-rawmaterialname'));
      $('#lbl_woname').html(workOrder);
      $('#lbl_heat').html(heat);
      $('#lbl_steelgrade').html(steelGrade);

      let left = foo.offset().left;
      if (foo.position().left + info_pane.width() < 1920)
        info_pane.css({ 'left': left + 35 });

      else
        info_pane.css({ 'left': left - info_pane.width() - 35 });

      info_pane.show();
    }, 1000);
  });

  $('.zone-body').on('mouseout', '.raw-material', function () {
    const info_pane = $('.info-pane');
    info_pane.hide();
    clearTimeout(timer);
  });
});

function ReadData() {
  AjaxReqestHelperSilent(URL("Furnace", "GetMaterialInFurnace"), {}, PrintData, PrintData);
}

function PrintData(data) {
  $('.zone-body').find('div').remove();
  for (var i = 0; i < data.length; i++) {
    let place = ".furnace-middle > .zone-body";
    if (i === 0) {
      place = ".furnace-entry > .zone-body";
    }
    else if (i === data.length - 1) {
      place = ".furnace-exit > .zone-body";
    }
    let pointner = "";
    if (data[i].WorkOrderId !== null && data[i].HeatId !== null && data[i].SteelGradeId)
      pointner = "clickable";


    $(place).append('<div id = "' + data[i].RawMaterialId + '"' +
      'onClick="OpenSlideScreen(' + data[i].RawMaterialId + ',' + data[i].WorkOrderId + ',' + data[i].SteelGradeId + ',' + data[i].HeatId + ')"' +
      'class= "raw-material ' + pointner + '"' +
      'data-workorder="' + data[i].WorkOrderName + '"' +
      'data-rawmaterialname="' + data[i].RawMaterialName + '"' +
      'data-steelgrade="' + data[i].SteelgradeCode + '"' +
      'data-heat="' + data[i].HeatName + '"' +
      'data-woid="' + data[i].WorkOrderId + '"' +
      'data-heatid="' + data[i].HeatId + '"' +
      'data-steelgradeid="' + data[i].SteelGradeId + '"' +
      '>' +
      "<div class='raw-name'>" + data[i].RawMaterialName + "</div>" +
      "<div class='raw-length'>" + Math.round(data[i].Length * 100) / 100 + "</div>" +
      "<div class='raw-weight'>" + Math.round(data[i].Weight * 100) / 100 + "</div>" +
      "<div class=seq-number><p>" + data[i].Sorting + "</p></div>" +
      '</div>');
    GetColor(i);

    if (data[i].WorkOrderId !== null) {
      let woDiv = $('*[data-workorderid="' + data[i].WorkOrderId + '"');
      if (woDiv.length > 0) {
        if ($('div.work-order').last().attr('data-workorderid') == data[i].WorkOrderId) {
          let width = ($('.raw-material').last().position().left - woDiv.first().position().left) + 15;
          woDiv.width(width);
        }
        else {
          let left = $(place + ' >  div.raw-material').last().position().left + 3;
          let color = GetColorModulo16(data[i].WorkOrderId);
          $(place).append("<div onclick='OpenSlideWorkOrderScreen(" + data[i].WorkOrderId + ")' class='work-order detail-block' title=" + data[i].WorkOrderName + " style='width:22px;' data-workorderid = '" + data[i].WorkOrderId + "'>" + data[i].WorkOrderName + "</div>");
          $('*[data-workorderid="' + data[i].WorkOrderId + '"').last().css({ 'left': left });
          $('*[data-workorderid="' + data[i].WorkOrderId + '"').last().css({ 'background-color': color });
        }
      }
      else {
        let left = $(place + ' >  div.raw-material').last().position().left + 3;
        let color = GetColorModulo16(data[i].WorkOrderId);
        $(place).append("<div onclick='OpenSlideWorkOrderScreen(" + data[i].WorkOrderId + ")' class='work-order detail-block' title=" + data[i].WorkOrderName + " style='width:22px;' data-workorderid = '" + data[i].WorkOrderId + "'>" + data[i].WorkOrderName + "</div>");
        $('*[data-workorderid="' + data[i].WorkOrderId + '"').last().css({ 'left': left });
        $('*[data-workorderid="' + data[i].WorkOrderId + '"').last().css({ 'background-color': color });
      }
      if (i === 0) {
        $('.detail-block[data-workorderid="' + data[i].WorkOrderId + '"').last().css({ 'top': $('.detail-block[data-workorderid="' + data[i].WorkOrderId + '"').last().position().top + 1 })
      }
    }
    if (data[i].HeatId !== null) {
      let heatDiv = $('div.heat[data-heatid="' + data[i].HeatId + '"');
      if (heatDiv.length > 0) {
        if ($('div.heat').last().attr('data-heatid') == data[i].HeatId) {
          let width = ($('.raw-material').last().position().left - heatDiv.last().first().position().left) + 15;
          heatDiv.last().width(width);
        }
        else {
          let left = $(place + ' > div.raw-material').last().position().left + 3;
          let color = GetColorModulo16(data[i].HeatId);
          $(place).append("<div onclick='OpenSlideHeatScreen(" + data[i].HeatId +")' title=" + data[i].HeatName + " class='heat detail-block' style='width: 22px; ' data-heatid = '" + data[i].HeatId + "'>" + data[i].HeatName + "</div>");
          $('.detail-block[data-heatid="' + data[i].HeatId + '"').last().css({ 'left': left });
          $('.detail-block[data-heatid="' + data[i].HeatId + '"').last().css({ 'background-color': color });
        }
      }
      else {
        let left = $(place + ' > div.raw-material').last().position().left + 3;
        let color = GetColorModulo16(data[i].HeatId);
        $(place).append("<div onclick='OpenSlideHeatScreen(" + data[i].HeatId +")' title=" + data[i].HeatName + " class='heat detail-block' style='width:22px;' data-heatid = '" + data[i].HeatId + "'>" + data[i].HeatName + "</div>");
        $('.detail-block[data-heatid="' + data[i].HeatId + '"').last().css({ 'left': left });
        $('.detail-block[data-heatid="' + data[i].HeatId + '"').last().css({ 'background-color': color });
      }
      if (i === 0) {
        $('.detail-block[data-heatid="' + data[i].HeatId + '"').last().css({ 'top': $('.detail-block[data-heatid="' + data[i].HeatId + '"').last().position().top +1 })
      }
    }
  }
}

function OpenSlideScreen(id, workorderid, steelgradeid, heatid) {
  let dataToSend = {
    RawMaterialId: id,
    WorkOrderId: workorderid,
    SteelgradeId: steelgradeid,
    HeatId: heatid
  };
  if (workorderid !== null || steelgradeid !== null || heatid !== null)
    openSlideScreen('Furnace', 'MaterialDetails', dataToSend);
}

function OpenSlideWorkOrderScreen(workorderid) {
  let dataToSend = {
    workOrderId: workorderid
  };
  openSlideScreen('WorkOrder', 'ElementDetails', dataToSend);
}
function OpenSlideHeatScreen(heatId) {
  let dataToSend = {
    heatId: heatId
  };
  openSlideScreen('Heat', 'ElementDetails', dataToSend);
}

function GetColor(index) {
  if (index > 8)
    index = 8;

  let color = colorArray[index];

  $('.raw-material').last().css({ 'background-color': color });
  $('.raw-material>div.raw-weight').last().css({ 'background-color': 'rgb(255,255,255,0.5)' });
  $('.raw-material>div.raw-length').last().css({ 'background-color': 'rgb(255,255,255,0.3)' });
}

const colorArray = ['#2D2D2D', '#545454', '#656564', '#6F6761', '#817166', '#958071', '#AD805D', '#CC7E42', '#E77721'];