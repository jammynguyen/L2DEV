CREATE TABLE [dbo].[MVHMeasurements] (
    [MeasurementId]    BIGINT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKFeatureId]      BIGINT     NOT NULL,
    [FKRawMaterialId]  BIGINT     NULL,
    [PassNo]           SMALLINT   CONSTRAINT [DF_MVHMeasurements_PassNo] DEFAULT ((1)) NOT NULL,
    [IsLastPass]       BIT        CONSTRAINT [DF_MVHMeasurements_IsLastPass] DEFAULT ((1)) NOT NULL,
    [IsValid]          BIT        CONSTRAINT [DF_MVMeasurements_Valid] DEFAULT ((1)) NOT NULL,
    [CreatedTs]        DATETIME   CONSTRAINT [DF_Measurements_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [NoOfSamples]      SMALLINT   CONSTRAINT [DF_MVMeasurements_NoOfSamples] DEFAULT ((0)) NOT NULL,
    [ValueAvg]         FLOAT (53) NOT NULL,
    [ValueMin]         FLOAT (53) NULL,
    [ValueMax]         FLOAT (53) NULL,
    [FirstMeasurement] DATETIME   NULL,
    [LastMeasurement]  DATETIME   NULL,
    [ActualLength]     FLOAT (53) NULL,
    CONSTRAINT [PK_MVMeasurements] PRIMARY KEY CLUSTERED ([MeasurementId] ASC) ON [MV_MEASUREMENTS],
    CONSTRAINT [FK_MVHMeasurements_MVHFeatures] FOREIGN KEY ([FKFeatureId]) REFERENCES [dbo].[MVHFeatures] ([FeatureId]),
    CONSTRAINT [FK_MVHMeasurements_MVHRawMaterials] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[MVHRawMaterials] ([RawMaterialId])
) ON [MV_MEASUREMENTS];












GO
CREATE NONCLUSTERED INDEX [NCI_RawMaterialId]
    ON [dbo].[MVHMeasurements]([FKRawMaterialId] ASC)
    ON [MV_MEASUREMENTS];


GO
CREATE NONCLUSTERED INDEX [NCI_FeatureId]
    ON [dbo].[MVHMeasurements]([FKFeatureId] ASC)
    INCLUDE([FKRawMaterialId], [PassNo], [IsLastPass], [IsValid], [CreatedTs], [ValueAvg], [ValueMin], [ValueMax], [ActualLength])
    ON [MV_MEASUREMENTS];






GO
CREATE NONCLUSTERED INDEX [NCI_ValueAvg_FKFeatureId]
    ON [dbo].[MVHMeasurements]([ValueAvg] ASC, [FKFeatureId] ASC)
    INCLUDE([FKRawMaterialId], [PassNo], [IsLastPass], [IsValid], [CreatedTs], [NoOfSamples], [ValueMin], [ValueMax], [FirstMeasurement], [LastMeasurement], [ActualLength])
    ON [MV_MEASUREMENTS];


GO
CREATE NONCLUSTERED INDEX [NCI_FKFeatureId_IsValid_CreatedTs]
    ON [dbo].[MVHMeasurements]([FKFeatureId] ASC, [IsValid] ASC, [CreatedTs] ASC)
    INCLUDE([ValueAvg])
    ON [MV_MEASUREMENTS];


GO
CREATE NONCLUSTERED INDEX [NCI_FKFeatureId_IsValid]
    ON [dbo].[MVHMeasurements]([FKFeatureId] ASC, [IsValid] ASC)
    INCLUDE([CreatedTs], [ValueAvg])
    ON [MV_MEASUREMENTS];


GO
CREATE NONCLUSTERED INDEX [NCI_FKFeatureId_CreatedTs]
    ON [dbo].[MVHMeasurements]([FKFeatureId] ASC, [CreatedTs] ASC)
    INCLUDE([PassNo], [IsLastPass], [IsValid], [ValueAvg], [ValueMin], [ValueMax])
    ON [MV_MEASUREMENTS];


GO
CREATE NONCLUSTERED INDEX [NCI_FKFeatureId]
    ON [dbo].[MVHMeasurements]([FKFeatureId] ASC)
    INCLUDE([FKRawMaterialId], [PassNo], [IsLastPass], [IsValid], [CreatedTs], [NoOfSamples], [ValueAvg], [ValueMin], [ValueMax], [FirstMeasurement], [LastMeasurement], [ActualLength])
    ON [MV_MEASUREMENTS];


GO
CREATE NONCLUSTERED INDEX [NCI_CreatedTs]
    ON [dbo].[MVHMeasurements]([CreatedTs] ASC)
    INCLUDE([FKFeatureId], [PassNo], [IsLastPass], [IsValid], [ValueAvg], [ValueMin], [ValueMax])
    ON [MV_MEASUREMENTS];

