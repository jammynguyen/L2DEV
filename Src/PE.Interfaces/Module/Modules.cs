using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Module
{
  public static class Modules
  {
    public static readonly ModuleDescription Wdog = new ModuleDescription("wdog", "watchdog module");
    public static readonly ModuleDescription Hmiexe = new ModuleDescription("hmi", "hmi module");

    public static readonly ModuleDescription ModuleA = new ModuleDescription("moduleA", "Module A");
    public static readonly ModuleDescription ModuleB = new ModuleDescription("moduleB", "Module B");
    public static readonly ModuleDescription Adapter = new ModuleDescription("adapter", "Adapter Module");
    public static readonly ModuleDescription DBAdapter = new ModuleDescription("dBAdapter", "DB Adapter Module");
    public static readonly ModuleDescription ProdManager = new ModuleDescription("prodManager", "Production Manager Module");
    public static readonly ModuleDescription ProdPlaning = new ModuleDescription("prodPlaning", "Production Planing Module");
    public static readonly ModuleDescription MvHistory = new ModuleDescription("MVHistory", "MV History");
    public static readonly ModuleDescription Delay = new ModuleDescription("delays", "Delay Module");
    public static readonly ModuleDescription Tracking = new ModuleDescription("tracking", "Tracking Module");
    public static readonly ModuleDescription WalkingBeamFurnance = new ModuleDescription("walkingBeamFurnance", "Walking Beam Furnance Module");
    public static readonly ModuleDescription L1Sim = new ModuleDescription("l1Sim", "Level 1 Simulation");
    public static readonly ModuleDescription L3Sim = new ModuleDescription("SimL3", "Level 3 Simulation");
    public static readonly ModuleDescription TcpProxy = new ModuleDescription("tcpProxy", "L1 TCP Communication");
    public static readonly ModuleDescription ZebraPrinter = new ModuleDescription("zebraPrinterConnector", "Zebra Printer Communication");
    public static readonly ModuleDescription Setup = new ModuleDescription("Setup", "Setup");
    public static readonly ModuleDescription Simulation = new ModuleDescription("Simulation", "Simulation");
    public static readonly ModuleDescription Dispatcher = new ModuleDescription("Dispatcher", "Dispatcher");
    public static readonly ModuleDescription RollShop = new ModuleDescription("RollShop", "RollShop");
    public static readonly ModuleDescription Maintenance = new ModuleDescription("Maintenance", "Maintenance");
    public static readonly ModuleDescription Quality = new ModuleDescription("Quality", "Quality");

    //CUSTOM
    public static readonly ModuleDescription L1Adapter = new ModuleDescription("L1Adapter", "L1Adapter");
  }
}
