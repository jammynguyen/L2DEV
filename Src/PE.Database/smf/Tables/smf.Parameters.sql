CREATE TABLE [smf].[Parameters] (
    [ParameterId]      BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LastUpdateTs]     DATETIME       CONSTRAINT [DF_Parameters_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [Description]      NVARCHAR (200) NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [ValueInt]         INT            NULL,
    [ValueFloat]       FLOAT (53)     NULL,
    [ValueText]        NVARCHAR (50)  NULL,
    [ValueDate]        DATETIME       NULL,
    [ValueType]        INT            NOT NULL,
    [UnitId]           BIGINT         NULL,
    [ParameterGroupId] BIGINT         NOT NULL,
    CONSTRAINT [PK_ParameterId] PRIMARY KEY CLUSTERED ([ParameterId] ASC),
    CONSTRAINT [FK_Parameters_ParameterGroupId] FOREIGN KEY ([ParameterGroupId]) REFERENCES [smf].[ParameterGroup] ([ParameterGroupId]),
    CONSTRAINT [FK_SMFParameters_SMFUnitOfMeasure] FOREIGN KEY ([UnitId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId]),
    CONSTRAINT [UQ_Parameters_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);




GO
CREATE NONCLUSTERED INDEX [NCI_UnitId]
    ON [smf].[Parameters]([UnitId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ParameterGroupId]
    ON [smf].[Parameters]([ParameterGroupId] ASC);

