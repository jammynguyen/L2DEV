using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.DTO.Internal.RollShop
{
  public struct SingleRollDataInfo
  {

    private long _rollId; //PK from LRRolls
    private double _diameter; //roll diamater [m]


    public long RollId
    {
      get { return _rollId; }
      set { _rollId = value; }
    }

    public double Diameter
    {
      get { return _diameter; }
      set { _diameter = value; }
    }
  }
}
