using PE.DTO.Internal.ProdManager;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Interfaces.Managers
{
  public interface IProductManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DCProductData> ProcessMaterialProductionFinishAsync(DCRawMaterialData materialData);
  }
}
