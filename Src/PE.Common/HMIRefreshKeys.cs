using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.Common
{
  public static class HmiAlarmPrefixes
  {
    public static readonly string System = "System";
    public static readonly string Tracking = "Tracking";
    public static readonly string Module = "Module";
  }
  public static class HMIRefreshKeys
  {
    //system
    public static readonly string SysAlarmRefresh = String.Format("{0}.AlarmRefresh", HmiAlarmPrefixes.System);
    public static readonly string SysDb2DbRefresh = String.Format("{0}.Db2DbRefresh", HmiAlarmPrefixes.System);
    public static readonly string SysExtTelArchiveRefresh = String.Format("{0}.ExtTelArchive", HmiAlarmPrefixes.System);
    public static readonly string Limits = String.Format("{0}.Limits", HmiAlarmPrefixes.System);
    public static readonly string L3TransferTable = String.Format("{0}.L3TransferTable", HmiAlarmPrefixes.System);

    //catalogues
    public static readonly string Steelgrade = String.Format("{0}.Steelgrade", HmiAlarmPrefixes.Module);
    public static readonly string Steelgroup = String.Format("{0}.Steelgroup", HmiAlarmPrefixes.Module);
    public static readonly string RawMaterialCatalogue = String.Format("{0}.RawMaterialCatalogue", HmiAlarmPrefixes.Module);
    public static readonly string ProductCatalogue = String.Format("{0}.ProductCatalogue", HmiAlarmPrefixes.Module);
    public static readonly string ProductCatalogueType = String.Format("{0}.ProductCatalogueType", HmiAlarmPrefixes.Module);
    public static readonly string ReheatingGroup = String.Format("{0}.ReheatingGroup", HmiAlarmPrefixes.Module);
    public static readonly string ReheatingTime = String.Format("{0}.ReheatingTime", HmiAlarmPrefixes.Module);
    public static readonly string DefectCatalogue = String.Format("{0}.DefectCatalogue", HmiAlarmPrefixes.Module);
    public static readonly string ProfileConfiguration = String.Format("{0}.CutOptimalization", HmiAlarmPrefixes.Module);
    public static readonly string Heat = String.Format("{0}.Heat", HmiAlarmPrefixes.Module);
    public static readonly string PrimaryDataInput = String.Format("{0}.PrimaryDataInput", HmiAlarmPrefixes.Module);
    public static readonly string TrackingMaterials = String.Format("{0}.TrackingMaterials", HmiAlarmPrefixes.Module);
    public static readonly string PrimaryDataOutput = String.Format("{0}.PrimaryDataOutput", HmiAlarmPrefixes.Module);
    public static readonly string MaterialCatalogue = String.Format("{0}.MaterialCatalogue", HmiAlarmPrefixes.Module);
    public static readonly string DelayCatalogue = String.Format("{0}.DelayCatalogue", HmiAlarmPrefixes.Module);
    public static readonly string Delay = String.Format("{0}.Delay", HmiAlarmPrefixes.Module);
    public static readonly string Products = String.Format("{0}.Products", HmiAlarmPrefixes.Module);
    public static readonly string RawMaterialDetails = String.Format("{0}.RawMaterialDetails", HmiAlarmPrefixes.Module);
    public static readonly string EquipmentGroups = String.Format("{0}.EquipmentGroups", HmiAlarmPrefixes.Module);
    public static readonly string Equipment = String.Format("{0}.Equipment", HmiAlarmPrefixes.Module);
    public static readonly string ProductQualityMgm = String.Format("{0}.ProductQualityMgm", HmiAlarmPrefixes.Module);



    //Rollshop
    public static readonly string Roll = String.Format("{0}.Roll", HmiAlarmPrefixes.Module);


    // Lite
    public static readonly string Schedule = String.Format("{0}.Schedule", HmiAlarmPrefixes.Module);

  }
}
