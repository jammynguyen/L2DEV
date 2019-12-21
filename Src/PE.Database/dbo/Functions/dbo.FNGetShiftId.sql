CREATE FUNCTION [dbo].[FNGetShiftId](@DateTimeStamp DATETIME)
RETURNS INT
AS
     BEGIN
         DECLARE @ShiftCalendarId BIGINT;
         SELECT @ShiftCalendarId = ShiftCalendarId
         --SELECT @ShiftCalendarId = MIN(ShiftCalendarId)
         FROM ShiftCalendar
         --WHERE @DateTimeStamp BETWEEN StartTime AND EndTime;
         --IF @ShiftCalendarId IS NULL
         --    SELECT @ShiftCalendarId = MIN(ShiftCalendarId)
         --    FROM ShiftCalendar
         WHERE @DateTimeStamp BETWEEN PlannedStartTime AND PlannedEndTime;
         RETURN @ShiftCalendarId;
     END;