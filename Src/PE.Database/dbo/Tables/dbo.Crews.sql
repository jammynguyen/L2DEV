CREATE TABLE [dbo].[Crews] (
    [CrewId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedTs]    DATETIME       CONSTRAINT [DF_Crews_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs] DATETIME       CONSTRAINT [DF_Crews_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [CrewName]     NVARCHAR (50)  NOT NULL,
    [Description]  NVARCHAR (100) NULL,
    [LeaderName]   NVARCHAR (100) NULL,
    [DfltCrewSize] SMALLINT       NULL,
    [OrderSeq]     SMALLINT       NULL,
    [NextCrewId]   BIGINT         NULL,
    CONSTRAINT [PK_Crews] PRIMARY KEY CLUSTERED ([CrewId] ASC),
    CONSTRAINT [FK_Crews_Crews] FOREIGN KEY ([NextCrewId]) REFERENCES [dbo].[Crews] ([CrewId]),
    CONSTRAINT [UQ_Crews_Name] UNIQUE NONCLUSTERED ([CrewName] ASC)
);



