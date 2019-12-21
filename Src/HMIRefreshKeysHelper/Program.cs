using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;
using System.IO;

namespace HMIRefreshKeysHelper
{
  class Program
  {
    static void Main(string[] args)
    {
      string refreshKeys = "var HmiRefreshKeys = " + GetClasses("HMIRefreshKeys") + ";";
      OverwriteRefreshKeysFile(refreshKeys);
    }
    public static string TryGetSolutionDirectoryInfo(string currentPath = null)
    {
      DirectoryInfo directory = new DirectoryInfo(
          currentPath ?? Directory.GetCurrentDirectory());
      while (directory != null && !directory.GetFiles("*.sln").Any())
      {
        directory = directory.Parent;
      }
      FileInfo fileInfo = directory.GetFiles("*pe_lite_dev.sln").FirstOrDefault();
      return fileInfo != null ? fileInfo.FullName : null;
    }

    static string GetClasses(string nameSpace)
    {
      Assembly assembly = Assembly.GetAssembly(typeof(PE.Common.HMIRefreshKeys));
      Dictionary<string, object> constants = typeof(PE.Common.HMIRefreshKeys).GetFields().ToDictionary(x => x.Name, x => x.GetValue(null));
      return new JavaScriptSerializer().Serialize(constants);
    }

    static void OverwriteRefreshKeysFile(string line)
    {
      DirectoryInfo di = new DirectoryInfo(Environment.GetCommandLineArgs()[0]);
      string path = di.Parent.Parent.Parent.Parent.FullName;
      File.WriteAllText(path + @"\PE.HMIWWW\Scripts\contents.js", line);
    }
  }
}
