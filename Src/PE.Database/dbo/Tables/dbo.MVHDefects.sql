CREATE TABLE [dbo].[MVHDefects] (
    [DefectId]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedTs]           DATETIME       CONSTRAINT [DF_Defects_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [FKDefectCatalogueId] BIGINT         NOT NULL,
    [FKRawMaterialId]     BIGINT         NULL,
    [FKProductId]         BIGINT         NULL,
    [DefectName]          NVARCHAR (50)  NULL,
    [DefectPosition]      SMALLINT       NULL,
    [DefectFrequency]     SMALLINT       NULL,
    [DefectScale]         SMALLINT       NULL,
    [DefectDescription]   NVARCHAR (300) NULL,
    CONSTRAINT [PK_Defects] PRIMARY KEY CLUSTERED ([DefectId] ASC),
    CONSTRAINT [FK_Defects_DefectCatalogue] FOREIGN KEY ([FKDefectCatalogueId]) REFERENCES [dbo].[MVHDefectCatalogue] ([DefectCatalogueId]),
    CONSTRAINT [FK_Defects_MVHRawMaterials] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[MVHRawMaterials] ([RawMaterialId]),
    CONSTRAINT [FK_MVHDefects_PRMProducts] FOREIGN KEY ([FKProductId]) REFERENCES [dbo].[PRMProducts] ([ProductId])
);






GO
CREATE NONCLUSTERED INDEX [NCI_RawMaterialId]
    ON [dbo].[MVHDefects]([FKRawMaterialId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_DefectCatalogueId]
    ON [dbo].[MVHDefects]([FKDefectCatalogueId] ASC);

