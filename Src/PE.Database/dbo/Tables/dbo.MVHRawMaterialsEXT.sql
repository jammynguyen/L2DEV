CREATE TABLE [dbo].[MVHRawMaterialsEXT] (
    [FKRawMaterialId] BIGINT   NOT NULL,
    [CreatedTs]       DATETIME CONSTRAINT [DF_RawMaterialsEXT_CreatedTs] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_RawMaterialsIndexEXT] PRIMARY KEY CLUSTERED ([FKRawMaterialId] ASC),
    CONSTRAINT [FK_RawMaterialsIndexEXT_RawMaterialsIndex] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[MVHRawMaterials] ([RawMaterialId]) ON DELETE CASCADE
);

