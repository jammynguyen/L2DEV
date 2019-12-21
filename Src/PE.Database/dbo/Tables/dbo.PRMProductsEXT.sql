CREATE TABLE [dbo].[PRMProductsEXT] (
    [FKProductId] BIGINT   NOT NULL,
    [CreatedTs]   DATETIME CONSTRAINT [DF_PRMProductsEXT_CreatedTs] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PRMProductsEXT] PRIMARY KEY CLUSTERED ([FKProductId] ASC),
    CONSTRAINT [FK_PRMProductsEXT_PRMProducts] FOREIGN KEY ([FKProductId]) REFERENCES [dbo].[PRMProducts] ([ProductId]) ON DELETE CASCADE
);

