
CREATE VIEW V_Limits AS

SELECT L.[LimitId], 
       L.[CreatedTs], 
       L.[LastUpdateTs], 
       L.[Name], 
       L.[Description], 
       L.[UpperValueFloat], 
       L.[LowerValueFloat], 
       L.[UnitId], 
       L.[UpperValueInt], 
       L.[LowerValueInt], 
       L.[UpperValueDate], 
       L.[LowerValueDate], 
       L.[ValueType], 
       dbo.FNGetEnumKeyword('LimitValueType', L.ValueType) ValueTypeName, 
       L.[LimitGroupId], 
       PG.[Name] LimitGroupName, 
       PG.[Description] LimitGroupDescription
FROM [smf].[Limits] L
     INNER JOIN smf.ParameterGroup PG ON L.LimitGroupId = PG.ParameterGroupId;