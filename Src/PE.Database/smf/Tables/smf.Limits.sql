CREATE TABLE [smf].[Limits] (
    [LimitId]         BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]       DATETIME       CONSTRAINT [DF_Limits_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]    DATETIME       CONSTRAINT [DF_Limits_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [Name]            NVARCHAR (50)  NOT NULL,
    [Description]     NVARCHAR (200) NULL,
    [UpperValueFloat] FLOAT (53)     NULL,
    [LowerValueFloat] FLOAT (53)     NULL,
    [UnitId]          BIGINT         NULL,
    [UpperValueInt]   INT            NULL,
    [LowerValueInt]   INT            NULL,
    [UpperValueDate]  DATE           NULL,
    [LowerValueDate]  DATE           NULL,
    [ValueType]       INT            NOT NULL,
    [LimitGroupId]    BIGINT         NOT NULL,
    CONSTRAINT [PK_LimitId] PRIMARY KEY CLUSTERED ([LimitId] ASC),
    CONSTRAINT [CK_Limits_ValueDate] CHECK ([UpperValueDate]>=[LowerValueDate]),
    CONSTRAINT [CK_Limits_ValueFloat] CHECK ([UpperValueFloat]>=[LowerValueFloat]),
    CONSTRAINT [CK_Limits_ValueInt] CHECK ([UpperValueInt]>=[LowerValueInt]),
    CONSTRAINT [FK_Limits_LimitGroupId] FOREIGN KEY ([LimitGroupId]) REFERENCES [smf].[ParameterGroup] ([ParameterGroupId]),
    CONSTRAINT [FK_Limits_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId]),
    CONSTRAINT [UQ_Limits_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);






GO
CREATE NONCLUSTERED INDEX [NCI_UnitId]
    ON [smf].[Limits]([UnitId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_LimitGroupId]
    ON [smf].[Limits]([LimitGroupId] ASC);

