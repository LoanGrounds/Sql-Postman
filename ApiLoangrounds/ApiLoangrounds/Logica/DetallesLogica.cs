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
    public class DetallesLogica
    {
        #region Metodos de la clase
        public static DetallePrestamo readerToObject(SqlDataReader lector)
        {
            DetallePrestamo aux = null;
            if (lector != null)
            {
                aux = new DetallePrestamo();
                aux.Id  = Convert.ToInt32(lector["Id"]);
                aux.Monto = (lector["Monto"] == DBNull.Value) ? -1.0 :  Convert.ToDouble(lector["Monto"]);
                aux.IdEstadoDePrestamo =  Convert.ToInt32(lector["IdEstadoDePrestamo"]);
                aux.InteresXCuota = (lector["InteresXCuota"] == DBNull.Value) ? -1.0 :  Convert.ToDouble(lector["InteresXCuota"]);
                aux.DiasEntreCuotas = (lector["DiasEntreCuotas"] == DBNull.Value) ? -1 : Convert.ToInt16(lector["DiasEntreCuotas"]);
                aux.DiasTolerancia = (lector["DiasTolerancia"] == DBNull.Value) ? -1 : Convert.ToInt16(lector["DiasTolerancia"]);
                aux.CantidadCuotas = (lector["CantidadCuotas"] == DBNull.Value) ? -1 : Convert.ToInt16(lector["CantidadCuotas"]);
                aux.FechaDeAcuerdo = (lector["FechaDeAcuerdo"] == DBNull.Value) ? (DateTime?) null : Convert.ToDateTime(lector["FechaDeAcuerdo"]);
            }
            return aux;
        }

        public static bool esValido(DetallePrestamo d, out string errores)
        {
            bool aux = true;
            errores = "";
            if(!ValidacionesHelpers.esPositivo(d.Monto))
            {
                aux = false;
                errores = "error, el monto no es valido";
            }
            if (!ValidacionesHelpers.esPositivo(d.DiasEntreCuotas))
            {
                aux = false;
                errores = "error, los dias no pueden ser 0 o menos";
            }
            if (!ValidacionesHelpers.esPositivo(d.DiasTolerancia))
            {
                aux = false;
                errores = "por favor deje al menos un día de tolerancia";
            }
            if (!ValidacionesHelpers.esPositivo(d.InteresXCuota))
            {
                aux = false;
                errores = "error, el interes no puede ser negativo";
            }
            /*if (!ValidacionesHelpers.esFechaValida(d.FechaDeAcuerdo))
            {
                aux = false;
                errores = "error, la fecha no es valida";
            }*/

            return aux;
        }
        #endregion
        #region Por Get
        public static List<DetallePrestamo> traerTodos()
        {
            List<DetallePrestamo> lista = new List<DetallePrestamo>();
            DetallePrestamo aux;

            SqlDataReader lector = BD.traerLector("Detalles_obtenerTodos");
            try
            {
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        aux = readerToObject(lector);
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
        public static DetallePrestamo obtenerPorId(int id)
        {
            DetallePrestamo aux = new DetallePrestamo();
            SqlParameter param = new SqlParameter ( "@IdDetalle", id );
            SqlDataReader lector = BD.traerLector("DetallePrestamo_ObtenerPorId", param);
            try
            {
                if (lector.HasRows)
                {
                    lector.Read();
                    aux = readerToObject(lector);
                }               
            }
            catch(Exception ex)
            {
                LogManager.LogException(ex);
            }
            BD.CloseAndDisposeReader(ref lector);
            return aux;
        }
        #endregion
        #region NonQuery
        public static int eliminar(int id)
        {
            SqlParameter param = new SqlParameter("@idDetalle", id);
            return BD.ExecuteNonQuery("BorrarDetalle", param);
        }
        public static int Cambiar(DetallePrestamo detalle)
        {
            DetallePrestamo aux = DetallesLogica.obtenerPorId(detalle.Id);
            if (detalle.FechaDeAcuerdo != null) aux.FechaDeAcuerdo = detalle.FechaDeAcuerdo;
            if (detalle.CantidadCuotas > 0) aux.CantidadCuotas = detalle.CantidadCuotas;
            if (detalle.DiasEntreCuotas > 0) aux.DiasEntreCuotas = detalle.DiasEntreCuotas;
            if (detalle.DiasTolerancia > 0) aux.DiasTolerancia = detalle.DiasEntreCuotas;
            if (detalle.InteresXCuota > 0) aux.InteresXCuota = detalle.InteresXCuota;
            if (detalle.Monto > 0) aux.Monto = detalle.Monto;
            if (detalle.IdEstadoDePrestamo > 0) aux.IdEstadoDePrestamo = detalle.IdEstadoDePrestamo;
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@idDetalle", aux.Id),
                new SqlParameter("@Monto", aux.Monto),
                new SqlParameter("@idEstadoPrestamo", aux.Id),
                new SqlParameter("@CantCuotas", aux.CantidadCuotas),
                new SqlParameter("@InteresXCuota", aux.InteresXCuota),
                new SqlParameter("@DiasEntreCuotas",  aux.DiasEntreCuotas),
                new SqlParameter("@DiasTolereancia", aux.DiasTolerancia),
                new SqlParameter("@FechaAcuerdo", aux.FechaDeAcuerdo)
             };
            return BD.ExecuteNonQuery("DetallePrestamo_Update", parametros.ToArray());
        }
        public static int insertar(DetallePrestamo detalle)
        {
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@Monto", detalle.Monto),
                new SqlParameter("@idEstadoPrestamo", detalle.IdEstadoDePrestamo),
                new SqlParameter("@CantCuotas", detalle.CantidadCuotas),
                new SqlParameter("@InteresXCuota", detalle.InteresXCuota),
                new SqlParameter("@DiasEntreCuotas",  detalle.DiasEntreCuotas),
                new SqlParameter("@DiasTolereancia", detalle.DiasTolerancia),
                new SqlParameter("@FechaAcuerdo", detalle.FechaDeAcuerdo)
             };
            return Convert.ToInt32(BD.ExecuteScalar("InsertarDetalle_scalar", parametros.ToArray()));
        }
        public static int insertarValido(DetallePrestamo d, out string errorres)
        {
            int nuevoDetalle = 0;
            if (esValido(d, out errorres))
            {
                nuevoDetalle = insertar(d);
            }
            return nuevoDetalle;
        }
        #endregion
     
    }
}