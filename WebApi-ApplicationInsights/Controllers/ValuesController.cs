using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi_ApplicationInsights.Controllers
{
	[Authorize]
	public class ValuesController : ApiController
	{
		// GET api/values
		public IHttpActionResult Get()
		{
			var resultado = 0;
			var capa = new Calculo();

			Trace.TraceInformation("Iniciando metodo Get...");

			try
			{
				Trace.TraceInformation("Accediendo a la logica de negocio Dividir...");
				resultado = capa.Dividir(a: 100, b: 0);
			}
			catch (Exception)
			{
				throw;
			}

			Trace.TraceInformation("El resultado de [a/b] fue: {0}", resultado);

			return Ok(resultado);
		}

		// GET api/values/5
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}
	}
}
