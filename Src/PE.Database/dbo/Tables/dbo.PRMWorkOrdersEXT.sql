CREATE TABLE [dbo].[PRMWorkOrdersEXT] (
    [FKWorkOrderId] BIGINT   NOT NULL,
    [CreatedTs]     DATETIME CONSTRAINT [DF_PRMWorkOrdersEXT_CreatedTs] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PEWorkOrdersExt] PRIMARY KEY CLUSTERED ([FKWorkOrderId] ASC),
    CONSTRAINT [FK_WorkOrdersEXT_WorkOrders] FOREIGN KEY ([FKWorkOrderId]) REFERENCES [dbo].[PRMWorkOrders] ([WorkOrderId]) ON DELETE CASCADE
);

