using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Core.HtmlHelpers
{
	public static class ResxHelper
	{
		public static string GetResxByKey(string resxKey)
		{
			string resxValue = Resources.VM_Resources.ResourceManager.GetString(resxKey);
			if (resxValue == null || resxValue == string.Empty)
			{
				resxValue = Resources.VM_Resources.ResourceManager.GetString(resxKey);

				if (resxValue == null || resxValue == string.Empty)
				{
					resxValue = String.Format("({0}) - {1}", resxKey, Resources.VM_Resources.ResourceManager.GetString("GLOB_SNDIR"));
				}
			}
			return resxValue;
		}

		public static string GetResxButtonText(string functionName)
		{
			string resxKey = string.Format("BUTTON_{0}", functionName);
			string resxValue = Resources.VM_Resources.ResourceManager.GetString(resxKey);
			if (resxValue == null || resxValue == string.Empty)
			{
				resxValue = Resources.VM_Resources.ResourceManager.GetString(resxKey);

				if (resxValue == null || resxValue == string.Empty)
				{
					resxValue = String.Format("({0}) - {1}", resxKey, Resources.VM_Resources.ResourceManager.GetString("GLOB_SNDIR"));
				}
			}
			return resxValue;
		}

    public static MvcHtmlString GetResourceByKey(string resourceKey)
    {
      return MvcHtmlString.Create(GetResxByKey(resourceKey));
    }
  }
}