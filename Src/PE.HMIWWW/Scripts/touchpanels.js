var $stayFocus = false;
var $drop = false;
var $count = 0;

function init() {
    //scrollTable();
    scrollIn();
    switchCheked();

    ///focus portlets and inputs
    $('.portlet,input[type=text],select:not(.dropdown)').on('click', function (event) {
        $('.active:not(td,li)').removeClass('active');
        $(this).addClass('active');
        event.stopPropagation();
    });

    $('.dropdown').on('click', function (event) {
        event.stopPropagation();
        $('.dropdown').removeClass('open');
        if ($(this).hasClass('open'))
            $(this).removeClass('open');
        else
            $(this).addClass('open');

        $('.active:not(td,li)').removeClass('active');
        $(this).parent().parent().parent().addClass('active');
    });

    ///remove focus from elements
    $('html').click(function () {
        $('.active:not(td,li)').removeClass('active');
    });

    //switcher
    $('.switch:not(.ownAction) button').on('click', function () {
        $(this).parent().find('button').removeClass('checked').addClass('unchecked');
        $(this).removeClass().addClass('btn-touch checked');
        var check = $(this).parent().find('input');
        if ($(this).val() == 1) {
            check.attr('checked', true);
        }
        else {
            check.attr('checked', false);
        }
    });

    //focus row in table
    $('table:not(.col-select)').on('click', 'tbody tr', function () {
        var parent = $(this).parents("tbody");
        parent.find('.tr-active').each(function () {
            $(this).removeClass("tr-active");
        });
        $(this).addClass('tr-active');
    });

    $('.numbers').on('keydown', function (e) {
        -1 !== $.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) || (/65|67|86|88/.test(e.keyCode) &&
            (e.ctrlKey === true || e.metaKey === true)) &&
            (!0 === e.ctrlKey || !0 === e.metaKey) || 35 <= e.keyCode && 40 >= e.keyCode || (e.shiftKey || 48 > e.keyCode || 57 < e.keyCode) &&
            (96 > e.keyCode || 105 < e.keyCode) && e.preventDefault()
    });
}


function scrollTable() {
    $(".scrollTable").each(function () {
        var $height = 200;
        var $table = $(this).find('table');
        var width = [];

        if ($(this).html().indexOf('height:' > 0)) {
            $height = $(this).height() - $table.find('thead').height();
        };

        //add row in head
        $table.find('thead tr').append('<th></th>');
        $table.find('tbody tr').append('<td></td>');

        //get width
        var head = $table.find('thead').html();
        var body = $table.find('tbody').html();

        //width of columns
        $('th', this).each(function (i, e) {
            var width = 0;
            if ($(e).attr("data-width") != null) {
                width = $(e).attr("data-width") + 'px';
                $(e).width(width);
                $(body).find('td:eq(' + i + ')').css('width', width);
            }
            if ($(e).attr("data-visible") == 'false') {
                $(body).find('td:eq(' + i + ')').css('display', 'none');
            }
        })

        head = '<table class="table"><thead>' + head + '</thead></table>';
        body = '<div class="scrollIn dragscroll" style="height:' + $height + 'px"><table class="table"><tbody>' + body + '</tbody></table></div>';
        var result = head + body;
        $(this).html("");
        $(this).append(head);
        $(this).append(body);
        LoadData($(this));
    });
};

function scrollIn() {
    $('.scrollIn').each(function () {

        if ($(this).html().indexOf('height:' > 0))
            $height = $(this).height();
        else
            $height = 200;

        if ($height == 0)
            $height = 200;

        $($(this)).slimScroll({
            distance: '15px',
            size: '10px',
            opacity: 1,
            height: $height,
            alwaysVisible: true,
            railVisible: true,
            railColor: '#0C2340',
            color: '#E87722',
            railOpacity: 1,
        });
    });
};

function switchCheked() {
    $('.switch').each(function () {
        var check = $(this).find('input');
        if (check.attr('checked')) {
            $('button[value="1"]', this).addClass('checked').removeClass('unchecked');
            $('button[value="0"]', this).addClass('unchecked').removeClass('checked');
        }
        else {
            $('button[value="0"]', this).addClass('checked').removeClass('unchecked');
            $('button[value="1"]', this).addClass('unchecked').removeClass('checked');
        }
    });
};

function toastAlert(type) {

    var toast = '';
    switch (type) {
        case "success": toast = '<div id="toast" class="toast success">Success!</div>'; break;
        case "error": toast = '<div id="toast" class="toast error">Error!</div>'; break;
        //case "success": toast = '<div id="toast" class="maintoast success"><i class="glyphicon glyphicon-check" style="color:green;top:3px; left:-25px;"></i>Success !</div>'; break;
        //case "error": toast = '<div id="toast" class="maintoast error"><i class="glyphicon glyphicon-remove" style="color:red;top:3px; left:-25px;"></i>Error !</div>'; break;

    }
    $('body').append(toast);
    setTimeout(function () {
        $('#toast').remove();
    }, 2000);
}


function PrepareText(txt) {

    var result = txt;
    result = result.replace(/\<div>/g, '\n');
    result = result.replace(/\<br>/g, '\n');
    result = result.replace(/\&nbsp;/g, ' ');
    result = result.replace(/<\/div>/g, '');
    result = result.trim();
    result = result.replace(/(<([^>]+)>)/ig, "");

    return result;
}

function PrepareHTML(txt) {
    return txt.replace(/\n/g, '<br/>');
}

function ToNumber(txt) {
    txt = txt.replace(/\s/g, '').replace(/\,/g, '.').replace(/[.](?=.*[.])/g, "");
    txt = parseFloat(txt);
    console.log(txt);
    return txt;        
};


function LoadData(element, stayFocus) {
    stayFocus = true;
    var tbody = $(element).find('tbody');
    var select = 0;
    var print = true;
    if (stayFocus == true)
        select = tbody.find('.tr-active').find("td:eq(0)").html();

    //tbody.empty();
    var rowCount = tbody.find('tr').length;
    var columnCount = $(element).find('thead > tr > th').length;
    var url = $(element).attr("data-url");

    $.ajax({
        url: url,
        dataType: 'json',
        success: function (result) {
            //set row count
            var dataLength = result.length - rowCount;
            for (var i = 0; i < Math.abs(dataLength); i++) {
                if (dataLength >= 0) {
                    var last = tbody.find('tr:last');
                    if (rowCount === 0 && i == 0) {
                        var addRow = '<tr>';
                        for (var j = 0; j < columnCount; j++) {

                            var th = $('thead', element).find('th:eq(' + j + ')');
                            var style = "style='width: ";
                            var visible = '';

                            if (th.attr("data-width") != null) {
                                width = th.attr("data-width");
                                th.width(parseInt(width) - 16);
                            }
                            else
                                width = th.width() + 16;

                            style = style + width + 'px !important;';

                            if (th.attr("data-visible") == 'false') {
                                style = style + ' display:none;';
                                width = 0;
                                th.css('display', 'none');
                            }
                            style = style + "'";
                            addRow = addRow + '<td ' + style + '></td>';
                        }
                        addRow = addRow + '</tr>';
                        tbody.append(addRow);
                    }
                    else {
                        var newRow = last.clone();
                        last.after(newRow);
                    }
                }
                else {
                    tbody.find('tr:last').remove();
                }
            }
            //fill with data

            $('tr', tbody).each(function (i, e) {
                //get Row data
                var rowData = result[i];
                //set id of row
                var id = "";
                if ($(element).attr("data-id") != null) {
                    id = rowData[$(element).attr("data-id")];
                    $(e).attr('id', id);
                }

                $('td:not(:last)', e).each(function (index, ele) {
                    var value = '';
                    var thLog = $('thead', element).find('th:eq(' + index + ')');
                    var dataSource = thLog.attr("data-source");

                    if (dataSource && rowData.hasOwnProperty(dataSource)) {
                        value = rowData[dataSource];
                        if (thLog.attr("data-date") == 'true') {
                            value = new Date(parseInt(rowData[dataSource].substr(6))).toLocaleString();
                        }
                        if (thLog.attr("data-precision")) {
                            var precision = parseInt(thLog.attr("data-precision"));
                            value = rowData[dataSource].toFixed(precision);
                        }
                        if (thLog.attr("data-formatter")) {
                            switch (thLog.attr("data-formatter")) {
                                case 'bool': if (value) value = '<i class="glyphicon glyphicon-ok"></i>'; else value = '<i class="glyphicon glyphicon-remove"></i>'; break;
                                case 'bool-reverse': if (value) value = '<i class="glyphicon glyphicon-remove"></i>'; else value = '<i class="glyphicon glyphicon-ok"></i>'; break;
                            }
                        }
                    }
                    $(ele).html(value);
                })
            });


            if (stayFocus == true)
                $('#' + select).addClass('tr-active');
        }
    });
}
