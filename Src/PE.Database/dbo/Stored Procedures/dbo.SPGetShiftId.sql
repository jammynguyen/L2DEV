CREATE PROCEDURE [dbo].[SPGetShiftId](@DateTimeStamp DATETIME2)
AS
    BEGIN
        BEGIN TRY
            DECLARE @ShiftCalendarId BIGINT;
            SELECT @ShiftCalendarId = ShiftCalendarId
            --SELECT @ShiftCalendarId = MIN(ShiftCalendarId)
            FROM ShiftCalendar
            --WHERE @DateTimeStamp BETWEEN StartTime AND EndTime;
            --IF @ShiftCalendarId IS NULL
            --    SELECT @ShiftCalendarId = MIN(ShiftCalendarId)
            --    FROM ShiftCalendar
            WHERE @DateTimeStamp BETWEEN PlannedStartTime AND PlannedEndTime;
            -- RETURN @ShiftCalendarId
            PRINT @ShiftCalendarId;
            EXEC SPLogError 
                 'SP', 
                 '[SPGetShiftId]', 
                 @ShiftCalendarId;
        END TRY
        BEGIN CATCH
            EXEC SPLogError 
                 'SP', 
                 '[SPGetShiftId]', 
                 @ShiftCalendarId;
        END CATCH;
    END;