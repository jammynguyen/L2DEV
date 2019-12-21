using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity;
using SMF.HMIWWW.UnitConverter;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;

namespace PE.HMIWWW.ViewModel.Module.Lite.Steelgrade
{
  public class VM_Steelgrade: VM_Base
  {
    public long Id { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "SteelgradeCode", "NAME_SteelgradeCode")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfRequired]
    public string SteelgradeCode { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "SteelgradeName", "NAME_SteelGradeName")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string SteelgradeName { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "ParentSteelgradeCode", "NAME_ParentSteelgradeCode")]
    public string ParentSteelgradeCode { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "Description", "NAME_Description")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength", ErrorMessageResourceType = typeof(VM_Resources))]
    public string Description { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "Density", "NAME_SteelDensity")]
    [SmfFormat("FORMAT_Density", NullDisplayText = "-")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfUnit("UNIT_Density")]
    //[SmfRange(typeof(VM_Steelgrade), "Density", "SteelDensity")]
    public double? Density { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "IsDefault", "NAME_IsDefault")]
    public bool? IsDefault { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "QualitySpecification", "NAME_QualitySpecification")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string QualitySpecification { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CommercialGroup", "NAME_CommercialGroup")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string CommercialGroup { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CustomerUseCode", "NAME_CustomerUseCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string CustomerUseCode { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CustomerUseDescription", "NAME_CustomerUseDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string CustomerUseDescription { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "GroupCode", "NAME_GroupCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]

    public string GroupCode { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "GroupDescription", "NAME_GroupDescription")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string GroupDescription { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "OvenRecipeTemperature", "NAME_OvenRecipeTemp")]
    [SmfFormat("FORMAT_Temperature", NullDisplayText = "-")]
    [SmfUnit("UNIT_Temperature")]
    [SmfRange(typeof(VM_Steelgrade), "OvenRecipeTemperature", "OvenRecipeTemp")]
    public double? OvenRecipeTemperature { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "FeMax", "NAME_SteelGradeCC_FeMax")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "FeMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    //[SmfDisplayAttribute(typeof(VM_Steelgrade), "FeMax", "NAME_SteelGradeCC_FeMax")]
    //[DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public double? FeMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "FeMin", "NAME_SteelGradeCC_FeMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "FeMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? FeMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CMax", "NAME_SteelGradeCC_CMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "CMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    //[DisplayFormat(DataFormatString ="{0:N3}")]
    public double? CMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CMin", "NAME_SteelGradeCC_CMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "CMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? CMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "MnMax", "NAME_SteelGradeCC_MnMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "MnMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? MnMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "MnMin", "NAME_SteelGradeCC_MnMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "MnMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? MnMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CrMax", "NAME_SteelGradeCC_CrMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "CrMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? CrMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CrMin", "NAME_SteelGradeCC_CrMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "CrMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? CrMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "MoMax", "NAME_SteelGradeCC_MoMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "MoMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? MoMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "MoMin", "NAME_SteelGradeCC_MoMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "MoMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? MoMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "VMax", "NAME_SteelGradeCC_VMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "VMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? VMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "VMin", "NAME_SteelGradeCC_VMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "VMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? VMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "NiMax", "NAME_SteelGradeCC_NiMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "NiMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? NiMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "NiMin", "NAME_SteelGradeCC_NiMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "NiMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? NiMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CoMax", "NAME_SteelGradeCC_CoMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "CoMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? CoMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CoMin", "NAME_SteelGradeCC_CoMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "CoMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? CoMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "SiMax", "NAME_SteelGradeCC_SiMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "SiMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? SiMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "SiMin", "NAME_SteelGradeCC_SiMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "SiMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? SiMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "PMax", "NAME_SteelGradeCC_PMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "PMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? PMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "PMin", "NAME_SteelGradeCC_PMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "PMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? PMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "SMax", "NAME_SteelGradeCC_SMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "SMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? SMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "SMin", "NAME_SteelGradeCC_SMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "SMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? SMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CuMax", "NAME_SteelGradeCC_CuMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "CuMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? CuMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CuMin", "NAME_SteelGradeCC_CuMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "CuMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? CuMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "NbMax", "NAME_SteelGradeCC_NbMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "NbMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? NbMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "NbMin", "NAME_SteelGradeCC_NbMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "NbMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? NbMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "AlMax", "NAME_SteelGradeCC_AlMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "AlMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? AlMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "AlMin", "NAME_SteelGradeCC_AlMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "AlMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? AlMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "NMax", "NAME_SteelGradeCC_NMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "NMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? NMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "NMin", "NAME_SteelGradeCC_NMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "NMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? NMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CaMax", "NAME_SteelGradeCC_CaMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "CaMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? CaMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CaMin", "NAME_SteelGradeCC_CaMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "CaMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? CaMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "BMax", "NAME_SteelGradeCC_BMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "BMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? BMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "BMin", "NAME_SteelGradeCC_BMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "BMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? BMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "TiMax", "NAME_SteelGradeCC_TiMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "TiMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? TiMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "TiMin", "NAME_SteelGradeCC_TiMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "TiMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? TiMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "SnMax", "NAME_SteelGradeCC_SnMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "SnMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? SnMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "SnMin", "NAME_SteelGradeCC_SnMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "SnMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? SnMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "OMax", "NAME_SteelGradeCC_OMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "OMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? OMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "OMin", "NAME_SteelGradeCC_OMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "OMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? OMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "HMax", "NAME_SteelGradeCC_HMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "HMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? HMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "HMin", "NAME_SteelGradeCC_HMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "HMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? HMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "WMax", "NAME_SteelGradeCC_WMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "WMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? WMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "WMin", "NAME_SteelGradeCC_WMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "WMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? WMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "PbMax", "NAME_SteelGradeCC_PbMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "PbMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? PbMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "PbMin", "NAME_SteelGradeCC_PbMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "PbMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? PbMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "ZnMax", "NAME_SteelGradeCC_ZnMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "ZnMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? ZnMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "ZnMin", "NAME_SteelGradeCC_ZnMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "ZnMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? ZnMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "AsMax", "NAME_SteelGradeCC_AsMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "AsMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? AsMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "AsMin", "NAME_SteelGradeCC_AsMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "AsMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? AsMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "MgMax", "NAME_SteelGradeCC_MgMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "MgMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? MgMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "MgMin", "NAME_SteelGradeCC_MgMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "MgMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? MgMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "SbMax", "NAME_SteelGradeCC_SbMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "SbMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? SbMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "SbMin", "NAME_SteelGradeCC_SbMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "SbMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? SbMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "BiMax", "NAME_SteelGradeCC_BiMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "BiMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? BiMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "BiMin", "NAME_SteelGradeCC_BiMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "BiMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? BiMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "TaMax", "NAME_SteelGradeCC_TaMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "TaMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? TaMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "TaMin", "NAME_SteelGradeCC_TaMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "TaMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? TaMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "ZrMax", "NAME_SteelGradeCC_ZrMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "ZrMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? ZrMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "ZrMin", "NAME_SteelGradeCC_ZrMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "ZrMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? ZrMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CeMax", "NAME_SteelGradeCC_CeMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "CeMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? CeMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "CeMin", "NAME_SteelGradeCC_CeMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "CeMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? CeMin { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "TeMax", "NAME_SteelGradeCC_TeMax")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "TeMax", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? TeMax { get; set; }
    [SmfDisplay(typeof(VM_Steelgrade), "TeMin", "NAME_SteelGradeCC_TeMin")]
    [SmfFormat("FORMAT_Percent_Steelgrade", NullDisplayText = "-")]
    [SmfRange(typeof(VM_Steelgrade), "TeMin", "ChemAnalysis")]
    [SmfUnit("UNIT_Percent")]
    public double? TeMin { get; set; }

    public long? FkGroupId { get; set; }



    public VM_Steelgrade()
    {

    }

    public VM_Steelgrade(PRMSteelgrade sg)
    {
      Id = sg.SteelgradeId;
      SteelgradeCode = sg.SteelgradeCode;
      SteelgradeName = sg.SteelgradeName;
      Description = sg.SteelgradeDescription;
      Density = sg.Density;
      IsDefault = sg.IsDefault;
      QualitySpecification = sg.QualitySpecification;
      CommercialGroup = sg.CommercialGroup;
      CustomerUseCode = sg.CustomerUseCode;
      CustomerUseDescription = sg.CustomerUseDescription;
      GroupCode = sg.PRMSteelGroup?.SteelGroupCode;
      GroupDescription = sg.PRMSteelGroup?.SteelGroupDescription;
      OvenRecipeTemperature = sg.OvenRecipeTemperature;
      ParentSteelgradeCode = sg.PRMSteelgrade1?.SteelgradeCode;
      FkGroupId = sg.FKSteelGroupId;

      //ChemicalComposition
      FeMax = sg.PRMSteelgradeChemicalComposition?.FeMax;
      FeMin = sg.PRMSteelgradeChemicalComposition?.FeMin;
      CMax = sg.PRMSteelgradeChemicalComposition?.CMax;
      CMin = sg.PRMSteelgradeChemicalComposition?.CMin;
      MnMax = sg.PRMSteelgradeChemicalComposition?.MnMax;
      MnMin = sg.PRMSteelgradeChemicalComposition?.MnMin;
      CrMax = sg.PRMSteelgradeChemicalComposition?.CrMax;
      CrMin = sg.PRMSteelgradeChemicalComposition?.CrMin;
      MoMax = sg.PRMSteelgradeChemicalComposition?.MoMax;
      MoMin = sg.PRMSteelgradeChemicalComposition?.MoMin;
      VMax = sg.PRMSteelgradeChemicalComposition?.VMax;
      VMin = sg.PRMSteelgradeChemicalComposition?.VMin;
      NiMax = sg.PRMSteelgradeChemicalComposition?.NiMax;
      NiMin = sg.PRMSteelgradeChemicalComposition?.NiMin;
      CoMax = sg.PRMSteelgradeChemicalComposition?.CoMax;
      CoMin = sg.PRMSteelgradeChemicalComposition?.CoMin;
      SiMax = sg.PRMSteelgradeChemicalComposition?.SiMax;
      SiMin = sg.PRMSteelgradeChemicalComposition?.SiMin;
      PMax = sg.PRMSteelgradeChemicalComposition?.PMax;
      PMin = sg.PRMSteelgradeChemicalComposition?.PMin;
      SMax = sg.PRMSteelgradeChemicalComposition?.SMax;
      SMin = sg.PRMSteelgradeChemicalComposition?.SMin;
      CuMax = sg.PRMSteelgradeChemicalComposition?.CuMax;
      CuMin = sg.PRMSteelgradeChemicalComposition?.CuMin;
      NbMax = sg.PRMSteelgradeChemicalComposition?.NbMax;
      NbMin = sg.PRMSteelgradeChemicalComposition?.NbMin;
      AlMax = sg.PRMSteelgradeChemicalComposition?.AlMax;
      AlMin = sg.PRMSteelgradeChemicalComposition?.AlMin;
      NMax = sg.PRMSteelgradeChemicalComposition?.NMax;
      NMin = sg.PRMSteelgradeChemicalComposition?.NMin;
      CaMax = sg.PRMSteelgradeChemicalComposition?.CaMax;
      CaMin = sg.PRMSteelgradeChemicalComposition?.CaMin;
      BMax = sg.PRMSteelgradeChemicalComposition?.BMax;
      BMin = sg.PRMSteelgradeChemicalComposition?.BMin;
      TiMax = sg.PRMSteelgradeChemicalComposition?.TiMax;
      TiMin = sg.PRMSteelgradeChemicalComposition?.TiMin;
      SnMax = sg.PRMSteelgradeChemicalComposition?.SnMax;
      SnMin = sg.PRMSteelgradeChemicalComposition?.SnMin;
      OMax = sg.PRMSteelgradeChemicalComposition?.OMax;
      OMin = sg.PRMSteelgradeChemicalComposition?.OMin;
      HMax = sg.PRMSteelgradeChemicalComposition?.HMax;
      HMin = sg.PRMSteelgradeChemicalComposition?.HMin;
      WMax = sg.PRMSteelgradeChemicalComposition?.WMax;
      WMin = sg.PRMSteelgradeChemicalComposition?.WMin;
      PbMax = sg.PRMSteelgradeChemicalComposition?.PbMax;
      PbMin = sg.PRMSteelgradeChemicalComposition?.PbMin;
      ZnMax = sg.PRMSteelgradeChemicalComposition?.ZnMax;
      ZnMin = sg.PRMSteelgradeChemicalComposition?.ZnMin;
      AsMax = sg.PRMSteelgradeChemicalComposition?.AsMax;
      AsMin = sg.PRMSteelgradeChemicalComposition?.AsMin;
      MgMax = sg.PRMSteelgradeChemicalComposition?.MgMax;
      MgMin = sg.PRMSteelgradeChemicalComposition?.MgMin;
      SbMax = sg.PRMSteelgradeChemicalComposition?.SbMax;
      SbMin = sg.PRMSteelgradeChemicalComposition?.SbMin;
      BiMax = sg.PRMSteelgradeChemicalComposition?.BiMax;
      BiMin = sg.PRMSteelgradeChemicalComposition?.BiMin;
      TaMax = sg.PRMSteelgradeChemicalComposition?.TaMax;
      TaMin = sg.PRMSteelgradeChemicalComposition?.TaMin;
      ZrMax = sg.PRMSteelgradeChemicalComposition?.ZrMax;
      ZrMin = sg.PRMSteelgradeChemicalComposition?.ZrMin;
      CeMax = sg.PRMSteelgradeChemicalComposition?.CeMax;
      CeMin = sg.PRMSteelgradeChemicalComposition?.CeMin;
      TeMax = sg.PRMSteelgradeChemicalComposition?.TeMax;
      TeMin = sg.PRMSteelgradeChemicalComposition?.TeMin;

      UnitConverterHelper.ConvertToLocal(this);
    }
  }
}
