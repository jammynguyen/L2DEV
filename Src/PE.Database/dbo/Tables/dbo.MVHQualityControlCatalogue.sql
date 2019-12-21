CREATE TABLE [dbo].[MVHQualityControlCatalogue] (
    [QualityControlCatalogueId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [QualityControlCode]              NVARCHAR (10)  NOT NULL,
    [QualityControlName]              NVARCHAR (50)  NOT NULL,
    [QualityControlDescription]       NVARCHAR (100) NULL,
    [FKUnitOfMeasureId]               BIGINT         NOT NULL,
    [QualityControlDescriptionMethod] SMALLINT       CONSTRAINT [DF_MVHQualityControlCatalogue_QualityControlDescriptionMethod] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVHQualityControlCatalogue] PRIMARY KEY CLUSTERED ([QualityControlCatalogueId] ASC)
);

