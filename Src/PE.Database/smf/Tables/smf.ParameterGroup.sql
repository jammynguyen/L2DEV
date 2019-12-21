CREATE TABLE [smf].[ParameterGroup] (
    [ParameterGroupId] BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]        DATETIME       CONSTRAINT [DF_ParameterGroup_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]     DATETIME       CONSTRAINT [DF_ParameterGroup_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [Description]      NVARCHAR (200) NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_ParameterGroupId] PRIMARY KEY CLUSTERED ([ParameterGroupId] ASC)
);



