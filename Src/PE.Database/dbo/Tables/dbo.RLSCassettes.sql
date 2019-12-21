CREATE TABLE [dbo].[RLSCassettes] (
    [CassetteId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]           DATETIME       CONSTRAINT [DF_Cassettes_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]        DATETIME       CONSTRAINT [DF_Cassettes_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [FKCassetteTypeId]    BIGINT         NOT NULL,
    [FKStandId]           BIGINT         NULL,
    [CassetteName]        NVARCHAR (50)  NOT NULL,
    [CassetteDescription] NVARCHAR (100) NULL,
    [Status]              SMALLINT       CONSTRAINT [DF_RLSCassettes_Status] DEFAULT ((0)) NOT NULL,
    [NumberOfPositions]   SMALLINT       CONSTRAINT [DF_Cassettes_NumberOfPositions] DEFAULT ((1)) NOT NULL,
    [Arrangement]         SMALLINT       CONSTRAINT [DF_RLSCassettes_Arrangement] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Cassettes] PRIMARY KEY CLUSTERED ([CassetteId] ASC),
    CONSTRAINT [FK_Cassettes_CassetteTypes] FOREIGN KEY ([FKCassetteTypeId]) REFERENCES [dbo].[RLSCassetteTypes] ([CassetteTypeId]),
    CONSTRAINT [FK_Cassettes_Stands] FOREIGN KEY ([FKStandId]) REFERENCES [dbo].[RLSStands] ([StandId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0 - undefined, 1 - horizontal, 2- veritical, 3- other', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSCassettes', @level2type = N'COLUMN', @level2name = N'Arrangement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'SRC.Core.Constants.CassetteStatus', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSCassettes', @level2type = N'COLUMN', @level2name = N'Status';

