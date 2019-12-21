CREATE TABLE [dbo].[MVHDefectCatalogue] (
    [DefectCatalogueId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedTs]                   DATETIME       CONSTRAINT [DF_MVHDefectCatalogue_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]                DATETIME       CONSTRAINT [DF_MVHDefectCatalogue_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [DefectCatalogueCode]         NVARCHAR (10)  NOT NULL,
    [DefectCatalogueName]         NVARCHAR (50)  NOT NULL,
    [DefectCatalogueDescription]  NVARCHAR (100) NULL,
    [IsActive]                    BIT            CONSTRAINT [DF_DefectCatalogue_IsActive] DEFAULT ((1)) NULL,
    [IsDefault]                   BIT            CONSTRAINT [DF_DefectCatalogue_IsDefault] DEFAULT ((0)) NOT NULL,
    [FKDefectCatalogueCategoryId] BIGINT         NOT NULL,
    [ParentDefectCatalogueId]     BIGINT         NULL,
    CONSTRAINT [PK_DefectCatalogue] PRIMARY KEY CLUSTERED ([DefectCatalogueId] ASC),
    CONSTRAINT [FK_DefectCatalogue_DefectCatalogue] FOREIGN KEY ([ParentDefectCatalogueId]) REFERENCES [dbo].[MVHDefectCatalogue] ([DefectCatalogueId]),
    CONSTRAINT [FK_DefectCatalogue_DefectCatalogueCategory] FOREIGN KEY ([FKDefectCatalogueCategoryId]) REFERENCES [dbo].[MVHDefectCatalogueCategory] ([DefectCatalogueCategoryId])
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DefectCatalogueCode]
    ON [dbo].[MVHDefectCatalogue]([DefectCatalogueCode] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ParentDefectCatalogueId]
    ON [dbo].[MVHDefectCatalogue]([ParentDefectCatalogueId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_DefectCatalogueCategoryId]
    ON [dbo].[MVHDefectCatalogue]([FKDefectCatalogueCategoryId] ASC);

