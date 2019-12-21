CREATE TABLE [dbo].[DataTypes] (
    [DataTypeId]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [DataTypeName]       NVARCHAR (50) NOT NULL,
    [DataTypeNameDotNet] NVARCHAR (50) NULL,
    [DataType]           NVARCHAR (50) NULL,
    [MaxLength]          SMALLINT      NULL,
    CONSTRAINT [PK_DataTypes] PRIMARY KEY CLUSTERED ([DataTypeId] ASC)
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DataTypeName]
    ON [dbo].[DataTypes]([DataTypeName] ASC);

