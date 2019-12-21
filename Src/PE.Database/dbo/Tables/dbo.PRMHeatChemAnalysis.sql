CREATE TABLE [dbo].[PRMHeatChemAnalysis] (
    [HeatChemAnalysisId] BIGINT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKHeatId]           BIGINT     NOT NULL,
    [CreatedTs]          DATETIME   CONSTRAINT [DF_HeatChemAnalysis_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]       DATETIME   CONSTRAINT [DF_HeatChemAnalysis_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [SampleTaken]        DATETIME   NULL,
    [Laboratory]         SMALLINT   NULL,
    [Fe]                 FLOAT (53) NULL,
    [C]                  FLOAT (53) NULL,
    [Mn]                 FLOAT (53) NULL,
    [Cr]                 FLOAT (53) NULL,
    [Mo]                 FLOAT (53) NULL,
    [V]                  FLOAT (53) NULL,
    [Ni]                 FLOAT (53) NULL,
    [Co]                 FLOAT (53) NULL,
    [Si]                 FLOAT (53) NULL,
    [P]                  FLOAT (53) NULL,
    [S]                  FLOAT (53) NULL,
    [Cu]                 FLOAT (53) NULL,
    [Nb]                 FLOAT (53) NULL,
    [Al]                 FLOAT (53) NULL,
    [N]                  FLOAT (53) NULL,
    [Ca]                 FLOAT (53) NULL,
    [B]                  FLOAT (53) NULL,
    [Ti]                 FLOAT (53) NULL,
    [Sn]                 FLOAT (53) NULL,
    [O]                  FLOAT (53) NULL,
    [H]                  FLOAT (53) NULL,
    [W]                  FLOAT (53) NULL,
    [Pb]                 FLOAT (53) NULL,
    [Zn]                 FLOAT (53) NULL,
    [As]                 FLOAT (53) NULL,
    [Mg]                 FLOAT (53) NULL,
    [Sb]                 FLOAT (53) NULL,
    [Bi]                 FLOAT (53) NULL,
    [Ta]                 FLOAT (53) NULL,
    [Zr]                 FLOAT (53) NULL,
    [Ce]                 FLOAT (53) NULL,
    [Te]                 FLOAT (53) NULL,
    CONSTRAINT [PK_HeatChemAnalysis] PRIMARY KEY CLUSTERED ([HeatChemAnalysisId] ASC),
    CONSTRAINT [FK_PEHeatChemAnalysis_PEHeats] FOREIGN KEY ([FKHeatId]) REFERENCES [dbo].[PRMHeats] ([HeatId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [NCI_HeatId]
    ON [dbo].[PRMHeatChemAnalysis]([FKHeatId] ASC)
    INCLUDE([C], [Mn], [Cr], [V], [Ni], [Si], [P], [S], [Cu], [Al], [N], [Ti], [H]);

