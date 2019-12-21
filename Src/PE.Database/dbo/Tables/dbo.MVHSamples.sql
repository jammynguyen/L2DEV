CREATE TABLE [dbo].[MVHSamples] (
    [SampleId]        BIGINT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKMeasurementId] BIGINT     NOT NULL,
    [IsValid]         SMALLINT   CONSTRAINT [DF_MVSamples_Valid] DEFAULT ((1)) NOT NULL,
    [CreatedTs]       DATETIME   CONSTRAINT [DF_Samples_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [Value]           FLOAT (53) NOT NULL,
    [Offset]          FLOAT (53) CONSTRAINT [DF_MVSamples_Offset] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVSamples] PRIMARY KEY CLUSTERED ([SampleId] ASC) ON [MV_SAMPLES],
    CONSTRAINT [FK_MVSamples_MVMeasurements] FOREIGN KEY ([FKMeasurementId]) REFERENCES [dbo].[MVHMeasurements] ([MeasurementId]) ON DELETE CASCADE
) ON [MV_SAMPLES];






GO
CREATE NONCLUSTERED INDEX [NCI_MeasurementId]
    ON [dbo].[MVHSamples]([FKMeasurementId] ASC)
    INCLUDE([CreatedTs], [Value], [IsValid], [Offset])
    ON [MV_SAMPLES];

