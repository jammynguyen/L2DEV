CREATE TABLE [dbo].[PRMWorkOrders] (
    [WorkOrderId]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [CreatedTs]              DATETIME      CONSTRAINT [DF_WorkOrders_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]           DATETIME      CONSTRAINT [DF_WorkOrders_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [WorkOrderStatus]        SMALLINT      CONSTRAINT [DF_PRMWorkOrders_WorkOrderStatus] DEFAULT ((0)) NOT NULL,
    [WorkOrderName]          NVARCHAR (50) NOT NULL,
    [FKProductCatalogueId]   BIGINT        NOT NULL,
    [FKMaterialCatalogueId]  BIGINT        NOT NULL,
    [IsTestOrder]            BIT           CONSTRAINT [DF_WorkOrders_IsTestOrder] DEFAULT ((0)) NOT NULL,
    [TargetOrderWeight]      FLOAT (53)    NOT NULL,
    [TargetOrderWeightMin]   FLOAT (53)    NULL,
    [TargetOrderWeightMax]   FLOAT (53)    NULL,
    [CreatedInL3]            DATETIME      CONSTRAINT [DF_PRMWorkOrders_CreatedInL3] DEFAULT (getdate()) NOT NULL,
    [ToBeCompletedBefore]    DATETIME      CONSTRAINT [DF_PRMWorkOrders_ToBeCompletedBefore] DEFAULT (getdate()) NOT NULL,
    [FKCustomerId]           BIGINT        NULL,
    [FKReheatingGroupId]     BIGINT        NULL,
    [NextAggregate]          NVARCHAR (50) NULL,
    [OperationCode]          NVARCHAR (50) NULL,
    [QualityPolicy]          NVARCHAR (50) NULL,
    [ExtraLabelInformation]  NVARCHAR (50) NULL,
    [ExternalSteelgradeCode] NVARCHAR (50) NULL,
    CONSTRAINT [PK_WorkOrders] PRIMARY KEY CLUSTERED ([WorkOrderId] ASC),
    CONSTRAINT [FK_PRMWorkOrders_PRMCustomers] FOREIGN KEY ([FKCustomerId]) REFERENCES [dbo].[PRMCustomers] ([CustomerId]),
    CONSTRAINT [FK_PRMWorkOrders_PRMMaterialCatalogue] FOREIGN KEY ([FKMaterialCatalogueId]) REFERENCES [dbo].[PRMMaterialCatalogue] ([MaterialCatalogueId]),
    CONSTRAINT [FK_PRMWorkOrders_PRMReheatingGroup] FOREIGN KEY ([FKReheatingGroupId]) REFERENCES [dbo].[PRMReheatingGroup] ([ReheatingGroupId]),
    CONSTRAINT [FK_WorkOrders_ProductCatalogue] FOREIGN KEY ([FKProductCatalogueId]) REFERENCES [dbo].[PRMProductCatalogue] ([ProductCatalogueId]),
    CONSTRAINT [UQ_WorkOrderName] UNIQUE NONCLUSTERED ([WorkOrderName] ASC)
);












GO
CREATE NONCLUSTERED INDEX [NCI_WorkOrderStatus]
    ON [dbo].[PRMWorkOrders]([WorkOrderStatus] ASC)
    INCLUDE([WorkOrderName]);




GO
CREATE NONCLUSTERED INDEX [NCI_ReheatingGroupId]
    ON [dbo].[PRMWorkOrders]([FKReheatingGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ProductCatalogueId]
    ON [dbo].[PRMWorkOrders]([FKProductCatalogueId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_MaterialCatalogueId]
    ON [dbo].[PRMWorkOrders]([FKMaterialCatalogueId] ASC);


GO







GO
CREATE NONCLUSTERED INDEX [NCI_CustomerId]
    ON [dbo].[PRMWorkOrders]([FKCustomerId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ToBeCompletedBefore]
    ON [dbo].[PRMWorkOrders]([ToBeCompletedBefore] ASC);

