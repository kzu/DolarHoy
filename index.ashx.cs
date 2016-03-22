using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

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
				context.Response.Write (string.Format ("Compra: {0}, Venta: {1}", compra, venta));
			}
		}

		public bool IsReusable => true;
	}
}