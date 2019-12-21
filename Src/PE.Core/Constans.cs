using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.Core
{
  /// <summary>
  /// static class for storing project parameters
  /// </summary>
  public static class Constants
  {

    public static readonly int ExtMessageBufferSize = 90000000;
    public static readonly string ExtDateTimeFormat = "yyyyMMddHHmmss";

    //opc tag types in config file
    public const string TypeString = "string";
    public const string TypeInteger = "int";
    public const string TypeShort = "short";
    public const string TypeFloat = "float";
    public const string TypeDouble = "double";
    public const string TypeDateAsString = "datestring";
    public const string TypeBool = "bool";
    public const string TypeUnsignedShort = "ushort";
    public const string TypeUnsignedInteger = "uint";

    public static readonly string KeyL1TcpStatus = "SYS_STAT_TCP";
    public static readonly string KeyL1OpcStatus = "SYS_STAT_OPC";
    public static readonly string KeyExtDBStatus = "SYS_STAT_ExtDB";





    #region Modules
    public const string SmfAuthorization_Module_Alarms = "Alarms";
    public const string SmfAuthorization_Module_System = "System";
    public const string SmfAuthorization_Module_Watchdog = "Watchdog";
    public const string SmfAuthorization_Module_ProdManager = "ProdManager";
    public const string SmfAuthorization_Module_ConsumptionMonitoring = "ConsumptionMonitoring";
    public const string SmfAuthorization_Module_ProdPlaning = "ProdPlaning";
    public const string SmfAuthorization_Module_MVHistory = "MVHistory";
    public const string SmfAuthorization_Module_EventCalendar = "EventCalendar";
    public const string SmfAuthorization_Module_WeighingMonitor = "WeighingMonitor";
    public const string SmfAuthorization_Module_Maintenance = "Maintenance";
    public const string SmfAuthorization_Module_Quality = "Quality";

    #endregion

    #region Controllers
    //framework
    public const string SmfAuthorization_Controller_Alarm = "CONTROLLER_Alarms";
    public const string SmfAuthorization_Controller_Watchdog = "CONTROLLER_Watchdog";
    public const string SmfAuthorization_Controller_Limit = "CONTROLLER_Limit";
    public const string SmfAuthorization_Controller_Service = "CONTROLLER_Service";
    public const string SmfAuthorization_Controller_Parameter = "CONTROLLER_Parameter";
    public const string SmfAuthorization_Controller_Db2DbCommunication = "CONTROLLER_Db2DbCommunication";
    public const string SmfAuthorization_Controller_Crew = "CONTROLLER_Crew";
    public const string SmfAuthorization_Controller_ExtTelArchive = "CONTROLLER_ExtTelArchive";
    public const string SmfAuthorization_Controller_ShiftCalendar = "CONTROLLER_ShiftCalendar";
    public const string SmfAuthorization_Controller_Role = "CONTROLLER_Role";
    public const string SmfAuthorization_Controller_UserAccounts = "CONTROLLER_UserAccounts";
    public const string SmfAuthorization_Controller_UserAccountAdministration = "CONTROLLER_UserAccountAdministration";
    public const string SmfAuthorization_Controller_ViewsStatistics = "CONTROLLER_ViewsStatistics";
    public const string SmfAuthorization_Controller_Test = "CONTROLLER_Test";
    public const string SmfAuthorization_Controller_WidgetConfigurations = "CONTROLLER_WidgetConfigurations";
    public const string SmfAuthorization_Controller_L3CommStatus = "CONTROLLER_L3CommStatus";

    //system
    public const string SmfAuthorization_Controller_FeatureMap = "CONTROLLER_FeatureMap";
    public const string SmfAuthorization_Controller_Asset = "CONTROLLER_Asset";
    //system
    public const string SmfAuthorization_Controller_Steelgrade = "CONTROLLER_Steelgrade";
    public const string SmfAuthorization_Controller_Product = "CONTROLLER_Product";
    public const string SmfAuthorization_Controller_Billet = "CONTROLLER_Billet";
    public const string SmfAuthorization_Controller_Defect = "CONTROLLER_Defect";
    public const string SmfAuthorization_Controller_Delays = "CONTROLLER_Delays";
    public const string SmfAuthorization_Controller_WorkOrder = "CONTROLLER_WorkOrder";
    public const string SmfAuthorization_Controller_Schedule = "CONTROLLER_Schedule";
    public const string SmfAuthorization_Controller_Heat = "CONTROLLER_Heat";
    public const string SmfAuthorization_Controller_Material = "CONTROLLER_Material";
    public const string SmfAuthorization_Controller_RawMaterial = "CONTROLLER_RawMaterial";
    public const string SmfAuthorization_Controller_MaterialFurnace = "CONTROLLER_MaterialFurnace";
    public const string SmfAuthorization_Controller_Setup = "CONTROLLER_Setup";
    public const string SmfAuthorization_Controller_Products = "CONTROLLER_Products";
    public const string SmfAuthorization_Controller_ConsumptionMonitoring = "CONTROLLER_ConsumptionMonitoring";
    public const string SmfAuthorization_Controller_EventCalendar = "CONTROLLER_EventCalendar";
    public const string SmfAuthorization_Controller_Furnace = "CONTROLLER_Furnace";
    public const string SmfAuthorization_Controller_Furnace2 = "CONTROLLER_Furnace2";
    public const string SmfAuthorization_Controller_Tracking = "CONTROLLER_Tracking";
    public const string SmfAuthorization_Controller_WeighingMonitor = "CONTROLLER_WeighingMonitor";
    public const string SmfAuthorization_Controller_RollsManagement = "CONTROLLER_RollsManagement";
    public const string SmfAuthorization_Controller_Visualization = "CONTROLLER_Visualization";
    public const string SmfAuthorization_Controller_EquipmentGroups = "CONTROLLER_EquipmentGroups";
    public const string SmfAuthorization_Controller_Equipment = "CONTROLLER_Equipment";
    public const string SmfAuthorization_Controller_RollTypes = "CONTROLLER_RollType";
    public const string SmfAuthorization_Controller_GrooveTemplates = "CONTROLLER_GrooveTemplate";
    public const string SmfAuthorization_Controller_RollSetManagement = "CONTROLLER_RollSetManagement";
    public const string SmfAuthorization_Controller_CassetteType = "CONTROLLER_CassetteType";
    public const string SmfAuthorization_Controller_Cassette = "CONTROLLER_Cassette";
    public const string SmfAuthorization_Controller_GrindingTurning = "CONTROLLER_GrindingTurning";
    public const string SmfAuthorization_Controller_ActualStandConfiguration = "CONTROLLER_ActualStandConfiguration";
    public const string SmfAuthorization_Controller_RollSetToCassette = "CONTROLLER_RollSetToCassette";
    public const string SmfAuthorization_Controller_ProductQualityMgm = "CONTROLLER_ProductQualityMgm";




    #endregion


  }
}
