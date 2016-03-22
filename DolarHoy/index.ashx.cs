using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;

namespace DolaryHoy
{
	public class Index : IHttpHandler
	{
		public void ProcessRequest (HttpContext context)
		{
			var json = new WebClient().DownloadString("http://contenidos.lanacion.com.ar/json/dolar");
			json = json.Substring (
				json.IndexOf ('{'),
				json.LastIndexOf ('}') - json.IndexOf ('{') + 1);

			dynamic data = JObject.Parse(json);
			string compra = data.CasaCambioCompraValue;
			string venta = data.CasaCambioVentaValue;

			if (context.Request.AcceptTypes.Any(s => s == "application/json") || 
				context.Request.QueryString.AllKeys.Contains("json") ||
				(context.Request.QueryString.Count != 0 && context.Request.QueryString.GetValues (0).Any (s => s == "json"))) {
				dynamic result = new JObject();
				result.compra = compra;
				result.venta = venta;

				context.Response.ContentType = "application/json";
				context.Response.AddHeader ("Access-Control-Allow-Origin", "*");
				((JObject)result).WriteTo (new JsonTextWriter (context.Response.Output) { Formatting = Formatting.Indented });
			} else {
				var addDate = context.Request.QueryString.AllKeys.Contains("date") ||
					(context.Request.QueryString.Count != 0 && context.Request.QueryString.GetValues (0).Any (s => s == "date"));

				if (addDate)
					context.Response.Write (string.Format ("Compra: {0}, Venta: {1} ({2})", compra, venta,
						TimeZoneInfo.ConvertTimeFromUtc (DateTime.UtcNow, TimeZoneInfo.CreateCustomTimeZone ("GMT-0300", new TimeSpan (-3, 0, 0), "GMT-0300", "GMT-0300")).ToString ()));
				else
					context.Response.Write (string.Format ("Compra: {0}, Venta: {1}", compra, venta));
			}
		}

		public bool IsReusable => true;
	}
}