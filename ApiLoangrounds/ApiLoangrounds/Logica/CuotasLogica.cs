using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using spreadOperatorEquivalent;
using System.Data.SqlClient;
using ApiLoangrounds.Models;
using ApiLoangrounds.Helpers;
using ApiLoangrounds.Utils;


namespace ApiLoangrounds.Logica
{
    public static class CuotasLogica
    {
        #region Metodos de la clase

        private static Cuota readerToCouta(SqlDataReader lector) {
            Cuota aux = new Cuota();
            if (lector != null) {
                aux.IdDetalle = BD.readerToObject<int>(lector, "IdDetalle");
                aux.NroCuota = BD.readerToObject<int>(lector, "NroCuota");
                aux.FechaVencimiento = BD.readerToObject<DateTime>(lector, "fechaVencimiento");
                aux.IdEstadoCouta = BD.readerToObject<int>(lector, "IdEstadoCouta");
                aux.Monto = BD.readerToObject<double>(lector, "Monto");
            }
            return aux;
        }
        #endregion
        #region por Get

        public static List<Cuota> obtenerCoutasPorPrestamo(int idDetalle)
        {
            List<Cuota> lista = new List<Cuota>();
            Cuota aux;
            SqlParameter param = new SqlParameter("@idDetalle", idDetalle);
            SqlDataReader lector = BD.traerLector("Cuotas_obtenerTodos", param);
            try
            {
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        aux = readerToCouta(lector);
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
        public static Cuota obtenerPorId(int idDetalle, int nroCouta)
        {
            Cuota aux = new Cuota();
            List<SqlParameter> parametros = new List<SqlParameter>() {
                new SqlParameter("@IdDetalle", idDetalle),
                new SqlParameter("@nroCuota", nroCouta)
                 };
            SqlDataReader lector = BD.traerLector("Cuotas_obtenerPorId", parametros.ToArray());
            try
            {
                if (lector.HasRows)
                {
                    lector.Read();
                    aux = readerToCouta(lector);
                }
            }
            catch (Exception ex)
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
            return BD.ExecuteNonQuery("Cuotas_borrar", param);
        }

        public static int Cambiar(Cuota c)
        {
            Cuota aux = obtenerPorId(c.IdDetalle, c.NroCuota);
            if (aux == null) return -1;
            aux = SpreadEquivalent.spread(aux, c);
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@idDetalle", aux.IdDetalle),
                new SqlParameter("@monto", aux.Monto),
                new SqlParameter("@nroCuota", aux.NroCuota),
                new SqlParameter("@fechaVencimiento", aux.FechaVencimiento),
                new SqlParameter("@idEstado", aux.IdEstadoCouta),
             };
            return BD.ExecuteNonQuery("Cuotas_Update", parametros.ToArray());
        }

        public static int insertar (Cuota c)
        {
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@idDetalle", c.IdDetalle),
                new SqlParameter("@monto", c.Monto),
                new SqlParameter("@nroCuota", c.NroCuota),
                new SqlParameter("@fechaVencimiento", c.FechaVencimiento),
                new SqlParameter("@idEstado", c.IdEstadoCouta),
             };
            return Convert.ToInt32(BD.ExecuteScalar("Cuotas_insertar", parametros.ToArray()));
        }

        public static void insertarCuotasDeUnPrestamo(DetallePrestamo detalle)
        {
            if (detalle == null) return;
            double precioCuota = calcularPrecioCouta(detalle);
            for (int i = 0; i < detalle.CantidadCuotas; i++)
            {
                Cuota aux = new Cuota();
                aux.IdDetalle = (int)detalle.Id;
                aux.Monto = precioCuota;
                aux.NroCuota = i+1;
                aux.IdEstadoCouta = 1;
                aux.FechaVencimiento = calcularFechaCuota(detalle, aux.NroCuota);
                insertar(aux);
            }
        }

        public static DateTime calcularFechaCuota(DetallePrestamo d, int nroCuota)
        {
            if (d.FechaDeAcuerdo == null) return default;
            return d.FechaDeAcuerdo.Value.AddDays((double)d.DiasEntreCuotas * nroCuota);
        }

        public static double calcularPrecioCouta (DetallePrestamo detalle)
        {

            return (double)((detalle.Monto/detalle.CantidadCuotas) +(detalle.Monto * detalle.InteresXCuota / 100));
        }
        #endregion
    }
}