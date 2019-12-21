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
	public class VM_Crew : VM_Base
	{
		#region properties
		public virtual long? CrewId { get; set; }
		[SmfDisplayAttribute(typeof(VM_Crew), "CreatedTs", "NAME_DateTimeCreated")]
		public virtual DateTime CreatedTs { get; set; }
		[SmfDisplayAttribute(typeof(VM_Crew), "LastUpdateTs", "NAME_DateTimeLastUpdate")]
		public virtual DateTime LastUpdateTs { get; set; }
    //[Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfDisplayAttribute(typeof(VM_Crew), "CrewName", "NAME_Name")]
    [SmfRequired]
    public virtual String CrewName { get; set; }
		[Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfDisplayAttribute(typeof(VM_Crew), "Description", "NAME_Description")]
		public virtual String Description { get; set; }
		public virtual String Color { get; set; }
    [SmfDisplayAttribute(typeof(VM_Crew), "LeaderName", "NAME_Leader")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public virtual String LeaderName { get; set; }

    #endregion

    #region ctor
    public VM_Crew()
		{
		}
		public VM_Crew(Crew entity)
		{
			this.CrewId = entity.CrewId;
			this.CreatedTs = entity.CreatedTs;
			this.LastUpdateTs = entity.LastUpdateTs;
			this.CrewName = entity.CrewName;
			this.Description = entity.Description;
      this.LeaderName = entity.LeaderName;


            this.Color = "#ff917e"; //Temporary for test
		}
		#endregion
	}

	
}
