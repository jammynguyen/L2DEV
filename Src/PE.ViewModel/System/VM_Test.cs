using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.System
{
    public class VM_Test : VM_Base
    {
        public VM_Test()
        {
        }
        public VM_Test(String text)
        {
            Text = text;
        }

        [SmfDisplayAttribute(typeof(VM_Test), "Text", "NAME_Text")]
        public virtual String Text { get; set; }
    }
}
