CREATE TABLE [smf].[AlarmValueDefinitions] (
    [AlarmValueDefinitionId]   BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Description]              NVARCHAR (100) NULL,
    [AlarmMessageDefinitionId] BIGINT         NOT NULL,
    [UnitId]                   BIGINT         NULL,
    CONSTRAINT [PK_AlarmValueDefinitionId] PRIMARY KEY CLUSTERED ([AlarmValueDefinitionId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_AlarmValueDefinitions_AlarmMessageDefinitionId] FOREIGN KEY ([AlarmMessageDefinitionId]) REFERENCES [smf].[AlarmMessageDefinitions] ([AlarmMessageDefinitionId]),
    CONSTRAINT [FK_SMFAlarmValueDefinitions_SMFAlarmValueDefinitions] FOREIGN KEY ([UnitId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId])
);




GO
CREATE NONCLUSTERED INDEX [NCI_UnitId]
    ON [smf].[AlarmValueDefinitions]([UnitId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_AlarmValueDefinitionsId]
    ON [smf].[AlarmValueDefinitions]([AlarmMessageDefinitionId] ASC);

