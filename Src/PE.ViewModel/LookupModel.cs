using PE.HMIWWW.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel
{
    [Serializable]
    public class LookupModel
    {
        public LookupModel(long id, string name)
        {
            Id = id;
            Name = name;
        }
        public LookupModel()
        {
            Id = 0;
            Name = "";
        }
        public string Name { get; set; }
        public long Id { get; set; }
    }

    //public class LookupModelList : VM_BaseList<LookupModel> { }
}
