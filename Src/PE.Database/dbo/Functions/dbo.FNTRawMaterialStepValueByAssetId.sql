CREATE   FUNCTION [dbo].[FNTRawMaterialStepValueByAssetId](@RawMaterialId BIGINT, 
                                                               @AssetId       BIGINT) RETURNS TABLE AS RETURN
															   --DECLARE @RawMaterialId BIGINT = 38563
															   --DECLARE @AssetId       BIGINT = 29

                                                                                                       SELECT [Weight] = MAX([Weight]), 
                                                                                                              Width = MAX(Width), 
                                                                                                              Thickness = MAX(Thickness), 
                                                                                                              [Length] = MAX([Length])
                                                                                                       FROM dbo.MVHRawMaterialsSteps AS RMS
                                                                                                       WHERE FKRawMaterialId = @RawMaterialId
                                                                                                             AND FKAssetId = @AssetId
																											 GROUP BY FKRawMaterialId

--SELECT * FROM [dbo].[MVHRawMaterials]
--SELECT * FROM [dbo].[V_RawMaterialOverwiew] ORDER BY RAWMATERIALID