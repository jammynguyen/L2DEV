
CREATE procedure [dbo].[SPAlarmsClear] @DayOffset as smallint = 30
as 
BEGIN
	DELETE FROM [smf].[Alarms] WHERE AlarmDate <= GETDATE() - @DayOffset
END