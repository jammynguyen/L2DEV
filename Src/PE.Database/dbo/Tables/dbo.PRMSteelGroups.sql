CREATE TABLE [dbo].[PRMSteelGroups] (
    [SteelGroupId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]             DATETIME       CONSTRAINT [DF_SteelGroups_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]          DATETIME       CONSTRAINT [DF_SteelGroups_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [SteelGroupCode]        NVARCHAR (10)  NULL,
    [SteelGroupName]        NVARCHAR (50)  NOT NULL,
    [SteelGroupDescription] NVARCHAR (100) NULL,
    [ParentSteelGroupId]    BIGINT         NULL,
    [IsDefault]             BIT            CONSTRAINT [DF_SteelGroups_IsDefault] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SteelGroupId] PRIMARY KEY CLUSTERED ([SteelGroupId] ASC),
    CONSTRAINT [FK_SteelGroups_ParentSteelGroupId] FOREIGN KEY ([ParentSteelGroupId]) REFERENCES [dbo].[PRMSteelGroups] ([SteelGroupId]),
    CONSTRAINT [UQ_SteelGroupName] UNIQUE NONCLUSTERED ([SteelGroupName] ASC)
);






GO
CREATE NONCLUSTERED INDEX [NCI_ParentSteelgroupId]
    ON [dbo].[PRMSteelGroups]([ParentSteelGroupId] ASC);

