CREATE TABLE [smf].[AlarmMessageDefinitions] (
    [AlarmMessageDefinitionId] BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Message]                  NVARCHAR (250) NULL,
    [AlarmDefinitionId]        BIGINT         NOT NULL,
    [LanguageId]               BIGINT         CONSTRAINT [DF_AlarmMessageDefinitions_LanguageId] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_AlarmMessageDefinitionId] PRIMARY KEY CLUSTERED ([AlarmMessageDefinitionId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_AlarmMessageDefinition_AlarmDefinitionId] FOREIGN KEY ([AlarmDefinitionId]) REFERENCES [smf].[AlarmDefinitions] ([AlarmDefinitionId]),
    CONSTRAINT [FK_AlarmMessageDefinition_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [smf].[Languages] ([LanguageId])
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AlarmDefinitionId]
    ON [smf].[AlarmMessageDefinitions]([AlarmDefinitionId] ASC, [LanguageId] ASC);

