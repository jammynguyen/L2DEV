CREATE TABLE [dbo].[MVHFeatures] (
    [FeatureId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKAssetId]           BIGINT         NOT NULL,
    [FKUnitOfMeasureId]   BIGINT         NOT NULL,
    [FKDataTypeId]        BIGINT         NOT NULL,
    [IsActive]            BIT            CONSTRAINT [DF_Features_Status] DEFAULT ((1)) NOT NULL,
    [IsTrigger]           BIT            CONSTRAINT [DF_MVFeatures_IsTrigger] DEFAULT ((0)) NOT NULL,
    [IsMaterialRelated]   BIT            CONSTRAINT [DF_MVFeatures_MaterialRelated] DEFAULT ((1)) NOT NULL,
    [IsLengthRelated]     BIT            CONSTRAINT [DF_MVFeatures_LengthRelated] DEFAULT ((0)) NOT NULL,
    [IsNewProcessingStep] BIT            CONSTRAINT [DF_MVHFeatures_IsNewProcessingStep] DEFAULT ((0)) NOT NULL,
    [FeatureCode]         INT            NOT NULL,
    [FeatureName]         NVARCHAR (50)  NOT NULL,
    [FeatureDescription]  NVARCHAR (100) NULL,
    [EnumFeatureType]     SMALLINT       CONSTRAINT [DF_MVHFeatures_EnumFeatureType] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVFeatures] PRIMARY KEY CLUSTERED ([FeatureId] ASC),
    CONSTRAINT [FK_MVHFeatures_DataTypes] FOREIGN KEY ([FKDataTypeId]) REFERENCES [dbo].[DataTypes] ([DataTypeId]),
    CONSTRAINT [FK_MVHFeatures_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_MVHFeatures_UnitOfMeasure] FOREIGN KEY ([FKUnitOfMeasureId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId])
);










GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_FeatureCode]
    ON [dbo].[MVHFeatures]([FeatureCode] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_UnitOfMeasureId]
    ON [dbo].[MVHFeatures]([FKUnitOfMeasureId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_DataTypeId]
    ON [dbo].[MVHFeatures]([FKDataTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_AssetId]
    ON [dbo].[MVHFeatures]([FKAssetId] ASC);

