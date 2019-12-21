using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.Core.Resources
{
  public static class ResourceController
  {
    public static string GetMenuDisplayName(string nameFromDb)
    {
      string displayName = VM_Resources.ResourceManager.GetString(String.Format("HMI_MENU_{0}", nameFromDb));
      if (displayName == null)
        displayName = String.Format("{0} - [Translation needed in PE.HMIWWW.Core.Resources]", nameFromDb);
      return displayName;
    }

    public static string GetResourceTextByResourceKey(string resourceKey)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(String.Format("{0}", resourceKey));
      if (resourceString == null)
        resourceString = String.Format("{0} - [Translation needed in PE.HMIWWW.Core.Resources]", resourceKey);
      return resourceString;
    }

    public static string GetGlobalResourceTextByResourceKey(string resourceKey)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(String.Format("GLOB_{0}", resourceKey));
      if (resourceString == null)
        resourceString = String.Format("GLOB_{0} - [Translation needed in PE.HMIWWW.Core.Resources]", resourceKey);
      return resourceString;
    }

    public static string GetErrorText(string resourceKey)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(String.Format("ERROR_{0}", resourceKey));
      if (resourceString == null)
        resourceString = String.Format("ERROR_{0} - [Translation needed in PE.HMIWWW.Core.Resources]", resourceKey);
      return resourceString;
    }

    public static string GetEnumValue(string enumName, int value, CultureInfo cultureInfo = null)
    {
      if (cultureInfo != null)
        return VM_Resources.ResourceManager.GetString(string.Format("ENUM_{0}_{1}", enumName, value), cultureInfo);
      else
        return VM_Resources.ResourceManager.GetString(string.Format("ENUM_{0}_{1}", enumName, value));
    }

    public static string GePageTitleValue(string controller, string action)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(string.Format("PAGE_TITLE_{0}_{1}", controller, action));
      if (resourceString == null)
        resourceString = string.Format("Resource PAGE_TITLE_{0}_{1} not defined in PE.HMIWWW.Core.Resources", controller, action);
      return resourceString;
    }

    public static string GetBilletMillDirectionValue(string value)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(string.Format("GLOB_BilletMillDirection_{0}", value));
      if (resourceString == null)
        resourceString = string.Format("Resource GLOB_BilletMillDirection_{0}", value);
      return resourceString;
    }

    public static string GetBilletStatusValue(string value)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(string.Format("GLOB_MaterialStatus_{0}", value));
      if (resourceString == null)
        resourceString = string.Format("Resource GLOB_MaterialStatus_{0}", value);
      return resourceString;
    }

    public static string GetMeasurementValueName(string value)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(string.Format("GLOB_MeasuredValueIds_{0}", value));
      if (resourceString == null)
        resourceString = string.Format("Resource GLOB_MeasuredValueIds_{0}", value);
      return resourceString;
    }

    public static string GetStandStatusValue(string value)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(string.Format("GLOB_StandStatus_{0}", value));
      if (resourceString == null)
        resourceString = string.Format("Resource GLOB_StandStatus_{0}", value);
      return resourceString;
    }

    public static string GetArrangementValue(string value)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(string.Format("GLOB_CassetteArrangement_{0}", value));
      if (resourceString == null)
        resourceString = string.Format("Resource GLOB_CassetteArrangement_{0}", value);
      return resourceString;
    }

    public static string GetCassetteStatusValue(string value)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(string.Format("GLOB_CASSETTE_StatusEnum_{0}", value));
      if (resourceString == null)
        resourceString = string.Format("Resource GLOB_CASSETTE_StatusEnum_{0}", value);
      return resourceString;
    }

    public static string GetRollSetStatusValue(string value)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(string.Format("GLOB_ROLLSETS_StatusEnum_{0}", value));
      if (resourceString == null)
        resourceString = string.Format("Resource GLOB_ROLLSETS_StatusEnum_{0}", value);
      return resourceString;
    }

    public static string GetRollSetTypeValue(string value)
    {
      string resourceString = VM_Resources.ResourceManager.GetString(string.Format("GLOB_ROLLSETTYPE_Enum_{0}", value));
      if (resourceString == null)
        resourceString = string.Format("Resource GLOB_ROLLSETTYPE_Enum_{0}", value);
      return resourceString;
    }
  }
}
