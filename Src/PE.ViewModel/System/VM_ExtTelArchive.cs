using System;
using SMF.HMIWWW.Attributes;
using SMF.DbEntity.Model;
using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;
using PE.DbEntity.Model;
using PE.DbEntity.Models;

namespace PE.HMIWWW.ViewModel.System
{
	public class VM_ExtTelArchive : VM_Base
	{
		#region properties
		//[SmfDisplayAttribute(typeof(VM_ExtTelArchive), "Id", "NAME_Id")]
		public virtual Int64 Id { get; set; }

		[SmfDisplayAttribute(typeof(VM_ExtTelArchive), "TelId", "NAME_TelegramId")]
		public virtual Int32? TelId { get; set; }

		[SmfDisplayAttribute(typeof(VM_ExtTelArchive), "Sender", "NAME_TelegramSender")]
		public virtual String Sender { get; set; }

		[SmfDisplayAttribute(typeof(VM_ExtTelArchive), "IsValid", "NAME_TelegramIsValid")]
		public virtual int? IsValid { get; set; }

		[SmfDisplayAttribute(typeof(VM_ExtTelArchive), "TelTimeStamp", "NAME_TelegramTimeStamp")]
		public virtual DateTime TelTimeStamp { get; set; }
		#endregion

		#region ctor
		//public VM_ExtTelArchive(ExtTelArchive entity)
		//{
		//	this.Id = entity.id;
		//	this.TelId = entity.TelId;
		//	this.Sender = entity.Sender;
		//	this.IsValid = entity.IsValid;
		//	this.TelTimeStamp = entity.TelTimeStamp;
		//}
		#endregion
	}

	//public class VM_ExtTelArchiveList : VM_BaseList<VM_ExtTelArchive>
	//{
	//	public VM_ExtTelArchiveList()
	//	{
	//	}
	//	//public VM_CrewsList(IList<Crew> dbClass)
	//	//{
	//	//	foreach (Crew item in dbClass)
	//	//	{
	//	//		this.Add(new VM_Crews(item));
	//	//	}
	//	//}
	//}
}
