CREATE   VIEW [dbo].[V_DW_DimAsset]
AS SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
          CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
          CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
          A1.AssetId DimAssetKey, 
          ISNULL(CAST(ROW_NUMBER() OVER(
          ORDER BY A1.OrderSeq) AS SMALLINT), 0) AssetSeq, 
          A1.AssetCode, 
          A1.AssetName, 
          A1.AssetDescription, 
          A1.ParentAssetId ParentAssetKey, 
          A2.AssetName ParentAssetName, 
          A1.IsCheckpoint, 
          A1.IsReversible
   FROM dbo.MVHAssets A1
        LEFT JOIN dbo.MVHAssets A2 ON A1.ParentAssetId = A2.AssetId;