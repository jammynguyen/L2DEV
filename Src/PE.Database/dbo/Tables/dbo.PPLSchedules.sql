CREATE TABLE [dbo].[PPLSchedules] (
    [ScheduleId]       BIGINT   IDENTITY (1, 1) NOT NULL,
    [CreatedTs]        DATETIME CONSTRAINT [DF_PPLSchedules_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]     DATETIME CONSTRAINT [DF_PPLSchedules_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [FKWorkOrderId]    BIGINT   NOT NULL,
    [OrderSeq]         SMALLINT CONSTRAINT [DF_PPLScheduleItems_OrderSeq] DEFAULT ((0)) NOT NULL,
    [PlannedTime]      BIGINT   CONSTRAINT [DF_PPLScheduleItems_PlannedTime] DEFAULT ((0)) NOT NULL,
    [PlannedStartTime] DATETIME NOT NULL,
    [PlannedEndTime]   DATETIME NOT NULL,
    CONSTRAINT [PK_PPLScheduleItems] PRIMARY KEY CLUSTERED ([ScheduleId] ASC),
    CONSTRAINT [FK_PPLSchedules_PRMWorkOrders1] FOREIGN KEY ([FKWorkOrderId]) REFERENCES [dbo].[PRMWorkOrders] ([WorkOrderId])
);








GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PlannedStartTime]
    ON [dbo].[PPLSchedules]([PlannedStartTime] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PlannedEndTime]
    ON [dbo].[PPLSchedules]([PlannedEndTime] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_OrderSeq]
    ON [dbo].[PPLSchedules]([OrderSeq] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_WorkOrderId]
    ON [dbo].[PPLSchedules]([FKWorkOrderId] ASC);

