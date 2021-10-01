using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ApiLoangrounds.Helpers;
namespace ApiLoangrounds.Utils
{
    public static class BD
    {
        private static string coneccion = AppSettingsHelper.GetAppSetting("DatabaseConnection","");

        public static SqlConnection conectar()
        {
            SqlConnection con = new SqlConnection(coneccion);
            con.Open();
            return con;
        }

        public static bool desconectar(SqlConnection con)
        {
            bool pudoDesconectar = false;
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
                pudoDesconectar = true;

            }
            return pudoDesconectar;
        }

        public static int ExecuteNonQuery(string nombre_SP, SqlParameter[] parametros)
        {
            int regsitrosAfectados=-1;
            try
            {
                SqlConnection con = conectar();
                SqlCommand consulta = con.CreateCommand();
                consulta.CommandText = nombre_SP;
                consulta.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    consulta.Parameters.AddRange(parametros);
                    int a = consulta.ExecuteNonQuery();
                    regsitrosAfectados = a;
                }


                desconectar(con);

            }
            catch (Exception ex)
            {
                throw;
            }
            return regsitrosAfectados;

        }

        public static int ExecuteNonQuery(string nombre_SP, SqlParameter parametro)
        {
            int regsitrosAfectados = -1;
            try
            {
                SqlParameter[] p = new SqlParameter[1] { parametro };
                regsitrosAfectados = ExecuteNonQuery(nombre_SP,p);                                                                             
            }
            catch (Exception ex)
            {
                throw;
            }
            return regsitrosAfectados;
        }

        public static int ExecuteNonQuery(string nombre_SP)
        {
            int regsitrosAfectados = -1;
            try
            {
                regsitrosAfectados = ExecuteNonQuery(nombre_SP, (SqlParameter[])null);
            }
            catch (Exception ex)
            {
                throw;
            }
            return regsitrosAfectados;
        }

        public static object ExecuteScalar(string nombre_SP)
        {
            object returnObject = null;
            try
            {
                returnObject = ExecuteScalar(nombre_SP,(SqlParameter[])null);

            }
            catch (Exception ex)
            {
                throw;
            }
            return returnObject;
        }
        public static object ExecuteScalar(string nombre_SP, SqlParameter parametro)
        {
            object returnObject = null;
            try
            {
                SqlParameter[] p = new SqlParameter[1] { parametro };
                returnObject = ExecuteScalar(nombre_SP,p);          
                
            }
            catch (Exception ex)
            {
                throw;
            }
            return returnObject;
        }

        public static object ExecuteScalar(string nombre_SP, SqlParameter[] parametros)
        {
            object returnObject = null;
            try
            {
                SqlConnection con = conectar();
                SqlCommand consulta = con.CreateCommand();
                consulta.CommandText = nombre_SP;
                consulta.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    consulta.Parameters.AddRange(parametros);
                    returnObject = consulta.ExecuteScalar();
                }


                desconectar(con);



            }
            catch (Exception ex)
            {
                throw;
            }
            return returnObject;
        }

        public static SqlDataReader traerLector(string nombre_SP, SqlParameter parametro)
        {
            SqlDataReader lector;
            try
            {
                SqlParameter[] p = new SqlParameter[1] { parametro };
                lector = traerLector(nombre_SP, p);
            }
            catch (Exception ex)
            {
                throw;
            }
            return lector;
        }


        public static SqlDataReader traerLector(string nombre_SP, SqlParameter[] parametros)
        {
            SqlDataReader lector;
            try
            {
                SqlConnection con = conectar();
                SqlCommand consulta = con.CreateCommand();
                consulta.CommandText = nombre_SP;
                consulta.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    consulta.Parameters.AddRange(parametros);

                }
                lector = consulta.ExecuteReader(System.Data.CommandBehavior.CloseConnection);


             



            }
            catch (Exception ex)
            {
                throw;
            }
            return lector;
        }

        public static SqlDataReader traerLector(string nombre_SP)
        {
            SqlDataReader lector;
            try
            {
                lector = traerLector(nombre_SP,   (SqlParameter[])null);
            }
            catch (Exception ex)
            {
                throw;
            }
            return lector;
        }

        public static void CloseAndDisposeReader(ref SqlDataReader currentReader)
        {
            if (currentReader != null)
            {
                if (!currentReader.IsClosed)
                {
                    currentReader.Close();
                }
                currentReader.Dispose();
                currentReader = null;
            }
        }

   
        
        public static T readerToObject<T> (SqlDataReader lector, string key)
        {
            try
            {
                return lector[key] == DBNull.Value ? default(T) : (T)Convert.ChangeType(lector[key], typeof(T));

            }
            catch { throw; }            
          
        }
      
    }
}