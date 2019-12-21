CREATE PROCEDURE [dbo].[SPFillShiftCalendar]
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @DefaultCrew as bigint;
	DECLARE @LastCrew as bigint;
	DECLARE @NextCrew as bigint;
	DECLARE @InsertDayId as bigint;
	DECLARE @InsertDay as date
	DECLARE @DayCounter as int;
	DECLARE @ShiftCounter as int;
	DECLARE @StartDate as date;
	DECLARE @MaxStartDateTime as datetime;
	DECLARE @DaysQty as int; 
	DECLARE @ShiftQty as int;
	DECLARE @ShiftStart as time;
	DECLARE @ShiftDef as int;
	DECLARE @PlannedStartTime as time;
	DECLARE @PlannedEndTime as time;
	DECLARE @ShiftEndsNextDay as bit;
	SELECT @StartDate=MAX(PlannedEndTime), @MaxStartDateTime=MAX(PlannedStartTime) FROM [dbo].[ShiftCalendar];
	--SET @StartDate = '2019-01-01 00:00:00'
	SET @DaysQty = (SELECT ValueInt FROM [smf].[Parameters] WHERE [Name] = 'ShiftCalendarGenerate');
	--SET @DaysQty = 7;
	SET @LastCrew = (SELECT FKCrewId FROM ShiftCalendar WHERE PlannedStartTime=@MaxStartDateTime);
	--SET @LastCrew = 4;
	SET @NextCrew = (SELECT NextCrewId FROM Crews WHERE CrewId=@LastCrew);
	SET @DefaultCrew = (SELECT Min([CrewId]) FROM [dbo].[Crews]);
	SET @ShiftQty = (SELECT COUNT([ShiftDefinitionId]) FROM [dbo].[ShiftDefinitions]);
	SELECT @InsertDayId = DaysOfYearId, @InsertDay = DateDay FROM [dbo].[DaysOfYear] WHERE DateDay = @StartDate;
	--SELECT @InsertDayId, @NextCrew, @DaysQty
	/* DAYS LOOP */
	SET @DayCounter = 0;
	WHILE @DaysQty - @DayCounter > 0 
	BEGIN
		SET @ShiftCounter = 0;
		SET @ShiftStart = (SELECT MIN(DefaultStartTime) FROM dbo.ShiftDefinitions); --First Shift Starts
		/* SHIFT LOOP */
			WHILE @ShiftQty - @ShiftCounter > 0	
			BEGIN
				SET @ShiftDef = (SELECT [ShiftDefinitionId]  FROM [dbo].[ShiftDefinitions] WHERE [DefaultStartTime] = @ShiftStart);
				SELECT @PlannedStartTime=[DefaultStartTime], @PlannedEndTime=[DefaultEndTime], @ShiftEndsNextDay=[ShiftEndsNextDay] 
					FROM [dbo].[ShiftDefinitions] WHERE [ShiftDefinitionId] = @ShiftDef;
				IF NOT EXISTS(SELECT * FROM [dbo].[ShiftCalendar] WHERE ((FKDaysOfYear = @InsertDayId) AND (FKShiftDefinitionId = @ShiftDef)))
				BEGIN
					INSERT INTO [dbo].[ShiftCalendar](FKShiftDefinitionId, FKCrewId, FKDaysOfYear, PlannedStartTime, PlannedEndTime)
					VALUES (@ShiftDef, @NextCrew, @InsertDayId, CAST(@InsertDay as datetime)+CAST(@PlannedStartTime as datetime), 
					CASE WHEN @ShiftEndsNextDay=1 THEN CAST(DATEADD(DAY,1,@InsertDay) as datetime) ELSE CAST(@InsertDay as datetime) END + CAST(@PlannedEndTime as datetime));
				END;
				SET @ShiftCounter = @ShiftCounter + 1;
				SET @ShiftStart = @PlannedEndTime;
				SET @NextCrew = (SELECT NextCrewId FROM Crews WHERE CrewId=@NextCrew);
			END;
		SET @DayCounter = @DayCounter + 1;
		SET @InsertDayId = (SELECT [DaysOfYearId] FROM [dbo].[DaysOfYear] WHERE [DateDay] = DATEADD(DAY,@DayCounter, @StartDate));
		SET @InsertDay = (SELECT [DateDay] FROM [dbo].[DaysOfYear] WHERE [DaysOfYearId] = @InsertDayId);
	END;
	INSERT INTO DBLogs(LogType,LogSource,LogValue) values ('SP','[SPFillShiftCalendar]','Day counter: '+CAST(@DaysQty AS NVARCHAR(10))+' ')
END;