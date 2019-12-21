﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PE.HMIWWW.Core.HtmlHelpers
{
	public class JsonNetResult : ActionResult
	{
		public Encoding ContentEncoding { get; set; }
		public string ContentType { get; set; }
		public object Data { get; set; }

		public JsonSerializerSettings SerializerSettings { get; set; }
		public Formatting Formatting { get; set; }

		public JsonNetResult()
		{
			SerializerSettings = new JsonSerializerSettings();
		}

		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");

			HttpResponseBase response = context.HttpContext.Response;

			response.ContentType = !string.IsNullOrEmpty(ContentType)
																 ? ContentType
																 : "application/json";

			if (ContentEncoding != null)
				response.ContentEncoding = ContentEncoding;

			if (Data != null)
			{
				var writer = new JsonTextWriter(response.Output) { Formatting = Formatting };

				JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
				serializer.Serialize(writer, Data);

				writer.Flush();
			}
		}
	}
	public class MyDataSource
	{
		public object AggregateResults { get; set; }
		public object Data { get; set; }
		public object Errors { get; set; }
		public int Total { get; set; }
	}
}