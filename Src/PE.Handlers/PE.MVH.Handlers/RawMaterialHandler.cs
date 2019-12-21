using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.DTO.Internal.MVHistory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PE.MVH.Handlers
{
  public sealed class RawMaterialHandler
  {
    public async Task<MVHRawMaterial> FindRawMaterialByExternalIdAsync(PEContext ctx, long externalId, bool includeSteps = false)
    {
      return includeSteps ?
             await ctx.MVHRawMaterials.Where(w => w.ExternalRawMaterialId == externalId).OrderByDescending(x => x.CreatedTs).Include(i => i.MVHRawMaterialsSteps).FirstOrDefaultAsync() :
             await ctx.MVHRawMaterials.Where(w => w.ExternalRawMaterialId == externalId).OrderByDescending(x => x.CreatedTs).FirstOrDefaultAsync();
    }
    public async Task<MVHRawMaterial> FindRawMaterialByIdAsync(PEContext ctx, long id, bool includeSteps = false)
    {
      return includeSteps ?
        await ctx.MVHRawMaterials.Where(w => w.RawMaterialId == id).OrderByDescending(x => x.CreatedTs).Include(i => i.MVHRawMaterialsSteps).FirstOrDefaultAsync() :
        await ctx.MVHRawMaterials.Where(w => w.RawMaterialId == id).OrderByDescending(x => x.CreatedTs).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Creation of RawMaterial with default values 
    /// ExternalRawMaterialId is generated ( rand  - but not extisting in db ) 
    /// Func automaticly add Overall step to steps collection
    /// </summary>
    /// <param name="ctx">PEContext</param>
    /// <param name="rawMaterialName">optional</param>
    /// <returns></returns>
    public async Task<MVHRawMaterial> CreateRawMaterial(PEContext ctx, bool isDummy = false, string rawMaterialName = "")
    {
      if (ctx == null) { ctx = new PEContext(); }

      MVHRawMaterial rawMaterial = new MVHRawMaterial()
      {
        CreatedTs = DateTime.Now,
        LastUpdateTs = DateTime.Now,
        IsDummy = isDummy,
        IsVirtual = false,
        Status = RawMaterialStatus.ENUM_Unassigned,
        LastProcessingStepNo = RawMaterialStepNo.FirstStep,
        CuttingSeqNo = 0,
        ChildsNo = 0,
        ParentRawMaterialId = null,
        FKMaterialId = null,
        FKProductId = null,
        ExternalRawMaterialId = await GenerateNotExistingExternalRawMaterialId(ctx),
      };

      if (rawMaterialName.Length > 0)
      {
        rawMaterial.RawMaterialName = rawMaterialName;
      }
      else
      {
        rawMaterial.RawMaterialName = "DefaultNamedRM000" + ctx.MVHRawMaterials.Count();
      }

      //add overall step
      rawMaterial.MVHRawMaterialsSteps.Add(new MVHRawMaterialsStep()
      {
        CreatedTs = DateTime.Now,
        ProcessingStepNo = RawMaterialStepNo.OverallStep,
        LastUpdateTs = DateTime.Now,
        IsReversed = false,
        Weight = 0,
        Width = 0,
        Thickness = 0,
        Length = 0,
        FKAssetId = null,
      });

      //add init step
      rawMaterial.MVHRawMaterialsSteps.Add(new MVHRawMaterialsStep()
      {
        CreatedTs = DateTime.Now,
        ProcessingStepNo = RawMaterialStepNo.FirstStep,
        LastUpdateTs = DateTime.Now,
        IsReversed = false,
        Weight = 0,
        Width = 0,
        Thickness = 0,
        Length = 0,
        FKAssetId = null,
        Elongation = 1,
      });

      return rawMaterial;
    }


    public async Task<long> GenerateNotExistingExternalRawMaterialId(PEContext ctx)
    {
      if (ctx == null) { ctx = new PEContext(); }

      long maxBasId = await ctx.MVHRawMaterials.MaxAsync(x => x.ExternalRawMaterialId);

      maxBasId = ++maxBasId % uint.MaxValue;
      if (maxBasId <= 0)
        maxBasId = 1;

      return maxBasId;
    }

    public void AddRawMaterial(PEContext ctx, MVHRawMaterial rawMaterial)
    {
      if (ctx == null) { ctx = new PEContext(); }

      ctx.MVHRawMaterials.Add(rawMaterial);
    }








    /// <summary>
    /// From given rawMaterial data create RawMaterialStep
    /// </summary>
    /// <param name="rawMaterial"></param>
    /// <returns></returns>
    public MVHRawMaterialsStep CreateRawMaterialStep(MVHRawMaterial rawMaterial)
    {
      if (rawMaterial.MVHRawMaterialsSteps.Count == 0)
      {
        throw new ArgumentNullException() { Source = System.Reflection.MethodBase.GetCurrentMethod().ToString() };
      }

      MVHRawMaterialsStep overall = rawMaterial.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == RawMaterialStepNo.OverallStep).Single();

      MVHRawMaterialsStep newStep = new MVHRawMaterialsStep()
      {
        CreatedTs = DateTime.Now,
        LastUpdateTs = DateTime.Now,
        ProcessingStepNo = ++rawMaterial.LastProcessingStepNo,
        FKRawMaterialId = rawMaterial.RawMaterialId,
        IsReversed = overall.IsReversed,
        Weight = overall.Weight,
        Width = overall.Width,
        Thickness = overall.Thickness,
        Length = overall.Length,
      };

      return newStep;
    }

    /// <summary>
    /// Create Head Cut Step
    /// </summary>
    /// <param name="rawMaterial">material with steps included</param>
    /// <param name="message"></param>
    /// <returns></returns>
    public MVHRawMaterialsStep CreateHeadCutStep(MVHRawMaterial rawMaterial, DCL1CutData message)
    {

      if (rawMaterial.MVHRawMaterialsSteps.Count == 0)
      {
        throw new ArgumentNullException() { Source = System.Reflection.MethodBase.GetCurrentMethod().ToString() };
      }

      MVHRawMaterialsStep overall = rawMaterial.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == RawMaterialStepNo.OverallStep).Single();

      MVHRawMaterialsStep step = CreateRawMaterialStep(rawMaterial);
      {
        step.FKAssetId = message.LocationId;
        step.Length = overall.Length - message.CutLength;
        step.HeadPartLength = message.CutLength;
        step.TailPartLength = overall.Length - message.CutLength;
        step.EnumTypeOfCut = TypeOfCut.HeadCut;
        step.HeadPartCumm = overall.HeadPartCumm.GetValueOrDefault() + message.CutLength;
        step.TailPartCumm = overall.TailPartCumm;
        //TODO change RawMaterialStepNo.FirstStep when possible
        step.Elongation = (step.TailPartCumm.GetValueOrDefault() + step.HeadPartCumm.GetValueOrDefault() + step.Length.GetValueOrDefault())
                            / rawMaterial.MVHRawMaterialsSteps.Where(W => W.ProcessingStepNo == RawMaterialStepNo.FirstStep).Single().Length;
        step.Weight = overall.Weight - (message.CutLength / (step.TailPartLength.GetValueOrDefault() + step.HeadPartLength.GetValueOrDefault()) * overall.Weight);
      }

      return step;
    }

    /// <summary>
    /// Create Tail Cut Step
    /// </summary>
    /// <param name="rawMaterial">material with steps included</param>
    /// <param name="message"></param>
    /// <returns></returns>
    public MVHRawMaterialsStep CreateTailCutStep(MVHRawMaterial rawMaterial, DCL1CutData message)
    {

      if (rawMaterial.MVHRawMaterialsSteps.Count == 0)
      {
        throw new ArgumentNullException() { Source = System.Reflection.MethodBase.GetCurrentMethod().ToString() };
      }

      MVHRawMaterialsStep overall = rawMaterial.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == 0).Single();

      MVHRawMaterialsStep step = CreateRawMaterialStep(rawMaterial);
      {
        step.FKAssetId = message.LocationId;
        step.Length = overall.Length - message.CutLength;
        step.HeadPartLength = overall.Length - message.CutLength;
        step.TailPartLength = message.CutLength;
        step.EnumTypeOfCut = TypeOfCut.TailCut;
        step.HeadPartCumm = overall.HeadPartCumm;
        step.TailPartCumm = overall.TailPartCumm.GetValueOrDefault() + message.CutLength;
        step.Elongation = (step.TailPartCumm.GetValueOrDefault() + step.HeadPartCumm.GetValueOrDefault() + step.Length.GetValueOrDefault())
                            / rawMaterial.MVHRawMaterialsSteps.Where(W => W.ProcessingStepNo == RawMaterialStepNo.FirstStep).Single().Length;
        step.Weight = overall.Weight - (message.CutLength / (step.TailPartLength.GetValueOrDefault() + step.HeadPartLength.GetValueOrDefault()) * overall.Weight);

      }


      return step;
    }

    /// <summary>
    /// Create Sample Cut Step
    /// </summary>
    /// <param name="rawMaterial">material with steps included</param>
    /// <param name="message"></param>
    /// <returns></returns>
    public MVHRawMaterialsStep CreateSampleCutStep(MVHRawMaterial rawMaterial, DCL1CutData message)
    {

      if (rawMaterial.MVHRawMaterialsSteps.Count == 0)
      {
        throw new ArgumentNullException() { Source = System.Reflection.MethodBase.GetCurrentMethod().ToString() };
      }

      MVHRawMaterialsStep overall = rawMaterial.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == 0).Single();

      MVHRawMaterialsStep step = CreateRawMaterialStep(rawMaterial);
      {
        step.FKAssetId = message.LocationId;
        step.Length = overall.Length - message.CutLength;
        step.HeadPartLength = message.CutLength;
        step.TailPartLength = overall.Length - message.CutLength;
        step.EnumTypeOfCut = TypeOfCut.SampleCut;
        step.HeadPartCumm = overall.HeadPartCumm.GetValueOrDefault() + message.CutLength;
        step.TailPartCumm = overall.TailPartCumm;
        step.Elongation = (step.TailPartCumm.GetValueOrDefault() + step.HeadPartCumm.GetValueOrDefault() + step.Length.GetValueOrDefault())
                            / rawMaterial.MVHRawMaterialsSteps.Where(W => W.ProcessingStepNo == RawMaterialStepNo.FirstStep).Single().Length;
        step.Weight = overall.Weight - (message.CutLength / (step.TailPartLength.GetValueOrDefault() + step.HeadPartLength.GetValueOrDefault()) * overall.Weight);

      }

      return step;
    }


    /// <summary>
    /// Update overall step of material
    /// </summary>
    /// <param name="uow">ref to unit of work</param> 
    /// <param name="rawMatIdx">raw material of which overall step should be updated</param>
    public void UpdateOverallStep(MVHRawMaterial rawMat)
    {

      if (rawMat == null || rawMat.MVHRawMaterialsSteps.Count == 0)
      {
        throw new ArgumentNullException() { Source = System.Reflection.MethodBase.GetCurrentMethod().ToString() }; ;
      }

      MVHRawMaterialsStep overall = rawMat.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == RawMaterialStepNo.OverallStep).Single();
      MVHRawMaterialsStep last_step = rawMat.MVHRawMaterialsSteps.Last();

      overall.LastUpdateTs = last_step.LastUpdateTs;
      rawMat.LastProcessingStepNo = last_step.ProcessingStepNo;

      PropertyInfo[] propertyInfos = typeof(MVHRawMaterialsStep).GetProperties();

      foreach (PropertyInfo prop in propertyInfos)
      {
        if (prop.Name == "RawMaterialStepId" || prop.Name == "ProcessingStepNo" || prop.Name == "FKRawMaterialId" || prop.Name == "CreatedTs")
        {
          continue;
        }

        object overall_value = prop.GetValue(overall);
        object last_value = prop.GetValue(last_step);

        if (last_value == null)
        {
          continue;
        }

        if (overall_value == null)
        {
          prop.SetValue(overall, prop.GetValue(last_step), null);
          continue;
        }
        if (last_value.ToString() != overall_value.ToString())
        {
          prop.SetValue(overall, prop.GetValue(last_step), null);
        }
      }
    }



    /// <summary>
    /// Copy properties of given RM step and return as new
    /// </summary>
    /// <param name="rawMaterialStep"></param>
    /// <returns></returns>
    public MVHRawMaterialsStep CloneRawMaterialStep(MVHRawMaterialsStep rawMaterialStep)
    {
      MVHRawMaterialsStep rawMaterialsStep = new MVHRawMaterialsStep();
      foreach (PropertyInfo property in typeof(MVHRawMaterialsStep).GetProperties())
      {
        if (!(property.Name == "RawMaterialStepId") || !(property.Name == "LastUpdateTs") || !(property.Name == "FkRawMaterialId") || !(property.Name == "CreatedTs") || !(property.Name == "MVHRawMaterial"))
        {
          object obj = property.GetValue((object)rawMaterialStep);
          property.SetValue((object)rawMaterialsStep, obj, (object[])null);
        }

        rawMaterialsStep.MVHRawMaterialsStepsEXT = new MVHRawMaterialsStepsEXT()
        {
          CreatedTs = DateTime.Now,
        };
      }
      return rawMaterialsStep;
    }

    public void UpadeRawMaterialStep(MVHRawMaterialsStep newRawMaterialStep, MVHRawMaterialsStep step)
    {
      foreach (PropertyInfo property in typeof(MVHRawMaterialsStep).GetProperties())
      {
        if (!(property.Name == "RawMaterialStepId") && !(property.Name == "LastUpdateTs") && !(property.Name == "FKRawMaterialId") && !(property.Name == "CreatedTs") && !(property.Name == "MVHRawMaterial"))
        {
          object obj = property.GetValue((object)step);
          property.SetValue((object)newRawMaterialStep, obj, (object[])null);
        }
      }
      newRawMaterialStep.LastUpdateTs = DateTime.Now;
    }

    /// <summary>
    /// Create Division step for cutting message
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="motherRawMaterial"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public MVHRawMaterialsStep MakeDivisionStep(PEContext ctx, MVHRawMaterial motherRawMaterial, DCL1MaterialDivision message, out double? tempWeight)
    {

      MVHRawMaterialsStep overallStep = GetOverallStep(ctx, motherRawMaterial);
      MVHRawMaterialsStep rawMaterialsStep = new MVHRawMaterialsStep()
      {
        FKRawMaterialId = motherRawMaterial.RawMaterialId,
        ProcessingStepNo = ++motherRawMaterial.LastProcessingStepNo,
        CreatedTs = message.TimeStamp,
        LastUpdateTs = message.TimeStamp,
        FKAssetId = ctx.MVHAssets.Where(w => w.AssetId == message.LocationId).Select(s => s.AssetId).Single(),
        IsReversed = overallStep.IsReversed,
        Width = overallStep.Width,
        Thickness = overallStep.Thickness,
        Length = overallStep.Length - message.NewMaterialLength,
        HeadPartLength = message.NewMaterialLength,
        TailPartLength = overallStep.Length - message.NewMaterialLength,
        Elongation = overallStep.Elongation,
        MotherOffset = overallStep.MotherOffset,
        RelLength = overallStep.RelLength,
        PassNo = (short)message.CutNumberInParent,
        EnumTypeOfCut = TypeOfCut.DivideCutN,
      };

      tempWeight = (message.NewMaterialLength / (rawMaterialsStep.TailPartLength.GetValueOrDefault() + rawMaterialsStep.HeadPartLength.GetValueOrDefault()) * overallStep.Weight.GetValueOrDefault());
      rawMaterialsStep.Weight = overallStep.Weight - tempWeight;

      if (!overallStep.IsReversed)
      {
        rawMaterialsStep.HeadPartCumm = overallStep.HeadPartCumm + message.NewMaterialLength;
        rawMaterialsStep.TailPartCumm = overallStep.TailPartCumm;
      }
      else
      {
        rawMaterialsStep.TailPartCumm = overallStep.TailPartCumm + message.NewMaterialLength;
        rawMaterialsStep.HeadPartCumm = overallStep.HeadPartCumm;
      }

      return rawMaterialsStep;
    }

    /// <summary>
    /// Overall Step Getter
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="material"></param>
    /// <returns></returns>
    public MVHRawMaterialsStep GetOverallStep(PEContext ctx, MVHRawMaterial material)
    {
      MVHRawMaterialsStep rawMaterialsStep;
      if (material.MVHRawMaterialsSteps.Any<MVHRawMaterialsStep>())
      {
        rawMaterialsStep = material.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == RawMaterialStepNo.OverallStep).Single();
      }
      else
      {
        rawMaterialsStep = ctx.MVHRawMaterialsSteps.Where(w => w.FKRawMaterialId == material.RawMaterialId && (int)w.ProcessingStepNo == 0).Single();
      }

      return rawMaterialsStep;
    }

    public MVHRawMaterialsStep GetFirstStepStep(PEContext ctx, MVHRawMaterial material)
    {
      MVHRawMaterialsStep rawMaterialsStep;
      if (material.MVHRawMaterialsSteps.Any<MVHRawMaterialsStep>())
      {
        rawMaterialsStep = material.MVHRawMaterialsSteps.Where(w => w.ProcessingStepNo == RawMaterialStepNo.FirstStep).Single();
      }
      else
      {
        rawMaterialsStep = ctx.MVHRawMaterialsSteps.Where(w => w.FKRawMaterialId == material.RawMaterialId && (int)w.ProcessingStepNo == 1).Single();
      }

      return rawMaterialsStep;
    }

    public MVHAsset GetFMVHAssetByStep(PEContext ctx, MVHRawMaterialsStep step)
    {
      MVHAsset mvhAsset = null;
      if (step.FKAssetId != null)
      {
        mvhAsset = ctx.MVHAssets.Where(w => w.AssetId == step.FKAssetId).Single();
      }
      return mvhAsset;
    }

    /// <summary>
    /// Create Step form step creating measurement
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="rawMaterial"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public MVHRawMaterialsStep CreateRawMaterialStep(PEContext ctx, MVHRawMaterial rawMaterial, DCMeasData message, List<long> lengthChagneFeatures, List<long> weightChangeFeatures)
    {
      MVHRawMaterialsStep newStep = CreateRawMaterialStep(rawMaterial);

      newStep.PassNo = (short)message.PassNumber;
      newStep.FKAssetId = ctx.MVHFeatures.Where(w => w.FeatureCode == message.EventCode).Select(s => s.FKAssetId).Single();
      newStep.IsReversed = message.IsReversed;
      newStep.IsLastPass = message.IsLastPass;
      newStep.FKFeatureIdRef = ctx.MVHFeatures.Where(w => w.FeatureCode == message.EventCode).Select(s => s.FeatureId).Single();

      long featureId = ctx.MVHFeatures.Where(z => z.FeatureCode == message.EventCode).Select(s => s.FeatureId).Single();
      if (featureId > 0)
      {
        if (lengthChagneFeatures.Contains(featureId))
        {
          newStep.Length = Math.Round(Convert.ToDouble(message.Avg), 2);
          MVHRawMaterialsStep mVHRawMaterialsStep = GetFirstStepStep(ctx, rawMaterial);
          if (mVHRawMaterialsStep != null)
          {
            if (ctx.MVHFeaturesEXTs.Where(z => z.IsLengthChange == true && z.FKFeatureId == featureId).Any() && (mVHRawMaterialsStep.Length == 0 || mVHRawMaterialsStep.Length == null))
            {
              mVHRawMaterialsStep.Length = newStep.Length;
            }
          }
          if ((newStep.TailPartCumm.GetValueOrDefault() + newStep.HeadPartCumm.GetValueOrDefault() + mVHRawMaterialsStep.Length.GetValueOrDefault() == Math.Round(Convert.ToDouble(message.Avg), 2)))
            newStep.Elongation = 1;
          else
            newStep.Elongation = (newStep.TailPartCumm.GetValueOrDefault() + newStep.HeadPartCumm.GetValueOrDefault() + newStep.Length.GetValueOrDefault())
                              / rawMaterial.MVHRawMaterialsSteps.Where(W => W.ProcessingStepNo == RawMaterialStepNo.FirstStep).Single().Length.Value;
        }

        if (weightChangeFeatures.Contains(featureId))
        {
          newStep.Weight = message.Avg;
        }
      }

      return newStep;
    }


  }

}
