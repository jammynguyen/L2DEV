[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PE.HMIWWW.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PE.HMIWWW.App_Start.NinjectWebCommon), "Stop")]

namespace PE.HMIWWW.App_Start
{
  using Microsoft.Web.Infrastructure.DynamicModuleHelper;
  using Ninject;
  using Ninject.Web.Common;
  using Ninject.Web.Common.WebHost;
  using PE.HMIWWW.Core.Service;
  using PE.HMIWWW.Services.Module;
  using PE.HMIWWW.Services.Module.PE.Lite;
  using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
  using PE.HMIWWW.Services.System;
  using PE.Services.System;
  using System;
  using System.Web;

  public static class NinjectWebCommon
  {
    private static readonly Bootstrapper bootstrapper = new Bootstrapper();

    /// <summary>
    /// Starts the application
    /// </summary>
    public static void Start()
    {
      DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
      DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
      bootstrapper.Initialize(CreateKernel);
    }

    /// <summary>
    /// Stops the application.
    /// </summary>
    public static void Stop()
    {
      bootstrapper.ShutDown();
    }

    /// <summary>
    /// Creates the kernel that will manage your application.
    /// </summary>
    /// <returns>The created kernel.</returns>
    private static IKernel CreateKernel()
    {
      StandardKernel kernel = new StandardKernel();
      try
      {
        kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
        kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
        RegisterServices(kernel);
        return kernel;
      }
      catch
      {
        kernel.Dispose();
        throw;
      }
    }

    /// <summary>
    /// Load your modules or register your services here!
    /// </summary>
    /// <param name="kernel">The kernel.</param>
    private static void RegisterServices(IKernel kernel)
    {

      kernel.Bind<IBaseService>().To<BaseService>();
      kernel.Bind<IMainMenuService>().To<MainMenuService>();
      kernel.Bind<IAlarmsService>().To<AlarmsService>();
      kernel.Bind<IWatchdogService>().To<WatchdogService>();
      kernel.Bind<IAccessUnitsService>().To<AccessUnitsService>();
      kernel.Bind<ILimitService>().To<LimitService>();
      kernel.Bind<IParameterService>().To<ParameterService>();
      kernel.Bind<IViewsStaticsService>().To<ViewsStaticsService>();
      kernel.Bind<ICrewService>().To<CrewService>();
      kernel.Bind<IRoleService>().To<RoleService>();
      kernel.Bind<IShiftCalendarService>().To<ShiftCalendarService>();
      kernel.Bind<IAccountService>().To<AccountService>();
      kernel.Bind<ITestService>().To<TestService>();
      kernel.Bind<IWidgetConfigurationService>().To<WidgetConfigurationService>();
      kernel.Bind<IL3CommStatusService>().To<L3CommStatusService>();

      //Modules
      kernel.Bind<IBilletService>().To<BilletService>();
      kernel.Bind<IDefectService>().To<DefectService>();
      kernel.Bind<IDelaysService>().To<DelaysService>();
      kernel.Bind<IProductService>().To<ProductService>();
      kernel.Bind<IConsumptionMonitoringService>().To<ConsumptionMonitoringService>();
      kernel.Bind<ISteelgradeService>().To<SteelgradeService>();
      kernel.Bind<IWorkOrderService>().To<WorkOrderService>();
      kernel.Bind<ITrackingService>().To<TrackingService>();
      kernel.Bind<IScheduleService>().To<ScheduleService>();
      kernel.Bind<IHeatService>().To<HeatService>();
      kernel.Bind<IMaterialService>().To<MaterialService>();
      kernel.Bind<IRawMaterialService>().To<RawMaterialService>();
      kernel.Bind<IFeatureMap>().To<FeatureMapService>();
      kernel.Bind<IAssetService>().To<AssetService>();
      kernel.Bind<IMaterialFurnaceService>().To<MaterialFurnaceService>();
      kernel.Bind<ISetupService>().To<SetupService>();
      kernel.Bind<IEventCalendarService>().To<EventCalendarService>();
      kernel.Bind<IProductsService>().To<ProductsService>();
			kernel.Bind<IFurnaceService>().To<FurnaceService>();
      kernel.Bind<IWeighingMonitorService>().To<WeighingMonitorService>();
      kernel.Bind<IRollsManagementService>().To<RollsManagementService>();
      kernel.Bind<IRollSetManagementService>().To<RollSetManagementService>();
      kernel.Bind<IRollTypeService>().To<RollTypeService>();
      kernel.Bind<IGrooveTemplateService>().To<GrooveTemplateService>();
      kernel.Bind<ICassetteTypeService>().To<CassetteTypeService>();
      kernel.Bind<ICassetteService>().To<CassetteService>();
      kernel.Bind<IGrindingTurningService>().To<GrindingTurningService>();
      kernel.Bind<IActualStandsConfigurationService>().To<ActualStandsConfigurationService>();
      kernel.Bind<IRollsetToCassetteService>().To<RollsetToCassetteService>();
      kernel.Bind<IEquipmentGroupsService>().To<EquipmentGroupsService>();
      kernel.Bind<IEquipmentService>().To<EquipmentService>();
      kernel.Bind<IProductQualityMgmService>().To<ProductQualityMgmService>();

    }
	}
}
