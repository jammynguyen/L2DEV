using PE.DbEntity.Models;
using PE.DTO.Internal.ProdManager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PE.PRM.Handlers
{
    public sealed class MaterialHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="workOrderId"></param>
        /// <param name="heatId"></param>
        /// <param name="rawMaterialNumber"></param>
        public List<PRMMaterial> CreateMaterials(PEContext ctx, PRMMaterialCatalogue materialCatalogue, short rawMaterialNumber, PRMHeat heat, double weight)
        {
            if (ctx == null) { ctx = new PEContext(); }

            List<PRMMaterial> materialsList = new List<PRMMaterial>();
            for (int i = 0; i < rawMaterialNumber; i++)
            {
                materialsList.Add(new PRMMaterial()
                {
                    MaterialName = "Lite_" + DateTime.Now.TimeOfDay + "_" + i,
                    PRMHeat = heat,
                    Weight = weight,
                    //PRMMaterialCatalogue = materialCatalogue,
                    CreatedTs = DateTime.Now,
                    LastUpdateTs = DateTime.Now,
                });
            }

            //ctx.PRMMaterials.AddRange(materialsList);
            //ctx.SaveChanges();
            return materialsList;
        }

        public List<PRMMaterial> CreateTestMaterials(PEContext ctx, PRMMaterialCatalogue materialCatalogue, short rawMaterialNumber, PRMHeat heat, double weight)
        {
            if (ctx == null) { ctx = new PEContext(); }

            List<PRMMaterial> materialsList = new List<PRMMaterial>();
            for (int i = 0; i < rawMaterialNumber; i++)
            {
                materialsList.Add(new PRMMaterial()
                {
                    MaterialName = "TestMaterial_" + DateTime.Now.TimeOfDay + "_" + i,
                    PRMHeat = heat,
                    Weight = weight,
                    //PRMMaterialCatalogue = materialCatalogue,
                    CreatedTs = DateTime.Now,
                    LastUpdateTs = DateTime.Now,
                });
            }

            return materialsList;
        }



        public void DeleteOldMaterialsAfterWOUpdate(PEContext ctx, long workOrderId)
        {
            if (ctx == null) { ctx = new PEContext(); }

            IQueryable<PRMMaterial> workOrderToDelete = ctx.PRMMaterials.Where(x => x.FKWorkOrderId == workOrderId);

            ctx.PRMMaterials.RemoveRange(workOrderToDelete);
        }

        /// <summary>
        /// Create test material to add it to test work order - called from HMI.
        /// </summary>
        /// <param name="heat"></param>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        public PRMMaterial CreateTestMaterial(PRMHeat heat, PRMWorkOrder workOrder)
        {
            if (heat is null || workOrder is null)
            {
                throw new Exception() { Source = "CreateTestMaterial" };
            }

            return new PRMMaterial()
            {
                CreatedTs = DateTime.Now,
                LastUpdateTs = DateTime.Now,
                IsDummy = true,
                MaterialName = "TestMaterial" + DateTime.Now,
                PRMHeat = heat,
                PRMWorkOrder = workOrder,
            };
        }

        public PRMMaterial GetLastMaterialByWorkOrderIdAsync(PEContext ctx, long workOrderId)
        {
            if (ctx == null)
            {
                ctx = new PEContext();
            }

            PRMMaterial material = ctx.PRMMaterials.Where(w => w.FKWorkOrderId == workOrderId).OrderBy(o => o.MaterialId).FirstOrDefault();
            return material;
        }

        /// <summary>
        /// Get RawMaterial by internal Id
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public async Task<PRMMaterial> GetMaterialByIdAsync(PEContext ctx, long materialId)
        {
            if (ctx == null)
            {
                ctx = new PEContext();
            }

            return await ctx.PRMMaterials.Where(w => w.MaterialId == materialId).Include(i => i.PRMWorkOrder).SingleOrDefaultAsync();
        }

        public void CreateMaterialByUser(PEContext ctx, DCMaterial dc, long workOrderId)
        {
            if (ctx == null) { ctx = new PEContext(); }
            DateTime currentTime = DateTime.Now;
            //double matWeight = Convert.ToDouble(dc.Weight / dc.MaterialsNumber);
            for (int i = 1; i <= dc.MaterialsNumber; i++)
            {
                PRMMaterial material = new PRMMaterial
                {
                    CreatedTs = currentTime,
                    LastUpdateTs = currentTime,
                    IsDummy = false,
                    MaterialName =  dc.FKWorkOrderIdRef + "_"+ i,
                    FKWorkOrderId = workOrderId,
                    Weight = dc.Weight,
                    IsAssigned = true,
                    FKHeatId = Convert.ToInt64(dc.FKHeatId)
                };

                ctx.PRMMaterials.Add(material);
            }
        }

        public async Task<long> CheckMaterialsNumberAsync(PEContext ctx, long materials, long workOrderId)
        {
            if (ctx == null) { ctx = new PEContext(); }

            materials -= await ctx.PRMMaterials.Where(x => x.FKWorkOrderId == workOrderId).CountAsync();

            return materials;
        }

        public void RemoveMaterialFromWorkOrder(PEContext ctx, long workOrderId)
        {
            if (ctx == null) { ctx = new PEContext(); }

            PRMMaterial material = GetLastMaterialByWorkOrderIdAsync(ctx, workOrderId);
            ctx.PRMMaterials.Remove(material);
        }

        public void AddMaterialToWorkOrderAsync(PEContext ctx, DCMaterial dc, long workOrderId, int i)
        {
            if (ctx == null) { ctx = new PEContext(); }
            DateTime currentTime = DateTime.Now;
            TimeSpan testName = DateTime.Now.TimeOfDay;
            double matWeight = Convert.ToDouble(dc.Weight / dc.MaterialsNumber);
            PRMMaterial material = new PRMMaterial
            {
                CreatedTs = currentTime,
                LastUpdateTs = currentTime,
                IsDummy = false,
                MaterialName = "TestMat_" + testName + "_" + i,
                FKWorkOrderId = workOrderId,
                Weight = matWeight,
                IsAssigned = false,
                FKHeatId = Convert.ToInt64(dc.FKHeatId)
            };

            ctx.PRMMaterials.Add(material);
        }
    }
}
