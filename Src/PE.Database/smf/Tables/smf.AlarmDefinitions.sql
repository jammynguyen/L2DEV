CREATE TABLE [smf].[AlarmDefinitions] (
    [AlarmDefinitionId] BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AlarmCode]         NVARCHAR (10)  NOT NULL,
    [Description]       NVARCHAR (100) NULL,
    [AlarmType]         INT            NOT NULL,
    [ToConfirm]         BIT            NOT NULL,
    [AlarmCategoryId]   BIGINT         NOT NULL,
    [ShowPopup]         BIT            CONSTRAINT [DF_AlarmDefinitions_ShowPopup] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AlarmDefinitionId] PRIMARY KEY CLUSTERED ([AlarmDefinitionId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_AlarmDefinitions_AlarmCategoryId] FOREIGN KEY ([AlarmCategoryId]) REFERENCES [smf].[AlarmCategories] ([AlarmCategoryId]),
    CONSTRAINT [UQ_AlarmDefinitions_Name] UNIQUE NONCLUSTERED ([AlarmCode] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [NCI_AlarmCategoryId]
    ON [smf].[AlarmDefinitions]([AlarmCategoryId] ASC);

