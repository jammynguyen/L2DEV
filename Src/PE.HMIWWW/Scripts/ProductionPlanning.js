var pixToMin = 1.430;

//PRINT TABLE
function PrintTable(date, url, rollshopUrl, workOrderUrl, CampaignId) {
    $('#table_body').empty();
    var days = daysInMonth(date) + 1;
    for (let index = 1; index < days; index++) {
        var table = "";

        table = table + "<tr><td class='head'><div class='head-date'>" + index + "</div><p style='margin-left:55px;margin-top:15px;'>Products</p></td>";
        for (let hour = 0; hour < 16; hour++) {
            if (hour < 8)
                table = table + '<td id="' + index + '-' + (hour + 6) + '" class="firstShift families"><div style="position:relative"></div></td>';
            else
                table = table + '<td id="' + index + '-' + (hour + 6) + '" class="secondShift families"><div style="position:relative"></div></td>';
        }
        table = table + "</tr><tr><td class='head'><p style='margin-left:55px;margin-top:15px;'>Orders</p></td>";
        for (let hour = 0; hour < 16; hour++) {
            if (hour < 8)
                table = table + '<td id="' + index + '-' + (hour + 6) + '-O" class="firstShift orders"><div style="position:relative"></div></td>';
            else
                table = table + '<td id="' + index + '-' + (hour + 6) + '-O" class="secondShift orders"><div style="position:relative"></div></td>';
        }
        table = table + "</tr><tr><td class='head'><p style='margin-left:55px;margin-top:15px;'>RollShop</p></td>";
        for (let hour = 0; hour < 16; hour++) {
            if (hour < 8)
                table = table + '<td id="' + index + '-' + (hour + 6) + '-R" class="firstShift rollshopRow"></td>';
            else
                table = table + '<td id="' + index + '-' + (hour + 6) + '-R" class="secondShift rollshopRow"></td>';
        }
        table = table + '</tr>';
        $('#table_body').append(table);
    }

    insertData(new Date(date), url, CampaignId);
    insertRollShop(new Date(date), rollshopUrl, CampaignId);
    insertWorkOrders(new Date(date), workOrderUrl, CampaignId);
};

//INSERT EVENTS TO TABLE
function insertData(date, url, CampaignId) {
    var d = new Date(date);
    var startDate = "";
    var endDate = "";
    var families = "";
    var carriage = "";
    var tonnage = "";
    var configId = "";
    var startDatePopUp = "";
    var endDatePopUp = "";

    $.ajax({
        url: url,
        data: { month: d.getMonth() + 1, year: d.getFullYear(), CampaignId: CampaignId },
        dataType: 'json',
        success: function (result) {
            $('.event').remove();
            dataSet = result;
            for (let i = 0; i < dataSet.length; i++) {
                //var relatedTask = [];

                startDate = new Date(parseInt(dataSet[i]['StartTime'].replace('/Date(', '')));

                section = dataSet[i]['ProductSection'];
                families = dataSet[i]['FamilyName'];
                carriage = dataSet[i]['CarriageNo'];
                configId = dataSet[i]['FklrpplConfigs'];
                sequence = dataSet[i]['SequenceNumber'];
                endDate = new Date(parseInt(dataSet[i]['EndTime'].replace('/Date(', '')));


                if (i + 1 < dataSet.length && dataSet[i + 1]['FklrpplConfigs'] == configId && dataSet[i + 1]['CarriageNo'] == carriage
                    && new Date(parseInt(dataSet[i + 1]['StartTime'].replace('/Date(', ''))).getDay() != startDate.getDay()) {
                    startDatePopUp = new Date(parseInt(dataSet[i]['StartTime'].replace('/Date(', '')));
                    endDatePopUp = new Date(parseInt(dataSet[i + 1]['EndTime'].replace('/Date(', '')));
                    tonnage = dataSet[i]['PlannedTonnage'] / 1000 + dataSet[i + 1]['PlannedTonnage'] / 1000;
                    relatedTask = dataSet[i + 1]['LrCampaignScheduleId'];
                }
                else if (i != 0 && dataSet[i - 1]['FklrpplConfigs'] == configId && dataSet[i - 1]['CarriageNo'] == carriage
                    && new Date(parseInt(dataSet[i - 1]['StartTime'].replace('/Date(', ''))).getDay() != startDate.getDay()) {
                    startDatePopUp = new Date(parseInt(dataSet[i - 1]['StartTime'].replace('/Date(', '')));
                    endDatePopUp = new Date(parseInt(dataSet[i]['EndTime'].replace('/Date(', '')));
                    tonnage = dataSet[i]['PlannedTonnage'] / 1000 + dataSet[i - 1]['PlannedTonnage'] / 1000;
                    relatedTask = dataSet[i - 1]['LrCampaignScheduleId'];
                }
                else {
                    startDatePopUp = new Date(parseInt(dataSet[i]['StartTime'].replace('/Date(', '')));
                    endDatePopUp = new Date(parseInt(dataSet[i]['EndTime'].replace('/Date(', '')));
                    tonnage = dataSet[i]['PlannedTonnage'] / 1000;
                    relatedTask = 0;
                }
                let type = dataSet[i]['ElementType'];
                let profile = 'Profile_' + dataSet[i]['ProfileCode'];

                let startId = startDate.getDate() + '-' + startDate.getHours();
                let duration = Math.floor((Math.abs(endDate - startDate)) / 1000 / 60);
                //console.log("duration: "+ duration + " width: "+duration * pixToMin);
                let width = duration * pixToMin;// 1.433;
                let fromLeft = startDate.getMinutes() * pixToMin;//1.433;

                var classDiv = '" class= "form-control event ';
                if (type == 3)
                    classDiv = classDiv + "maitenance";
                else if (type == 2)
                    classDiv = classDiv + "delay ";
                else {
                    classDiv = classDiv + profile;
                    classDiv = classDiv + " carrige_" + carriage + " ";
                    classDiv = classDiv + dataSet[i]['ShapeType'];
                }
                classDiv = classDiv + '" ';
                //FAMILIES EVENT
                var contentDiv =
                    '<div  id="' + dataSet[i]['LrCampaignScheduleId']
                    + classDiv
                    + ' style="width:' + width + 'px;margin-left:' + fromLeft + 'px;" '
                    + 'data-section="' + section + '" '
                    + 'data-start="' + startDate + '" '
                    + 'data-carriage="' + carriage + '" '
                    + 'data-tonnage="' + tonnage + '" '
                    + 'data-families="' + families + '" '
                    + 'data-configId="' + configId + '" '
                    + 'data-shape="' + dataSet[i]['ShapeType'] + '" '
                    + 'data-campaign="' + dataSet[i]['CampaignId'] + '" '
                    + 'data-sequence="' + sequence + '" '
                    + 'data-startPopup="' + startDatePopUp + '" '
                    + 'data-endPopup="' + endDatePopUp + '" '
                    + 'data-relatedTask="' + relatedTask + '" '
                    + 'data-end="' + endDate +
                    '">';
                if (type == 1) {
                    if (width > 100) {
                        contentDiv = contentDiv + '<span class="time-info always-show" style="margin-left:5px;margin-bottom:-5px">' + startDate.getHours() + ':' + formatMinutes(startDate.getMinutes()) + '-' + endDate.getHours() + ':' + formatMinutes(endDate.getMinutes()) + '</span>';
                        contentDiv = contentDiv + '<h4 style="margin-left:5px;margin-top:5px;" class="always-show">' + section + '</h4>';
                        contentDiv = contentDiv + "<button class='btn-delete' style='left:" + (width - 20) + "px'>X</button>";
                    }
                    else if (width >= 35) {
                        contentDiv = contentDiv + '<span class="time-info zoom-show" style="margin-left:5px;margin-bottom:-5px">' + startDate.getHours() + ':' + formatMinutes(startDate.getMinutes()) + '-' + endDate.getHours() + ':' + formatMinutes(endDate.getMinutes()) + '</span>';
                        switch (section.length) {
                            case 2: if (width > 35) contentDiv = contentDiv + '<h4 style="margin-left:5px;margin-top:20px;" class="always-show">' + section + '</h4>'; break;
                            case 3: if (width > 40) contentDiv = contentDiv + '<h4 style="margin-left:5px;margin-top:20px;" class="always-show">' + section + '</h4>'; break;
                            default:
                                if (width > 50)
                                    contentDiv = contentDiv + '<h4 style="margin-left:5px;margin-top:20px;" class="always-show">' + section + '</h4>';
                                break;
                        }
                        contentDiv = contentDiv + "<button class='btn-delete' style='left:" + (width - 20) + "px'>X</button>";
                    }
                    else if (width >= 15) {
                        contentDiv = contentDiv + '<span class="time-info zoom-show" style="margin-left:5px;margin-bottom:-5px">' + startDate.getHours() + ':' + formatMinutes(startDate.getMinutes()) + '-' + endDate.getHours() + ':' + formatMinutes(endDate.getMinutes()) + '</span>';
                        contentDiv = contentDiv + '<h4 style="margin-left:5px;margin-top:5px;" class="zoom-show">' + section + '</h4>';
                    }
                    else {
                        contentDiv = contentDiv + '<span class="time-info never-show" style="margin-left:5px;margin-bottom:-5px">' + startDate.getHours() + ':' + formatMinutes(startDate.getMinutes()) + '-' + endDate.getHours() + ':' + formatMinutes(endDate.getMinutes()) + '</span>';
                        contentDiv = contentDiv + '<h4 style="margin-left:5px;margin-top:5px;" class="never-show">' + section + '</h4>';
                    }
                    contentDiv = contentDiv + '</div>';
                }
                else {
                    if (width > 85) {
                        contentDiv = contentDiv + '<span class="time-info" style="margin-left:5px;margin-bottom:-5px">' + startDate.getHours() + ':' + formatMinutes(startDate.getMinutes()) + '-' + endDate.getHours() + ':' + formatMinutes(endDate.getMinutes()) + '</span>';
                        //if (width > 50)
                        if (type == 2)
                            contentDiv = contentDiv + '</br><h4 style="margin-left:5px;margin-top:5px;">Delay</h4>';
                        if (type == 3)
                            contentDiv = contentDiv + '</br><h4 style="margin-left:5px;margin-top:5px;">Maintenance</h4>';
                    }
                    contentDiv = contentDiv + '</div>';
                }
                $('#' + startId).append(contentDiv);
            }
        }
    });

};


function insertRollShop(date, url, CampaignId) {
    var d = new Date(date);
    var startDate = "";
    var endDate = "";
    var description = "";
    var actionType = "";
    var top = "";


    $.ajax({
        url: url,
        data: { month: d.getMonth() + 1, year: d.getFullYear(), CampaignId: CampaignId },
        dataType: 'json',
        success: function (result) {
            $('.rollshop').remove();

            dataSet = result;
            for (let i = 0; i < dataSet.length; i++) {
                startDate = new Date(parseInt(dataSet[i]['PlannedStartTime'].replace('/Date(', '')));
                endDate = new Date(parseInt(dataSet[i]['PlannedEndTime'].replace('/Date(', '')));
                section = dataSet[i]['ProductSection'];
                description = dataSet[i]['JobDescription'];
                actionType = dataSet[i]['ActionType'];
                let type = dataSet[i]['ActionType'];

                let startId = startDate.getDate() + '-' + startDate.getHours() + "-R";
                let duration = Math.floor((Math.abs(endDate - startDate)) / 1000 / 60);
                let width = duration * pixToMin;//1.518;
                let fromLeft = startDate.getMinutes() * pixToMin;//1.518;

                var classDiv = '" class= "form-control rollshop Roll_' + actionType;
                top = "";
                //if (actionType == 1)
                //    top = " margin-top:1px;"
                classDiv = classDiv + '" ';
                //FAMILIES EVENT
                var contentDiv =
                    '<div  id="' + dataSet[i]['Id']
                    + classDiv
                    + ' style="width:' + width + 'px;margin-left:' + fromLeft + 'px;' + top + '" '
                    + 'data-start="' + startDate + '" '
                    + 'data-description="' + description + '" '
                    + 'data-actionType="' + actionType + '" '
                    + 'data-end="' + endDate +
                    '">';

                if (width > 180) {
                    contentDiv = contentDiv + '<span  style="margin-left:5px">' + description + '</span>';
                }

                contentDiv = contentDiv + '</div>';

                $('#' + startId).append(contentDiv);
                Colision();
            }
        }
    });

};


function insertWorkOrders(date, url, CampaignId) {
    var d = new Date(date);
    var startDate = "";
    var endDate = "";
    var section = "";
    var name = "";
    var shape = "";
    var tonnage = "";
    $.ajax({
        url: url,
        data: { month: d.getMonth() + 1, year: d.getFullYear(), CampaignId: CampaignId },
        dataType: 'json',
        success: function (result) {
            $('.workorder').remove();
            dataSet = result;
            for (let i = 0; i < dataSet.length; i++) {
                startDate = new Date(parseInt(dataSet[i]['StartTime'].replace('/Date(', '')));
                endDate = new Date(parseInt(dataSet[i]['EndTime'].replace('/Date(', '')));
                section = dataSet[i]['ProductSection'];
                name = dataSet[i]['WorkOrderName'];
                shape = dataSet[i]['ShapeType'];
                name = dataSet[i]['WorkOrderName'];

                tonnage = dataSet[i]['WoTonnage'] / 1000;
                var billets = false;

                if (dataSet[i]['BilletsInWorkOrder'] != null)
                    billets = true;

                let startId = startDate.getDate() + '-' + startDate.getHours() + '-O';
                let duration = Math.floor((Math.abs(endDate - startDate)) / 1000 / 60);
                let width = duration * pixToMin;// 1.518;
                let fromLeft = startDate.getMinutes() * pixToMin;//1.518;

                var classDiv = '" class= "form-control workorder ';
                if (dataSet[i]['WorkOrderId'] == null) {
                    classDiv = classDiv + " order-delay ";
                }

                if (billets)
                    classDiv = classDiv + " with-billets ";
                classDiv = classDiv + '" ';
                //FAMILIES EVENT
                var contentDiv =
                    '<div  id="' + dataSet[i]['WorkOrderId']
                    + classDiv
                    + ' style="width:' + width + 'px;margin-left:' + fromLeft + 'px;" '
                    + 'data-name="' + name + '" '
                    + 'data-start="' + startDate + '" '
                    + 'data-section="' + section + '" '
                    + 'data-shape="' + shape + '" '
                    + 'data-tonnage="' + tonnage + '" '
                    + 'data-end="' + endDate +
                    '">';

                $('#' + startId).append(contentDiv);
                //add delay
                if (i + 1 < dataSet.length) {
                    var currDate = new Date(parseInt(dataSet[i]['EndTime'].replace('/Date(', '')));
                    var nextDate = new Date(parseInt(dataSet[i + 1]['StartTime'].replace('/Date(', '')));
                    printDelay(currDate, nextDate);

                }
            }
            //print delay to end of tasks  

            let durationLast = 0;
            let widthDelayLast = 0;// 1.518;
            let fromLeftLast = 0;//1.518;

            var delayDivLast = "";
            var ifLast = "";
            //from begin to first task
            var firstTask = $('.event:first');
            var firstTaskDate = new Date(firstTask.attr('data-start'));
            var lastTask = $('.event:last');
            lastTaskData = new Date(lastTask.attr('data-end'));



            if (dataSet.length > 0) {

                var firstWODate = new Date(parseInt(dataSet[0]['StartTime'].replace('/Date(', '')));
                printDelay(firstTaskDate, firstWODate);
                //take last date from ordes and last from task
                var lastWODate = new Date(parseInt(dataSet[dataSet.length - 1]['EndTime'].replace('/Date(', '')));
                printDelay(lastWODate, lastTaskData);
            }
            else {
                //take date from first task and last
                printDelay(firstTaskDate, lastTaskData);

            }
        }
    });

};

//PRINT DELAY ON WORK ORDERS AXIS
function printDelay(start, end) {
    var startDate = new Date(start);
    var endDate = new Date(end);
    let startId = startDate.getDate() + '-' + startDate.getHours() + '-O';
    let duration = Math.floor((Math.abs(endDate - startDate)) / 1000 / 60);
    let width = duration * pixToMin;
    let fromLeft = startDate.getMinutes() * pixToMin;
    var delayDiv = "";
    if (duration > 5) {
        if (startDate.getDate() == endDate.getDate()) {
            var delayDiv =
                '<div class="order-delay workorder" style="width:' + width + 'px;margin-left:' + fromLeft + 'px "'
                + 'data-start="' + startDate + '" '
                + 'data-end="' + endDate + '" '
                + '>'
            if (width > 100) {
                delayDiv = delayDiv + '<span class="time-info always-show" style="margin-left:5px;margin-top:-5px;margin-bottom:-15px">' + startDate.getHours() + ':' + formatMinutes(startDate.getMinutes()) + '-' + endDate.getHours() + ':' + formatMinutes(endDate.getMinutes()) + '</span>';
                delayDiv = delayDiv + '<h4 style="margin-left:5px;margin-top:5px;" class="always-show">Delay</h4>';
            }

            delayDiv = delayDiv + '</div> ';
            $('#' + startId).append(delayDiv);
        }
        else {
            if (endDate.getDate() - startDate.getDate() > 1) {
                var iter = endDate.getDate() - startDate.getDate();
                var newStart = new Date(startDate.getFullYear(), startDate.getMonth(), startDate.getDate(), 6, 0, 0, 0);
                var newEnd = new Date(startDate.getFullYear(), startDate.getMonth(), startDate.getDate(), 22, 0, 0, 0);
                for (var i = 0; i < iter - 1; i++) {
                    var newStart = new Date(newStart.getFullYear(), newStart.getMonth(), newStart.getDate() + 1, 6, 0, 0, 0);
                    var newEnd = new Date(newEnd.getFullYear(), newEnd.getMonth(), newEnd.getDate() + 1, 22, 0, 0, 0);
                    printDelay(newStart, newEnd);
                }

            }

            var prevDayEnd = new Date(startDate.getFullYear(), startDate.getMonth(), startDate.getDate(), 22, 0, 0, 0);
            var nextDayStart = new Date(endDate.getFullYear(), endDate.getMonth(), endDate.getDate(), 6, 0, 0, 0);

            let durationDelayPrevDay = Math.floor((Math.abs(startDate - prevDayEnd)) / 1000 / 60);
            let widthDelayPrevDay = durationDelayPrevDay * pixToMin;// 1.518;
            let fromLeftDelayPrevDay = startDate.getMinutes() * pixToMin;//1.518;

            var delayDiv =
                '<div class="order-delay workorder" style="width:' + widthDelayPrevDay + 'px;margin-left:' + fromLeftDelayPrevDay + 'px "'
                + 'data-start="' + startDate + '" '
                + 'data-end="' + prevDayEnd + '" '
                + '>'
            if (widthDelayPrevDay > 100) {
                delayDiv = delayDiv + '<span class="time-info always-show" style="margin-left:5px;margin-top:-5px;margin-bottom:-15px">' + startDate.getHours() + ':' + formatMinutes(startDate.getMinutes()) + '-' + prevDayEnd.getHours() + ':' + formatMinutes(prevDayEnd.getMinutes()) + '</span>';
                delayDiv = delayDiv + '<h4 style="margin-left:5px;margin-top:5px;" class="always-show">Delay</h4>';
            }

            delayDiv = delayDiv + '</div> ';
            $('#' + startId).append(delayDiv);

            let durationDelayNextDay = Math.floor((Math.abs(nextDayStart - endDate)) / 1000 / 60);
            let widthDelayNextDay = durationDelayNextDay * pixToMin;// 1.518;

            var delayDivNextDay =
                '<div class="order-delay workorder" style="width:' + widthDelayNextDay + 'px;margin-left:0px "'
                + 'data-start="' + nextDayStart + '" '
                + 'data-end="' + endDate + '" '
                + '>'
            if (widthDelayNextDay > 100) {
                delayDivNextDay = delayDivNextDay + '<span class="time-info always-show" style="margin-left:5px;margin-top:-5px;margin-bottom:-15px">' + nextDayStart.getHours() + ':' + formatMinutes(nextDayStart.getMinutes()) + '-' + endDate.getHours() + ':' + formatMinutes(endDate.getMinutes()) + '</span>';
                delayDivNextDay = delayDivNextDay + '<h4 style="margin-left:5px;margin-top:5px;" class="always-show">Delay</h4>';
            }
            var nextStartId = nextDayStart.getDate() + '-6-O';
            delayDivNextDay = delayDivNextDay + '</div> ';
            $('#' + nextStartId).append(delayDivNextDay);
        }
    }
}

//DATE HELPERS
function formatMinutes(minutes) {
    if (minutes < 10)
        return '0' + minutes;
    else return minutes;
};

function formatDate(date) {
    var d = new Date(date);
    var hour = d.getHours() < 10 ? '0' + d.getHours() : d.getHours();
    var year = d.getFullYear();
    var min = d.getMinutes() < 10 ? '0' + d.getMinutes() : d.getMinutes();
    var day = d.getDate() < 10 ? '0' + d.getDate() : d.getDate();
    var month = d.getMonth() < 9 ? '0' + (d.getMonth() + 1) : d.getMonth() + 1;

    return year + '-' + month + '-' + day + " " + hour + ':' + min;// + ' ' + day + "-" + month + '-' + year;
};

function daysInMonth(date) {
    return new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
};

//FIND COLISION
//FIND COLISION
function Colision() {
    var element = $('.rollshop:last');
    var id = element.attr('id');
    var left = element.offset().left;
    var right = element.offset().left + element.width();

    var tr = element.closest('tr');

    $('.rollshop', tr).each(function (i, e) {
        var e_id = $(e).attr('id');
        var e_top = $(e).offset().top;
        var e_left = $(e).offset().left;
        var e_bottom = $(e).offset().top + $(e).height();
        var e_right = $(e).offset().left + $(e).width();
        if (((left >= e_left && left <= e_right) || (right >= e_left && right <= e_right)) && id != e_id) {
            $(e).addClass('little');
            element.addClass('little');

            e_top = $(e).offset().top;
            e_bottom = $(e).offset().top + $(e).height();
            var top = element.offset().top;
            var bottom = element.offset().top + element.height();

            if ((e_top <= top && e_bottom >= top)) {
                if ($(e).hasClass('down') || $(e).hasClass('midlle') || $(e).hasClass('same-top') || $(e).hasClass('deep-down')) {
                    tr.addClass('row-grow');
                    tr.find('.head').css('height', '71px');
                    element.addClass('deep-down');
                    tr.find('.down').addClass('middle').removeClass('down');
                    tr.find('.little:not(.down,.middle,.deep-dow)').addClass('same-top');
                    if ($(e).hasClass('deep-down')) {
                        element.addClass('middle').removeClass('deep-down');
                    }
                    if ($(e).hasClass('same-top')) {
                        element.addClass('middle').removeClass('deep-down');
                    }
                }
                else {
                    element.addClass('down');
                }
            }
            else if (tr.hasClass('row-grow')) {
                element.addClass('same-top');
            }

        }
    });

};