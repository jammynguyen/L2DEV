CREATE TABLE [dbo].[MVHMeasurementsEXT] (
    [FKMeasurementId] BIGINT   NOT NULL,
    [CreatedTs]       DATETIME CONSTRAINT [DF_MeasurementsEXT_CreatedTs] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_MVMeasurementsEXT] PRIMARY KEY CLUSTERED ([FKMeasurementId] ASC) ON [MV_MEASUREMENTS],
    CONSTRAINT [FK_MeasurementsEXT_Measurements] FOREIGN KEY ([FKMeasurementId]) REFERENCES [dbo].[MVHMeasurements] ([MeasurementId]) ON DELETE CASCADE
) ON [MV_MEASUREMENTS];

