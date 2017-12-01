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
		public int Dividir(int a, int b)
		{
			var resultado = 0;

			try
			{
				Trace.TraceInformation("Asignando valores a variables a:{0} b:{1}", a, b);

				Trace.TraceInformation("Tratando de realizar division [a/b]");

				resultado = a / b;
			}
			catch(Exception)
			{
				throw;
			}

			return resultado;
		}
    }
}
