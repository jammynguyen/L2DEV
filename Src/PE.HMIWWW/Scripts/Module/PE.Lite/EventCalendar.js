var date = new Date();
var $shiftDefinition;
var firstInit = true;
var $click = false; //flaga czy info popup został otwarty przez dblclick, jeżeli tak nie chowa się

//DATE HELPERS
//formatowanie minut
function formatMinutes(minutes) {
    if (minutes < 10)
        return '0' + minutes;
    else return minutes;
}
//format daty
function formatDate(date) {
    var d = new Date(date);
    var hour = d.getHours() < 10 ? '0' + d.getHours() : d.getHours();
    var year = d.getFullYear();
    var min = d.getMinutes() < 10 ? '0' + d.getMinutes() : d.getMinutes();
    var day = d.getDate() < 10 ? '0' + d.getDate() : d.getDate();
    var month = d.getMonth() < 9 ? '0' + (d.getMonth() + 1) : d.getMonth() + 1;

    return year + '-' + month + '-' + day + " " + hour + ':' + min;// + ' ' + day + "-" + month + '-' + year;
}
//pobieranie liczby dni w wybranym miesiącu
function daysInMonth(date) {
    return new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
}

$(document).ready(function () {
    //MONTH CHANGE
    $('#dateName').text(date.getMonth() + 1 + '-' + date.getFullYear());
    //zmiana miesiąca na następny
    $('#next').on('click', function () {
        date.setMonth(date.getMonth() + 1);
        $('#dateName').text(date.getMonth() + 1 + '-' + date.getFullYear());
        RefreshTable();
    });
    // zmiana miesiąca na poprzedni
    $('#prev').on('click', function () {
        date.setMonth(date.getMonth() - 1);
        $('#dateName').text(date.getMonth() + 1 + '-' + date.getFullYear());
        RefreshTable();
    });
    // powrót do dnia dzisiejszego
    $('#today').on('click', function () {
        date = new Date();
        $('#dateName').text(date.getMonth() + 1 + '-' + date.getFullYear());
        RefreshTable();
    });


    //zamykanie info popup
    $('.info-pane-close').on('click', function () {
        $click = false;
        $('.info-pane').hide();
    });
});

//po zmianie daty odświeża dane w kalendarzu, ukrywa lub pokazuje dni wg ilości w miesiącu
function RefreshTable() {
    $('.event').remove();
    var days = daysInMonth(date);
    for (var j = 28; j < 32; j++) {
        if (j > days)
            $('.tr-' + j).hide();
        else
            $('.tr-' + j).show();
    }
    var multiselect = $("#filters").data("kendoMultiSelect").value();
    for (var i = 0; i < multiselect.length; i++) {
        readData(multiselect[i]);
    }
}

// funkcja init do generowania tabeli, parametry wejściowe:id diva, godzina rozpoczęcia,godzina zakończenia, definicja zmian z bazy
function GenerateTable(options) {
    let element = $('#' + options.id);
    let width = 1344 / Math.ceil(((options.endHour - options.startHour))) + 'px';
    var head = "<table id='event-calendar' data-start=" + options.startHour + " data-end=" + options.endHour + " class='table border-table event-table'><thead><tr><th></th><th></th>";
    //print Head
    for (var i = options.startHour; i < options.endHour; i++) {
        head += "<th scope='col' class='hour-head' style='min-width:" + width + ";max-width:" + width + "'>" + i + "</th>";
    }
    head += "</tr></thead>";
    var body = "<tbody>";
    $shiftDefinition = options.shiftDefinition;
    //print body
    for (let index = 1; index < 32; index++) {
        body += "<tr class='tr-" + index + "'><th class='head' id='head-" + index + "'><div class='head-date'>" + index + "</div></th><th class='event-name'></th>";
        for (let hour = options.startHour; hour < options.endHour; hour++) {
            let shiftClass;
            for (var s = 0; s < $shiftDefinition.length; s++) {
                if ($shiftDefinition[s].EndNextDay === true && (hour >= $shiftDefinition[s].ShiftStart.Hours || hour < $shiftDefinition[s].ShiftEnd.Hours))
                    shiftClass = 'shift-' + s;
                else if (hour >= $shiftDefinition[s].ShiftStart.Hours && hour < $shiftDefinition[s].ShiftEnd.Hours)
                    shiftClass = 'shift-' + s;

            }
            body += '<td class="' + shiftClass + '" style="min-width: ' + width + ';max-width: ' + width + '" id="' + index + '-' + hour + '"><div class="td-div"></div></td>';
        }
    }
    body += "</tbody></table>";

    let days = daysInMonth(date);
    for (var j = 28; j < 32; j++) {
        if (j > days)
            $('.tr-' + j).hide();
    }

    element.append(head + body);
    putDateChangLine();
}
// pogrubienie lini pomiędzy dniami
function putDateChangLine() {
    $('.day-change-line').removeClass('day-change-line');
    var numberOfRows = daysInMonth(date);
    for (var i = 1; i < numberOfRows + 1; i++) {
        $('.tr-' + i).last().children().addClass('day-change-line');
    }
}

//dodawanie nowego wiersza w obrębie dnia z nowo wybranym zdarzeniem
function onSelect(e) {

    //debugger
    if ($('.k-input')) {
        $('.k-input').css("width", "100%");
    }

    var dataItem = e.dataItem;
    var multiselect = $("#filters").data("kendoMultiSelect").value();
    var numberOfRows = daysInMonth(date);
    var startHour = $('#event-calendar').data('start');
    var endHour = $('#event-calendar').data('end');
    if (multiselect.length > 0) {
        for (var i = 1; i < 32; i++) {
            var row =
                "<tr class='tr-" + i + "' data-eventId=" + dataItem.EventType + ">" +
                "<th class='head'><div class='head-date'></div></th>" +
                "<th class='event-name'><img src='/Content/Functions/Small/" + dataItem.EventIcon + "'></th>";


            for (var j = startHour; j < endHour; j++) {
                let shiftClass;
                for (var s = 0; s < $shiftDefinition.length; s++) {
                    if ($shiftDefinition[s].EndNextDay === true && (j >= $shiftDefinition[s].ShiftStart.Hours || j < $shiftDefinition[s].ShiftEnd.Hours))
                        shiftClass = 'shift-' + s;
                    else if (j >= $shiftDefinition[s].ShiftStart.Hours && j < $shiftDefinition[s].ShiftEnd.Hours)
                        shiftClass = 'shift-' + s;


                }
                row += '<td class="' + shiftClass + '" id="' + dataItem.EventType + '-' + i + '-' + j + '"><div class="td-div"></div></td>';
            }
            row += "</tr>";
            $('#event-calendar>tbody>tr.tr-' + i + ':last').after(row);
            if (i > numberOfRows)
                $('.tr-' + i).hide();
        }
    }
    else {
        $('#event-calendar tr').attr('data-eventId', dataItem.EventType);
        $('#event-calendar tr th.event-name').html("<img src='/Content/Functions/Small/" + dataItem.EventIcon + "'>");

            let tr = $('tbody tr');
            tr.each(function (i) {
                let td = $(this).find('td');
                td.each(function (j) {
                    $(this).attr('id', null);
                    $(this).attr('id', i + 1 + '-' + j);
                });
            })
            $('#event-calendar').find('td').each(function () {
                $(this).attr('id', dataItem.EventType + '-' + $(this).attr('id'));
           
            })
        }

    
    putDateChangLine();

    readData(dataItem.EventType);
}

//remove row from the calendar after deleting from select list
function onDeselect(e) {
    var events = $('tr');
    var dataItem = e.dataItem;
    var multiselect = $("#filters").data("kendoMultiSelect").value();

    //if (multiselect[0] === dataItem.EventType) {
    //$('#event-calendar>tbody> tr').attr('data-eventid', null);
    //    $('#event-calendar tr th.event-name').html('');
    //}

    if (multiselect[0] === dataItem.EventType && multiselect.length > 1) {
        $('#event-calendar>tbody>tr[data-eventId=' + dataItem.EventType + ']').remove();
        let rows = $("tbody tr");
        let dayNumber = 1; 
        rows[0].firstChild.innerHTML = "<div class='head-date'>" + dayNumber + "</div>";
        dayNumber += 1;
        rows.each(function (index) {
            if (rows[index + 1]) {
                if (rows[index].className !== rows[index + 1].className) {
                    rows[index + 1].firstChild.innerHTML = "<div class='head-date'>" + dayNumber + "</div>";
                    dayNumber += 1;
                };
            }
        })
    } else if (multiselect.length > 1) {
        $('#event-calendar>tbody>tr[data-eventId=' + dataItem.EventType + ']').remove();
        //$('#event-calendar>tbody> tr').attr('data-eventid', null);
        //$('#event-calendar tr th.event-name').html('');
    } else {

        $('#event-calendar>tbody> tr').attr('data-eventid', null);
        $('#event-calendar tr th.event-name').html('');
        $('#event-calendar>tbody>tr[data-eventId=' + dataItem.EventType + ']').remove();
        var td = $('.td-div');
        for (let i = 0; i < td.length; i++) {
            if (td[i].innerHTML) {
                td[i].innerHTML = '';
            }
        }
    }

    putDateChangLine();

}

//reading data from the database for a given event, drawing events on the calendar
function readData(eventId) {

    var data = AjaxGetDataHelper(URL("EventCalendar", "EventCalendarData"), { eventId: eventId, date: formatDate(date) });
    var color, link, editable;
    var td_width = $('.td-div:first').parent().width() + 2;

    var multiselect = $("#filters").data("kendoMultiSelect").dataSource.data();
    for (let i = 0; i < multiselect.length; i++) {
        if (multiselect[i].EventType === eventId) {
            color = multiselect[i].EventColor.split(",");
            link = multiselect[i].EditLink;
            editable = multiselect[i].Editable;
            break;
        }
    }

    //printing data
    for (let i = 0; i < data.length; i++) {
        var startDate = new Date(data[i].Start.match(/\d+/)[0] * 1);
        var endDate = new Date(data[i].End.match(/\d+/)[0] * 1);
        var title = data[i].Title !== null ? data[i].Title.replace(/:/g, "\:") : "";
        var desc = data[i].Description !== null ? data[i].Description.replace(/:/g, "\:") : "";
        var top = 4;
        var duration = Math.round((endDate - startDate) / 60000);

        if (startDate.getFullYear() === endDate.getFullYear() && startDate.getMonth() === endDate.getMonth() &&
            startDate.getDate() === endDate.getDate()) {

            var selector = $('#' + eventId + '-' + startDate.getDate() + '-' + startDate.getHours() + " > div");
            var width = duration === 0 ? 10 : duration * (td_width / 60);
            var left = startDate.getMinutes() * (td_width / 60);

            selector.append(
                '<div class="event" ' +
                'style="background-color:' + color[data[i].Color - 1] + ';left:' + left + 'px;top:' + top + 'px;width:' + width + 'px"' +
                ' data-eventId=' + data[i].EventType +
                ' data-editable=' + editable +
                ' data-link=' + link +
                ' data-id=' + data[i].Id +
                ' data-eventName="' + title.toString() +
                '" data-eventDescription="' + desc.toString() +
                '" data-start="' + startDate.toLocaleTimeString() + " " + startDate.toLocaleDateString() +
                '" data-end="' + endDate.toLocaleTimeString() + " " + endDate.toLocaleDateString() + '">' +
                '</div>');
        }
        else {
            let workStart = $('#event-calendar').data('start'),
                workEnd = $('#event-calendar').data('end'),
                width,
                left = 0,
                selector,
                dayDiff = Math.ceil(duration / (60 * 24));

            for (var j = 0; j <= dayDiff; j++) {
                //first day
                if (j === 0) {
                    selector = $('#' + eventId + '-' + startDate.getDate() + '-' + startDate.getHours() + " > div");
                    left = startDate.getMinutes() * (td_width / 60);
                    width = (60 - startDate.getMinutes() + ((workEnd - 1) - startDate.getHours()) * 60) * (td_width / 60);
                }
                //other day
                else if (startDate.getDate() + j !== endDate.getDate()) {
                    selector = $('#' + eventId + '-' + startDate.getDate() + j + '-' + workStart + " > div");
                    width = (workEnd - workStart) * td_width;
                }
                //last day
                else {
                    selector = $('#' + eventId + '-' + endDate.getDate() + '-' + workStart + " > div");
                    width = ((endDate.getHours() - workStart) * 60 + endDate.getMinutes()) * (td_width / 60);
                }
                selector.append(
                    '<div class="event" ' +
                    'style="background-color:' + color[data[i].Color - 1] + ';left:' + left + 'px;top:' + top + 'px;width:' + width + 'px"' +
                    ' data-eventId=' + data[i].EventType +
                    ' data-editable=' + editable +
                    ' data-link=' + link +
                    ' data-id=' + data[i].Id +
                    ' data-eventName="' + title.toString() +
                    '" data-eventDescription="' + desc.toString() +
                    '" data-start="' + startDate.toLocaleTimeString() + " " + startDate.toLocaleDateString() +
                    '" data-end="' + endDate.toLocaleTimeString() + " " + endDate.toLocaleDateString() + '">' +
                    '</div>');
            }
        }
    }
    $('.event').on('click', function () {
        $click = true;
        $('#lbl_start').html($(this).data('start'));
        $('#lbl_end').html($(this).data('end'));
        $('#lbl_name').html($(this).data('eventname'));
        $('#lbl_desc').html($(this).data('eventdescription'));

        if ($(this).data('editable') === true) {
            let link = $(this).data('link');
            let id = $(this).data('id');
            $('.edit-button').attr('onclick', "OpenPopup('" + link + "'," + id + ")");
            $('.edit-button').show();
        }
        else
            $('.edit-button').hide();
        let top = $(this).offset().top;
        let left = $(this).offset().left;
        $('.info-pane').css({ 'top': top - ($('.info-pane').height() / 2) });
        $('.info-pane').css({ 'left': left - $('.info-pane').width() - 35 });

        $('.info-pane').show();
    });

    $('.event').on('mouseover', function () {
        let id = $(this).data('id');
        let eventType = $(this).data('eventid');

        $('.event[data-id="' + id + '"][data-eventid="' + eventType + '"]').addClass('hovered');
        if ($click === false) {
            $('#lbl_start').html($(this).data('start'));
            $('#lbl_end').html($(this).data('end'));
            $('#lbl_name').html($(this).data('eventname'));
            $('#lbl_desc').html($(this).data('eventdescription'));

            if ($(this).data('editable') === true) {
                let link = $(this).data('link');
                let id = $(this).data('id');
                $('.edit-button').attr('onclick', "OpenPopup('" + link + "'," + id + ")");
                //$('.edit-button').show();
                $('.edit-button').hide();
            }
            else
                $('.edit-button').hide();
            let top = $(this).offset().top;
            let left = $(this).offset().left;
            $('.info-pane').css({ 'top': top - ($('.info-pane').height() / 2) });
            $('.info-pane').css({ 'left': left - $('.info-pane').width() - 35 });

            $('.info-pane').show();
        }

    });

    $('.event').on('mouseout', function () {
        let id = $(this).data('id');
        let eventType = $(this).data('eventid');

        $('.event[data-id="' + id + '"][data-eventid="' + eventType + '"]').removeClass('hovered');
        if ($click === false) {
            $('.info-pane').hide();
        }
    });

}

//opening the edit popup if link is given in the database
function OpenPopup(link, id) {
    var split = link.split("/");
    $('.info-pane').hide();
    OpenInPopupWindow({
        controller: split[0],
        method: split[1],
        data: { id: id },
        afterClose: RefreshTable
    });
}

