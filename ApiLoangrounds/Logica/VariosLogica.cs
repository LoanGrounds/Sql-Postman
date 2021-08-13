using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ApiLoangrounds.Utils;
using ApiLoangrounds.Models;


namespace ApiLoangrounds.Logica
{
    public static class VariosLogica
    {
        public static List<Genero> traerGeneros()
        {
            List<Genero> aux = new List<Genero>();
           
                SqlDataReader lector = BD.traerLector("Generos_ObtenerTodos");
                Genero generoAux;
            try
            {
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        generoAux = new Genero();
                        generoAux.Id = Convert.ToInt32(lector["Id"]);
                        generoAux.Nombre = (lector["Nombre"] == DBNull.Value) ? "" : Convert.ToString(lector["Nombre"]);
                        generoAux.Orden = (lector["Orden"] == DBNull.Value) ? -1 : Convert.ToInt32(lector["Orden"]);
                        aux.Add(generoAux);

                    }
                    lector.Close();
                }
            }

            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
            BD.CloseAndDisposeReader(ref lector);
            return aux;
        }
        public static List<Provincia> obtenerTodasLasProvincias()
        {
            List<Provincia> lista = new List<Provincia>();
            SqlDataReader lector = BD.traerLector("Provincias_ObtenerTodos");
            try { 
                if (lector.HasRows)
                {


                    while (lector.Read())
                    {
                        Provincia aux = new Provincia();
                        aux.Id = Convert.ToInt32(lector["Id"]);
                        aux.Nombre = (lector["Nombre"] == DBNull.Value) ? "" : Convert.ToString(lector["Nombre"]);
                        lista.Add(aux);
                    }


                    lector.Close();

                }
            }
          
               catch(Exception ex)
            {
                LogManager.LogException(ex);
            }
            BD.CloseAndDisposeReader(ref lector);
            return lista;
        }
        public static List<Localidad> obtenerLocalidadesDeUnDepto(int id)
        {
            List<Localidad> lista = new List<Localidad>();
            
                SqlParameter param = new SqlParameter("@intIdDepto", id);
                SqlDataReader lector = BD.traerLector("Localidades_ObtenerPorIdDepto", param);
            try
            {
                if (lector.HasRows)
                {


                    while (lector.Read())
                    {
                        Localidad aux = new Localidad();
                        aux.Id = Convert.ToInt32(lector["Id"]);
                        aux.Nombre = (lector["Nombre"] == DBNull.Value) ? "" : Convert.ToString(lector["Nombre"]);
                        lista.Add(aux);
                    }

                    lector.Close();

                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
            BD.CloseAndDisposeReader(ref lector);
            return lista;
        }
        public static List<EstadoDePrestamo> traerEstadosPosibles()
        {
            List<EstadoDePrestamo> lista = new List<EstadoDePrestamo>();
            EstadoDePrestamo aux;

            SqlDataReader lector = BD.traerLector("EstadosDePrestamo_obtenerTodos");

            try
            {
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        aux = new EstadoDePrestamo();
                        aux.Id = Convert.ToInt32(lector["Id"]);
                        aux.Nombre = (lector["Nombre"] == DBNull.Value) ? "" : Convert.ToString(lector["Nombre"]);
                        aux.Comentarios = (lector["Comentarios"] == DBNull.Value) ? "" : Convert.ToString(lector["Comentarios"]);
                        lista.Add(aux);
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
            BD.CloseAndDisposeReader(ref lector);
            return lista;
        }
        public static List<Faqs> obtenerFaqs()
        {
            List<Faqs> lista = new List<Faqs>();
            SqlDataReader lector = BD.traerLector("PreguntasFrecuentes_obtenerTodos");
            try { 
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        Faqs aux = new Faqs();
                        aux.Id = Convert.ToInt32(lector["Id"]);
                        aux.pregunta = (lector["Pregunta"] == DBNull.Value) ? "" : Convert.ToString(lector["Pregunta"]);
                        aux.Respuesta = (lector["Respuesta"] == DBNull.Value) ? "" : Convert.ToString(lector["Respuesta"]);
                        lista.Add(aux);
                    }
                    lector.Close();
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
            BD.CloseAndDisposeReader(ref lector);
            return lista;
        
        }
        
    }
}