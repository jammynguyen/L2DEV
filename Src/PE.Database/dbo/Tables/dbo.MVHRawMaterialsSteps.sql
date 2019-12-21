CREATE TABLE [dbo].[MVHRawMaterialsSteps] (
    [RawMaterialStepId] BIGINT     IDENTITY (1, 1) NOT NULL,
    [ProcessingStepNo]  SMALLINT   NOT NULL,
    [FKRawMaterialId]   BIGINT     NOT NULL,
    [FKAssetId]         BIGINT     NULL,
    [FKFeatureIdRef]    BIGINT     NULL,
    [PassNo]            SMALLINT   CONSTRAINT [DF_MVHRawMaterialsSteps_PassNo] DEFAULT ((1)) NOT NULL,
    [IsLastPass]        BIT        CONSTRAINT [DF_MVHRawMaterialsSteps_IsLastPass] DEFAULT ((1)) NOT NULL,
    [IsReversed]        BIT        CONSTRAINT [DF_MVHRawMaterialsSteps_IsReversed] DEFAULT ((0)) NOT NULL,
    [CreatedTs]         DATETIME   CONSTRAINT [DF_PERawMaterialsExt_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]      DATETIME   CONSTRAINT [DF_PERawMaterialsExt_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [Weight]            FLOAT (53) NULL,
    [Width]             FLOAT (53) NULL,
    [Thickness]         FLOAT (53) NULL,
    [Length]            FLOAT (53) NULL,
    [RelLength]         FLOAT (53) NULL,
    [Elongation]        FLOAT (53) NULL,
    [MotherOffset]      FLOAT (53) NULL,
    [EnumTypeOfCut]     SMALLINT   NULL,
    [HeadPartLength]    FLOAT (53) NULL,
    [TailPartLength]    FLOAT (53) NULL,
    [HeadPartCumm]      FLOAT (53) NULL,
    [TailPartCumm]      FLOAT (53) NULL,
    CONSTRAINT [PK_RawMaterialStepId] PRIMARY KEY CLUSTERED ([RawMaterialStepId] ASC),
    CONSTRAINT [FK_MVHRawMaterialsSteps_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_MVHRawMaterialsSteps_MVHFeatures] FOREIGN KEY ([FKFeatureIdRef]) REFERENCES [dbo].[MVHFeatures] ([FeatureId]),
    CONSTRAINT [FK_PERawMaterialsStep_PERawMaterialsIndex] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[MVHRawMaterials] ([RawMaterialId]) ON DELETE CASCADE
);




















GO





GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PE.Core.Constants.MaterialShapeType', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MVHRawMaterialsSteps';


GO
CREATE NONCLUSTERED INDEX [NCI_ProcessingStepNo]
    ON [dbo].[MVHRawMaterialsSteps]([ProcessingStepNo] ASC)
    INCLUDE([FKRawMaterialId], [FKAssetId], [FKFeatureIdRef], [PassNo], [IsLastPass], [IsReversed], [CreatedTs], [LastUpdateTs], [Weight], [Width], [Thickness], [Length], [RelLength], [Elongation], [MotherOffset], [EnumTypeOfCut], [HeadPartLength], [TailPartLength], [HeadPartCumm], [TailPartCumm]);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [NCI_RawMaterialId_ProcessingStepNo]
    ON [dbo].[MVHRawMaterialsSteps]([FKRawMaterialId] ASC, [ProcessingStepNo] ASC)
    INCLUDE([FKAssetId], [LastUpdateTs], [Weight], [Length]);


GO
CREATE NONCLUSTERED INDEX [NCI_RawMaterialId]
    ON [dbo].[MVHRawMaterialsSteps]([FKRawMaterialId] ASC)
    INCLUDE([ProcessingStepNo], [FKAssetId], [PassNo], [IsLastPass], [IsReversed], [Thickness], [Length], [HeadPartLength], [TailPartLength], [CreatedTs], [LastUpdateTs], [Weight], [Width], [HeadPartCumm], [TailPartCumm], [RelLength], [Elongation], [MotherOffset], [EnumTypeOfCut]);


GO
CREATE NONCLUSTERED INDEX [NCI_FeatureIdRef]
    ON [dbo].[MVHRawMaterialsSteps]([FKFeatureIdRef] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_AssetId]
    ON [dbo].[MVHRawMaterialsSteps]([FKAssetId] ASC)
    INCLUDE([FKRawMaterialId], [Weight]);




GO
CREATE NONCLUSTERED INDEX [NCI_ProcessingStepNo_AssetId]
    ON [dbo].[MVHRawMaterialsSteps]([ProcessingStepNo] ASC, [FKAssetId] ASC)
    INCLUDE([FKRawMaterialId], [LastUpdateTs], [Weight], [Length]);






GO
-- =============================================
-- Author:		Klakla
-- Create date: 2019
-- Description:	LastUpdateTs
-- =============================================
CREATE TRIGGER [dbo].[TrgRawMaterialsStepsUpdate] ON [dbo].[MVHRawMaterialsSteps]
AFTER UPDATE
--INSTEAD OF UPDATE
AS
     BEGIN
         SET NOCOUNT ON;
         UPDATE [dbo].[MVHRawMaterialsSteps]
           SET 
               [LastUpdateTs] = GETDATE()
         FROM [dbo].[MVHRawMaterialsSteps]
              INNER JOIN inserted ON [dbo].[MVHRawMaterialsSteps].RawMaterialStepId = inserted.RawMaterialStepId;
     END;
GO
CREATE NONCLUSTERED INDEX [NCI_CreatedTs]
    ON [dbo].[MVHRawMaterialsSteps]([CreatedTs] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ProcessingStepNo_FKAssetId]
    ON [dbo].[MVHRawMaterialsSteps]([ProcessingStepNo] ASC, [FKAssetId] ASC)
    INCLUDE([FKRawMaterialId], [PassNo], [CreatedTs], [Length]);

