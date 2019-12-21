CREATE TABLE [smf].[AlarmsArchive] (
    [Id]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [AlarmDate]         DATETIME       NULL,
    [AlarmOwner]        NVARCHAR (30)  NULL,
    [AlarmMessage]      NVARCHAR (300) NULL,
    [AlarmDefinitionId] BIGINT         NULL,
    [Confirmation]      BIT            NULL,
    [ConfirmationDate]  DATETIME       NULL,
    [ConfirmationBy]    NVARCHAR (128) NULL,
    [AlarmCode]         NVARCHAR (10)  NULL,
    CONSTRAINT [PK_SMFAlarmsArchive] PRIMARY KEY CLUSTERED ([Id] ASC)
);

