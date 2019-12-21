using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.External;
using PE.DTO.External.DBAdapter;
using PE.DTO.External.MVHistory;
using PE.DTO.External.Setup;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.Setup;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.Interfaces.Interfaces.SendOffice
{
  public interface IAdapterL1SendOffice
  {
    Task<SendOfficeResult<DCPEBasId>> SendL1BaseIdRequestAsync(DCL1BasIdRequest dataToSend);
    Task<SendOfficeResult<DataContractBase>> SendL1CutMessageAsync(DCL1CutData dataToSend);
    Task<SendOfficeResult<DataContractBase>> SendL1ScrapMessageAsync(DCL1ScrapData dataToSend);
    Task<SendOfficeResult<DCPEBasId>> SendL1DivisionMessageAsync(DCL1MaterialDivision internalTel);
    Task<SendOfficeResult<DataContractBase>> SendL1MeasDataMessageAsync(DCMeasData internalTel);
    Task<SendOfficeResult<DataContractBase>> SendL1SampleMeasMessageAsync(DCMeasDataSample internalTel);

    Task<SendOfficeResult<DCDefaultExt>> SendSetupDataToL1TCPAsync(DCTCPSetpointTelegramEnvelopeExt internalTel);
    Task<SendOfficeResult<DataContractBase>> SendSetupDataToL1AdapterAsync(DCCommonSetupStructure internalTel);
    Task<SendOfficeResult<DataContractBase>> SendSetupUpdateRequestToL1AdapterAsync(DCCommonSetupStructure internalTel);
    Task<SendOfficeResult<DataContractBase>> SendSetupUpdateAsync(DCCommonSetupStructure internalTel);
    
  }
}
