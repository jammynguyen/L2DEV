using System;
using SMF.HMIWWW.Attributes;
using SMF.DbEntity.Model;
using System.Collections.Generic;
using PE.HMIWWW.Core.ViewModel;
using System.ComponentModel.DataAnnotations;
using PE.HMIWWW.Core.Resources;
using PE.DbEntity.Model;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.Models;

namespace PE.HMIWWW.ViewModel.Module
{
  public class VM_GrooveTemplate : VM_Base
  {
    #region properties
    public virtual Int64? Id { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "CreatedTs", "NAME_DateTimeCreated")]
    public virtual DateTime CreatedTs { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "LastUpdatedTs", "NAME_DateTimeLastUpdate")]
    public virtual DateTime LastUpdatedTs { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "Shape", "NAME_Shape")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual String Shape { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "GrooveTemplateName", "NAME_GrooveTemplateName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual String GrooveTemplateName { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "Status", "NAME_Status")]
    public virtual short Status { get; set; }

    //public virtual String GrStatus { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "R1", "NAME_Radius1")]
    //	[SmfRangeAttribute(typeof(VM_BilletCatalogue), "R1", "RANGE_MIN_GrooveRadius", "RANGE_MAX_GrooveRadius")]
    [SmfFormatAttribute("FORMAT_Radius")]
    [SmfUnitAttribute("UNIT_Radius")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? R1 { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "R2", "NAME_Radius2")]
    //[SmfRangeAttribute(typeof(VM_BilletCatalogue), "R2", "RANGE_MIN_GrooveRadius", "RANGE_MAX_GrooveRadius")]
    [SmfFormatAttribute("FORMAT_Radius")]
    [SmfUnitAttribute("UNIT_Radius")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? R2 { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "R3", "NAME_Radius3")]
    //[SmfRangeAttribute(typeof(VM_BilletCatalogue), "R3", "RANGE_MIN_GrooveRadius", "RANGE_MAX_GrooveRadius")]
    [SmfFormatAttribute("FORMAT_Radius")]
    [SmfUnitAttribute("UNIT_Radius")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? R3 { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "D1", "NAME_Depth1")]
    //	[SmfRangeAttribute(typeof(VM_BilletCatalogue), "D1", "RANGE_MIN_GrooveDepth", "RANGE_MAX_GrooveDepth")]
    [SmfFormatAttribute("FORMAT_Depth")]
    [SmfUnitAttribute("UNIT_Depth")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? D1 { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "D2", "NAME_Depth2")]
    //[SmfRangeAttribute(typeof(VM_BilletCatalogue), "D2", "RANGE_MIN_GrooveDepth", "RANGE_MAX_GrooveDepth")]
    [SmfFormatAttribute("FORMAT_Depth")]
    [SmfUnitAttribute("UNIT_Depth")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? D2 { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "W1", "NAME_Width1")]
    //[SmfRangeAttribute(typeof(VM_BilletCatalogue), "W1", "RANGE_MIN_GrooveWidth", "RANGE_MAX_GrooveWidth")]
    [SmfFormatAttribute("FORMAT_Width")]
    [SmfUnitAttribute("UNIT_WidthSmall")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? W1 { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "W2", "NAME_Width2")]
    //[SmfRangeAttribute(typeof(VM_BilletCatalogue), "W2", "RANGE_MIN_GrooveWidth", "RANGE_MAX_GrooveWidth")]
    [SmfFormatAttribute("FORMAT_Width")]
    [SmfUnitAttribute("UNIT_WidthSmall")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? W2 { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "Angle1", "NAME_Angle1")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormatAttribute("FORMAT_Angle")]
    public virtual double? Angle1 { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "Angle2", "NAME_Angle2")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    [SmfFormatAttribute("FORMAT_Angle")]
    public virtual double? Angle2 { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "SpreadFactor", "NAME_SpreadFactor")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? SpreadFactor { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "GrindingProgrammeName", "NAME_GrindingProgrammeName")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual String GrindingProgramName { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "Description", "NAME_Description")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual String Description { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "GroovePlane", "NAME_Plane")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    //	[SmfRangeAttribute(typeof(VM_GrooveTemplate), "GroovePlane", "RANGE_MIN_Plane", "RANGE_MAX_Plane")]
    [SmfFormatAttribute("FORMAT_Plane")]
    [SmfUnitAttribute("UNIT_ProfilePlane")]
    public virtual double? GroovePlane { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "NameShort", "NAME_GrooveNameShort")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual String NameShort { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "Code", "NAME_Code")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual String GrooveTemplateCode { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "Ds", "NAME_Ds")]  // diameter of material
    [SmfFormatAttribute("FORMAT_Depth")]
    [SmfUnitAttribute("UNIT_Depth")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? Ds { get; set; }

    [SmfDisplayAttribute(typeof(VM_GrooveTemplate), "Dw", "NAME_Dw")]   // diameter of tool
    [SmfFormatAttribute("FORMAT_Depth")]
    [SmfUnitAttribute("UNIT_Depth")]
    [DisplayFormat(NullDisplayText = @"&nbsp;", HtmlEncode = false)]
    public virtual double? Dw { get; set; }


    #endregion
    #region ctor
    public VM_GrooveTemplate()
    {
    }
    public VM_GrooveTemplate(RLSGrooveTemplate grooveTemplate)
    {
      this.Id = grooveTemplate.GrooveTemplateId;
      this.Shape = grooveTemplate.Shape;
      this.GrooveTemplateName = grooveTemplate.GrooveTemplateName;
      this.Status = (short)grooveTemplate.Status;
      //	this.GrStatus = String.Format("{0}", grooveTemplate.Status);
      this.R1 = grooveTemplate.R1;
      this.R2 = grooveTemplate.R2;
      this.R3 = grooveTemplate.R3;
      this.D1 = grooveTemplate.D1;
      this.D2 = grooveTemplate.D2;
      this.W1 = grooveTemplate.W1;
      this.W2 = grooveTemplate.W2;
      this.Angle1 = grooveTemplate.Angle1;
      this.Angle2 = grooveTemplate.Angle2;
      this.SpreadFactor = grooveTemplate.SpreadFactor;
      this.GrindingProgramName = grooveTemplate.GrindingProgramName;
      this.Description = grooveTemplate.GrooveTemplateDescription;
      this.GroovePlane = grooveTemplate.Plane;
      this.NameShort = grooveTemplate.GrooveTemplateCode;
      this.Ds = grooveTemplate.Ds;
      this.Dw = grooveTemplate.Dw;
      this.GrooveTemplateCode = grooveTemplate.GrooveTemplateCode;

      UnitConverterHelper.ConvertToLocal<VM_GrooveTemplate>(this);
    }
    #endregion
  }



}
