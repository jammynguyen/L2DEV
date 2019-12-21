namespace PE.DbEntity.Enums
//[SF] Connection string: data source=148.56.68.169;initial catalog=PE_Lite;integrated security=False;user id=sa;password=Primetals.;MultipleActiveResultSets=True;App=EntityFramework

{
    public enum TelegramIds : short
    {
        TelID_FurnaceDischarging = 1113,
        TelID_ExtTransferTableData = 5001,
        TelID_EmptyTelegramForTCP = 5000,
        TelID_RetExtTransferTableData = 6001,
        TelID_BilletLoadedOnGrill = 1001,
        TelID_BilletUnloadedFromGrill = 1002,
        TelID_FurnaceCharged = 1003,
        TelID_FurnaceUncharged = 1004,
        TelID_FurnaceDischarged = 1005,
        TelID_FurnaceDischargeCmd = 1006,
        TelID_FurnaceUndischarged = 1007,
        TelID_FurnaceChargeCmd = 1008,
        TelID_ChargingWeight = 1009,
        TelID_FurnaceMap = 1010,
        TelID_FurnaceMapCyclic = 1011,
        TelID_FCECharge = 1012,
        TelID_FCEWeight = 1013,
        TelID_FCELength = 1014,
        TelID_FurnaceReport = 3001,
        TelID_AdapterStatus = 2000,
        TelId_L1TCPStatus = 2001,
        TelID_L1OPCStatus = 2002,
        TelID_ExtDBStatus = 2003,
        TelId_TrackingStatus = 2004,
        TelID_ExtOutputTransferOrderTableData = 6002,
        TelID_ExtOutputTransferProductTableData = 6003,
        TelID_ExtUniversalTelegram = 7001,
        TelID_ExtWorkOrderTransferTableData = 5003,
    }

    public enum YesNo : short
    {
        No = 0,
        Yes = 1,
    }

    public enum OnOff : short
    {
        Off = 0,
        On = 1,
    }

    public enum Correct : short
    {
        Incorrect = 0,
        Correct = 1,
    }

    public enum TransferTableDataReadingStatus : short
    {
        New = 1,
        Processing = 2,
        AttempsExceeded = 3,
        DataBaseError = 4,
        ProcessingInternalError = 5,
        Processed = 6,
        Canceled = 7,
        ForceProcessing = 8,
    }

    public enum ProcessingMessageStatus : short
    {
        NotProcessed = 0,
        Processed = 1,
    }

    public enum ShiftTime : short
    {
        Day = 1,
        Afternoon = 2,
        Night = 3,
    }

    public enum ChargeProcessingFlag : short
    {
        UseBilletName = 0,
        UseOrderAndSeqNumber = 1,
    }

    public enum DischargeProcessingFlag : short
    {
        Default = 0,
        VerifyFurnaceBillet = 1,
    }

    public enum DelayStatus : short
    {
        Undefined = 0,
        Closed = 1,
        Open = 2,
    }

    public enum DelayCategory : short
    {
        Undefined = 0,
        Scheduled = 1,
        NotScheduled = 2,
    }

    public enum MaintenanceRepeat : short
    {
        Never = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Yearly = 4,
    }

    public enum MaintenanceStatus : short
    {
        Undefined = 0,
        Scheduled = 1,
        Completed = 2,
        Cancelled = 3,
        NotCompleted = 4,
    }

    public enum EquipmentStatus : short
    {
        Undefined = 0,
        InStock = 1,
        InOperation = 2,
        Dismounted = 3,
        Scrapped = 4,
    }

    public enum EquipmentAction : short
    {
        Undefined = 0,
        Check = 1,
        Replace = 2,
        Remove = 3,
        Repair = 4,
        Scrap = 5,
    }

    public enum EquipmentResult : short
    {
        Undefined = 0,
        VerifiedOk = 1,
        Replaced = 2,
        Removed = 3,
        Repaired = 4,
        Postponed = 5,
        Unsolved = 6,
    }

    public enum Planned : short
    {
        NoPlanned = 0,
        Planned = 1,
    }

    public enum AccumulationEvent : short
    {
        NoEvent = 0,
        BilletDischarged = 1,
        BilletRolled = 2,
        ProductCreated = 3,
    }

    public enum AccumulationType : short
    {
        Undefined = 0,
        PerMaterial = 1,
        PerTime = 2,
    }

    public enum OrderStatus : short
    {
        ENUM_Undefined = 0,
        ENUM_NewOrIncomplete = 1,
        ENUM_Completed = 2,
        ENUM_Scheduled = 3,
        ENUM_InRealization = 4,
        ENUM_Finished = 5,
        ENUM_Cancelled = 6,
    }

    public enum HeatSource : short
    {
        ENUM_Undefined = 0,
        ENUM_L3Input = 1,
        ENUM_L2Hmi = 2,
        ENUM_L1Input = 3,
    }

    public enum LenghtType : short
    {
        Trading = 0,
        Specified = 1,
        Combined = 2,
    }

    public enum NewProductProcessingFlag : short
    {
        Default = 0,
        CusomerSpecific1 = 1,
        CusomerSpecific2 = 2,
        CusomerSpecific3 = 3,
    }

    public enum HeatFlag : short
    {
        Suspected = 1,
        Alarm = 2,
        Critical = 3,
    }

    public enum HeatStatus : short
    {
        ENUM_Invalid = 0,
        ENUM_New = 1,
        ENUM_InProduction = 2,
        ENUM_Produced = 3,
        ENUM_Scrapped = 4,
    }

    public enum RawMaterialStatus : short
    {
        ENUM_Invalid = 0,
        ENUM_Unassigned = 1,
        ENUM_InStorage = 2,
        ENUM_InTransport = 3,
        ENUM_Charged = 4,
        ENUM_Discharged = 5,
        ENUM_InMill = 6,
        ENUM_InFinalProduction = 7,
        ENUM_Rolled = 8,
        ENUM_ScrapCanBeRolledAgain = 9,
        ENUM_ScrapFromMill = 10,
    }

    public enum RejectFlag : short
    {
        NotRejected = 0,
        Heating = 1,
        Dimension = 2,
        Unscheduled = 3,
    }

    public enum RawMaterialType : short
    {
        Undefined = 0,
        Billet = 1,
        Ingot = 2,
        Slab = 3,
        Coil = 4,
        Wire = 5,
        Ore = 6,
        Coke = 7,
    }

    public enum RawMaterialStepNo : short
    {
        OverallStep = 0,
        FirstStep = 1,
        FirstProcessingLineStep = 2,
    }

    public enum ReportType : short
    {
        PDF = 0,
        XLS = 1,
    }

    public enum ProductionReportNames : short
    {
        ProductionReport = 0,
        CompactProductionReport = 1,
    }

    public enum ProductStatus : short
    {
        ENUM_Invalid = 0,
        ENUM_Produced = 1,
        ENUM_InTransport = 2,
        ENUM_InProductYard = 3,
        ENUM_OutForDelivery = 4,
        ENUM_Scrapped = 5,
        ENUM_PalletArea = 6,
    }

    public enum ProductQuality : short
    {
        NotEvaluated = 0,
        QualityOK = 1,
        QualityScrap = 2,
    }

    public enum HeatingStatus : short
    {
        Undefined = 0,
        HeatingNotFinished = 1,
        HeatingTimeOK = 2,
        HeatingTimeExceedRisk = 3,
        HeatingTimeExceeded = 4,
    }

    public enum DivisionFlag : short
    {
        Undefined = 0,
        DividedAndOriginal = 1,
        DividedAndNew = 2,
    }

    public enum WeightSource : short
    {
        Undefined = 0,
        Calculated = 1,
        Measured = 2,
        Operator = 3,
    }

    public enum ProductType : short
    {
        Product = 0,
        ScrapMill = 1,
        Scrap = 2,
    }

    public enum WeightStatus : short
    {
        NotWeighed = 0,
        WeightFromCradle1 = 1,
        WeightFromCradle2 = 2,
        WeightFromScale = 9,
    }

    public enum BilletHeatSource : short
    {
    }

    public enum PitFurnaceStatus : short
    {
        Undefined = 0,
        InOperation = 1,
        ShutDown = 2,
        Disabled = 3,
    }

    public enum ChargeType : short
    {
        Undefined = 0,
        SingleMaterial = 1,
        BatchOfMaterials = 2,
    }

    public enum MaterialShapeType : short
    {
        Undefined = 0,
        SquareSmall = 1,
        SquareMedium = 2,
        SquareLarge = 3,
        RoundSquareSmall = 4,
        RoundSquareMedium = 5,
        RoundSquareLarge = 6,
        RoundSmall = 7,
        RoundMedium = 8,
        RoundLarge = 9,
        FlatSmall = 10,
        FlatMedium = 11,
        FlatLarge = 12,
        BatchOfMaterials = 99,
    }

    public enum DailyPlanStatus : short
    {
        Undefined = 0,
        InPreparation = 1,
        Released = 2,
        Completed = 3,
    }

    public enum IsTestOrder : short
    {
        NoTest = 0,
        IsTest = 1,
    }

    public enum OperationType : short
    {
        Add = 0,
        Delete = 1,
    }

    public enum MillStatus : short
    {
        NotWorking = 0,
        Working = 1,
    }

    public enum LaboratoryCode : short
    {
        InputMaterialData = 0,
    }

    public enum TypeOfCut : short
    {
        HeadCut = 1,
        TailCut = 2,
        DivideCutN = 10,
        SampleCut = 3,
        Chopping = 4,
    }

    public enum EvalExecutionStatus : short
    {
        SUCCESS = 1,
        FAILED = 2,
    }

    public enum Aggregation : short
    {
        UNKNOWN = -1,
        FIRST = 1,
        LAST = 2,
        MIN = 3,
        MAX = 4,
        COUNT = 5,
        MAJORITY = 6,
        ZERO = 0,
    }

    public enum ParamType : short
    {
        NUMBER = 1,
        TEXT = 2,
        BOOLEAN = 3,
        TIMESTAMP = 4,
        RULES_OBJECT = 5,
        LENGTH_SERIES = 6,
        UNKNOWN = -1,
    }

    public enum Category : short
    {
        UNKNOWN = -1,
        MEASUREMENT = 1,
        SPECIFICATION = 2,
        RATING = 3,
    }

    public enum Direction : short
    {
        UNKNOWN = -1,
        INPUT = 1,
        OUTPUT = 2,
    }

    public enum TypeOfScrap : short
    {
        CanBeRolled = 1,
        ScrapFromMill = 2,
        CanOrCannotLogicInL2 = 3,
    }

    public enum MeasurementType : short
    {
        BIT = 1,
        SHORT = 2,
        INT = 3,
        FLOAT = 4,
        DOUBLE = 5,
        TIMESTAMP = 6,
        TEXT = 7,
        MEAS_DATA = 8,
        LONG = 9,
    }

    public enum Grading : short
    {
        Optimum = 1,
        Normal = 2,
        Warning = 3,
        Notconform = 4,
        Outofcontro = 5,
        EvaluateFailed = 0,
    }

    public enum KPiTime : short
    {
        Minute = 1,
        Hour = 2,
        Shift = 3,
        Asset = 4,
    }

    public enum RatingValue : short
    {
        NAME_RatingType_Null = 0,
        NAME_RatingType_Good = 1,
        NAME_RatingType_Sufficient = 2,
        NAME_RatingType_Caution = 3,
        NAME_RatingType_Bad = 4,
        NAME_RatingType_Critical = 5,
    }

    public enum PartOfMaterialType : short
    {
        HeadPart = 0,
        MaterialPart = 1,
        TailPart = 2,
    }

    public enum CommStatus : short
    {
        ENUM_COMMSTATUS_New = 0,
        ENUM_COMMSTATUS_BeingProcessed = 1,
        ENUM_COMMSTATUS_OK = 2,
        ENUM_COMMSTATUS_ValidationError = -1,
        ENUM_COMMSTATUS_ProcessingError = -2,
    }

    public enum ScheduleMoveOperator : short
    {
        Up = -1,
        Down = 1,
        Non = 0,
    }

    public enum TriggerType : short
    {
        Charge = 1,
        Discharge = 2,
        UnCharge = 3,
        UnDischarge = 4,
        MarkAsFinished = 5,
        ProdEnd_Bar = 6,
        ProdEnd_WireRod = 7,
        Scrap = 8,
        TailLeaves = 9,
        HeadEnter = 10,
    }

    public enum AssetsArea : short
    {
        ENUM_Furnace = 1,
        ENUM_Rolling = 2,
        ENUM_AfterCut = 3,
        ENUM_Charging = 0,
    }

    public enum FeatureType : short
    {
        Charge = 4,
        Discharge = 5,
        UnCharge = 44,
        UnDischarge = 55,
        InMill = 6,
        InFinalProduction = 7,
        Rolled = 8,
        Undefined = 0,
        L3MaterialAssignment = 3,
        WireRodProductCreation = 11,
        ProductionGapStart = 13,
        ProductionGapEnd = 14,
        Consumption = 9,
    }

    public enum RollScrapReason : short
    {
        Other = 0,
        Used = 1,
        Broken = 2,
        DamagedChock = 3,
    }

    public enum RollStatus : short
    {
        Undefined = 0,
        New = 1,
        InRollSet = 2,
        Unassigned = 3,
        NotAvailable = 4,
        Turning = 5,
        Scrapped = 6,
        SchedulledForRollset = 7,
    }

    public enum RollSetStatus : short
    {
        Undefined = 0,
        Empty = 1,
        Turning = 2,
        Ready = 3,
        ScheduledForCassette = 4,
        ReadyForMounting = 5,
        Mounted = 6,
        Dismounted = 7,
        NotAvailable = 8,
        ScheduledForAssemble = 9,
    }

    public enum RollSetHistoryStatus : short
    {
        Undefined = 0,
        Actual = 1,
        Old = 2,
        Planned = 3,
    }

    public enum GrindingTurningAction : short
    {
        Undefined = 0,
        Plan = 1,
        Confirm = 2,
        Cancel = 3,
    }

    public enum RollSetType : short
    {
        Undefined = 0,
        TwoActiveRollsRM = 1,
        TwoActiveRollsIM = 2,
    }

    public enum RollGrooveStatus : short
    {
        Undefined = 0,
        Actual = 1,
        Active = 2,
        Old = 3,
        PlannedAndNoChange = 4,
        PlannedAndTurning = 5,
        PlannedAndNotAvailable = 6,
        NotAvailable = 7,
    }

    public enum CassetteStatus : short
    {
        Undefined = 0,
        New = 1,
        Empty = 2,
        RollSetInside = 3,
        MountedInStand = 4,
        InRegeneration = 5,
        NotAvailable = 6,
        AssembleIncomplete = 7,
        NoStatus = 8,
        ScheduledForAssemble = 9,
        MountedInCasette = 10,
        ReadyForMountingWithRollSet = 11,
        ForStore = 12,
    }

    public enum CassetteArrangement : short
    {
        Undefined = 0,
        Horizontal = 1,
        Vertical = 2,
        Other = 3,
    }

    public enum CassetteType : short
    {
        RMCassette = 0,
        IMCassette = 1,
        MorganCassette = 2,
    }

    public enum RollSetCassetteAction : short
    {
        Undefined = 0,
        PlanRollSet = 1,
        CancelPlan = 2,
        ConfirmRollSet = 3,
        RemoveRollSet = 4,
    }

    public enum StandStatus : short
    {
        Undefined = 0,
        Active = 1,
        Inactive = 2,
    }

    public enum RollChangeAction : short
    {
        Undefined = 0,
        SwapCassette = 1,
        SwapRollSetOnly = 2,
        DismountWithCassette = 3,
        DismountRollSetOnly = 4,
        MountWithCassette = 5,
        MountRollSetOnly = 6,
        Edit = 7,
    }

    public enum StandActivity : short
    {
        Offline = 0,
        Online = 1,
    }

}
