using System;
using System.Web.Mvc;
using Microsoft.ApplicationInsights;
using System.Web.Http.ExceptionHandling;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.ApplicationInsights.DataContracts;

namespace WebApi_ApplicationInsights.ErrorHandler
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class AiHandleErrorAttribute : HandleErrorAttribute
	{
		public override void OnException(System.Web.Mvc.ExceptionContext filterContext)
		{
			//if (filterContext != null && filterContext.HttpContext != null && filterContext.Exception != null)
			//{
			//	//If customError is Off, then AI HTTPModule will report the exception
			//	if (filterContext.HttpContext.IsCustomErrorEnabled)
			//	{
			//		var ai = new TelemetryClient();
			//		ai.TrackException(filterContext.Exception);
			//	}
			//}
			base.OnException(filterContext);
		}
	}

	public class WebApiExceptionLogger : ExceptionLogger
	{
		private readonly TelemetryClient _telemetryClient;

		public WebApiExceptionLogger(TelemetryClient telemetryClient)
		{
			_telemetryClient = telemetryClient;
		}

		public override void Log(ExceptionLoggerContext context)
		{
			var texto = new StringBuilder();

			texto = RegistrarError(texto, context, context.Exception, false, 1);

			EventLog.WriteEntry("BTS-ONE", texto.ToString(), System.Diagnostics.EventLogEntryType.Error, 260);

			_telemetryClient.TrackException(new Exception(texto.ToString(), context.Exception));

		}

		private StringBuilder RegistrarError(StringBuilder texto, ExceptionLoggerContext context, Exception ex, bool isInnerException, int idAnomalia)
		{
			if (!isInnerException)
			{
				texto.AppendFormat("DATOS GENERALES DE LA PETICION ==============================|\n\n");
				texto.AppendFormat("Method:\n\n\t{0}\n\n", context.Request.Method.Method);
				texto.AppendFormat("URL Servicio:\n\n\t{0}\n\n", context.Request.RequestUri.ToString());

				var usuario = (context.RequestContext.Principal.Identity as ClaimsIdentity);

				usuario.AddClaims(new List<Claim>()
				{
					new Claim("username", usuario.Name, "DatosUsuario", "BTS-ONE"),
					new Claim("oficina", "Mexicali", "DatosUsuario", "BTS-ONE")
				});

				texto.AppendFormat("PROPIEDADES DE LA SESION DE USUARIO =========================|\n\n", usuario.Name);

				foreach (Claim item in usuario.Claims)
				{
					if (item.Issuer == "BTS-ONE")
						texto.AppendFormat("{0}:\n\n\t{1}\n\n", item.Type, item.Value);
				}
			}

			texto.AppendFormat("ANOMALIA {0} ================================================|\n\n", idAnomalia);

			texto.AppendFormat("Source error:\n\n\t{0}\n\n", ex.Source);
			texto.AppendFormat("Message error:\n\n\t{0}\n\n", ex.Message);
			texto.AppendFormat("Method that throws the current exception:\n\n\t{0}\n\n", ex.TargetSite);
			texto.AppendFormat("Stack trace:\n\n{0}\n\n", ex.StackTrace);

			if (ex.InnerException != null)
			{
				texto.Append(RegistrarError(texto, context, ex.InnerException, true, idAnomalia + 1));
			}

			return texto;
		}
	}
}