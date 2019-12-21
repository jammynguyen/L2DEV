var configuredModules = 0;
var resultActions;
var timer;
var lastWdogData;
var counter = 0;
 
$(document).ready(function () {
	
    HmiHub.client.system2HmiBroadcastWDogState = function (wdogData) {
        console.log(wdogData);
		BuildWdogTable(wdogData, true);
	};
});


function BuildWdogTable(data, useTimeout) {

    if (data === null || data === undefined) {
        WarningMessage("Data is null");
        return;
    }

    if (data.ProcessStates === null) {
        WarningMessage("Data.ProcessStates is null");
        return;
    }

    if (useTimeout) {
        clearTimeout(timer);
        timer = setTimeout(TimeOutFunction, 5000);
        lastWdogData = data;

      let d = new Date();
    }

    try {
        counter++;
        let counterDisplay = (counter%2===0?"/ ":"\\ ");
        const table = $("#wdog-table");
        if (configuredModules !== data.ProcessStates.length) {
            cols = 6;
            configuredModules = data.ProcessStates.length;

            for (let i = 1; i < data.ProcessStates.length + 1; i++) {
                table.append('<tr></tr>');
                table.find('tr').eq(i).attr('data-row', data.ProcessStates[i - 1].ProgramName);

                for (let j = 0; j < cols; j++) {
                    table.find('tr').eq(i).append('<td class="wdog-col-'+j+'"></td>');
                    table.find('tr').eq(i).find('td').eq(j).attr('data-row', data.ProcessStates[i - 1].ProgramName).attr('data-col', j);
                }

                table.find('tr').eq(i).find('td').eq(cols - 1).append(BtnProcessUnderWd(data.ProcessStates[i - 1].ProgramName));

            }
        }



        for (var ii = 0; ii < data.ProcessStates.length; ii++) {
            var row = $('tr[data-row="' + data.ProcessStates[ii].ProgramName + '"]');

            row.find('td').eq(0).text(counterDisplay+data.ProcessStates[ii].ProgramName);
            row.find('td').eq(1).text(data.ProcessStates[ii].StartTimeText);
            row.find('td').eq(2).text(data.ProcessStates[ii].ProcessStateText);
            //row.find('td').eq(3).text(data.ProcessStates[ii].CpuUsage.toFixed(1) + "% / " + data.ProcessStates[ii].MemoryUsage.toFixed(1) + "MB");
            row.find('td').eq(3).text(data.ProcessStates[ii].LastIncomingCall);
            row.find('td').eq(4).text(data.ProcessStates[ii].LastOutgoingCall);
            //row.find('td').eq(6).text(data.ProcessStates[ii].LastAlarmText);


            if (data.ProcessStates[ii].ProcessState === 0) {
                row.css('background', '#b2b2b2').css('color', '#ffffff');
            }
            if (data.ProcessStates[ii].ProcessState === 1 || data.ProcessStates[ii].ProcessState === 3) {
                row.css('background', '#ffcd00').css('color', '#ffffff');
            }
            else if (data.ProcessStates[ii].ProcessState === 2) {
                row.css('background', '#ce0037').css('color', '#ffffff');
            }
            else if (data.ProcessStates[ii].ProcessState === 4) {
                row.css('background', '#7a9a01').css('color', '#ffffff');
            }
            else if (data.ProcessStates[ii].ProcessState === 10) {
                row.css('background', '#8c4799').css('color', '#ffffff');
            }


            if (data.ProcessStates[ii].UnderWdControl) {
                $("#wd_set_" + data.ProcessStates[ii].ProgramName).attr('class', "image-button-hidden");
                $("#wd_unset_" + data.ProcessStates[ii].ProgramName).attr('class', "image-button");
            }
            else {
                $("#wd_set_" + data.ProcessStates[ii].ProgramName).attr('class', "image-button");
                $("#wd_unset_" + data.ProcessStates[ii].ProgramName).attr('class', "image-button-hidden");
            }

        }
    }
    catch (ex) {
        WarningMessage(ex);
    }

}

function ModuleAction(moduleName, action) {

    url = serverAddress + "/Watchdog/" + action;
    data = 'moduleName=' + moduleName;

    AjaxReqestHelper(url, data);
}

function ProcessAction(moduleName, action) {

    url = serverAddress + "/Watchdog/" + action;
    data = null;

    AjaxReqestHelper(url, data);
}

function AlarmsForProcess(moduleName) {
	var url = serverAddress + "/system/Alarm/Alarms?programName=" + moduleName;
	ShowInPopupWindow(url, "Alarm", "");
}

function TimeOutFunction() {

	for (var ii = 0; ii < lastWdogData.ProcessStates.length; ii++)
	{
		lastWdogData.ProcessStates[ii].ProcessState = 10;
		lastWdogData.ProcessStates[ii].ProcessStateText = "";
		lastWdogData.ProcessStates[ii].CpuUsage = 0;
		lastWdogData.ProcessStates[ii].MemoryUsage = 0;	
	}

	BuildWdogTable(lastWdogData, false);


	var d = new Date();
	console.log(d.toString()+"Wdog Timeout!!!");
	//WarningMessage("timeout, last time:" + timer);
}




