CREATE TABLE [dbo].[PRMProducts] (
    [ProductId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [CreatedTs]     DATETIME      CONSTRAINT [DF_Product_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]  DATETIME      CONSTRAINT [DF_Product_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [IsDummy]       BIT           CONSTRAINT [DF_PRMProducts_IsDummy] DEFAULT ((0)) NOT NULL,
    [ProductName]   NVARCHAR (50) NOT NULL,
    [FKWorkOrderId] BIGINT        NULL,
    [Weight]        FLOAT (53)    NOT NULL,
    [IsAssigned]    BIT           CONSTRAINT [DF_PRMProducts_IsAssigned] DEFAULT ((0)) NOT NULL,
    [QualityEnum]   SMALLINT      CONSTRAINT [DF_PRMProducts_QualityEnum] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductId] ASC),
    CONSTRAINT [FK_Products_WorkOrders] FOREIGN KEY ([FKWorkOrderId]) REFERENCES [dbo].[PRMWorkOrders] ([WorkOrderId])
);












GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProductName]
    ON [dbo].[PRMProducts]([ProductName] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_WorkOrderId]
    ON [dbo].[PRMProducts]([FKWorkOrderId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_QualityEnum]
    ON [dbo].[PRMProducts]([QualityEnum] ASC)
    INCLUDE([CreatedTs], [ProductName], [FKWorkOrderId], [Weight]);

