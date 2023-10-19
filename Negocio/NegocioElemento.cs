using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioElemento
    {
        public List<Elemento> listar()
        {
			List<Elemento> lista = new List<Elemento>();
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.SetearConsulta("Select id, Descripcion From ELEMENTOS");
				datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Elemento aux = new Elemento();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
                }

                return lista;
			}
			catch (Exception ex)
			{

				throw ex;
			}
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
