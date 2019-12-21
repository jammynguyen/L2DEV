CREATE TABLE [xfr].[L3L2WorkOrderDefinition] (
    [CounterId]              BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreatedTs]              DATETIME       CONSTRAINT [DF_L3L2WorkOrderDefinition_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [UpdatedTs]              DATETIME       CONSTRAINT [DF_L3L2WorkOrderDefinition_UpdatedTs] DEFAULT (getdate()) NOT NULL,
    [CommStatus]             SMALLINT       CONSTRAINT [DF_L3L2WorkOrderDefinition_CommStatus] DEFAULT ((0)) NULL,
    [CommMessage]            NVARCHAR (400) NULL,
    [WorkOrderName]          NVARCHAR (50)  NULL,
    [CustomerName]           NVARCHAR (100) NULL,
    [ProductName]            NVARCHAR (100) NULL,
    [HeatName]               NVARCHAR (100) NULL,
    [CreatedInL3]            DATETIME       NULL,
    [ToBeCompletedBefore]    DATETIME       NULL,
    [RawMaterialNumber]      SMALLINT       NULL,
    [TargetOrderWeight]      FLOAT (53)     NULL,
    [TargetOrderWeightMin]   FLOAT (53)     NULL,
    [TargetOrderWeightMax]   FLOAT (53)     NULL,
    [RawMaterialThickness]   FLOAT (53)     NULL,
    [RawMaterialWidth]       FLOAT (53)     NULL,
    [RawMaterialLength]      FLOAT (53)     NULL,
    [RawMaterialWeight]      FLOAT (53)     NULL,
    [RawMaterialType]        NVARCHAR (10)  NULL,
    [RawMaterialShapeType]   NVARCHAR (10)  NULL,
    [SteelgradeCode]         NVARCHAR (10)  NULL,
    [ExternalSteelgradeCode] NVARCHAR (50)  NULL,
    [NextAggregate]          NVARCHAR (50)  NULL,
    [OperationCode]          NVARCHAR (50)  NULL,
    [ReheatingGroupName]     NVARCHAR (50)  NULL,
    [QualityPolicy]          NVARCHAR (50)  NULL,
    [ExtraLabelInformation]  NVARCHAR (50)  NULL,
    [ValidationCheck]        NVARCHAR (400) NULL,
    [WorkOrderNameStatus]    NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Counter] PRIMARY KEY CLUSTERED ([CounterId] ASC)
);










GO
CREATE TRIGGER [xfr].[ValidateOnInsert] 
   ON  xfr.L3L2WorkOrderDefinition
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;
	DECLARE @CounterId BIGINT;
	DECLARE @CommStatus SMALLINT;
	DECLARE @WorkOrderName nvarchar(30);
	DECLARE @WorkOrderNameStatus nvarchar(50);
	DECLARE @ShapeSymbol nvarchar(10);
	DECLARE @SteelgradeCode nvarchar(10);
	DECLARE @ProductName nvarchar(100);
	DECLARE @CHK_CommStatus BIT=0;
	DECLARE @CHK_WorkOrderName BIT=0;
	DECLARE @CHK_ProductName BIT=0;
	DECLARE @CHK_CreatedInL3 BIT=0;
	DECLARE @CHK_RawMaterialLength BIT=0;
	DECLARE @CHK_RawMaterialNumber BIT=0;
	DECLARE @CHK_RawMaterialShapeType BIT=0;
	DECLARE @CHK_RawMaterialThickness BIT=0;
	DECLARE @CHK_RawMaterialWeight BIT=0;
	DECLARE @CHK_SteelgradeCode BIT=0;
	DECLARE @CHK_RawMaterialWidth BIT=0;
	DECLARE @CHK_TargetOrderWeight BIT=0;
	DECLARE @CHK_TargetOrderWeightMax BIT=0;
	DECLARE @CHK_TargetOrderWeightMin BIT=0;
	DECLARE @CHK_ToBeCompletedBefore BIT=0;
	DECLARE @MessageInfo NVARCHAR(4000);

SELECT @ShapeSymbol = INSERTED.RawMaterialShapeType	FROM INSERTED
SELECT @SteelgradeCode = INSERTED.SteelgradeCode	FROM INSERTED
SELECT @ProductName = INSERTED.ProductName	FROM INSERTED

IF EXISTS (SELECT * FROM PRMShapes WHERE ShapeSymbol=@ShapeSymbol) SET @CHK_RawMaterialShapeType=1 ELSE SET @CHK_RawMaterialShapeType=0
IF EXISTS (SELECT * FROM PRMSteelgrades WHERE SteelgradeCode=@SteelgradeCode) SET @CHK_SteelgradeCode=1 ELSE SET @CHK_SteelgradeCode=0
IF EXISTS (SELECT * FROM PRMProductCatalogue WHERE ProductCatalogueName=@ProductName) SET @CHK_ProductName=1 ELSE SET @CHK_ProductName=0

SELECT @CounterId = CounterId, @CommStatus = CommStatus, @WorkorderName = WorkOrderName,
@CHK_CommStatus = CASE WHEN ([CommStatus]>=(-2) AND [CommStatus]<=(2)) THEN 1 ELSE 0 END,
@CHK_WorkOrderName = CASE WHEN ([WorkOrderName] IS NOT NULL) THEN 1 ELSE 0 END,
@CHK_CreatedInL3 = CASE WHEN ([CreatedInL3]<=getdate()) THEN 1 ELSE 0 END,
@CHK_RawMaterialLength = CASE WHEN ([RawMaterialLength]>=(0) AND [RawMaterialLength]<=(50)) THEN 1 ELSE 0 END,
@CHK_RawMaterialNumber = CASE WHEN ([RawMaterialNumber]>(0)) THEN 1 ELSE 0 END,
@CHK_RawMaterialWeight = CASE WHEN ([RawMaterialWeight]>(10)) THEN 1 ELSE 0 END,
@CHK_RawMaterialThickness = CASE WHEN ([RawMaterialThickness]>=(0) AND [RawMaterialThickness]<=(1)) THEN 1 ELSE 0 END,
@CHK_RawMaterialWidth = CASE WHEN ([RawMaterialWidth]>=(0) AND [RawMaterialWidth]<=(5)) THEN 1 ELSE 0 END,
@CHK_TargetOrderWeight = CASE WHEN ([TargetOrderWeight]>(10)) AND [TargetOrderWeight] BETWEEN [TargetOrderWeightMin] AND [TargetOrderWeightMax] THEN 1 ELSE 0 END,
@CHK_TargetOrderWeightMax = CASE WHEN ([TargetOrderWeightMax]>(10)) THEN 1 ELSE 0 END,
@CHK_TargetOrderWeightMin = CASE WHEN ([TargetOrderWeightMin]>(10)) THEN 1 ELSE 0 END,
@CHK_ToBeCompletedBefore = CASE WHEN ([ToBeCompletedBefore]>=[CreatedInL3]) THEN 1 ELSE 0 END
FROM INSERTED

IF (@CommStatus IS NOT NULL)
SET @CommStatus = CAST(@CHK_CommStatus AS INT) * CAST(@CHK_WorkOrderName AS INT) * CAST(@CHK_SteelgradeCode AS INT) * CAST(@CHK_CreatedInL3 AS INT) * CAST(@CHK_RawMaterialLength AS INT) * CAST(@CHK_RawMaterialNumber AS INT) * CAST(@CHK_RawMaterialShapeType AS INT) * CAST(@CHK_RawMaterialThickness AS INT) * CAST(@CHK_RawMaterialWeight AS INT) * CAST(@CHK_RawMaterialWidth AS INT) * CAST(@CHK_TargetOrderWeight AS INT) * CAST(@CHK_TargetOrderWeightMax AS INT) * CAST(@CHK_TargetOrderWeightMin AS INT) * CAST(@CHK_ToBeCompletedBefore AS INT)

IF (@CommStatus = 0) 
SET @CommStatus = -1 ELSE SET @CommStatus = 0

SET @MessageInfo = CONCAT(' CommStatus: ',@CHK_CommStatus,
							' WorkOrderName: ',@CHK_WorkOrderName,
							' ProductName: ',@CHK_ProductName,
							' SteelgradeCode: ',@CHK_SteelgradeCode,
							' CreatedInL3: ',@CHK_CreatedInL3,
							' RawMaterialLength: ',@CHK_RawMaterialLength,
							' RawMaterialNumber: ',@CHK_RawMaterialNumber,
							' RawMaterialShapeType: ',@CHK_RawMaterialShapeType,
							' RawMaterialThickness: ',@CHK_RawMaterialThickness,
							' RawMaterialWeight: ',@CHK_RawMaterialWeight, 
							' RawMaterialWidth: ',@CHK_RawMaterialWidth, 
							' TargetOrderWeight: ', @CHK_TargetOrderWeight, 
							' TargetOrderWeightMax: ',@CHK_TargetOrderWeightMax, 
							' TargetOrderWeightMin: ',@CHK_TargetOrderWeightMin, 
							' ToBeCompletedBefore: ', @CHK_ToBeCompletedBefore );
UPDATE [xfr].[L3L2WorkOrderDefinition] set ValidationCheck = @MessageInfo, CommStatus = @CommStatus, WorkOrderNameStatus='New' WHERE CounterId=@CounterId
UPDATE [xfr].[L3L2WorkOrderDefinition] set WorkOrderNameStatus='Updated', UpdatedTs=getdate() where WorkOrderName = @WorkOrderName and CounterId!=@CounterId

END
GO
CREATE NONCLUSTERED INDEX [NCI_CommStatus_UpdatedTs]
    ON [xfr].[L3L2WorkOrderDefinition]([CommStatus] ASC, [UpdatedTs] ASC)
    INCLUDE([CreatedTs], [CommMessage], [WorkOrderName], [CustomerName], [ProductName], [HeatName], [CreatedInL3], [ToBeCompletedBefore], [RawMaterialNumber], [TargetOrderWeight], [TargetOrderWeightMin], [TargetOrderWeightMax], [RawMaterialThickness], [RawMaterialWidth], [RawMaterialLength], [RawMaterialWeight], [RawMaterialType], [RawMaterialShapeType], [SteelgradeCode], [ExternalSteelgradeCode], [NextAggregate], [OperationCode], [ReheatingGroupName], [QualityPolicy], [ExtraLabelInformation], [ValidationCheck], [WorkOrderNameStatus]);


GO
CREATE NONCLUSTERED INDEX [NCI_WorkOrderName_CounterId]
    ON [xfr].[L3L2WorkOrderDefinition]([WorkOrderName] ASC, [CounterId] ASC);

