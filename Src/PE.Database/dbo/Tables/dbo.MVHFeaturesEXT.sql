CREATE TABLE [dbo].[MVHFeaturesEXT] (
    [FKFeatureId]               BIGINT         NOT NULL,
    [ResNum]                    SMALLINT       NULL,
    [ResDen]                    SMALLINT       NULL,
    [MinValue]                  FLOAT (53)     NULL,
    [MaxValue]                  FLOAT (53)     NULL,
    [ListValues]                NVARCHAR (400) NULL,
    [IsSampled]                 BIT            CONSTRAINT [DF_FeaturesEXT_IsSampled] DEFAULT ((0)) NOT NULL,
    [IsLengthChange]            BIT            CONSTRAINT [DF_MVHFeaturesEXT_IsLengthChange] DEFAULT ((0)) NOT NULL,
    [IsWeightChange]            BIT            CONSTRAINT [DF_MVHFeaturesEXT_IsWeightChange] DEFAULT ((0)) NULL,
    [IsPossibleInversion]       BIT            CONSTRAINT [DF_FeaturesEXT_PossibleInversion] DEFAULT ((0)) NOT NULL,
    [OnAssetEntry]              BIT            CONSTRAINT [DF_MVHFeaturesEXT_OnAssetEntry] DEFAULT ((1)) NULL,
    [EnumTypeOfCut]             SMALLINT       NULL,
    [ExternalFKUnitOfMeasureId] BIGINT         NULL,
    CONSTRAINT [PK_MVFeaturesEXT] PRIMARY KEY CLUSTERED ([FKFeatureId] ASC),
    CONSTRAINT [FK_MVFeaturesEXT_MVFeatures] FOREIGN KEY ([FKFeatureId]) REFERENCES [dbo].[MVHFeatures] ([FeatureId]) ON DELETE CASCADE
);





