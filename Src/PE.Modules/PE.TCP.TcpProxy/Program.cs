using PE.Interfaces.Lite;
using PE.TCP.TcpProxy.Communication;
using PE.TCP.TcpProxy.Module;
using SMF.Core.Log;
using SMF.Module.Core;
using System;
using PE.Interfaces.Managers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PE.TCP.TcpProxy
{
  class Program
  {
    #region members

    private static readonly string _moduleName = PE.Interfaces.Module.Modules.TcpProxy.Name;

    private static ITcpCommunicationListeners _tcpCommunicationListeners;
    private static ITcpCommunicationSenders _tcpCommunicationSenders;

    #endregion

    #region setup
    private static void Main(string[] args)
    {
      ModuleController.StartModule(ModuleInit, ModuleImplementationInit, _moduleName);
    }

    private static void ModuleInit()
    {
      try
      {
        ModuleController.ExternalAdapter = new ExternalAdapter(ModuleController.ModuleName);
      }
      catch (Exception ex)
      {
        LogHelper.LogException(ex);
      }

      // events
      ModuleController.ModuleStarted += ModuleController_ModuleStarted;
      ModuleController.ModuleInitialized += ModuleController_ModuleInitialized;
      ModuleController.ModuleClosed += ModuleController_ModuleClosed;
    }

    private static void ModuleImplementationInit()
    {
      try
      {
        _tcpCommunicationListeners = new TcpCommunicationListenersManagerExt();
        _tcpCommunicationSenders = new TcpCommunicationSenderManagerExt();
      }
      catch (Exception ex)
      {
        LogHelper.LogException(ex);
      }
    }

    #endregion

    #region module events

    private static void ModuleController_ModuleClosed(object sender, ModuleCloseEventArgs e)
    {
      try
      {
        ModuleController.Logger.Info("Module Closed");
      }
      catch (Exception ex)
      {
        LogHelper.LogException(ex);
      }
    }
    private static void ModuleController_ModuleInitialized(object sender, ModuleInitEventArgs e)
    {
      try
      {
        ModuleController.Logger.Info("Module Initialized");
      }
      catch (Exception ex)
      {
        LogHelper.LogException(ex);
      }

    }
    private static void ModuleController_ModuleStarted(object sender, ModuleStartEventArgs e)
    {
      try
      {
        ModuleController.Logger.Info("Module Started");
      }
      catch (Exception ex)
      {
        LogHelper.LogException(ex);
      }
    }

    #endregion
  }
}
