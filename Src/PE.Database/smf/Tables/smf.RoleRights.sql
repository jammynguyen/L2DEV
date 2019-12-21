CREATE TABLE [smf].[RoleRights] (
    [RoleId]         NVARCHAR (128) NOT NULL,
    [AccessUnitId]   BIGINT         NOT NULL,
    [PermissionType] SMALLINT       CONSTRAINT [DF_SMFRoleRights_PermissionType] DEFAULT ((1)) NOT NULL,
    [RoleRightId]    NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_RoleId] PRIMARY KEY CLUSTERED ([RoleRightId] ASC),
    CONSTRAINT [FK_SMFRoleRights_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [smf].[Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SMFRoleRights_SMFAccessUnits] FOREIGN KEY ([AccessUnitId]) REFERENCES [smf].[AccessUnits] ([AccessUnitId]),
    CONSTRAINT [UQ_RoleIdAccesUnitId] UNIQUE NONCLUSTERED ([RoleId] ASC, [AccessUnitId] ASC)
);



