CREATE VIEW [dbo].[V_Assets]
AS
     WITH TREE
          AS (SELECT CAST(A1.AssetId AS BIGINT) AssetId, 
                     [Level] = 1, 
                     [Path] = CAST('Root' AS VARCHAR(100))
              FROM dbo.MVHAssets A1
              WHERE A1.ParentAssetId IS NULL
              UNION ALL
              SELECT A2.AssetId, 
                     [Level] = TREE.[Level] + 1, 
                     [Path] = CAST(TREE.Path + '/' + RIGHT('0000000000' + CAST(ROW_NUMBER() OVER(
                                                           ORDER BY A2.OrderSeq) AS VARCHAR(10)), 10) AS VARCHAR(100))
              FROM dbo.MVHAssets A2
                   INNER JOIN TREE ON TREE.AssetId = A2.ParentAssetId)
          SELECT ISNULL(ROW_NUMBER() OVER(
                 ORDER BY [Path]), 0) Seq, 
                 A.AssetId, 
                 A.ParentAssetId, 
                 A.AssetName, 
                 A.AssetDescription, 
                 A.AssetCode, 
                 A.IsCheckpoint, 
                 A.OrderSeq AssetOrderSeq, 
                 REPLICATE('     ', TREE.[Level] - 1) + A.AssetName AS Levels, 
                 [Path], 
                 AEXT.TimeIn
          FROM TREE
               INNER JOIN MVHAssets A ON TREE.AssetId = A.AssetId
               LEFT JOIN MVHAssetsEXT AEXT ON A.AssetId = AEXT.FKAssetId;