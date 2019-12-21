CREATE TABLE [smf].[SystemStatuses] (
    [SystemStatusId] BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LastUpdateTs]   DATETIME       CONSTRAINT [DF_SMFSystemStatuses_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [Name]           NVARCHAR (50)  NOT NULL,
    [Status]         SMALLINT       NOT NULL,
    [Active]         BIT            NOT NULL,
    [CycleTime]      INT            NOT NULL,
    [SortOrder]      SMALLINT       NULL,
    [Link]           NVARCHAR (120) NULL,
    [DisplayName]    NVARCHAR (10)  NULL,
    CONSTRAINT [PK_SMFSystemStatuses] PRIMARY KEY CLUSTERED ([SystemStatusId] ASC),
    CONSTRAINT [CK_UniqueSystemStatusName] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO

CREATE TRIGGER [smf].[SMFSystemStatusesUTS] ON [smf].[SystemStatuses]
   AFTER UPDATE
AS 
BEGIN

    SET NOCOUNT ON;
	declare 
	@DateTS datetime,
	@Status int, -- 0-Unknown; 1-Error, 2-Warning, 3-OK
	@Id bigint

	set @DateTS = GETDATE()
	SELECT @Status = inserted.Status, @Id = inserted.SystemStatusId FROM INSERTED
	 
    UPDATE smf.SystemStatuses SET LastUpdateTs = @DateTS from smf.SystemStatuses, inserted where smf.SystemStatuses.SystemStatusId = @Id

	--if(@Status = 1)
	--	UPDATE dbo.SMFSystemStatuses SET LastTimeError = @dateTS from SMFSystemStatuses, inserted where SMFSystemStatuses.SystemStatusId = @Id
	--else if(@Status = 2)
	--	UPDATE dbo.SMFSystemStatuses SET LastTimeWarning = @dateTS from SMFSystemStatuses, inserted where SMFSystemStatuses.SystemStatusId = @Id
	--else if(@Status = 3)
	--	UPDATE dbo.SMFSystemStatuses SET LastTimeOK = @dateTS from SMFSystemStatuses, inserted where SMFSystemStatuses.SystemStatusId = @Id

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'in seconds', @level0type = N'SCHEMA', @level0name = N'smf', @level1type = N'TABLE', @level1name = N'SystemStatuses', @level2type = N'COLUMN', @level2name = N'CycleTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0 - Error; 1 - Warning; 2 - OK;', @level0type = N'SCHEMA', @level0name = N'smf', @level1type = N'TABLE', @level1name = N'SystemStatuses', @level2type = N'COLUMN', @level2name = N'Status';

