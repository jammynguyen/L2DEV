CREATE TABLE [dbo].[MNTEquipmentHistory] (
    [EquipmentHistoryId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedTs]          DATETIME       CONSTRAINT [DF_MNTEquipmentHistory_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]       DATETIME       CONSTRAINT [DF_MNTEquipmentHistory_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [FKEquipmentId]      BIGINT         NOT NULL,
    [MaterialsProcessed] BIGINT         NULL,
    [WeightProcessed]    FLOAT (53)     NULL,
    [EquipmentStatus]    SMALLINT       NOT NULL,
    [Remark]             NVARCHAR (200) NULL,
    CONSTRAINT [PK_MNTEquipmentHistory] PRIMARY KEY CLUSTERED ([EquipmentHistoryId] ASC),
    CONSTRAINT [FK_MNTEquipmentHistory_MNTEquipments] FOREIGN KEY ([FKEquipmentId]) REFERENCES [dbo].[MNTEquipments] ([EquipmentId]) ON DELETE CASCADE
);





