using PE.DbEntity.Models;
using PE.DTO.Internal.Maintenance;
using PE.DTO.Internal.ProdManager;
using PE.DTO.Internal.RollShop;
using PE.Interfaces.Interfaces.Managers;
using PE.Interfaces.Interfaces.SendOffice;
using PE.PRM.Handlers;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using System;
using System.Threading.Tasks;

namespace PE.PRM.Managers
{
  public class ProductManager : IProductManager
  {
    #region members

    private readonly IProdManagerProductSendOffice _sendOffice;

    #endregion
    #region handlers

    private readonly MaterialHandler _materialHandler;
    private readonly ProductHandler _productHandler;

    #endregion
    #region ctor
    public ProductManager(IProdManagerProductSendOffice sendOffice)
    {
      _sendOffice = sendOffice;
      _materialHandler = new MaterialHandler();
      _productHandler = new ProductHandler();
    }

    #endregion

    /// <summary>
    /// After production end trigger hits, module have to create product for produced material
    /// </summary>
    /// <param name="materialData"></param>
    /// <returns></returns>
    public virtual async Task<DCProductData> ProcessMaterialProductionFinishAsync(DCRawMaterialData materialData)
    {
      DCProductData result = null;

      using (PEContext ctx = new PEContext())
      {
        try
        {
          if (materialData.FKMaterialId is null)
          {
            throw new ArgumentException();
          }

          PRMMaterial material = await _materialHandler.GetMaterialByIdAsync(ctx, materialData.FKMaterialId.Value);

          PRMProduct product = _productHandler.CreateProduct(materialData.RawMaterialId, material, materialData.OverallWeight);

          ctx.PRMProducts.Add(product);
          await ctx.SaveChangesAsync();

          result = new DCProductData() { ProductId = product.ProductId, RawMaterialId = materialData.RawMaterialId };


          //Send product weight to RollShop to recalculate accumulated weight on current Grooves
          SendOfficeResult<DataContractBase> rollsAccuResult = await _sendOffice.AccumulateRollsUsageAsync(new DCRollsAccu() { MaterialWeight = materialData.OverallWeight });

          if (rollsAccuResult.OperationSuccess)
            NotificationController.Info("Forwarding production finished to RollShop - success");
          else
            NotificationController.Error("Forwarding production finished to RollShop  - error. Weight of material was not added to currently active grooves");

          //Send product weight to Maintenence to recalculate accumulated weight on equipment
          SendOfficeResult<DataContractBase> equipmentAccuResult = await _sendOffice.AccumulateEquipmentUsageAsync(new DCEquipmentAccu() { MaterialWeight = Convert.ToInt64(materialData.OverallWeight) });

          if (equipmentAccuResult.OperationSuccess)
            NotificationController.Info("Forwarding production finished to Maintenance - success");
          else
            NotificationController.Error("Forwarding production finished to Maintenance  - error. Weight of material was not added to currently active equipment");


          await ModuleController.HmiRefresh(Common.HMIRefreshKeys.Products);

        }
        catch (ArgumentException)
        {
          NotificationController.RegisterAlarm(Defs.ProductCatalogueNotFound, $"Product connected with RawMaterial {materialData.RawMaterialId} not found", materialData.RawMaterialId);
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        }
        catch (Exception)
        {
          NotificationController.RegisterAlarm(Defs.ProductCreationFailed, $"Critical error when creating product for RawMaterial {materialData.RawMaterialId}", materialData.RawMaterialId);
          NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        }
      }


      return result;
    }
  }
}
