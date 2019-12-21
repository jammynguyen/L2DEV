using PE.DbEntity.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PE.ZPC.Handlers
{
	public class ZebraConnectionHandler
	{
		public async Task<byte[]> RequestPngFromZebraWebServiceForHMIAsync(PEContext ctx, long productId)
		{
			byte[] picture = null;
      //PRMProduct target = ctx.PRMProducts.Single(x => x.ProductId == productId);
      V_ProductOverview target = ctx.V_ProductOverview.Where(x => x.ProductId == productId).Single();

			if (target != null)
			{
				string zplTemplate = @"^XA

^FX Top section with company logo, name and address.
^CF0,60
^FO50,50^GB100,100,100^FS
^FO75,75^FR^GB100,100,100^FS
^FO88,88^GB50,50,50^FS
^FO220,50^FDPrimetals Technologies^FS
^CF0,40
^FO220,100^FDStefana Korbonskiego 14B^FS
^FO220,135^FD30-443 Krakow^FS
^FO220,170^FDPoland^FS
^FO50,250^GB700,1,3^FS

^FX Second section with recipient address and permit information.
^CFA,30
^FO50,300^FDProductId: {0} ^FS
^FO50,340^FDProduct Name: {1} ^FS
^FO50,380^FDWeight: {2} kg^FS
^FO50,420^FDWork Order: {3}^FS
^CFA,15


^FX Third section with barcode.
^BY5,2,270
^FO100,550^BC^FD12345678^FS

^FX Fourth section (the two boxes on the bottom).
^FO50,900^GB700,250,3^FS
^FO400,900^GB1,250,3^FS
^CF0,40
^FO100,960^FDShipping Ctr. X34B-1^FS
^FO100,1010^FDREF1 F00B47^FS
^FO100,1060^FDREF2 BL4H8^FS
^CF0,190
^FO485,965^FDCA^FS

^XZ";
				string filledZpl = string.Format(zplTemplate, target.ProductId, target.ProductName, target.Weight, target.WorkOrderName);

				byte[] zpl = Encoding.UTF8.GetBytes(filledZpl);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.labelary.com/v1/printers/8dpmm/labels/4x6/0/");
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = zpl.Length;

        Stream requestStream = request.GetRequestStream();
				requestStream.Write(zpl, 0, zpl.Length);
				requestStream.Close();


        HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());
				Stream responseStream = response.GetResponseStream();

				picture = ReadFully(responseStream);
			}
			return picture;
		}

		public static byte[] ReadFully(Stream input)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				input.CopyTo(ms);
				return ms.ToArray();
			}
		}
	}
}
