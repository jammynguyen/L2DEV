CREATE TABLE [dbo].[MNTMembers] (
    [MemberId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedTs]      DATETIME       CONSTRAINT [DF_MNTMembers_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]   DATETIME       CONSTRAINT [DF_MNTMembers_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [MemberName]     NVARCHAR (50)  NOT NULL,
    [FKMemberRoleId] BIGINT         NOT NULL,
    [FKUserId]       NVARCHAR (128) NULL,
    [CostPerHour]    FLOAT (53)     CONSTRAINT [DF_MNTMembers_CostPerHour] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTMembers] PRIMARY KEY CLUSTERED ([MemberId] ASC),
    CONSTRAINT [FK_MNTMembers_MNTMemberRoles] FOREIGN KEY ([FKMemberRoleId]) REFERENCES [dbo].[MNTMemberRoles] ([MemberRoleId]),
    CONSTRAINT [FK_MNTMembers_Users] FOREIGN KEY ([FKUserId]) REFERENCES [smf].[Users] ([Id])
);

