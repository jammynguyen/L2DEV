using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Setup
{
	public class VM_TelegramStructure : VM_Base
	{


		public long? ElementId { get; set; }
		public long? ParentElementId { get; set; }
		[SmfDisplayAttribute(typeof(VM_TelegramStructure), "FeatureCode", "NAME_ElementCode")]
		[DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
		public string ElementCode { get; set; }
		[SmfDisplayAttribute(typeof(VM_TelegramStructure), "FeatureCode", "NAME_DataType")]
		[DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
		public string DataType { get; set; }
		public long? TelegramStructureId { get; set; }
		public long TelegramStructureIndex { get; set; }
		public string TelegramStructureIndexString { get; set; }
		public long? ParentTelegramStructureIndex { get; set; }
		public bool? IsStructure { get; set; }
		[SmfDisplayAttribute(typeof(VM_TelegramStructure), "FeatureCode", "NAME_StructurePath")]
		[DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
		public string StructurePath { get; set; }
		public string StructureCode { get; set; }
		public int? StructureLevel { get; set; }
		public long? Sort { get; set; }
		[SmfDisplayAttribute(typeof(VM_TelegramStructure), "FeatureCode", "NAME_Value")]
		[DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
		public string Value { get; set; }
		public long? TelegramId { get; set; }

		public VM_TelegramStructure()
		{

		}
		public VM_TelegramStructure(V_TelegramStructures entity)
		{
			this.ElementId = entity.ElementId;
			this.ParentElementId = entity.ParentElementId;
			this.ElementCode = entity.ElementCode;
			this.DataType = entity.DataType;
			this.TelegramStructureId = entity.TelegramStructureId;
			//this.TelegramStructureIndex = entity.TelegramStructureIndex;
			this.IsStructure = entity.IsStructure;
			this.StructurePath = entity.StructurePath;
			this.StructureCode = entity.StructureCode;
			this.StructureLevel = entity.StructureLevel;
			this.Sort = entity.Sort;

		}

		public VM_TelegramStructure(V_TelegramValues entity)
		{
			this.ElementId = entity.ElementId;
			this.ParentElementId = entity.ParentElementId;
			this.ElementCode = entity.ElementCode;
			this.DataType = entity.DataType;
			this.TelegramStructureId = entity.TelegramStructureId;
			this.TelegramStructureIndexString = entity.TelegramStructureIndex;
			this.TelegramStructureIndex = Convert.ToInt64(entity.TelegramStructureIndex.Replace(".", ""));
			this.ParentTelegramStructureIndex = entity.ParentElementId != null ? (long?)Convert.ToInt64(entity.TelegramStructureIndex.Substring(0, entity.TelegramStructureIndex.LastIndexOf('.')).Replace(".", "")) : null;
			this.IsStructure = entity.IsStructure;
			this.StructurePath = entity.StructurePath;
			this.StructureCode = entity.StructureCode;
			this.StructureLevel = entity.StructureLevel;
			this.Sort = entity.Sort;
			this.Value = entity.Value;
			this.TelegramId = entity.TelegramId;

		}
	}
}
