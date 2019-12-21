CREATE TABLE [dbo].[MVHRawMaterialsStepsEXT] (
    [FKRawMaterialStepId] BIGINT   NOT NULL,
    [CreatedTs]           DATETIME CONSTRAINT [DF_MVHRawMaterialsStepsEXT_CreatedTs] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_MVHRawMaterialsStepsEXT] PRIMARY KEY CLUSTERED ([FKRawMaterialStepId] ASC),
    CONSTRAINT [FK_MVHRawMaterialsStepsEXT_MVHRawMaterialsSteps] FOREIGN KEY ([FKRawMaterialStepId]) REFERENCES [dbo].[MVHRawMaterialsSteps] ([RawMaterialStepId]) ON DELETE CASCADE
);

