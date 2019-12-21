using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.DTO.Internal.RollShop
{
  public class DCGrooveTemplateData : DataContractBase
  {
    [DataMember]
    public long? GrooveTemplateId
    {
      get;
      set;
    }
    [DataMember]
    public DateTime? CreatedTs
    {
      get;
      set;
    }
    [DataMember]
    public DateTime? LastUpdatedTs
    {
      get;
      set;
    }
    [DataMember]
    public String Shape
    {
      get;
      set;
    }

    [DataMember]
    public long? ShapeId
    {
      get;
      set;
    }
    [DataMember]
    public String GrooveTemplateName
    {
      get;
      set;
    }
    [DataMember]
    public String GrooveTemplateCode
    {
      get;
      set;
    }
    [DataMember]
    public short Status
    {
      get;
      set;
    }
    [DataMember]
    public String GrStatus
    {
      get;
      set;
    }
    [DataMember]
    public double? R1
    {
      get;
      set;
    }
    [DataMember]
    public double? R2
    {
      get;
      set;
    }
    [DataMember]
    public double? R3
    {
      get;
      set;
    }
    [DataMember]
    public double? D1
    {
      get;
      set;
    }
    [DataMember]
    public double? D2
    {
      get;
      set;
    }
    [DataMember]
    public double? W1
    {
      get;
      set;
    }
    [DataMember]
    public double? W2
    {
      get;
      set;
    }
    [DataMember]
    public double? Angle1
    {
      get;
      set;
    }

    [DataMember]
    public double? Angle2
    {
      get;
      set;
    }

    [DataMember]
    public double? SpreadFactor
    {
      get;
      set;
    }
    [DataMember]
    public String GrindingProgramName
    {
      get;
      set;
    }

    [DataMember]
    public String Description
    {
      get;
      set;
    }
    [DataMember]
    public double? GroovePlane
    {
      get;
      set;
    }
    [DataMember]
    public String NameShort
    {
      get;
      set;
    }

    [DataMember]
    public double? Ds
    {
      get;
      set;
    }

    [DataMember]
    public double? Dw
    {
      get;
      set;
    }


  }
}

