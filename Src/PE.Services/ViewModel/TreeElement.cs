using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.Services.ViewModel
{
	public class TreeElement : TreeViewItemModel
	{
		public TreeElement(long passSchedId, int pid, string pname, string vmAccessString) : base()
		{
			Id = pid.ToString();
			Text = pname;
			if (passSchedId > 0)
			{
				//string url = String.Format("javascript:RenderItem({0},{1}", passSchedId, pid);
				Url = String.Format("javascript:RenderItem({0},'{1}')", passSchedId, vmAccessString);
			}
		}
	}
}
