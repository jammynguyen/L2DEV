create view V_Enums as 
SELECT EN.EnumName AS DimEnumName, 
       EV.Value AS EnumValue,
       CASE
           WHEN LEFT(EV.Keyword, 5) = 'ENUM_'
           THEN SUBSTRING(EV.Keyword, 6, 50)
           ELSE EV.Keyword
       END AS EnumKeyword
FROM smf.EnumNames AS EN
     INNER JOIN smf.EnumValues AS EV ON EN.EnumNameId = EV.FkEnumNameId;