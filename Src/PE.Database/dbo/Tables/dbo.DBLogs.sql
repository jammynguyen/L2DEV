CREATE TABLE [dbo].[DBLogs] (
    [DBLogId]      BIGINT          IDENTITY (1, 1) NOT NULL,
    [LogDate]      DATETIME        CONSTRAINT [DF_DBLogs_LogDate] DEFAULT (getdate()) NOT NULL,
    [LogType]      NCHAR (10)      NULL,
    [LogSource]    NVARCHAR (50)   NULL,
    [LogValue]     NVARCHAR (50)   NULL,
    [ErrorMessage] NVARCHAR (1000) NULL
);

