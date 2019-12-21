using System;
using SMF.HMIWWW.Attributes;
using SMF.DbEntity.Model;
using PE.HMIWWW.Core.ViewModel;
using System.Collections.Generic;
using SMF.DbEntity.Enums;

namespace PE.HMIWWW.ViewModel.System
{
	public class  VM_Parameter : VM_Base
	{
		[SmfDisplayAttribute(typeof(VM_Parameter), "Id", "NAME_Id")]
		public virtual Int64 Id { get; set; }
		[SmfDisplayAttribute(typeof(VM_Parameter), "Description", "NAME_Description")]
		public virtual String Description { get; set; }
		[SmfDisplayAttribute(typeof(VM_Parameter), "Name", "NAME_Name")]
		public virtual String Name { get; set; }
		[SmfDisplayAttribute(typeof(VM_Parameter), "GroupName", "NAME_GroupName")]
		public virtual String GroupName { get; set; }
		[SmfDisplayAttribute(typeof(VM_Parameter), "GroupId", "NAME_GroupId")]
		public virtual Int64 GroupId { get; set; }
		[SmfDisplayAttribute(typeof(VM_Parameter), "Value", "NAME_Value")]
		public virtual String Value { get; set; }
		[SmfDisplayAttribute(typeof(VM_Parameter), "Type", "NAME_Type")]
		public virtual String Type { get; set; }
		[SmfDisplayAttribute(typeof(VM_Parameter), "Unit", "NAME_Unit")]
		public virtual String Unit { get; set; }
	
		public VM_Parameter()
		{
			
		}
		public VM_Parameter(Parameter parameter)
		{
			this.Id = parameter.ParameterId;
			this.Description = parameter.Description;
			this.Name = parameter.Name;
			this.GroupId = parameter.ParameterGroup.ParameterGroupId;
			this.GroupName = parameter.ParameterGroup.Name;
			if (parameter.ValueType == (int)ParameterValueType.ValueText)
				this.Value = parameter.ValueText;
			else if (parameter.ValueType == (int)ParameterValueType.ValueDate)
				this.Value = parameter.ValueDate.ToString();
			else if (parameter.ValueType == (int)ParameterValueType.ValueFloat)
				this.Value = parameter.ValueFloat.ToString();
			else if (parameter.ValueType == (int)ParameterValueType.ValueInt)
				this.Value = parameter.ValueInt.ToString();

			if (parameter.UnitOfMeasure != null)
				Unit = parameter.UnitOfMeasure.UnitSymbol;
			this.Type = parameter.ValueType.ToString();
			ConvertToLocal(this);
		}
	}

	//public class VM_ParameterList : VM_BaseList<VM_Parameter>
	//{
	//	public VM_ParameterList()
	//	{
	//	}
	//	public VM_ParameterList(IList<Parameter> dbClass)
	//	{
	//		foreach (Parameter item in dbClass)
	//		{
	//			this.Add(new VM_Parameter(item));
	//		}
	//	}
	//}
}
