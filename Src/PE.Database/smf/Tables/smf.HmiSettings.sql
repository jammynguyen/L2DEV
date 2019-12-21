CREATE TABLE [smf].[HmiSettings] (
    [SettingId]   BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UserId]      NVARCHAR (128) NOT NULL,
    [ElementId]   NVARCHAR (100) NOT NULL,
    [Controller]  NVARCHAR (100) NOT NULL,
    [Action]      NVARCHAR (100) NOT NULL,
    [SettingName] NVARCHAR (30)  NOT NULL,
    [SettingText] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_SMFHmiSettings] PRIMARY KEY CLUSTERED ([UserId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_SMFHmiSettings_SMFUsers] FOREIGN KEY ([UserId]) REFERENCES [smf].[Users] ([Id])
);

