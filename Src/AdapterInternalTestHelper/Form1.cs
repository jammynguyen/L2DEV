using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PE.DTO.External.MVHistory;
using PE.Interfaces.Lite;
using SMF.Module.Core;
using PE.DTO.External.DBAdapter;
//using PE.DTO.External.MVHistory;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.MVHistory;
using PE.Interfaces.Interfaces.Lite;
using PE.Interfaces.Interfaces.SendOffice;
//using PE.Interfaces.Lite;
using SMF.Core;
using SMF.Core.DC;
//using SMF.Module.Core;
// using System.ServiceModel;
//using System.Threading.Tasks;
using System.Reflection;
using AdapterInternalTestHelper.DataStructure;
using AdapterInternalTestHelper.Communication;
using System.Threading;

namespace AdapterInternalTestHelper
{
  public partial class Form1 : Form
  {
    List<MethodStructure> MethodsStructuresList = new List<MethodStructure>();
    Thread th;
    public static UInt32 baseId = 0;
    public Form1()
    {
      InitializeComponent();

     
      // th.Start();

    }

    static async void NewThread()
    {
      DCMeasDataExt dc = new DCMeasDataExt();
      dc.Avg = 1;
      dc.BasId = Convert.ToUInt16(baseId);
      dc.EventCode = 301;
      dc.IsLastPass = 0;
      dc.IsReversed = 0;
      dc.Max = 0;
      dc.Min = 0;
      dc.PassNumber = 0;
      dc.Valid = 0;
      dc.BaseFeature = 301;

      for (int i = 0; ; i++)
      {
        await Sender.SendL1MeasDataMessageAsync(dc, baseId);
      }
    }

    private async void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        DCL1BasIdRequestExt dc = new DCL1BasIdRequestExt();
        SendOfficeResult<DCPEBasIdExt> result = await Sender.SendL1MaterialIdRequestToAdapterAsync(dc);
        if (result.DataConctract.BasId > 0)
        {
          AdapterMethodsBox.Items.Clear();
          textBox1.Text = Convert.ToString(result.DataConctract.BasId);
          baseId = Convert.ToUInt32(textBox1.Text);
          SendDataToAdapterButton.Enabled = true;
          AdapterMethodsBox.Enabled = true;
          AddMessageToStatusBox("Received Bas Id:" + result.DataConctract.BasId + " from Adapter");

          Type myType = (typeof(IAdapter));
          MethodInfo[] AdapterMethodsList = myType.GetMethods();
          if (AdapterMethodsList != null)
          {
            foreach (MethodInfo element in AdapterMethodsList)
            {
              MethodsStructuresList.Add(new MethodStructure(element.Name, element.GetParameters()[0].ParameterType.ToString(), element.ReturnParameter.ParameterType.ToString()));
              AdapterMethodsBox.Items.Add(element.Name);
            }
          }
        }
        else
        {
          throw new Exception("Cannot read data from adapter");
        }

      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }

    }


    #region Status display
    public void AddMessageToStatusBox(string message)
    {
      string fullMessage = DateTime.Now.ToString() + " : " + message;
      StatusBox.Items.Insert(0, fullMessage);
      if (StatusBox.Items.Count > 10)
      {
        StatusBox.Items.RemoveAt(10);
      }
    }
    #endregion

    private async void Button2_Click(object sender, EventArgs e)
    {
      SendOfficeResult<DCDefaultExt> result = await Sender.SendData(AdapterMethodsBox.Text, DCTypeTextBox.Text, DCInputTextBox.Text, Convert.ToUInt32(textBox1.Text));
    }

    private void AdapterMethodsBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      string curItem = AdapterMethodsBox.SelectedItem.ToString();
      if (MethodsStructuresList != null && MethodsStructuresList.Count > 0)
      {
        IEnumerable<MethodStructure> dataType = MethodsStructuresList.Where(z => z.MethodName == curItem);
        DCTypeTextBox.Text = dataType.ElementAt(0).InputDataType;
        DCOutputDataTypeTextBox.Text = dataType.ElementAt(0).OutputDataType;
      }
    }

    private void Button2_Click_1(object sender, EventArgs e)
    {
      th = new Thread(NewThread);
      th.Start();
    }

    private void Button3_Click(object sender, EventArgs e)
    {
      th.Abort();
    }
  }
}
