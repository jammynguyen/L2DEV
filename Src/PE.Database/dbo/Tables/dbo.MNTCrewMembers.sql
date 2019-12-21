CREATE TABLE [dbo].[MNTCrewMembers] (
    [CrewMemberId] BIGINT   IDENTITY (1, 1) NOT NULL,
    [CreatedTs]    DATETIME CONSTRAINT [DF_MNTCrewMembers_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs] DATETIME CONSTRAINT [DF_MNTCrewMembers_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [FKCrewId]     BIGINT   NOT NULL,
    [FKMemberId]   BIGINT   NOT NULL,
    CONSTRAINT [PK_MNTCrewMembers] PRIMARY KEY CLUSTERED ([CrewMemberId] ASC),
    CONSTRAINT [FK_MNTCrewMembers_Crews] FOREIGN KEY ([FKCrewId]) REFERENCES [dbo].[Crews] ([CrewId]),
    CONSTRAINT [FK_MNTCrewMembers_MNTMembers] FOREIGN KEY ([FKMemberId]) REFERENCES [dbo].[MNTMembers] ([MemberId])
);

