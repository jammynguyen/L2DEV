CREATE TABLE [dbo].[MNTMemberRoles] (
    [MemberRoleId]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [CreatedTs]             DATETIME      CONSTRAINT [DF_MNTMemberRoles_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]          DATETIME      CONSTRAINT [DF_MNTMemberRoles_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [MemberRoleName]        NVARCHAR (50) NOT NULL,
    [MemberRoleDescription] NCHAR (100)   NULL,
    CONSTRAINT [PK_MNTMemberRoles] PRIMARY KEY CLUSTERED ([MemberRoleId] ASC)
);

