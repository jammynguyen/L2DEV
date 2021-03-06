﻿using PE.DTO.External.MVHistory;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Managers
{
  public interface IL1CommunicationMeasurementsManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessMeasurementMessage<T>(T message);
  }
}
