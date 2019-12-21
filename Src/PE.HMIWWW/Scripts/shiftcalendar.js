
	function RefreshDataShiftCal() {
    	$("#scheduler").data("kendoScheduler").dataSource.read();
    	$("#scheduler").data("kendoScheduler").refresh();
	}
