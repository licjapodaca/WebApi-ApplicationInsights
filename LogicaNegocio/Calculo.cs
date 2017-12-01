using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Calculo
    {
		public async Task<int> Dividir(int a, int b)
		{
			var resultado = 0;

			try
			{
				Trace.TraceInformation("Asignando valores a variables a:{0} b:{1}", a, b);

				Trace.TraceInformation("Tratando de realizar division [a/b]");

				resultado = await Task.Run(() =>
				{
					//try
					//{
						return a / b;
					//}
					//catch(Exception)
					//{
					//	throw;
					//}
				});
			}
			catch(Exception)
			{
				throw;
			}

			return resultado;
		}
    }
}
