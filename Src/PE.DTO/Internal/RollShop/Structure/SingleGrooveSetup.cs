using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.RollShop
{
  public struct SingleGrooveSetup
  {
    private short _grooveIdx; //starts with 1
    private long _grooveTemplate; //PK from groove templates table
    private short _grooveStatus; //referemce to SRC.Core.Constants.RollGrooveStatus 	PlannedAndNoChange = 4, PlannedAndTurning = 5, PlannedAndNotAvailable = 6
    private short _grooveConfirmed;

    public short GrooveIdx
    {
      get { return _grooveIdx; }
      set { _grooveIdx = value; }
    }

    public long GrooveTemplate
    {
      get { return _grooveTemplate; }
      set { _grooveTemplate = value; }
    }

    public short GrooveStatus
    {
      get { return _grooveStatus; }
      set { _grooveStatus = value; }
    }

    public short GrooveConfirmed
    {
      get { return _grooveConfirmed; }
      set { _grooveConfirmed = value; }
    }
  }
}
