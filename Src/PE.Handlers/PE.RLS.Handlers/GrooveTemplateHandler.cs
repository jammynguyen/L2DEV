using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.DbEntity.Models;
using PE.DTO.Internal.RollShop;

namespace PE.RLS.Handlers
{
  public sealed class GrooveTemplateHandler
  {
    public RLSGrooveTemplate GetGrooveTemplateFromName(PEContext ctx, string grooveTemplateName)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSGrooveTemplate grooveTemplate = ctx.RLSGrooveTemplates.Where(x => x.GrooveTemplateName == grooveTemplateName).FirstOrDefault();
      return grooveTemplate;
    }

    public RLSGrooveTemplate GetGrooveTemplateFromId(PEContext ctx, long? grooveTemplateId)
    {
      if (ctx == null)
      {
        ctx = new PEContext();
      }

      RLSGrooveTemplate grooveTemplate = ctx.RLSGrooveTemplates.Where(x => x.GrooveTemplateId == grooveTemplateId).FirstOrDefault();
      return grooveTemplate;
    }

    public void CreateGrooveTemplate(PEContext ctx, ref RLSGrooveTemplate grooveTemplate, DCGrooveTemplateData dc)
    {
      grooveTemplate.Angle1 = dc.Angle1;
      grooveTemplate.Angle2 = dc.Angle2;
      grooveTemplate.CreatedTs = DateTime.Now;
      grooveTemplate.LastUpdateTs = DateTime.Now;
      grooveTemplate.D1 = dc.D1;
      grooveTemplate.D2 = dc.D2;
      grooveTemplate.GrooveTemplateDescription = dc.Description;
      grooveTemplate.GrindingProgramName = dc.GrindingProgramName;
      grooveTemplate.GrooveTemplateName = dc.GrooveTemplateName;
      grooveTemplate.GrooveTemplateCode = dc.GrooveTemplateCode;
      grooveTemplate.Plane = dc.GroovePlane;
      grooveTemplate.R1 = dc.R1;
      grooveTemplate.R2 = dc.R2;
      grooveTemplate.R3 = dc.R3;
      //grooveTemplate.FKShapeId = dc.Shape;
      grooveTemplate.Shape = dc.Shape;
      //grooveTemplate.GrooveTemplateCode = "DEF";
      grooveTemplate.SpreadFactor = dc.SpreadFactor;
      grooveTemplate.Status = 1;
      grooveTemplate.W1 = dc.W1;
      grooveTemplate.W2 = dc.W2;
      grooveTemplate.Ds = dc.Ds;
      grooveTemplate.Dw = dc.Dw;
    }

    public void UpdateGrooveTemplate(PEContext ctx, ref RLSGrooveTemplate grooveTemplate, DCGrooveTemplateData dc)
    {
      grooveTemplate.Angle1 = dc.Angle1;
      grooveTemplate.Angle2 = dc.Angle2;
      grooveTemplate.CreatedTs = DateTime.Now;
      grooveTemplate.D1 = dc.D1;
      grooveTemplate.D2 = dc.D2;
      grooveTemplate.GrooveTemplateDescription = dc.Description;
      grooveTemplate.GrindingProgramName = dc.GrindingProgramName;
      grooveTemplate.GrooveTemplateName = dc.GrooveTemplateName;
      grooveTemplate.GrooveTemplateCode = dc.GrooveTemplateCode;
      grooveTemplate.Plane = dc.GroovePlane;
      grooveTemplate.R1 = dc.R1;
      grooveTemplate.R2 = dc.R2;
      grooveTemplate.R3 = dc.R3;
      //grooveTemplate.FKShapeId = dc.ShapeId;
      //grooveTemplate.GrooveTemplateCode = "DEF";
      grooveTemplate.Shape = dc.Shape;
      grooveTemplate.SpreadFactor = dc.SpreadFactor;
      grooveTemplate.Status = 1;
      grooveTemplate.W1 = dc.W1;
      grooveTemplate.W2 = dc.W2;
      grooveTemplate.Ds = dc.Ds;
      grooveTemplate.Dw = dc.Dw;
    }
  }
}
