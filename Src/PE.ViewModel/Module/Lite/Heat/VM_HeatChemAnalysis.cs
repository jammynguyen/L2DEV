using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Heat
{
  public class VM_HeatChemAnalysis : VM_Base
  {
    #region properties
    public long HeatChemAnalysisId { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "CreatedTs", "NAME_CreatedTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime CreatedTs { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "LastUpdateTs", "NAME_LastUpdateTs")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime LastUpdateTs { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "SampleTaken", "NAME_SampleTaken")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public DateTime? SampleTaken { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Fe", "NAME_SteelGradeCC_Fe")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Fe { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "C", "NAME_SteelGradeCC_C")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? C { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Mn", "NAME_SteelGradeCC_Mn")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Mn { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Cr", "NAME_SteelGradeCC_Cr")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Cr { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Mo", "NAME_SteelGradeCC_Mo")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Mo { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "V", "NAME_SteelGradeCC_V")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? V { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Ni", "NAME_SteelGradeCC_Ni")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Ni { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Co", "NAME_SteelGradeCC_Co")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Co { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Si", "NAME_SteelGradeCC_Si")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Si { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "P", "NAME_SteelGradeCC_P")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? P { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "S", "NAME_SteelGradeCC_S")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? S { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Cu", "NAME_SteelGradeCC_Cu")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Cu { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Nb", "NAME_SteelGradeCC_Nb")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Nb { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Al", "NAME_SteelGradeCC_Al")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Al { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "N", "NAME_SteelGradeCC_N")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? N { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Ca", "NAME_SteelGradeCC_Ca")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Ca { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "B", "NAME_SteelGradeCC_B")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? B { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Ti", "NAME_SteelGradeCC_Ti")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Ti { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Sn", "NAME_SteelGradeCC_Sn")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Sn { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "O", "NAME_SteelGradeCC_O")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? O { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "H", "NAME_SteelGradeCC_H")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? H { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "W", "NAME_SteelGradeCC_W")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? W { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Pb", "NAME_SteelGradeCC_Pb")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Pb { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Zn", "NAME_SteelGradeCC_Zn")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Zn { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "As", "NAME_SteelGradeCC_As")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? As { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Mg", "NAME_SteelGradeCC_Mg")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Mg { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Sb", "NAME_SteelGradeCC_Sb")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Sb { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Bi", "NAME_SteelGradeCC_Bi")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Bi { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Ta", "NAME_SteelGradeCC_Ta")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Ta { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Zr", "NAME_SteelGradeCC_Zr")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Zr { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Ce", "NAME_SteelGradeCC_Ce")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Ce { get; set; }
    [SmfDisplay(typeof(VM_HeatChemAnalysis), "Te", "NAME_SteelGradeCC_Te")]
    [SmfFormat("FORMAT_Plane", NullDisplayText = "-")]
    [SmfUnit("UNIT_Percent")]
    public double? Te { get; set; }

    public long FKHeatId { get; set; }

    #endregion

    #region ctor
    public VM_HeatChemAnalysis() { }

    public VM_HeatChemAnalysis(PRMHeatChemAnalysi data)
    {
      HeatChemAnalysisId = data.HeatChemAnalysisId;
      FKHeatId = data.FKHeatId;
      CreatedTs = data.CreatedTs;
      LastUpdateTs = data.LastUpdateTs;
      SampleTaken = data.SampleTaken;

      //ChemicalComposition
      Fe = data.Fe;
      C = data.C;
      Mn = data.Mn;
      Cr = data.Cr;
      Mo = data.Mo;
      V = data.V;
      Ni = data.Ni;
      Co = data.Co;
      Si = data.Si;
      P = data.P;
      S = data.S;
      Cu = data.Cu;
      Nb = data.Nb;
      Al = data.Al;
      N = data.N;
      Ca = data.Ca;
      B = data.B;
      Ti = data.Ti;
      Sn = data.Sn;
      O = data.O;
      H = data.H;
      W = data.W;
      Pb = data.Pb;
      Zn = data.Zn;
      As = data.As;
      Mg = data.Mg;
      Sb = data.Sb;
      Bi = data.Bi;
      Ta = data.Ta;
      Zr = data.Zr;
      Ce = data.Ce;
      Te = data.Te;

      UnitConverterHelper.ConvertToLocal(this);
    }

    #endregion
  }
}
