CREATE TABLE [dbo].[MVHQualityControls] (
    [QualityControlId]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKQualityControlCatalogueId] BIGINT         NULL,
    [FKRawMaterialId]             BIGINT         NULL,
    [FKProductId]                 BIGINT         NULL,
    [Result]                      SMALLINT       NOT NULL,
    [MeasuredValue]               FLOAT (53)     NULL,
    [Comments]                    NVARCHAR (400) NULL,
    CONSTRAINT [PK_MVHQualityControls] PRIMARY KEY CLUSTERED ([QualityControlId] ASC),
    CONSTRAINT [FK_MVHQualityControls_MVHQualityControlCatalogue] FOREIGN KEY ([FKQualityControlCatalogueId]) REFERENCES [dbo].[MVHQualityControlCatalogue] ([QualityControlCatalogueId]),
    CONSTRAINT [FK_MVHQualityControls_MVHRawMaterials] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[MVHRawMaterials] ([RawMaterialId]),
    CONSTRAINT [FK_MVHQualityControls_PRMProducts] FOREIGN KEY ([FKProductId]) REFERENCES [dbo].[PRMProducts] ([ProductId])
);





