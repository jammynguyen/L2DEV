CREATE TABLE [dbo].[MNTEquipmentSuppliers] (
    [EquipmentSupplierId]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedTs]                DATETIME       CONSTRAINT [DF_MNTEquipmentSuppliers_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]             DATETIME       CONSTRAINT [DF_MNTEquipmentSuppliers_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [EquipmentSupplierName]    NVARCHAR (50)  NOT NULL,
    [EquipmentSupplierAddress] NVARCHAR (200) NULL,
    [EquipmentSupplierEmail]   NVARCHAR (150) NULL,
    [EquipmentSupplierPhone]   NVARCHAR (20)  NULL,
    [SAPKey]                   NVARCHAR (20)  NULL,
    [IsActive]                 BIT            CONSTRAINT [DF_MNTEquipmentSuppliers_IsActive] DEFAULT ((1)) NOT NULL,
    [IsDefault]                BIT            CONSTRAINT [DF_MNTEquipmentSuppliers_IsDefault] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTEquipmentSuppliers] PRIMARY KEY CLUSTERED ([EquipmentSupplierId] ASC)
);

