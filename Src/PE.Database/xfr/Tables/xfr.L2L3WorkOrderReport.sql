CREATE TABLE [xfr].[L2L3WorkOrderReport] (
    [Counter]                BIGINT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]              DATETIME      CONSTRAINT [DF_L2L3WorkOrderReport_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [UpdatedTs]              DATETIME      CONSTRAINT [DF_L2L3WorkOrderReport_UpdatedTs] DEFAULT (getdate()) NOT NULL,
    [CommStatus]             SMALLINT      CONSTRAINT [DF_L3L2WorkOrderReport_CommStatus] DEFAULT ((0)) NOT NULL,
    [WorkOrderName]          NVARCHAR (50) NOT NULL,
    [ProductName]            NVARCHAR (50) NOT NULL,
    [ProductsNumber]         SMALLINT      NOT NULL,
    [TotalProductsWeight]    FLOAT (53)    NOT NULL,
    [TotalRawMaterialWeight] FLOAT (53)    NOT NULL,
    [RawMaterialsPlanned]    SMALLINT      NOT NULL,
    [RawMaterialsDischarged] SMALLINT      NOT NULL,
    [RawMaterialsRolled]     SMALLINT      NOT NULL,
    [RawMaterialsScrapped]   SMALLINT      NOT NULL,
    [RawMaterialsRejected]   SMALLINT      NOT NULL,
    [MetallicYield]          FLOAT (53)    NOT NULL,
    [EventType]              SMALLINT      NOT NULL,
    [ProductionStart]        DATETIME      NULL,
    [ProductionEnd]          DATETIME      NULL,
    CONSTRAINT [PK_L2L3WorkOrderReport] PRIMARY KEY CLUSTERED ([Counter] ASC),
    CONSTRAINT [CHK_CommStatusL2] CHECK ([CommStatus]>=(0) AND [CommStatus]<=(3)),
    CONSTRAINT [CHK_EventType] CHECK ([EventType]>=(1) AND [EventType]<=(3)),
    CONSTRAINT [CHK_MetallicYield] CHECK ([MetallicYield]>=(0) AND [MetallicYield]<=(1)),
    CONSTRAINT [CHK_ProductsNumber] CHECK ([ProductsNumber]>=(0)),
    CONSTRAINT [CHK_RawMaterialsPlanned] CHECK ([RawMaterialsPlanned]>=(0)),
    CONSTRAINT [CHK_TotalProductsWeight] CHECK ([TotalProductsWeight]>=(0)),
    CONSTRAINT [CHK_TotalRawMaterialWeight] CHECK ([TotalRawMaterialWeight]>=(0))
);





