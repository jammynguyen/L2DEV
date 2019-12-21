CREATE TABLE [smf].[UserRoles] (
    [UserId]          NVARCHAR (128) NOT NULL,
    [RoleId]          NVARCHAR (128) NOT NULL,
    [IdentityUser_Id] NVARCHAR (128) NULL,
    [Description]     NVARCHAR (150) NULL,
    CONSTRAINT [PK_SMFUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.SMFUserRoles_dbo.SMFRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [smf].[Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.SMFUserRoles_dbo.SMFUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [smf].[Users] ([Id]) ON DELETE CASCADE
);



