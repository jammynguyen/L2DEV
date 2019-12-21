using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.Interfaces.Interfaces.SendOffice;
using SMF.Module.Core;

namespace PE.RLS.RollShop.Communication
{
  public class SendOffice : ModuleSendOfficeBase, 
                                                  IRollManagerSendOffice, 
                                                  IRollTypeManagerSendOffice, 
                                                  IGrooveTemplateManagerSendOffice, 
                                                  IRollSetManagerSendOffice, 
                                                  ICassetteTypeManagerSendOffice, 
                                                  ICassetteManagerSendOffice, 
                                                  IRollChangeManagerSendOffice, 
                                                  IRollSetHistoryManagerSendOffice,
                                                  ICustomRollGroovesManagerSendOffice
  {
  }
}
