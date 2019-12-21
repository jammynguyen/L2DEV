CREATE TABLE [smf].[Alarms] (
    [AlarmId]           BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LastUpdateTs]      DATETIME       CONSTRAINT [DF_Alarms_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [AlarmDate]         DATETIME       NOT NULL,
    [AlarmOwner]        NVARCHAR (30)  NULL,
    [ShortDescription]  NVARCHAR (150) NULL,
    [AlarmDefinitionId] BIGINT         NOT NULL,
    [Confirmation]      BIT            NULL,
    [ConfirmationDate]  DATETIME       NULL,
    [ConfirmedBy]       NVARCHAR (128) NULL,
    [DisplayFlag]       SMALLINT       NOT NULL,
    [AlarmCode]         NVARCHAR (50)  NULL,
    [Message]           NVARCHAR (300) NULL,
    [AlarmCategoryName] NVARCHAR (50)  NOT NULL,
    [AlarmType]         INT            NOT NULL,
    [LanguageCode]      NVARCHAR (10)  NULL,
    [ToConfirm]         BIT            NOT NULL,
    [ShowPopup]         BIT            NOT NULL,
    CONSTRAINT [PK_AlarmId] PRIMARY KEY CLUSTERED ([AlarmId] ASC),
    CONSTRAINT [FK_Alarms_AlarmDefinitionId] FOREIGN KEY ([AlarmDefinitionId]) REFERENCES [smf].[AlarmDefinitions] ([AlarmDefinitionId])
);












GO
CREATE NONCLUSTERED INDEX [NCI_ToConfirm]
    ON [smf].[Alarms]([ToConfirm] ASC)
    INCLUDE([Confirmation]);


GO
CREATE NONCLUSTERED INDEX [NCI_LanguageCode]
    ON [smf].[Alarms]([LanguageCode] ASC)
    INCLUDE([AlarmOwner]);






GO
CREATE NONCLUSTERED INDEX [NCI_DisplayFlag]
    ON [smf].[Alarms]([DisplayFlag] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_AlarmType]
    ON [smf].[Alarms]([AlarmType] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_AlarmOwner]
    ON [smf].[Alarms]([AlarmOwner] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_AlarmDefinitionId]
    ON [smf].[Alarms]([AlarmDefinitionId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_AlarmCode]
    ON [smf].[Alarms]([AlarmCode] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_AlarmCategoryName]
    ON [smf].[Alarms]([AlarmCategoryName] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_AlarmDate]
    ON [smf].[Alarms]([AlarmDate] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_AlarmType_LanguageCode]
    ON [smf].[Alarms]([AlarmType] ASC, [LanguageCode] ASC);

