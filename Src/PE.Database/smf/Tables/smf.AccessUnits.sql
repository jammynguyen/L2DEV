CREATE TABLE [smf].[AccessUnits] (
    [AccessUnitId]   BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]           NVARCHAR (50)  NOT NULL,
    [Decription]     NVARCHAR (250) NULL,
    [AccessUnitType] SMALLINT       CONSTRAINT [DF_SMFAccessUnits_AccessUnitType] DEFAULT ((1)) NOT NULL,
    [FKModuleId]     BIGINT         NULL,
    CONSTRAINT [PK_SMFAccessUnits] PRIMARY KEY CLUSTERED ([AccessUnitId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_SMFAccessUnits_SMFModule] FOREIGN KEY ([FKModuleId]) REFERENCES [smf].[Module] ([Id]),
    CONSTRAINT [UK_SMFAccessUnits_Name] UNIQUE NONCLUSTERED ([Name] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [NCI_ModuleId]
    ON [smf].[AccessUnits]([FKModuleId] ASC);

