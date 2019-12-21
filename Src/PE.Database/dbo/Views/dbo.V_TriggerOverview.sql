CREATE VIEW [dbo].[V_TriggerOverview]
AS
     SELECT ISNULL(ROW_NUMBER() OVER(
            ORDER BY T.TriggerCode, TF.OrderSeq), 0) AS Sorting, 
            T.TriggerCode, 
            T.TriggerName, 
            T.IsActive, 
			ETT.[EnumKeyword], 
            F.FeatureCode, 
            F.FeatureName, 
            A.AssetCode, 
            A.AssetName, 
            TF.PassNo, 
            TF.Relations
     FROM dbo.MVHTriggers AS T
          INNER JOIN dbo.MVHTriggersFeatures AS TF ON T.TriggerId = TF.FKTriggerId
          INNER JOIN dbo.MVHFeatures AS F ON TF.FKFeatureId = F.FeatureId
          INNER JOIN smf.UnitOfMeasure AS UOM ON F.FKUnitOfMeasureId = UOM.UnitId
		  INNER JOIN (
         SELECT EV.Value AS EnumValue, 
                SUBSTRING(EV.Keyword, 1, 50) AS EnumKeyword
         FROM smf.EnumNames AS EN
              INNER JOIN smf.EnumValues AS EV ON EN.EnumNameId = EV.FkEnumNameId
         WHERE EN.EnumName = 'TriggerType'
     ) ETT ON T.EnumTriggerType = ETT.EnumValue
          LEFT OUTER JOIN dbo.MVHAssets AS A ON F.FKAssetId = A.AssetId;