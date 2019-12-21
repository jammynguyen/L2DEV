CREATE TABLE [dbo].[MNTMaintenanceSchedules] (
    [MaintenanceScheduleId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedTs]                      DATETIME       CONSTRAINT [DF_MNTMaintenanceSchedules_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]                   DATETIME       CONSTRAINT [DF_MNTMaintenanceSchedules_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [PlannedStartTime]               DATETIME       NOT NULL,
    [PlannedEndTime]                 DATETIME       NOT NULL,
    [StartTime]                      DATETIME       NULL,
    [EndTime]                        DATETIME       NULL,
    [MaintenenaceScheduleStatus]     SMALLINT       NOT NULL,
    [MaintenanceScheduleName]        NVARCHAR (50)  NULL,
    [MaintenanceScheduleDescription] NVARCHAR (100) NULL,
    [FKEquipmentId]                  BIGINT         NOT NULL,
    CONSTRAINT [PK_MNTMaintenanceSchedules] PRIMARY KEY CLUSTERED ([MaintenanceScheduleId] ASC),
    CONSTRAINT [FK_MNTMaintenanceSchedules_MNTEquipments] FOREIGN KEY ([FKEquipmentId]) REFERENCES [dbo].[MNTEquipments] ([EquipmentId])
);

