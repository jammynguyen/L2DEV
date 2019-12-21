CREATE TABLE [dbo].[MNTMaintenanceActions] (
    [MaintenanceActionId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedTs]               DATETIME       CONSTRAINT [DF_MNTMaintenanceActions_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]            DATETIME       CONSTRAINT [DF_MNTMaintenanceActions_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [FKMaintenanceScheduleId] BIGINT         NOT NULL,
    [FKMemberId]              BIGINT         NOT NULL,
    [ActionStatus]            SMALLINT       CONSTRAINT [DF_MNTMaintenanceActions_ActionStatus] DEFAULT ((0)) NOT NULL,
    [ActionName]              NVARCHAR (50)  NOT NULL,
    [ActionDescription]       NVARCHAR (100) NULL,
    CONSTRAINT [FK_MNTMaintenanceActions_MNTMaintenanceSchedules] FOREIGN KEY ([FKMaintenanceScheduleId]) REFERENCES [dbo].[MNTMaintenanceSchedules] ([MaintenanceScheduleId]),
    CONSTRAINT [FK_MNTMaintenanceActions_MNTMembers] FOREIGN KEY ([FKMemberId]) REFERENCES [dbo].[MNTMembers] ([MemberId])
);

