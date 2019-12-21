using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.WeighingMonitor
{
  public class VM_WeighingOverview : VM_Base
  {
    #region prop
    public List<VM_RawMaterialOverview> MaterialsBeforeScale { get; set; }

    public VM_RawMaterialOverview MaterialOnScale { get; set; }

    public List<VM_RawMaterialOverview> MaterialsAfterScale { get; set; }
    #endregion

    #region ctor
    public VM_WeighingOverview()
    {

    }

    public VM_WeighingOverview(List<V_RawMaterialOverview> materialsBeforeScale, V_RawMaterialOverview materialOnScale, List<V_RawMaterialOverview> materialsAfterScale)
    {
      MaterialsBeforeScale = new List<VM_RawMaterialOverview>();
      MaterialsAfterScale = new List<VM_RawMaterialOverview>();
      
      foreach (V_RawMaterialOverview rawMat in materialsBeforeScale)
      {
        MaterialsBeforeScale.Add(new VM_RawMaterialOverview(rawMat));
      }

      foreach (V_RawMaterialOverview rawMat in materialsAfterScale)
      {
        MaterialsAfterScale.Add(new VM_RawMaterialOverview(rawMat));
      }

      MaterialOnScale = new VM_RawMaterialOverview(materialOnScale);
    }

   
    #endregion
  }
}
