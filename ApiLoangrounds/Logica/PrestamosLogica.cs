using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ApiLoangrounds.Helpers;
using ApiLoangrounds.Utils;
using ApiLoangrounds.Models;


namespace ApiLoangrounds.Logica
{
    public static class PrestamosLogica 
    {
        #region Metodos de la clase
        public static Prestamo ReaderToObject(SqlDataReader lector)
        {
            Prestamo aux = null;
            
            if (lector != null)
            {
                aux = new Prestamo();
                aux.Id = Convert.ToInt32(lector["Id"]);
                aux.IdDetallePrestamo = (lector["IdDetallePrestamo"] == DBNull.Value) ? -1 : Convert.ToInt32(lector["IdDetallePrestamo"]);
                aux.IdUsuarioPrestamista = (lector["IdUsuarioPrestamista"] == DBNull.Value) ? -1 : Convert.ToInt32(lector["IdUsuarioPrestamista"]);
                aux.IdUsuarioPrestador = (lector["IdUsuarioPrestador"] == DBNull.Value) ? -1 : Convert.ToInt32(lector["IdUsuarioPrestador"]);
            }
            return aux;
        }
        public static bool esValido(Prestamo p, out string errores)
        {
            bool aux = true;
            errores = " ";

            if (UsuariosLogica.traerPorId(p.IdUsuarioPrestamista) == null)
            {
                aux = false;
                errores += "uno de los usuarios que quiso formar parte de la operación no fue encontrado";
            }
            if (DetallesLogica.obtenerPorId(p.IdDetallePrestamo) == null)
            {
                aux = false;
                errores += "lo sentimos, hubo en error en encontrar los datos del préstamo a crear, por favor vuelva a intentarlo";
            }

            return aux;
        }
        public static PrestamoRecomendadoDTO ReaderToDTO(SqlDataReader lector)
        {
            PrestamoRecomendadoDTO pAux = null;

            if (lector != null)
            {

                pAux = new PrestamoRecomendadoDTO();
                pAux.Id = Convert.ToInt32(lector["Id"]);
                pAux.Monto = (lector["Monto"] == DBNull.Value) ? -1 : Convert.ToInt32(lector["Monto"]);
                pAux.UserName = (lector["UserName"] == DBNull.Value) ? "" : Convert.ToString(lector["UserName"]);
            }
            return pAux;
        }

        public static BusquedaFiltradaDTO readerToDTO(SqlDataReader lector)
        {
            BusquedaFiltradaDTO bus = new BusquedaFiltradaDTO();
            if(lector != null)
            {
                bus.Id = Convert.ToInt32(lector["Id"]);
                bus.UserName = (lector["UserName"] == DBNull.Value) ? "" : Convert.ToString(lector["UserName"]);
                bus.Monto = (lector["Monto"] == DBNull.Value) ? -1.0 : Convert.ToDouble(lector["Monto"]);
                bus.InteresXCuota = (lector["InteresXCuota"] == DBNull.Value) ? -1.0 : Convert.ToDouble(lector["InteresXCuota"]);
                bus.DiasEntreCuotas = (lector["DiasEntreCuotas"] == DBNull.Value) ? -1 : Convert.ToInt16(lector["DiasEntreCuotas"]);
                bus.DiasTolerancia = (lector["DiasTolerancia"] == DBNull.Value) ? -1 : Convert.ToInt16(lector["DiasTolerancia"]);
                bus.CantidadCuotas = (lector["CantidadCuotas"] == DBNull.Value) ? -1 : Convert.ToInt16(lector["CantidadCuotas"]);
            }
            return bus;
        }
        #endregion
        #region POR GET
        public static List<Prestamo> traerTodos()
        {
            List<Prestamo> lista = new List<Prestamo>();
            Prestamo aux;
           
            SqlDataReader lector = BD.traerLector("Prestamos_obtenerTodos");
            try
            {
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        aux = ReaderToObject(lector);
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
        public static Prestamo traerPorId(int id)
        {
                Prestamo p = new Prestamo();            
                SqlParameter param = new SqlParameter("@IdPrestamo", id);
                SqlDataReader lector = BD.traerLector("Prestamo_ObtenerPorId", param);
            try
            {
                if (lector.HasRows)
                {
                    lector.Read();
                    p = ReaderToObject(lector);
                }               
            }
            catch(Exception ex)
            {
                LogManager.LogException(ex);
            }
            BD.CloseAndDisposeReader(ref lector);
            return p;
        }


        public static List<PrestamoRecomendadoDTO> traerPorMonto(int montoMax, int idUsuario)
        {
            List<PrestamoRecomendadoDTO> aux = new List<PrestamoRecomendadoDTO>();

            SqlParameter[]parametros = {
                new SqlParameter("@MaxMonto", montoMax),
                new SqlParameter("@IdUsuario", idUsuario)
                    };
                SqlDataReader lector = BD.traerLector("Prestamo_obtenerPorMonto", parametros);             
                PrestamoRecomendadoDTO pAux;
            try
            {
                if (lector.HasRows) { 
                
                while (lector.Read())
                {
                    pAux = ReaderToDTO(lector);          
                    aux.Add(pAux);
                }                
               }
            }

            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
            BD.CloseAndDisposeReader(ref lector);
            return aux;
        }

        
        
        public static List<Prestamo> traerPrestamosDeUnPrestamista(int id)
        {
                List<Prestamo> lista = new List<Prestamo>();
                Prestamo aux;           
                SqlParameter param = new SqlParameter("@IdUsuario", id);
                SqlDataReader lector = BD.traerLector("ListarPrestamosDePrestamista", param);
            try { 
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        aux = ReaderToObject(lector);
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


        public static List<Prestamo> traerPrestamosDelQuePide(int id)
        {
            List<Prestamo> lista = new List<Prestamo>();
            Prestamo aux;           
            SqlParameter param = new SqlParameter("@IdUsuario", id);
            SqlDataReader lector = BD.traerLector("ListarPrestamosDelQuePide", param);
            try
            {
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        aux = ReaderToObject(lector);
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

        public static string[] traerActoresPrestamo(int id)
        {
             string[] nombres = new string[2];           
             SqlParameter param = new SqlParameter("@IdPrestamo", id);
             SqlDataReader lector = BD.traerLector("Prestamo_ObtenerActores", param);
            try { 
                if (lector.HasRows)
                {
                    lector.Read();
                    nombres[0] = (lector["Nombre_prestamista"] == DBNull.Value) ? "" : Convert.ToString(lector["Nombre_prestamista"]);
                    nombres[1] = (lector["Nombre_recibidor"] == DBNull.Value) ? "" : Convert.ToString(lector["Nombre_recibidor"]);
                }
                lector.Close();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
            BD.CloseAndDisposeReader(ref lector);
            return nombres;
        }

        public static List<BusquedaFiltradaDTO> BusquedaFiltrada(double? montoMax, double? maxInteres, int? minDiasT, int? minDiasCutoas, int? minCantCuotas, int IdUsuario)
        {
            BusquedaFiltradaDTO aux;
            List<BusquedaFiltradaDTO> lista = new List<BusquedaFiltradaDTO>();
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@MontoMax",montoMax),
                new SqlParameter("@MaxInteres",maxInteres),
                new SqlParameter("@MinDiasTolerancia",minDiasT),
                new SqlParameter("@MinDiasEntreCuotas",minDiasCutoas),
                new SqlParameter("@MinCantCutoas",minCantCuotas),
                new SqlParameter("@IdUsuario",IdUsuario)
            };
            SqlDataReader lector = BD.traerLector("Prestamos_busquedaFiltrada", parametros.ToArray());
            try
            {
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        aux = readerToDTO(lector);
                        lista.Add(aux);
                    }
                }
            }
            catch(Exception ex)
            {
                LogManager.LogException(ex);
            }
            BD.CloseAndDisposeReader(ref lector);
            return lista;
        }

        #endregion
        #region NonQuery
        public static int eliminar(int id)
        {
            Prestamo aux = traerPorId(id);
            try
            {
                DetallesLogica.eliminar(aux.IdDetallePrestamo);
                SqlParameter param = new SqlParameter("@idPrestamo", id);
                return BD.ExecuteNonQuery("BorrarPrestamo", param);
            }
            catch(Exception ex)
            {
                LogManager.LogException(ex);
                return -1;
            }
           
        }
        public static int insertar(Prestamo p)
        {
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@idDetalle", p.IdDetallePrestamo),
                new SqlParameter("@idPestamista", p.IdUsuarioPrestamista),
                };
            if (p.IdUsuarioPrestador > 0) parametros.Add(new SqlParameter("@idPrestador", p.IdUsuarioPrestador));
            return Convert.ToInt32(BD.ExecuteScalar("InsertarPrestamo_scalar", parametros.ToArray()));
        }
        public static int insertarValido(Prestamo p, out string errorres)
        {
            int nuevoPrestamo = 0;
            if (esValido(p, out errorres))
            {
                nuevoPrestamo = insertar(p);
            }
            return nuevoPrestamo;
        }

        public static int insertarEstado(EstadoDePrestamo e)
        {
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@Comentarios", e.Comentarios),
                new SqlParameter("@Nombre", e.Nombre),
                
             };
            return Convert.ToInt32(BD.ExecuteScalar("InsertarEstadoPrestamo", parametros.ToArray()));
        }

        public static int cambiar(Prestamo p)
        {
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@Id", p.Id),
                new SqlParameter("@idDetalle", p.IdDetallePrestamo),
                new SqlParameter("@idPestamista", p.IdUsuarioPrestamista),
                new SqlParameter("@idPrestador",p.IdUsuarioPrestador)
             };
            return BD.ExecuteNonQuery("Prestamos_update", parametros.ToArray());
        }
        #endregion

        

    }
}