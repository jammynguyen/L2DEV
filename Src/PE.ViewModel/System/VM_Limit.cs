using System;
using SMF.HMIWWW.Attributes;
using SMF.DbEntity.Model;
using SMF.DbEntity.Enums;
using PE.HMIWWW.Core.ViewModel;
using System.Collections.Generic;

namespace PE.HMIWWW.ViewModel.System
{
	public class VM_Limit : VM_Base
	{
		public VM_Limit()
		{
		}
		public VM_Limit(Limit limit)
		{
			Id = limit.LimitId;
			Description = limit.Description;
			Name = limit.Name;
			GroupId = limit.LimitGroupId;
			GroupName = limit.ParameterGroup.Name;
			switch (limit.ValueType)
			{
				case (int)LimitValueType.ValueDate:
					LowerValue = limit.LowerValueDate.ToString();
					UpperValue = limit.UpperValueDate.ToString();
					break;
				case (int)LimitValueType.ValueFloat:
					LowerValue = limit.LowerValueFloat.ToString();
					UpperValue = limit.UpperValueFloat.ToString();
					break;
				case (int)LimitValueType.ValueInt:
				default:
					LowerValue = limit.LowerValueInt.ToString();
					UpperValue = limit.UpperValueInt.ToString();
					break;
			}

			if (limit.UnitOfMeasure != null)
				Unit = limit.UnitOfMeasure.UnitSymbol;
			Type = limit.ValueType.ToString();
			ConvertToLocal(this);
		}

		[SmfDisplayAttribute(typeof(VM_Limit), "Id", "NAME_Id")]
		public virtual Int64 Id { get; set; }

		[SmfDisplayAttribute(typeof(VM_Limit), "Description", "NAME_Description")]
		public virtual String Description { get; set; }

		[SmfDisplayAttribute(typeof(VM_Limit), "Name", "NAME_Name")]
		public virtual String Name { get; set; }

		[SmfDisplayAttribute(typeof(VM_Limit), "GroupName", "NAME_GroupName")]
		public virtual String GroupName { get; set; }

		[SmfDisplayAttribute(typeof(VM_Limit), "GroupId", "NAME_Id")]
		public virtual Int64 GroupId { get; set; }

		[SmfDisplayAttribute(typeof(VM_Limit), "LowerValue", "NAME_LowerValue")]
		public virtual String LowerValue { get; set; }

		[SmfDisplayAttribute(typeof(VM_Limit), "UpperValue", "NAME_UpperValue")]
		public virtual String UpperValue { get; set; }

		[SmfDisplayAttribute(typeof(VM_Limit), "Type", "NAME_LimitType")]
		public virtual String Type { get; set; }

		[SmfDisplayAttribute(typeof(VM_Limit), "Unit", "NAME_Unit")]
		public virtual String Unit { get; set; }

	}
	//public class VM_LimitList : VM_BaseList<VM_Limit>
	//{
	//	public VM_LimitList()
	//	{
	//	}
	//	public VM_LimitList(IList<Limit> dbClass)
	//	{
	//		foreach (Limit item in dbClass)
	//		{
	//			this.Add(new VM_Limit(item));
	//		}
	//	}
	//}
}
