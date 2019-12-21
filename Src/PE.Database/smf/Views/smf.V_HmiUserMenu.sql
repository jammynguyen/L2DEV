CREATE VIEW [smf].[V_HMIUserMenu]
AS
     SELECT smf.HmiClientMenu.HmiClientMenuId, 
            smf.HmiClientMenu.Name, 
            smf.Module.ModuleName, 
            smf.Users.UserName, 
            smf.HmiClientMenu.DisplayName, 
            smf.HmiClientMenu.Controller, 
            smf.HmiClientMenu.Method, 
            smf.HmiClientMenu.ParentId, 
            smf.HmiClientMenu.DisplayOrder, 
            smf.HmiClientMenu.MethodParameter, 
            smf.HmiClientMenu.IconName, 
            smf.HmiClientMenu.Active, 
            MAX(smf.AccessUnits.AccessUnitId) AccessUnitId
     FROM smf.AccessUnits
          INNER JOIN smf.Module ON smf.AccessUnits.FKModuleId = smf.Module.Id
          INNER JOIN smf.RoleRights ON smf.AccessUnits.AccessUnitId = smf.RoleRights.AccessUnitId
          INNER JOIN smf.Roles ON smf.RoleRights.RoleId = smf.Roles.Id
          INNER JOIN smf.UserRoles ON smf.Roles.Id = smf.UserRoles.RoleId
          INNER JOIN smf.Users ON smf.UserRoles.UserId = smf.Users.Id
          INNER JOIN smf.HmiClientMenu ON smf.Module.Id = smf.HmiClientMenu.FKModuleId
     WHERE(smf.AccessUnits.AccessUnitType = 1)
     GROUP BY smf.HmiClientMenu.HmiClientMenuId, 
              smf.HmiClientMenu.Name, 
              smf.Module.ModuleName, 
              smf.Users.UserName, 
              smf.HmiClientMenu.DisplayName, 
              smf.HmiClientMenu.Controller, 
              smf.HmiClientMenu.Method, 
              smf.HmiClientMenu.ParentId, 
              smf.HmiClientMenu.DisplayOrder, 
              smf.HmiClientMenu.MethodParameter, 
              smf.HmiClientMenu.IconName, 
              smf.HmiClientMenu.Active;
GO



GO



GO


