using PE.DbEntity.Models;
using PE.DTO.External.MVHistory;
using PE.DTO.Internal.MVHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.L1S.Level1Simulation.Module
{
  class L1SimulationMaterialDivision
  {
    internal DCL1MaterialDivisionExt MaterialDivisionSimulation(MVHRawMaterial simulatedRawMaterial, long locationId)
    {
      MVHRawMaterialsStep lastStep = simulatedRawMaterial.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == 0).Single();
      double newLength = 0;
      if (lastStep.Length > 0)
        newLength = Convert.ToDouble(lastStep.Length) / 2;


      return new DCL1MaterialDivisionExt()
      {
        CutNumberInParent = Convert.ToUInt16(simulatedRawMaterial.ChildsNo+1),
        LocationId = Convert.ToUInt16(locationId),
        NewMaterialLength = Convert.ToSingle(newLength),
        ParentBasId = Convert.ToUInt32(simulatedRawMaterial.ExternalRawMaterialId),
        RequestToken = Convert.ToUInt16(new Random().Next(0, 60000)),
        Header = new DTO.External.Adapter.DCHeaderExt()
        {
          Counter = 1,
          MessageId = 2002,
          TimeStamp = DateTime.Now.ToString(),
        }
      };
    }

    internal  DCL1CutDataExt MaterialCutSimulation(MVHRawMaterial simulatedRawMaterial, long locationId, short typeOfCut)
    {
      MVHRawMaterialsStep lastStep = simulatedRawMaterial.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == 0).Single();
      double newLength = 0;

      if (lastStep.Length > 0)
        newLength = Convert.ToDouble(lastStep.Length) * 0.05;

      return new DCL1CutDataExt()
      {
        CutLength = Convert.ToSingle(newLength),
        BasId = Convert.ToUInt16(simulatedRawMaterial.ExternalRawMaterialId),
        TypeOfCut = Convert.ToUInt16(typeOfCut),
        LocationId = Convert.ToUInt16(locationId),
        Header = new DTO.External.Adapter.DCHeaderExt()
        {
          Counter = 1,
          TimeStamp = DateTime.Now.ToString(),
          MessageId = 2001,
        }
      };
    }
  }
}
