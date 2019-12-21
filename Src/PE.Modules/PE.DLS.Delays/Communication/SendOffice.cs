using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.MVHistory;
using PE.DTO.Internal.ProdManager;
using PE.DTO.Internal.Schedule;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Lite;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.DLS.Delays.Communication
{
  public class SendOffice : ModuleSendOfficeBase, IDelayManagerCatalogueSendOffice, IDelayManagerSendOffice 
  {

  }

}
