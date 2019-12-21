CREATE TABLE [dbo].[PRMMaterials] (
    [MaterialId]    BIGINT        IDENTITY (1, 1) NOT NULL,
    [CreatedTs]     DATETIME      CONSTRAINT [DF_Materials_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]  DATETIME      CONSTRAINT [DF_Materials_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [IsDummy]       BIT           CONSTRAINT [DF_Materials_IsDummy] DEFAULT ((0)) NOT NULL,
    [MaterialName]  NVARCHAR (50) NOT NULL,
    [FKWorkOrderId] BIGINT        NOT NULL,
    [Weight]        FLOAT (53)    CONSTRAINT [DF_PRMMaterials_Weight] DEFAULT ((0)) NOT NULL,
    [IsAssigned]    BIT           CONSTRAINT [DF_PRMMaterials_IsAssigned] DEFAULT ((0)) NOT NULL,
    [FKHeatId]      BIGINT        NOT NULL,
    CONSTRAINT [PK_Materials] PRIMARY KEY CLUSTERED ([MaterialId] ASC),
    CONSTRAINT [FK_Materials_WorkOrders] FOREIGN KEY ([FKWorkOrderId]) REFERENCES [dbo].[PRMWorkOrders] ([WorkOrderId]),
    CONSTRAINT [FK_PRMMaterials_PRMHeats] FOREIGN KEY ([FKHeatId]) REFERENCES [dbo].[PRMHeats] ([HeatId])
);


















GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_MaterialName]
    ON [dbo].[PRMMaterials]([MaterialName] ASC);


GO



GO



GO
CREATE NONCLUSTERED INDEX [NCI_WorkOrderId]
    ON [dbo].[PRMMaterials]([FKWorkOrderId] ASC)
    INCLUDE([CreatedTs], [LastUpdateTs], [IsDummy], [MaterialName], [Weight]);








GO
CREATE NONCLUSTERED INDEX [NCI_IsDummy_IsAssigned]
    ON [dbo].[PRMMaterials]([IsDummy] ASC, [IsAssigned] ASC)
    INCLUDE([FKWorkOrderId]);


GO
CREATE NONCLUSTERED INDEX [NCI_IsAssigned]
    ON [dbo].[PRMMaterials]([IsAssigned] ASC)
    INCLUDE([FKWorkOrderId]);

