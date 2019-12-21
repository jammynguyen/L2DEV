using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PE.SIM.L1L3Simulation.Communication;
using PE.SIM.L1L3Simulation.Workers;
using SMF.Core.Log;
using SMF.Module.Core;

namespace PE.SIM.L1L3Simulation
{
  public delegate void StatusDelegate(List<string> message);
  public partial class Form1 : Form
  {

    #region members


    #endregion


    #region ctor

    public Form1()
    { 
      InitializeComponent();
      L3Worker.Start(L3Message);


    }

    #endregion

    public void L3Message(List<string> message)
    {
      if (richTextBox1.InvokeRequired)
      {
        richTextBox1.Invoke(new Action<List<string>>(L3Message), new object[] { message });
        return;
      }
			foreach(string s in message)
				richTextBox1.Text= s;
    }
  }
}
