CREATE TABLE [dbo].[PRMMaterialsEXT] (
    [FKMaterialId] BIGINT   NOT NULL,
    [CreatedTs]    DATETIME CONSTRAINT [DF_PRMMaterialsEXT_CreatedTs] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PRMMaterialsEXT] PRIMARY KEY CLUSTERED ([FKMaterialId] ASC),
    CONSTRAINT [FK_PRMMaterialsEXT_PRMMaterials] FOREIGN KEY ([FKMaterialId]) REFERENCES [dbo].[PRMMaterials] ([MaterialId]) ON DELETE CASCADE
);

