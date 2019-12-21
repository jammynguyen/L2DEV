
CREATE PROCEDURE [dbo].[SPLogError] @LogType nchar(10), @LogSource nvarchar(50), @LogValue nvarchar(50)
AS
BEGIN
DECLARE @ErrorMessage NVARCHAR(1000);
    SET NOCOUNT ON;

    SET @ErrorMessage = 'Error ' + CONVERT(varchar(50), ERROR_NUMBER()) +
          ', Severity ' + CONVERT(varchar(10), ERROR_SEVERITY()) +
          ', State ' + CONVERT(varchar(10), ERROR_STATE()) + 
          ', Procedure ' + ISNULL(ERROR_PROCEDURE(), '-') + 
          ', Line ' + CONVERT(varchar(10), ERROR_LINE()) +
		  ', Error Message ' + ERROR_MESSAGE();

		  INSERT INTO DBLogs
            (LogType, 
             LogSource, 
             LogValue,
			 ErrorMessage
            )
            VALUES
            (@LogType, 
             @LogSource, 
             @LogValue,
			 @ErrorMessage
            );
END;