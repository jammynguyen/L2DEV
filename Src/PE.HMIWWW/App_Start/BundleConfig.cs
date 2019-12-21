using System.Web;
using System.Web.Optimization;

namespace PE.HMIWWW
{
  public class BundleConfig
  {
    // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/jquery-{version}.js",
                                    "~/Scripts/jquery.signalR-2.2.1.min.js",
                                    //"~/Scripts/jquery.unobtrusive-ajax.min.js"
                                    "~/Scripts/jquery.validate*",
                                    "~/Scripts/jquery.unobtrusive-ajax.min.js",
                                    "~/Scripts/jquery-ui.min.js",
                                    "~/Scripts/jQueryFixes.js"
                                    ));

      //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
      //            "~/Scripts/jquery.validate*"));

      // Use the development version of Modernizr to develop with and learn from. Then, when you're
      // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
      //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
      //            "~/Scripts/modernizr-*"));

      bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/respond.js"));



      bundles.Add(new ScriptBundle("~/bundles/telerikjs").Include(
                         "~/Scripts/kendo/2019.3.1023/kendo.all.min.js",
                         "~/Scripts/kendo/2019.3.1023/kendo.aspnetmvc.min.js",
                         "~/Scripts/kendo/2019.3.1023/cultures/kendo.culture.pl-PL.min.js"
                         ));


      bundles.Add(new ScriptBundle("~/bundles/service").Include(
                          "~/Scripts/service.js",
                          "~/Scripts/popup.js",
                          "~/Scripts/Notifications.js",
                          "~/Scripts/refreshHandler.js",
                          "~/Scripts/sweetalert.min.js",
                          "~/Scripts/peekabar.js",
                          "~/Scripts/jquery.popup.min.js",
                          "~/Scripts/System/navigation.js",
                          "~/Scripts/contents.js"));

      // Only for gauge - put it to screen
      //bundles.Add(new ScriptBundle("~/bundles/system-utils").Include("~/Scripts/Sytem/utils.js"));

      //bundles.Add(new ScriptBundle("~/bundles/chartjs").Include(
      //             "~/Scripts/ChartJS/Chart.bundle.min.js"
      //             ));

      //bundles.Add(new ScriptBundle("~/bundles/cryptojs").Include(
      //                "~/Scripts/crypto-js.min.js"
      //              ));

      //bundles.Add(new ScriptBundle("~/bundles/gauge").Include(
      //          "~/Scripts/gauge.min.js"//,
      //                                  //"~/Scripts/System/utils.js"
      //        ));

      bundles.Add(new StyleBundle("~/Content/css").Include(
          "~/Content/bootstrap.min.css",
          "~/Content/site.css",
          "~/Content/sweetalert.css",
          "~/Content/peekabar.css",
          "~/Content/Shared/navigation.css",
          "~/Content/jquery-ui.css",
          "~/Content/popup.css"));

      bundles.Add(new StyleBundle("~/Content/telerikcss").Include(
                "~/Content/kendo/2019.3.1023/kendo.common.min.css",
                "~/Content/kendo/2019.3.1023/kendo.bootstrap.min.css",
                "~/Content/kendo/2019.3.1023/kendo.uniform.min.css",
                "~/Content/kendo/22019.3.1023/kendo.dataviz.default.min.css",
                "~/Content/kendo/TelerikSkin.css",
                "~/Content/kendo/2019.3.1023/kendo.dataviz.min.css"));


      //bundles.Add(new StyleBundle("~/Content/service").Include(
      //                "~/Content/sweetalert.css",
      //                "~/Content/peekabar.css",
      //                "~/Content/popup.css"));



      // Module CSS and JS
      // ---------------------   MAIN PAGE   --------------------- // 

      bundles.Add(new StyleBundle("~/Content/Module/MainPage").Include(
      "~/Content/Module/PE.Lite/MainPage.css"
      ));


      // ---------------------   WORK ORDER   --------------------- // 
      bundles.Add(new StyleBundle("~/Content/Module/WorkOrder").Include(
        "~/Content/Module/PE.Lite/WorkOrder.css",
        "~/Content/expandCategoriesButton.css"
        ));

      bundles.Add(new ScriptBundle("~/Scripts/Module/WorkOrder").Include(
        "~/Scripts/Module/PE.Lite/WorkOrder.js",
        "~/Scripts/expandCategoriesButton.js"
        ));


      // ---------------------   HEAT  --------------------- // 
      bundles.Add(new StyleBundle("~/Content/Module/Heat").Include(
        "~/Content/Module/PE.Lite/Heat.css",
        "~/Content/expandCategoriesButton.css"
        ));
      bundles.Add(new ScriptBundle("~/Scripts/Module/Heat").Include(
        "~/Scripts/Module/PE.Lite/Heat.js",
        "~/Scripts/expandCategoriesButton.js"
        ));


      // ---------------------   TRACKING  --------------------- // 
      bundles.Add(new StyleBundle("~/Content/Module/Tracking").Include(
        "~/Content/Module/PE.Lite/Tracking.css"
        ));
      bundles.Add(new ScriptBundle("~/Scripts/Module/Tracking").Include(
        "~/Scripts/Module/PE.Lite/Tracking.js"
        ));


      // ---------------------   Material  --------------------- // 
      bundles.Add(new StyleBundle("~/Content/Module/Material").Include(
        "~/Content/Module/PE.Lite/Material.css",
        "~/Content/expandCategoriesButton.css"
        ));
      bundles.Add(new ScriptBundle("~/Scripts/Module/Material").Include(
        "~/Scripts/Module/PE.Lite/Material.js",
        "~/Scripts/expandCategoriesButton.js"
        ));

            // ---------------------   RawMaterial  --------------------- // 
            bundles.Add(new StyleBundle("~/Content/Module/RawMaterial").Include(
              "~/Content/Module/PE.Lite/RawMaterial.css",
              "~/Content/expandCategoriesButton.css"

              ));
            bundles.Add(new ScriptBundle("~/Scripts/Module/RawMaterial").Include(
              "~/Scripts/Module/PE.Lite/RawMaterial.js",
              "~/Scripts/expandCategoriesButton.js"
              ));


            // ---------------------   SCHEDULE   --------------------- // 
            bundles.Add(new StyleBundle("~/Content/Module/Schedule").Include(
        "~/Content/Module/PE.Lite/Schedule.css"
        ));
      bundles.Add(new ScriptBundle("~/Scripts/Module/Schedule").Include(
        "~/Scripts/Module/PE.Lite/Schedule.js"
        ));

      // ---------------------   WeighingMonitor   --------------------- // 

      bundles.Add(new ScriptBundle("~/Scripts/Module/WeighingMonitor").Include(
        "~/Scripts/Module/PE.Lite/WeighingMonitor.js"
        ));

      // ---------------------   RollsManagement   --------------------- // 

      bundles.Add(new ScriptBundle("~/Scripts/Module/RollsManagement").Include(
        "~/Scripts/Module/PE.Lite/RollsManagement.js"
        ));

      // ---------------------   RollSetManagement   --------------------- // 

      bundles.Add(new ScriptBundle("~/Scripts/Module/RollSetManagement").Include(
        "~/Scripts/Module/PE.Lite/RollSetManagement.js"
        ));

      // ---------------------   RollType   --------------------- // 

      bundles.Add(new ScriptBundle("~/Scripts/Module/RollType").Include(
        "~/Scripts/Module/PE.Lite/RollType.js"
        ));

      // ---------------------   GrindingTurning   --------------------- // 

      bundles.Add(new ScriptBundle("~/Scripts/Module/GrindingTurning").Include(
        "~/Scripts/Module/PE.Lite/GrindingTurning.js"
        ));

      // ---------------------   ActualStandConfiguration   --------------------- // 

      bundles.Add(new ScriptBundle("~/Scripts/Module/ActualStandConfiguration").Include(
        "~/Scripts/Module/PE.Lite/ActualStandConfiguration.js"
        ));

      // ---------------------   RollSetToCassette   --------------------- // 

      bundles.Add(new ScriptBundle("~/Scripts/Module/RollSetToCassette").Include(
        "~/Scripts/Module/PE.Lite/RollSetToCassette.js"
        ));

      // ---------------------   CassetteType   --------------------- // 

      bundles.Add(new ScriptBundle("~/Scripts/Module/CassetteType").Include(
        "~/Scripts/Module/PE.Lite/CassetteType.js"
        ));

      // ---------------------   Cassette   --------------------- // 

      bundles.Add(new ScriptBundle("~/Scripts/Module/Cassette").Include(
        "~/Scripts/Module/PE.Lite/Cassette.js"
        ));

      // ---------------------   GrooveTemplate   --------------------- // 

      bundles.Add(new ScriptBundle("~/Scripts/Module/GrooveTemplate").Include(
        "~/Scripts/Module/PE.Lite/GrooveTemplate.js"
        ));

      // ---------------------- Steelgrade ---------------------- //
      bundles.Add(new ScriptBundle("~/Scripts/Module/Steelgrade").Include(
                "~/Scripts/Module/PE.Lite/Steelgrade.js"
              ));

      // ---------------------- ProductCatalogue ---------------------- //
      bundles.Add(new ScriptBundle("~/Scripts/Module/ProductCatalogue").Include(
                "~/Scripts/Module/PE.Lite/ProductCatalogue.js"
              ));

      // ---------------------- DefectCatalogue ---------------------- //
      bundles.Add(new ScriptBundle("~/bundles/module/defectCatalogue").Include(
                "~/Scripts/Module/PE.Lite/DefectCatalogue.js"
              ));

      // ---------------------- DelayCatalogue ---------------------- //
      bundles.Add(new ScriptBundle("~/bundles/module/delayCatalogue").Include(
                "~/Scripts/Module/PE.Lite/DelayCatalogue.js"
              ));

      // ---------------------- MaterialCatalogue ---------------------- //
      bundles.Add(new ScriptBundle("~/bundles/module/materialCatalogue").Include(
                "~/Scripts/Module/PE.Lite/MaterialCatalogue.js"
              ));
      bundles.Add(new StyleBundle("~/Content/Module/MaterialCatalogue").Include(
        "~/Content/Module/PE.Lite/MaterialCatalogue.css"
        ));

      // ---------------------- Delay ---------------------- //
      bundles.Add(new ScriptBundle("~/bundles/module/delay").Include(
                "~/Scripts/Module/PE.Lite/Delay.js"
              ));
      // ---------------------- L3CommStatus ---------------------- //
      bundles.Add(new StyleBundle("~/Content/System/L3TransferTableWorkOrder").Include(
      "~/Content/System/L3TransferTableWorkOrder.css"
      ));
            bundles.Add(new ScriptBundle("~/Scripts/System/L3TransferTableWorkOrder").Include(
                    "~/Scripts/System/L3TransferTableWorkOrder.js"
                ));

      // ---------------------   Setup  --------------------- // 
      bundles.Add(new StyleBundle("~/Content/Module/Setup").Include(
        "~/Content/Module/PE.Lite/Setup.css"
        ));
      bundles.Add(new ScriptBundle("~/Scripts/Module/Setup").Include(
        "~/Scripts/Module/PE.Lite/Setup.js"
        ));

      // ---------------------   Products  --------------------- // 
      bundles.Add(new StyleBundle("~/Content/Module/Products").Include(
        "~/Content/Module/PE.Lite/Products.css",
        "~/Content/expandCategoriesButton.css"
        ));
      bundles.Add(new ScriptBundle("~/Scripts/Module/Products").Include(
        "~/Scripts/Module/PE.Lite/Products.js",
        "~/Scripts/expandCategoriesButton.js"
        ));

      // ----------------   ConsumptionMonitoring  ---------------- // 
      bundles.Add(new StyleBundle("~/Content/Module/ConsumptionMonitoring").Include(
        "~/Content/Module/PE.Lite/ConsumptionMonitoring.css"
        ));
      bundles.Add(new ScriptBundle("~/Scripts/Module/ConsumptionMonitoring").Include(
        "~/Scripts/Module/PE.Lite/ConsumptionMonitoring.js"
        ));

      // ---------------------   EventCalendar  --------------------- // 
      bundles.Add(new StyleBundle("~/Content/Module/EventCalendar").Include(
        "~/Content/Module/PE.Lite/EventCalendar.css"
        ));
      bundles.Add(new ScriptBundle("~/Scripts/Module/EventCalendar").Include(
        "~/Scripts/Module/PE.Lite/EventCalendar.js"
        ));
			// ---------------------   Furnace  --------------------- // 
			bundles.Add(new StyleBundle("~/Content/Module/Furnace").Include(
				"~/Content/Module/PE.Lite/Furnace.css"
				));
			bundles.Add(new ScriptBundle("~/Scripts/Module/Furnace").Include(
				"~/Scripts/Module/PE.Lite/Furnace.js"
				));

      // ---------------------   Maintenance  --------------------- // 
      bundles.Add(new ScriptBundle("~/Scripts/Module/EquipmentGroups").Include(
        "~/Scripts/Module/PE.Lite/EquipmentGroups.js"
      ));
      bundles.Add(new ScriptBundle("~/Scripts/Module/Equipment").Include(
				"~/Scripts/Module/PE.Lite/Equipment.js"
			));
      // ---------------------   Quality  --------------------- // 
      bundles.Add(new StyleBundle("~/Content/Module/Quality").Include(
       "~/Content/Module/PE.Lite/Quality.css"
      ));

      bundles.Add(new ScriptBundle("~/Scripts/Module/Quality").Include(
        "~/Scripts/Module/PE.Lite/Quality.js"
      ));
#if DEBUG
      BundleTable.EnableOptimizations = false;  // disable boundle optimizations
#else
      // this option will override any of the web.config debugging opions set - it will disable it
      BundleTable.EnableOptimizations = true;   // force bundle optimizations for release configuration
#endif

    }
  }
}
