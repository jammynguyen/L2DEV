CREATE TABLE [dbo].[RLSGrooveTemplates] (
    [GrooveTemplateId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]                 DATETIME       CONSTRAINT [DF_RLSGrooveTemplates_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]              DATETIME       CONSTRAINT [DF_RLSGrooveTemplates_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [Shape]                     NVARCHAR (5)   NOT NULL,
    [GrooveTemplateCode]        NVARCHAR (10)  NULL,
    [GrooveTemplateName]        NVARCHAR (50)  NOT NULL,
    [GrooveTemplateDescription] NVARCHAR (100) NULL,
    [GrindingProgramName]       NVARCHAR (50)  NULL,
    [Status]                    SMALLINT       CONSTRAINT [DF_RLSGrooveTemplates_Status] DEFAULT ((0)) NOT NULL,
    [R1]                        FLOAT (53)     NULL,
    [R2]                        FLOAT (53)     NULL,
    [R3]                        FLOAT (53)     NULL,
    [D1]                        FLOAT (53)     NULL,
    [D2]                        FLOAT (53)     NULL,
    [W1]                        FLOAT (53)     NULL,
    [W2]                        FLOAT (53)     NULL,
    [Angle1]                    FLOAT (53)     NULL,
    [Angle2]                    FLOAT (53)     NULL,
    [Ds]                        FLOAT (53)     NULL,
    [Dw]                        FLOAT (53)     NULL,
    [SpreadFactor]              FLOAT (53)     NULL,
    [Plane]                     FLOAT (53)     NULL,
    [IsDefault]                 BIT            NULL,
    CONSTRAINT [PK_GrooveTemplates] PRIMARY KEY CLUSTERED ([GrooveTemplateId] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'm2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'Plane';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Angle 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'Angle2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Angle 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'Angle1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Width 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'W2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Width 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'W1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Depth 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'D2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Depth 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'D1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Radius 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'R3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Radius 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'R2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Radius 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'R1';

