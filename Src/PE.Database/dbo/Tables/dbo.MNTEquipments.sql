CREATE TABLE [dbo].[MNTEquipments] (
    [EquipmentId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedTs]             DATETIME       CONSTRAINT [DF_MNTEquipments_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]          DATETIME       CONSTRAINT [DF_MNTEquipments_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [EquipmentCode]         NVARCHAR (10)  NOT NULL,
    [EquipmentName]         NVARCHAR (50)  NULL,
    [EquipmentDescription]  NVARCHAR (100) NULL,
    [FKEquipmentGroupId]    BIGINT         NOT NULL,
    [FKEquipmentSupplierId] BIGINT         NULL,
    [FKAssetId]             BIGINT         NULL,
    [ParentEquipmentId]     BIGINT         NULL,
    [EquipmentStatus]       SMALLINT       CONSTRAINT [DF_MNTEquipments_EquipmentStatus] DEFAULT ((0)) NOT NULL,
    [AccumulationType]      SMALLINT       CONSTRAINT [DF_MNTEquipments_AccumulationType] DEFAULT ((0)) NOT NULL,
    [ActualValue]           BIGINT         NULL,
    [WarningValue]          BIGINT         NULL,
    [AlarmValue]            BIGINT         NULL,
    [IsActive]              BIT            CONSTRAINT [DF_MNTEquipments_IsActive] DEFAULT ((1)) NOT NULL,
    [ServiceExpires]        DATETIME       NULL,
    [CntMatsProcessed]      BIGINT         NULL,
    CONSTRAINT [PK_MNTEquipments] PRIMARY KEY CLUSTERED ([EquipmentId] ASC),
    CONSTRAINT [FK_MNTEquipments_MNTEquipmentGroups] FOREIGN KEY ([FKEquipmentGroupId]) REFERENCES [dbo].[MNTEquipmentGroups] ([EquipmentGroupId]),
    CONSTRAINT [FK_MNTEquipments_MNTEquipments] FOREIGN KEY ([ParentEquipmentId]) REFERENCES [dbo].[MNTEquipments] ([EquipmentId]),
    CONSTRAINT [FK_MNTEquipments_MNTEquipmentSuppliers] FOREIGN KEY ([FKEquipmentSupplierId]) REFERENCES [dbo].[MNTEquipmentSuppliers] ([EquipmentSupplierId]),
    CONSTRAINT [FK_MNTEquipments_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId])
);





