using System;
using SMF.HMIWWW.Attributes;
using SMF.DbEntity.Model;
using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;
using PE.DbEntity.Model;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.Resources;
using PE.DbEntity.Models;

namespace PE.HMIWWW.ViewModel.System
{
	public class VM_WidgetConfigurations : VM_Base
	{
		#region properties
		public virtual long? Id { get; set; }
		[SmfDisplayAttribute(typeof(VM_WidgetConfigurations), "WidgetName", "NAME_WidgetName")]
		public virtual string WidgetName { get; set; }
		[SmfDisplayAttribute(typeof(VM_WidgetConfigurations), "WidgetFileName", "NAME_WidgetFileName")]
		public virtual string WidgetFileName { get; set; }
		[SmfDisplayAttribute(typeof(VM_WidgetConfigurations), "Active", "NAME_Active")]
		public virtual bool Active { get; set; }
		#endregion

		#region ctor
		public VM_WidgetConfigurations()
		{
		}
		public VM_WidgetConfigurations(HMIWidgetConfiguration entity)
		{
			this.Id = entity.WidgetConfigurationId;
			this.WidgetName = entity.WidgetName;
			this.WidgetFileName = entity.WidgetFileName;
			this.Active = entity.IsActive;
		}
		#endregion
	}

	
}
