CREATE FUNCTION [dbo].[FNGetEnumKeyword](@EnumName  VARCHAR(50), 
                                        @EnumValue BIGINT)
RETURNS VARCHAR(50)
AS
     BEGIN
         DECLARE @EnumKeyword VARCHAR(50);
         SELECT @EnumKeyword = CASE
                                   WHEN LEFT(EV.Keyword, 5) = 'ENUM_'
                                   THEN SUBSTRING(EV.Keyword, 6, 50)
                                   ELSE EV.Keyword
                               END
         FROM smf.EnumNames EN
              INNER JOIN smf.EnumValues EV ON EN.EnumNameId = EV.FkEnumNameId
         WHERE EN.EnumName = @EnumName
               AND EV.[Value] = @EnumValue;
         RETURN @EnumKeyword;
     END;

/*
select dbo.FNGetEnumKeyword ('FeatureType',3)
*/