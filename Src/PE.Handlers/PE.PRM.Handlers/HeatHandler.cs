using PE.DbEntity.Models;
using PE.DTO.Internal.ProdManager;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PE.PRM.Handlers
{
    public sealed class HeatHandler
    {
        /// <summary>
        /// Return Heat found by name or default (null) if any don't match
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="heatName"></param>
        /// <returns></returns>
        public async Task<PRMHeat> GetHeatByNameAsync(PEContext ctx, string heatName)
        {
            if (ctx == null)
            {
                ctx = new PEContext();
            }

            return await ctx.PRMHeats.Where(x => x.HeatName.ToLower().Equals(heatName.ToLower())).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Return new heat - need to be addet to ctx and saved
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="heatName"></param>
        /// <param name="materialCatalogueId"></param>
        /// <returns></returns>
        public async Task<PRMHeat> CreateNewHeatAsync(PEContext ctx, string heatName, PRMMaterialCatalogue materialCatalogue)
        {
            if (ctx == null)
            {
                ctx = new PEContext();
            }

            PRMHeat heat = new PRMHeat()
            {
                HeatName = heatName,
                CreatedTs = DateTime.Now,
                LastUpdateTs = DateTime.Now,
                PRMHeatsEXT = new PRMHeatsEXT()
                {
                    CreatedTs = DateTime.Now,
                },
                PRMHeatSupplier = await ctx.PRMHeatSuppliers.Where(x => x.IsDefault == true).SingleOrDefaultAsync(),
                PRMMaterialCatalogue = materialCatalogue
            };

            return heat;
        }

        /// <summary>
        /// Function create test heat - called from HMI, 
        /// many fileds are being copied form existing schedule - so param schedule have to have included heat.
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public PRMHeat CreateTestHeat(PRMHeat heat)
        {
            if (heat is null)
            {
                throw new Exception() { Source = "CreateTestHeat" };
            }

            return new PRMHeat()
            {
                CreatedTs = DateTime.Now,
                LastUpdateTs = DateTime.Now,
                HeatName = "TestHeat" + DateTime.Now.ToString(),
                FKMaterialCatalogueId = heat.FKMaterialCatalogueId,
                FKHeatSupplierId = heat.FKHeatSupplierId,
            };
        }

        public void CreateHeatByUser(PEContext ctx, DCHeat dc)
        {
            if (ctx == null) { ctx = new PEContext(); }

            PRMHeat heat = new PRMHeat()
            {
                CreatedTs = DateTime.Now,
                LastUpdateTs = DateTime.Now,
                HeatName = dc.HeatName,
                FKMaterialCatalogueId = dc.FKMaterialCatalogueId,
                FKHeatSupplierId = dc.FKHeatSupplierId,
                HeatWeightRef = dc.HeatWeightRef,
                IsDummy = dc.IsDummy
            };

            ctx.PRMHeats.Add(heat);
        }
    }
}
