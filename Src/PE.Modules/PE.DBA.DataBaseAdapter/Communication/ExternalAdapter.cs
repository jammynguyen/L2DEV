using PE.DTO.External.DBAdapter;
using PE.DTO.Internal.MVHistory;
using SMF.Core.DC;
using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.DBA.DataBaseAdapter.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, PE.Interfaces.Lite.IDBAdapter
  {
    #region ctor
    public ExternalAdapter() : base(typeof(PE.Interfaces.Lite.IDBAdapter)) { }
    #endregion

  }
}
