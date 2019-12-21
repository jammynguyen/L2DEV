
CREATE VIEW [dbo].[V_SimAssetSeq]
AS
     SELECT ISNULL(ROW_NUMBER() OVER(
            ORDER BY A.OrderSeq), 0) SeqNo, 
            A.AssetId, 
            A.AssetCode, 
            A.AssetName, 
			AEXT.EnumArea,
            AEXT.IsInitial, 
            AEXT.IsLast, 
            AEXT.MaxPassNo, 
            AEXT.TimeIn
     FROM dbo.MVHAssets A
          INNER JOIN dbo.MVHAssetsEXT AEXT ON A.AssetId = AEXT.FKAssetId
     WHERE 1 = 1
           AND AssetId IN
     (
         SELECT FKAssetId
         FROM dbo.MVHFeatures
     );