﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.RollShop;
using PE.Interfaces.Managers;
using SMF.Core.DC;

namespace PE.Interfaces.Interfaces.Managers
{
  public interface IGrooveTemplateManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertGrooveTemplateAsync(DCGrooveTemplateData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateGrooveTemplateAsync(DCGrooveTemplateData dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteGrooveTemplateAsync(DCGrooveTemplateData dc);
  }
}
