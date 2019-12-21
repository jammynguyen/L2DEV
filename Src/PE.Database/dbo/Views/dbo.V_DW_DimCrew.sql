CREATE   VIEW [dbo].[V_DW_DimCrew]
AS SELECT CAST('PE_Lite' AS NVARCHAR(50)) AS DataSource, 
          CONVERT(DATETIME2(3), SYSDATETIME()) AS ValidFrom, 
          CONVERT(DATETIME2(3), '99991231 23:59:59.999') AS ValidTo, 
          CrewId AS DimCrewKey, 
          CrewName, 
          [Description] AS CrewDescription, 
          LeaderName AS CrewLeaderName, 
          DfltCrewSize AS DefaultCrewSize, 
          OrderSeq AS CrewOrderSeq
   FROM dbo.Crews;