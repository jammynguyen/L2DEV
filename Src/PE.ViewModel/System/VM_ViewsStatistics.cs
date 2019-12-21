using System;
using SMF.HMIWWW.Attributes;
using SMF.DbEntity.Model;
using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.System
{
	public class VM_ViewsStatistics : VM_Base
	{
		#region properties
		public virtual Int64 Id { get; set; }
		[SmfDisplayAttribute(typeof(VM_ViewsStatistics), "Name", "NAME_Name")]
		public virtual string Name { get; set; }
		[SmfDisplayAttribute(typeof(VM_ViewsStatistics), "Created", "NAME_Created")]
		public virtual DateTime? Created { get; set; }
		[SmfDisplayAttribute(typeof(VM_ViewsStatistics), "Records", "NAME_Records")]
		public virtual int? Records { get; set; }
		[SmfDisplayAttribute(typeof(VM_ViewsStatistics), "Time", "NAME_Time")]
		public virtual int? Time { get; set; }
		[SmfDisplayAttribute(typeof(VM_ViewsStatistics), "TimePerRecord", "NAME_TimePerRecord")]
		public virtual double? TimePerRecord { get; set; }
		[SmfDisplayAttribute(typeof(VM_ViewsStatistics), "UsedInViews", "NAME_UsedInViews")]
		public virtual int? UsedInViews { get; set; }
		[SmfDisplayAttribute(typeof(VM_ViewsStatistics), "ViewsOwned", "NAME_ViewsOwned")]
		public virtual int? ViewsOwned { get; set; }
		#endregion

		#region ctor
		public VM_ViewsStatistics()
		{

		}
		public VM_ViewsStatistics(ViewsStatistic entity)
		{
			this.Id = entity.Id;
			this.Name = entity.Name;
			this.Created = entity.Created;
			this.Records = entity.Records;
			this.Time = entity.Time;
			this.TimePerRecord = entity.TimePerRecord;
			this.UsedInViews = entity.UsedInViews;
			this.ViewsOwned = entity.ViewsOwned;
		}
		#endregion
	}

}
