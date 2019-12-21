CREATE TABLE [dbo].[PRMHeatSuppliers] (
    [HeatSupplierId] BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SupplierName]   NVARCHAR (50)  NOT NULL,
    [IsDefault]      BIT            CONSTRAINT [DF_PRMHeatSuppliers_IsDefault] DEFAULT ((0)) NOT NULL,
    [Description]    NVARCHAR (200) NULL,
    CONSTRAINT [PK_HeatSuppliers] PRIMARY KEY CLUSTERED ([HeatSupplierId] ASC)
);

