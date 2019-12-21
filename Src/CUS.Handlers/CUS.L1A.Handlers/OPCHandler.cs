using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUS.L1A.Handlers
{
  public class OPCHandler
  {
    public async Task<bool> Send(string propertyName, bool value)
    {
      await Task.Delay(1); //replace with your opc access code
      return true;
    }
    public async Task<bool> Send(string propertyName, Int16 value)
    {
      await Task.Delay(1); //replace with your opc access code
      return true;
    }
    public async Task<bool> Send(string propertyName, float value)
    {
      await Task.Delay(1); //replace with your opc access code
      return true;
    }
    public async Task<bool> Send(string propertyName, byte[] value)
    {
      await Task.Delay(1); //replace with your opc access code
      return true;
    }
    public async Task<bool> Send(string propertyName, Int32 value)
    {
      await Task.Delay(1); //replace with your opc access code
      return true;
    }

    public async Task<T> Get<T>(string propertyName)
    {
      await Task.Delay(0); //replace with your opc access code
      return default(T);
    }
  
  }
}
