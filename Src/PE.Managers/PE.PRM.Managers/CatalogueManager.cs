using PE.Common;
using PE.DbEntity.Models;
using PE.DTO.Internal.Delay;
using PE.DTO.Internal.ProdManager;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Managers;
using PE.PRM.Handlers;
using SMF.Core.DC;
using SMF.Module.Core;
using SMF.Module.Notification;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.PRM.Managers
{
    public class CatalogueManager : ICatalogueManager
    {
        #region members

        private readonly IProdManagerCatalogueSendOffice _sendOffice;

        #endregion
        #region handlers

        private readonly SteelgradeHandler _steelgradeHandler;
        private readonly MaterialCatalogueHandler _materialCatalogueHandler;
        private readonly ProductCatalogueHandler _productCatalogueHandler;
        private readonly WorkOrderHandler _workOrderHandler;
        private readonly HeatHandler _heatHandler;
        private readonly MaterialHandler _materialHandler;

        #endregion
        #region ctor
        public CatalogueManager(IProdManagerCatalogueSendOffice sendOffice)
        {
            _sendOffice = sendOffice;
            _steelgradeHandler = new SteelgradeHandler();
            _materialCatalogueHandler = new MaterialCatalogueHandler();
            _productCatalogueHandler = new ProductCatalogueHandler();
            _workOrderHandler = new WorkOrderHandler();
            _heatHandler = new HeatHandler();
            _materialHandler = new MaterialHandler();
        }
        #endregion
        #region func

        public virtual async Task<DataContractBase> CreateSteelgradeCatalogueAsync(DCSteelgrade dc)
        {
            DataContractBase result = new DataContractBase();

            try
            {
                using (PEContext ctx = new PEContext())
                {
                    PRMSteelgrade steelgrade = await _steelgradeHandler.GetSteelgradeByCodeAsync(ctx, dc.SteelgradeCode);
                    if (steelgrade != null)
                    {
                        NotificationController.RegisterAlarm(Defs.SteelgradeAlreadyExists, "Error while creating new steelgrade. Steelgrade with specified code already exists in DB");
                        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                        throw new ModuleMessageException(ModuleController.ModuleName, Defs.SteelgradeAlreadyExists, "Error while creating new steelgrade.Steelgrade with specified code already exists in DB");
                    }
                    steelgrade = _steelgradeHandler.CreateSteelgrade(dc);
                    ctx.PRMSteelgrades.Add(steelgrade);

                    await ctx.SaveChangesAsync();
                    await ModuleController.HmiRefresh(HMIRefreshKeys.Steelgrade);
                }
            }
            catch (Exception ex)
            {
                NotificationController.RegisterAlarm(Defs.SteelgradeUpdate, "Error while creating new Steelgrade Catalogue");
                NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                throw new ModuleMessageException(ModuleController.ModuleName, Defs.SteelgradeUpdate, "Error while creating new Steelgrade Catalogue");
            }

            return result;
        }

        public virtual async Task<DataContractBase> UpdateSteelgradeCatalogueAsync(DCSteelgrade dc)
        {
            DataContractBase result = new DataContractBase();

            try
            {
                using (PEContext ctx = new PEContext())
                {
                    PRMSteelgrade steelgrade = await _steelgradeHandler.GetSteelgradeByCodeAsync(ctx, dc.SteelgradeCode);
                    _steelgradeHandler.UpdateSteelgrade(steelgrade, dc);

                    await ctx.SaveChangesAsync();
                    await ModuleController.HmiRefresh(HMIRefreshKeys.Steelgrade);
                }
            }
            catch
            {
                NotificationController.RegisterAlarm(Defs.SteelgradeUpdate, "Error while updating Steelgrade Catalogue");
                NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                throw new ModuleMessageException(ModuleController.ModuleName, Defs.SteelgradeUpdate, "Error while updating Steelgrade Catalogue");
            }

            return result;
        }

        public virtual async Task<DataContractBase> DeleteSteelgradeAsync(DCSteelgrade steelgrade)
        {
            DataContractBase result = new DataContractBase();

            try
            {
                using (PEContext ctx = new PEContext())
                {
                    PRMSteelgrade item = await _steelgradeHandler.GetSteelgradeByIdAsync(ctx, steelgrade.Id);
                    ctx.PRMSteelgrades.Remove(item);

                    await ctx.SaveChangesAsync();
                    await ModuleController.HmiRefresh(HMIRefreshKeys.Steelgrade);
                }
            }
            catch (Exception ex)
            {
                NotificationController.RegisterAlarm(Defs.SteelgradeUpdate, "Error while deleting Steelgrade Catalogue");
                NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                NotificationController.Error(ex.Message);
                throw new ModuleMessageException(ModuleController.ModuleName, Defs.SteelgradeUpdate, "Error while deleting Steelgrade Catalogue");
            }

            return result;
        }

        public virtual async Task<DataContractBase> CreateHeatAsync(DCHeat dc)
        {
            DataContractBase result = new DataContractBase();

            try
            {
                using (PEContext ctx = new PEContext())
                {
                    _heatHandler.CreateHeatByUser(ctx, dc);

                    await ctx.SaveChangesAsync();
                    await ModuleController.HmiRefresh(HMIRefreshKeys.Heat);
                }
            }
            catch
            {
                NotificationController.RegisterAlarm(Defs.HeatNotCreated, "Error while updating Heat Catalogue");
                NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                throw new ModuleMessageException(ModuleController.ModuleName, Defs.HeatNotCreated, "Error while updating Heat Catalogue");
            }

            return result;
        }

        public virtual async Task<DataContractBase> CreateMaterialCatalogueAsync(DCMaterialCatalogue dc)
        {
            DataContractBase result = new DataContractBase();

            try
            {
                using (PEContext ctx = new PEContext())
                {
                    PRMMaterialCatalogue catalogue = null;
                    try
                    {
                        catalogue = await _materialCatalogueHandler.GetMaterialCatalogueByNameAsync(ctx, dc.MaterialName);
                    }
                    catch { }

                    if (catalogue != null)
                    {
                        NotificationController.RegisterAlarm(Defs.MaterialCatalogueAlreadyExists, "Error while creating new material catalogue. Material catalogue with specified name already exists in DB");
                        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                        throw new ModuleMessageException(ModuleController.ModuleName, Defs.MaterialCatalogueAlreadyExists, "Error while creating new material catalogue. Material catalogue with specified name already exists in DB");
                    }
                    catalogue = _materialCatalogueHandler.CreateMaterialCatalogue(dc);
                    ctx.PRMMaterialCatalogues.Add(catalogue);

                    await ctx.SaveChangesAsync();

                    await ModuleController.HmiRefresh(HMIRefreshKeys.MaterialCatalogue);
                }
            }
            catch (Exception ex)
            {
                NotificationController.RegisterAlarm(Defs.MaterialCatalogueUpdate, "Error while creating Material Catalogue");
                NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                throw new ModuleMessageException(ModuleController.ModuleName, Defs.MaterialCatalogueUpdate, "Error while creating Material Catalogue");
            }
            return result;
        }

        public virtual async Task<DataContractBase> UpdateMaterialCatalogueAsync(DCMaterialCatalogue dc)
        {
            DataContractBase result = new DataContractBase();

            try
            {
                using (PEContext ctx = new PEContext())
                {
                    PRMMaterialCatalogue catalogue = await _materialCatalogueHandler.GetMaterialCatalogueById(ctx, dc.MaterialCatalogueId);
                    _materialCatalogueHandler.UpdateMaterialCatalogue(catalogue, dc);

                    await ctx.SaveChangesAsync();

                    await ModuleController.HmiRefresh(HMIRefreshKeys.MaterialCatalogue);
                }
            }
            catch
            {
                NotificationController.RegisterAlarm(Defs.MaterialCatalogueUpdate, "Error while updating Material Catalogue");
                NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                throw new ModuleMessageException(ModuleController.ModuleName, Defs.MaterialCatalogueUpdate, "Error while updating Material Catalogue");
            }
            return result;
        }

        public virtual async Task<DataContractBase> DeleteMaterialCatalogueAsync(DCMaterialCatalogue materialCatalogue)
        {
            DataContractBase result = new DataContractBase();

            try
            {
                using (PEContext ctx = new PEContext())
                {
                    PRMMaterialCatalogue catalogue = null;

                    catalogue = await _materialCatalogueHandler.GetMaterialCatalogueById(ctx, materialCatalogue.MaterialCatalogueId);
                    ctx.PRMMaterialCatalogues.Remove(catalogue);
                    await ctx.SaveChangesAsync();

                    await ModuleController.HmiRefresh(HMIRefreshKeys.MaterialCatalogue);
                }
            }
            catch (Exception ex)
            {
                NotificationController.RegisterAlarm(Defs.MaterialCatalogueUpdate, "Error while creating Material Catalogue");
                NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                throw new ModuleMessageException(ModuleController.ModuleName, Defs.MaterialCatalogueUpdate, "Error while creating Material Catalogue");
            }
            return result;
        }

        public virtual async Task<DataContractBase> CreateProductCatalogueAsync(DCProductCatalogue dc)
        {
            DataContractBase result = new DataContractBase();

            try
            {
                using (PEContext ctx = new PEContext())
                {
                    PRMProductCatalogue productCatalogue = null;
                    try
                    {
                        productCatalogue = await _productCatalogueHandler.GetProductCatalogueByNameAsync(ctx, dc.Name);
                    }
                    catch { }
                    if (productCatalogue != null)
                    {
                        NotificationController.RegisterAlarm(Defs.ProductCatalogueAlreadyExists, "Error while creating new product catalogue. Product catalogue with specified name already exists in DB");
                        NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                        throw new ModuleMessageException(ModuleController.ModuleName, Defs.ProductCatalogueAlreadyExists, "Error while creating new product catalogue. Product catalogue with specified name already exists in DB");
                    }

                    productCatalogue = _productCatalogueHandler.CreateProductCatalogue(dc);
                    ctx.PRMProductCatalogues.Add(productCatalogue);
                    await ctx.SaveChangesAsync();

                    await ModuleController.HmiRefresh(HMIRefreshKeys.ProductCatalogue);

                }
            }
            catch
            {
                NotificationController.RegisterAlarm(Defs.ProductCatalogueUpdate, "Error while creating Product Catalogue ");
                NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                throw new ModuleMessageException(ModuleController.ModuleName, Defs.ProductCatalogueUpdate, "Error while creating Product Catalogue ");
            }
            return result;
        }

        public virtual async Task<DataContractBase> UpdateProductCatalogueAsync(DCProductCatalogue dc)
        {
            DataContractBase result = new DataContractBase();

            try
            {
                using (PEContext ctx = new PEContext())
                {
                    PRMProductCatalogue productCatalogue = await _productCatalogueHandler.GetProductCatalogueByIdAsync(ctx, dc.Id);
                    _productCatalogueHandler.UpdateProductCatalogue(productCatalogue, dc);

                    await ctx.SaveChangesAsync();

                    await ModuleController.HmiRefresh(HMIRefreshKeys.ProductCatalogue);

                }
            }
            catch
            {
                NotificationController.RegisterAlarm(Defs.ProductCatalogueUpdate, "Error while updating Product Catalogue ");
                NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                throw new ModuleMessageException(ModuleController.ModuleName, Defs.ProductCatalogueUpdate, "Error while updating Product Catalogue ");
            }
            return result;
        }

        public virtual async Task<DataContractBase> DeleteProductCatalogueAsync(DCProductCatalogue dc)
        {
            DataContractBase result = new DataContractBase();

            try
            {
                using (PEContext ctx = new PEContext())
                {
                    PRMProductCatalogue productCatalogue = await _productCatalogueHandler.GetProductCatalogueByIdAsync(ctx, dc.Id);
                    ctx.PRMProductCatalogues.Remove(productCatalogue);
                    await ctx.SaveChangesAsync();

                    await ModuleController.HmiRefresh(HMIRefreshKeys.ProductCatalogue);

                }
            }
            catch
            {
                NotificationController.RegisterAlarm(Defs.ProductCatalogueUpdate, "Error while deleting Product Catalogue ");
                NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                throw new ModuleMessageException(ModuleController.ModuleName, Defs.ProductCatalogueUpdate, "Error while deleting Product Catalogue ");
            }
            return result;
        }

        public virtual async Task<DataContractBase> DeleteWorkOrderAsync(DCWorkOrder dc)
        {
            DataContractBase result = new DataContractBase();

            if (String.IsNullOrEmpty(dc.WorkOrderName))
            {
                NotificationController.RegisterAlarm(Defs.WorkOrderNotDeleted, "Error while removing Work Order. WorkOrderName is not valid");
                NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                throw new ModuleMessageException(ModuleController.ModuleName, Defs.WorkOrderNotDeleted, "Error while removing Work Order. WorkOrderName is not valid");
            }
            try
            {
                using (PEContext ctx = new PEContext())
                {

                    PRMWorkOrder workOrder = await _workOrderHandler.GetWorkOrderByNameAsync(ctx, dc.WorkOrderName);

                    if (workOrder != null)
                    {
                        ctx.PRMWorkOrders.Remove(workOrder);
                        await ctx.SaveChangesAsync();
                        await ModuleController.HmiRefresh(HMIRefreshKeys.Schedule);
                    }
                }
            }
            catch
            {
                NotificationController.RegisterAlarm(Defs.WorkOrderNotDeleted, "Error while removing Work Order.");
                NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                throw new ModuleMessageException(ModuleController.ModuleName, Defs.WorkOrderNotDeleted, "Error while removing WorkOrder.");
            }

            return result;
        }

        public virtual async Task<DataContractBase> CreateMaterialAsync(DCMaterial dc)
        {
            DataContractBase result = new DataContractBase();

            try
            {
                using (PEContext ctx = new PEContext())
                {
                    long workOrderId = await _workOrderHandler.WorkOrderIdByNameAsync(ctx, dc.FKWorkOrderIdRef);
                    _materialHandler.CreateMaterialByUser(ctx, dc, workOrderId);

                    await ctx.SaveChangesAsync();
                    await ModuleController.HmiRefresh(HMIRefreshKeys.Schedule);
                }
            }
            catch
            {
                NotificationController.RegisterAlarm(Defs.WorkOrderNotCreated, "Error while updating Materials");
                NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                throw new ModuleMessageException(ModuleController.ModuleName, Defs.WorkOrderNotCreated, "Error while updating Materials");
            }

            return result;
        }

        public virtual async Task<DataContractBase> UpdateMaterialAsync(DCMaterial dc)
        {
          DataContractBase result = new DataContractBase();

          try
          {
            using (PEContext ctx = new PEContext())
            {
              long workOrderId = await _workOrderHandler.WorkOrderIdByNameAsync(ctx, dc.FKWorkOrderIdRef);
              int previousMaterialsCount = ctx.PRMMaterials.Where(x => x.PRMWorkOrder.WorkOrderId == workOrderId).Count();
              PRMWorkOrder workOrder = ctx.PRMWorkOrders.Where(x => x.WorkOrderId == workOrderId).FirstOrDefault();
              PRMMaterial material = ctx.PRMMaterials.Where(x => x.PRMWorkOrder.WorkOrderId == workOrderId).OfType<PRMMaterial>().FirstOrDefault();
              material.PRMHeat = ctx.PRMHeats.Where(x => x.FKMaterialCatalogueId == workOrder.FKMaterialCatalogueId).FirstOrDefault();
              long previousHeat = material != null ? material.PRMHeat.HeatId : 0;
          
              if (previousHeat != dc.FKHeatId || previousMaterialsCount != dc.MaterialsNumber )
              {
                _materialHandler.DeleteOldMaterialsAfterWOUpdate(ctx, workOrderId);
                _materialHandler.CreateMaterialByUser(ctx, dc, workOrderId);

                await ctx.SaveChangesAsync();
              }

              await ModuleController.HmiRefresh(HMIRefreshKeys.Schedule);
            }
          }
          catch (Exception ex)
          {
            NotificationController.RegisterAlarm(Defs.WorkOrderNotCreated, "Error while updating Materials");
            NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
            throw new ModuleMessageException(ModuleController.ModuleName, Defs.WorkOrderNotCreated, "Error while updating Materials");
          }

          return result;
        }

        public virtual async Task<DataContractBase> CheckMaterialsNumberAsync(DCMaterial dc)
        {
            DataContractBase result = new DataContractBase();
            long numberOfMaterials = 0;

            try
            {
                using (PEContext ctx = new PEContext())
                {
                    long workOrderId = await _workOrderHandler.WorkOrderIdByNameAsync(ctx, dc.FKWorkOrderIdRef);
                    numberOfMaterials = await _materialHandler.CheckMaterialsNumberAsync(ctx, dc.MaterialId, workOrderId);

                    if (numberOfMaterials < 0)
                    {
                        for (int i = 0; i > numberOfMaterials; i--)
                        {
                            _materialHandler.RemoveMaterialFromWorkOrder(ctx, workOrderId);
                            await ctx.SaveChangesAsync();
                        }
                    }
                    if (numberOfMaterials > 0)
                    {
                        DateTime currentTime = DateTime.Now;
                        for (int i = 0; i < numberOfMaterials; i++)
                        {
                            _materialHandler.AddMaterialToWorkOrderAsync(ctx, dc, workOrderId, i);
                            await ctx.SaveChangesAsync();
                        }
                    }

                    //await ctx.SaveChangesAsync();
                    await ModuleController.HmiRefresh(HMIRefreshKeys.Schedule);
                }
            }
            catch
            {
                NotificationController.RegisterAlarm(Defs.WorkOrderNotCreated, "Error while updating Materials");
                NotificationController.Error($"[CRITICAL] fun {System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
                throw new ModuleMessageException(ModuleController.ModuleName, Defs.WorkOrderNotCreated, "Error while updating Materials");
            }

            return result;
        }

        #endregion
        #region private
        #endregion
    }
}
