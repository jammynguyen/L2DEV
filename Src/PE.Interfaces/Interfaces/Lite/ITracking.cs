﻿using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.MVHistory;
using SMF.Core.Interfaces;

namespace PE.Interfaces.Lite
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface ITracking : IBaseModule
  {
    //[OperationContract]
    //Task<DataContractBase> ProcessTriggerAsync(DCTriggerData dcTriggerData);
  }
}
