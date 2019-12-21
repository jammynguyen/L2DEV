CREATE TABLE [dbo].[MVHRawMaterials] (
    [RawMaterialId]         BIGINT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RawMaterialName]       NVARCHAR (50) NOT NULL,
    [CreatedTs]             DATETIME      CONSTRAINT [DF_Billets_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]          DATETIME      CONSTRAINT [DF_Billets_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [IsDummy]               BIT           CONSTRAINT [DF_Billets_IsDummyBillet] DEFAULT ((0)) NOT NULL,
    [IsVirtual]             BIT           CONSTRAINT [DF_MVHRawMaterials_IsVirtual] DEFAULT ((0)) NOT NULL,
    [Status]                SMALLINT      CONSTRAINT [DF_RawMaterialsIndex_Status] DEFAULT ((0)) NOT NULL,
    [LastProcessingStepNo]  SMALLINT      CONSTRAINT [DF_RawMaterialsIndex_LastProcessingStepNo] DEFAULT ((0)) NOT NULL,
    [CuttingSeqNo]          SMALLINT      CONSTRAINT [DF_MVHRawMaterials_CuttingSeqNo] DEFAULT ((0)) NOT NULL,
    [ChildsNo]              SMALLINT      CONSTRAINT [DF_MVHRawMaterials_ChildsNo] DEFAULT ((0)) NOT NULL,
    [FKMaterialId]          BIGINT        NULL,
    [FKProductId]           BIGINT        NULL,
    [ParentRawMaterialId]   BIGINT        NULL,
    [ExternalRawMaterialId] BIGINT        NOT NULL,
    [IsTransferred2DW]      BIT           CONSTRAINT [DF_MVHRawMaterials_IsTransferred2DW] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RawMaterialId] PRIMARY KEY CLUSTERED ([RawMaterialId] ASC),
    CONSTRAINT [FK_RawMaterialsIndex_Materials] FOREIGN KEY ([FKMaterialId]) REFERENCES [dbo].[PRMMaterials] ([MaterialId]),
    CONSTRAINT [FK_RawMaterialsIndex_Products] FOREIGN KEY ([FKProductId]) REFERENCES [dbo].[PRMProducts] ([ProductId]),
    CONSTRAINT [FK_RawMaterialsIndex_RawMaterialsIndex] FOREIGN KEY ([ParentRawMaterialId]) REFERENCES [dbo].[MVHRawMaterials] ([RawMaterialId])
);


















GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_RawMaterialName]
    ON [dbo].[MVHRawMaterials]([RawMaterialName] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_Status]
    ON [dbo].[MVHRawMaterials]([Status] ASC)
    INCLUDE([CreatedTs], [LastUpdateTs], [FKMaterialId]);




GO
CREATE NONCLUSTERED INDEX [NCI_ParentRawMaterialId]
    ON [dbo].[MVHRawMaterials]([ParentRawMaterialId] ASC);


GO



GO



GO
CREATE NONCLUSTERED INDEX [NCI_ExternalRawMaterialId]
    ON [dbo].[MVHRawMaterials]([ExternalRawMaterialId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ProductId]
    ON [dbo].[MVHRawMaterials]([FKProductId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_MaterialId]
    ON [dbo].[MVHRawMaterials]([FKMaterialId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_IsVirtual_Status]
    ON [dbo].[MVHRawMaterials]([IsVirtual] ASC, [Status] ASC)
    INCLUDE([RawMaterialName], [CreatedTs], [CuttingSeqNo], [ChildsNo], [FKMaterialId], [FKProductId], [ParentRawMaterialId], [ExternalRawMaterialId]);


GO
-- =============================================
-- Author:		Klakla
-- Create date: 2019
-- Description:	LastUpdateTs
-- =============================================
CREATE TRIGGER [dbo].[TrgRawMaterialsUpdate] ON [dbo].[MVHRawMaterials]
AFTER UPDATE
AS
     BEGIN
         SET NOCOUNT ON;
         DECLARE @RawMaterialId AS BIGINT;
         DECLARE @FKMaterialId AS BIGINT;
         DECLARE @FKProductId AS BIGINT;
         SELECT @RawMaterialId = inserted.RawMaterialId, 
                @FKMaterialId = inserted.FKMaterialId, 
                @FKProductId = inserted.FKProductId
         FROM inserted;
         UPDATE [dbo].[MVHRawMaterials]
           SET 
               [LastUpdateTs] = GETDATE()
         WHERE RawMaterialId = @RawMaterialId;
         --when Material is assigned then update L3 Materials
         IF @FKMaterialId > 0
             BEGIN
                 UPDATE [dbo].[PRMMaterials]
                   SET 
                       IsAssigned = 1
                 WHERE MaterialId = @FKMaterialId;
         END;
         --when Product is assigned then update Products
         IF @FKProductId > 0
             BEGIN
                 UPDATE [dbo].[PRMProducts]
                   SET 
                       IsAssigned = 1
                 WHERE ProductId = @FKProductId;
         END;
     END;
GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ExternalRawMaterialId]
    ON [dbo].[MVHRawMaterials]([ExternalRawMaterialId] ASC);

