using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.Interfaces.SendOffice;
using PE.L1S.Handlers.MillSetup;

namespace PE.SIM.Managers.MillOrganiztion
{
  class AfterCutArea : Area
  {
    public AfterCutArea(int capacity, List<MillAssetConfig> assets, Area precedingArea, ISimulationSendOffice sendOffice) : base(capacity, assets, precedingArea, sendOffice) { }

		public async Task TriggerMillMovemet()
    {
      await MoveMaterials();
    }
  }
}
