using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PE.DbEntity.Models;
using PE.DTO.Internal.RollShop;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using SMF.Module.Core;

public class GrooveTemplateService : BaseService, IGrooveTemplateService
{
  #region interface GrooveTemplatesService
  public DataSourceResult GetGrooveTemplateList(ModelStateDictionary modelState, [DataSourceRequest]DataSourceRequest request)
  {
    DataSourceResult result = null;

    using (PEContext ctx = new PEContext())
    {
      IQueryable<RLSGrooveTemplate> list = ctx.RLSGrooveTemplates.AsQueryable();
      result = list.ToDataSourceResult<RLSGrooveTemplate, VM_GrooveTemplate>(request, modelState, data => new VM_GrooveTemplate(data));
    }

    return result;
  }
  public VM_GrooveTemplate GetGrooveTemplate(ModelStateDictionary modelState, long id)
  {
    VM_GrooveTemplate returnValueVm = null;

    //VALIDATE ENTRY PARAMETERS
    if (id <= 0)
    {
      AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    }
    if (!modelState.IsValid)
    {
      return returnValueVm;
    }
    //END OF VALIDATION


    //DB OPERATION
    using (PEContext ctx = new PEContext())
    {
      RLSGrooveTemplate grooveTemplate = ctx.RLSGrooveTemplates.Where(x => x.GrooveTemplateId == id).First();
      if (grooveTemplate != null)
      {
        returnValueVm = new VM_GrooveTemplate(grooveTemplate);
      }
    }

      //END OF DB OPERATION

      return returnValueVm;
  }
  public async Task<VM_Base> InsertGrooveTemplate(ModelStateDictionary modelState, VM_GrooveTemplate viewModel)
  {
    VM_Base result = new VM_Base();

    if (!modelState.IsValid)
      return result;

    UnitConverterHelper.ConvertToSi(ref viewModel);

    DCGrooveTemplateData entryDataContract = new DCGrooveTemplateData();
    entryDataContract.Angle1 = viewModel.Angle1;
    entryDataContract.Angle2 = viewModel.Angle2;
    entryDataContract.D1 = viewModel.D1;
    entryDataContract.D2 = viewModel.D2;
    entryDataContract.Description = viewModel.Description;
    entryDataContract.GrindingProgramName = viewModel.GrindingProgramName;
    entryDataContract.GroovePlane = viewModel.GroovePlane;
    entryDataContract.GrooveTemplateName = viewModel.GrooveTemplateName;
    entryDataContract.GrooveTemplateCode = viewModel.GrooveTemplateCode;
    entryDataContract.NameShort = viewModel.NameShort;
    entryDataContract.R1 = viewModel.R1;
    entryDataContract.R2 = viewModel.R2;
    entryDataContract.R3 = viewModel.R3;
    entryDataContract.Shape = viewModel.Shape;
    entryDataContract.SpreadFactor = viewModel.SpreadFactor;
    entryDataContract.Status = viewModel.Status;
    entryDataContract.W1 = viewModel.W1;
    entryDataContract.W2 = viewModel.W2;
    entryDataContract.Ds = viewModel.Ds;
    entryDataContract.Dw = viewModel.Dw;

    SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.InsertGrooveTemplateAsync(entryDataContract);

    //handle warning information
    HandleWarnings(sendOfficeResult, ref modelState);

    //return view model
    return result;
  }
  public async Task<VM_Base> UpdateGrooveTemplate(ModelStateDictionary modelState, VM_GrooveTemplate viewModel)
  {
    VM_Base result = new VM_Base();

    if (!modelState.IsValid)
      return result;

    UnitConverterHelper.ConvertToSi(ref viewModel);

    DCGrooveTemplateData entryDataContract = new DCGrooveTemplateData();
    entryDataContract.Angle1 = viewModel.Angle1;
    entryDataContract.Angle2 = viewModel.Angle2;
    entryDataContract.D1 = viewModel.D1;
    entryDataContract.D2 = viewModel.D2;
    entryDataContract.Description = viewModel.Description;
    entryDataContract.GrindingProgramName = viewModel.GrindingProgramName;
    entryDataContract.GroovePlane = viewModel.GroovePlane;
    entryDataContract.GrooveTemplateName = viewModel.GrooveTemplateName;
    entryDataContract.GrooveTemplateCode = viewModel.GrooveTemplateCode;
    entryDataContract.NameShort = viewModel.NameShort;
    entryDataContract.R1 = viewModel.R1;
    entryDataContract.R2 = viewModel.R2;
    entryDataContract.R3 = viewModel.R3;
    entryDataContract.Shape = viewModel.Shape;
    entryDataContract.SpreadFactor = viewModel.SpreadFactor;
    entryDataContract.Status = viewModel.Status;
    entryDataContract.W1 = viewModel.W1;
    entryDataContract.W2 = viewModel.W2;
    entryDataContract.Ds = viewModel.Ds;
    entryDataContract.Dw = viewModel.Dw;
    entryDataContract.GrooveTemplateId = viewModel.Id;

    SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateGrooveTemplateAsync(entryDataContract);

    //handle warning information
    HandleWarnings(sendOfficeResult, ref modelState);

    //return view model
    return result;
  }
  public async Task<VM_Base> DeleteGrooveTemplate(ModelStateDictionary modelState, VM_LongId viewModel)
  {
    VM_Base result = new VM_Base();

    if (!modelState.IsValid)
      return result;

    UnitConverterHelper.ConvertToSi(ref viewModel);

    DCGrooveTemplateData entryDataContract = new DCGrooveTemplateData();
    entryDataContract.GrooveTemplateId = viewModel.Id;

    //DB OPERATION
    SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteGrooveTemplateAsync(entryDataContract);

    //handle warning information
    HandleWarnings(sendOfficeResult, ref modelState);

    //return view model
    return result;
  }
  #endregion

}
