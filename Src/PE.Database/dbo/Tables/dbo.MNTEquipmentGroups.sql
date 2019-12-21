CREATE TABLE [dbo].[MNTEquipmentGroups] (
    [EquipmentGroupId]          BIGINT         IDENTITY (100, 1) NOT NULL,
    [CreatedTs]                 DATETIME       CONSTRAINT [DF_MNTEquipmentGroups_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]              DATETIME       CONSTRAINT [DF_MNTEquipmentGroups_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [EquipmentGroupCode]        NVARCHAR (10)  NOT NULL,
    [EquipmentGroupName]        NVARCHAR (50)  NULL,
    [EquipmentGroupDescription] NVARCHAR (100) NULL,
    [ParentEquipmentGroupId]    BIGINT         NULL,
    [IsDefault]                 BIT            CONSTRAINT [DF_MNTEquipmentGroups_IsDefault] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTEquipmentGroups] PRIMARY KEY CLUSTERED ([EquipmentGroupId] ASC),
    CONSTRAINT [FK_MNTEquipmentGroups_MNTEquipmentGroups] FOREIGN KEY ([ParentEquipmentGroupId]) REFERENCES [dbo].[MNTEquipmentGroups] ([EquipmentGroupId])
);



