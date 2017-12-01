using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi_ApplicationInsights.Controllers
{
	[Authorize]
	public class ValuesController : ApiController
	{
		public async Task<IHttpActionResult> Get([FromUri]int a, [FromUri]int b)
		{
			var resultado = 0;
			var capa = new Calculo();

			Trace.TraceInformation("Iniciando metodo Get...");

			try
			{
				Trace.TraceInformation("Accediendo a la logica de negocio Dividir...");
				resultado = await capa.Dividir(a: a, b: b);
			}
			catch (Exception)
			{
				throw;
			}

			Trace.TraceInformation("El resultado de [a/b] fue: {0}", resultado);

			return Ok(resultado);
		}
	}
}
