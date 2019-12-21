CREATE TABLE [dbo].[RLSCassetteTypes] (
    [CassetteTypeId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]               DATETIME       CONSTRAINT [DF_CassetteTypes_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]            DATETIME       CONSTRAINT [DF_CassetteTypes_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [CassetteTypeName]        NVARCHAR (50)  NOT NULL,
    [CassetteTypeDescription] NVARCHAR (100) NULL,
    [NumberOfRolls]           SMALLINT       NULL,
    [Type]                    SMALLINT       NULL,
    [IsInterCassette]         BIT            CONSTRAINT [DF_RLSCassetteTypes_IsInterCassette] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CassetteTypes] PRIMARY KEY CLUSTERED ([CassetteTypeId] ASC)
);

