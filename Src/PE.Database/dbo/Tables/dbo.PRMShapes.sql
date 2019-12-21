CREATE TABLE [dbo].[PRMShapes] (
    [ShapeId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [ShapeSymbol]      NVARCHAR (10)  NOT NULL,
    [ShapeName]        NVARCHAR (100) NULL,
    [ShapeDescription] NVARCHAR (255) NULL,
    [IsDefault]        BIT            CONSTRAINT [DF_PRMShapes_IsDefault] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LRProductShape] PRIMARY KEY CLUSTERED ([ShapeId] ASC)
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ShapeSymbol]
    ON [dbo].[PRMShapes]([ShapeSymbol] ASC);

