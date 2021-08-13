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
    public static class UsuariosLogica
    {
        #region Metodos de la clase Usuario
        public static Usuario ReaderToObject(SqlDataReader lector, bool extended)
        {
            Usuario aux = null;

            if (lector != null)
            {
                if (extended)
                {
                    aux = readerToObjectExtended(lector);
                }
                else
                {
                    aux = new Usuario();
                    aux.Id = Convert.ToInt32(lector["Usuarios_Id"]);
                    aux.UserName = (lector["Usuarios_UserName"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_UserName"]);
                    aux.NombreGenero = (lector["Generos_Nombre"] == DBNull.Value) ? "" : Convert.ToString(lector["Generos_Nombre"]);
                    aux.ApiKey = Convert.ToString(lector["Usuarios_Apikey"]);
                }
           

               

            }
            return aux;
        }
        public static Usuario readerToObjectExtended(SqlDataReader lector)
        {
            Usuario aux = new Usuario();
            if (lector != null)
            {
                aux.NombreGenero = (lector["Generos_Nombre"] == DBNull.Value) ? "" : Convert.ToString(lector["Generos_Nombre"]);
                aux.Id = Convert.ToInt32(lector["Usuarios_Id"]);
                aux.UserName = (lector["Usuarios_UserName"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_UserName"]);
                aux.Password = (lector["Usuarios_Password"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_Password"]);
                aux.Nombre = (lector["Usuarios_Nombre"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_Nombre"]);
                aux.Apellido = (lector["Usuarios_Apellido"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_Apellido"]);
                aux.URLFoto = (lector["Usuarios_URLFoto"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_URLFoto"]);
                aux.NombreGenero = (lector["Generos_Nombre"] == DBNull.Value) ? "" : Convert.ToString(lector["Generos_Nombre"]);
                aux.Descripcion = (lector["Usuarios_Descripcion"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_Descripcion"]);
                aux.Mail = (lector["Usuarios_Mail"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_Mail"]);
                aux.Mail = (lector["Usuarios_Direccion"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_Direccion"]);
                aux.IdLocalidad = (lector["Usuarios_IdLocalidad"] == DBNull.Value) ? -1 : Convert.ToInt32(lector["Usuarios_IdLocalidad"]);
                aux.Ocupacion = (lector["Usuarios_Ocupacion"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_Ocupacion"]);
                aux.Puntos = (lector["Usuarios_Puntos"] == DBNull.Value) ? -1 : Convert.ToInt32(lector["Usuarios_Puntos"]);
                aux.Telefono = (lector["Usuarios_Telefono"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_Telefono"]);
                aux.ApiKey = Convert.ToString(lector["Usuarios_Apikey"]);
                aux.FechaCreacion = (lector["Usuarios_FechaNacimiento"] == DBNull.Value) ? default(DateTime) : Convert.ToDateTime(lector["Usuarios_FechaNacimiento"]);
                aux.CBUAlias = (lector["Usuarios_CBUAlias"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_CBUAlias"]);
                aux.CUIT = (lector["Usuarios_CUIT"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_CUIT"]);
                aux.CBU = (lector["Usuarios_CBU"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_CBU"]);
                aux.Dni = (lector["Usuarios_DNI"] == DBNull.Value) ? "" : Convert.ToString(lector["Usuarios_DNI"]);
                aux.NombreLocalidad = (lector["Localidades_Nombre"] == DBNull.Value) ? "" : Convert.ToString(lector["Localidades_Nombre"]);

                aux.CantidadPrestamosExitosos = (lector["Usuarios_CantidadPrestamosExitosos"] == DBNull.Value) ? -1 : Convert.ToInt32(lector["Usuarios_CantidadPrestamosExitosos"]);
                aux.FechaCreacion = (lector["Usuarios_FechaCreación"] == DBNull.Value) ? default(DateTime) : Convert.ToDateTime(lector["Usuarios_FechaCreación"]);
                aux.ApiKey = Convert.ToString(lector["Usuarios_Apikey"]);
            }
            return aux;
        }

        public static bool esvalido(Usuario user, out string errores)
        {
            bool aux = true;
            errores = "";

            if (user != null)
            {
                if (!ValidacionesHelpers.esStringValido(user.Nombre))
                {
                    errores += "Por favor complete el nombre ";
                    aux = false;
                }
                if (!ValidacionesHelpers.esStringValido(user.Apellido))
                {
                    errores += "Por favor complete el apellido";
                    aux = false;
                }
                if (!ValidacionesHelpers.esStringValido(user.UserName))
                {
                    errores += "Por favor complete el Nombre de Usuario";
                    aux = false;
                }
                if (!ValidacionesHelpers.esStringValido(user.Dni))
                {
                    errores += "Por favor ingrese su Dni";
                    aux = false;
                }
                if (!ValidacionesHelpers.esMailValido(user.Mail))
                {
                    errores += "Por favor ingrese un mail válido";
                    aux = false;
                }
                if (user.FechaNacimiento != null)
                {
                    if (CalcularEdad(user) < 18)
                    {
                        errores += "Lo sentimos esta aplicacion no puede ser usada legalmente por menores de 18 años de edad";
                        aux = false;
                    }
                }
                else
                {
                    errores += "Por favor ingrese su fecha de nacimiento";
                    aux = false;
                }

                //FALTAN VALIDAR EL CUIT, CBU Y ALGUNOS CMAPOS MÁS.

            }
            else
            {
                errores += "El objeto está nulo ";
            }

            return aux;
        }
        public static int CalcularEdad(Usuario u)
        {
            DateTime Now = DateTime.Today;
            return Now.Year - u.FechaNacimiento.Year;
        }
        public static bool VerificarApiKey(string key)
        {

            int rowCount = 0;
            SqlParameter param = new SqlParameter("@ApiKey", key);
            SqlDataReader lector = BD.traerLector("Usuarios_verificarApiKey", param);

            try
            {
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        rowCount = Convert.ToInt32(lector["CantidadAfectadas"]);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);

            }
            BD.CloseAndDisposeReader(ref lector);
            return rowCount > 0;

        }
    


        #endregion
        #region Metodos GET

        public static int obtenerIdPorApiKey(string key)
        {
            SqlParameter param = new SqlParameter("@ApiKey", key);
            int id = -1;
            SqlDataReader lector = BD.traerLector("Usuarios_obtenerIdPorApiKey",param);
            try
            {
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        id = (lector["Id"] == DBNull.Value)? -1 :  Convert.ToInt32(lector["Id"]);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);

            }
            BD.CloseAndDisposeReader(ref lector);
            return id;
        }

        public static Usuario login(string email, string contra)
        {
            SqlParameter[] param ={ new SqlParameter("@mail", email), new SqlParameter("@contra", contra) };
            Usuario aux = new Usuario();
            SqlDataReader lector = BD.traerLector("Usuarios_login", param);
            try
            {
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        aux = ReaderToObject(lector,true);
                    }
                }
            }
            catch(Exception ex)
            {
                LogManager.LogException(ex);
            }
            BD.CloseAndDisposeReader(ref lector);
            return aux;
        }

        public static List<Usuario> traerTodos()
        {
            List<Usuario> lista = new List<Usuario>();
            Usuario aux;
            SqlDataReader lector = BD.traerLector("Usuarios_obtenerTodos");
            try
            {
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        aux = ReaderToObject(lector, false);
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
        public static Usuario traerPorId(int id)
        {
            Usuario aux = new Usuario();
           
                SqlParameter param = new SqlParameter("@intId", id);
                SqlDataReader lector = BD.traerLector("Usuario_ObtenerPorId", param);
            try
            {
                if (lector.HasRows)
                {
                    lector.Read();
                    aux = ReaderToObject(lector,false);
                }
                
                
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
            }
            BD.CloseAndDisposeReader(ref lector);
            return aux;

        }

        public static Usuario traerPorUserName(string userName)
        {
            Usuario aux = new Usuario();
           
                SqlParameter param = new SqlParameter("@UserName", userName);
                SqlDataReader lector = BD.traerLector("Usuario_ObtenerPorNombreUsuario", param);
            try
            {
                if (lector.HasRows)
                {
                    lector.Read();
                    aux = ReaderToObject(lector,false);
                }
                
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);

            }
            BD.CloseAndDisposeReader(ref lector);
            return aux;

        }
        public static List<Usuario> traerPorGenero(int id)
        {
                List<Usuario> lista = new List<Usuario>();
                Usuario aux;
                SqlParameter param = new SqlParameter("@Idgenero", id);
                SqlDataReader lector = BD.traerLector("Usuario_ObtenerPorIdGenero", param);
            try
            {
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        aux = ReaderToObject(lector,false);
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

        public static List<Usuario> traerPorLocalidad(int id)
        {
            List<Usuario> lista = new List<Usuario>();
            Usuario aux;
                SqlParameter param = new SqlParameter("@intIdLocalidad", id);
                SqlDataReader lector = BD.traerLector("Usuario_ObtenerPorIdLocalidad", param);
            try
            {
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        aux = ReaderToObject(lector,false);
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
        public static List<Usuario> traerPorPuntos(int id)
        {
            List<Usuario> lista = new List<Usuario>();
            Usuario aux;
                SqlParameter param = new SqlParameter("@puntos", id);
                SqlDataReader lector = BD.traerLector("Usuario_ObtenerPorPuntos", param);
            try
            {
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        aux = ReaderToObject(lector,false);
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
        #endregion
        #region Metodos NonQuery
        public static int eliminar(int id)
        {
            SqlParameter param = new SqlParameter("@idUsuario", id);
            return BD.ExecuteNonQuery("BorrarUsuario", param);
        }
        public static int Cambiar(Usuario user)
        {
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@idUsuario", user.Id),

                new SqlParameter("@Nombre", user.Nombre),
                new SqlParameter("@Apellido", user.Apellido),
                new SqlParameter("@Username", user.UserName),
                new SqlParameter("@password", user.Password),
                new SqlParameter("@Direccion", user.Direccion),
                new SqlParameter("@Telefono", user.Telefono),
                new SqlParameter("@Dni", user.Dni),
                new SqlParameter("@Mail", user.Mail),
                new SqlParameter("@CBU", user.CBU),
                new SqlParameter("@CBUAlias", user.CBUAlias),
                new SqlParameter("@Cuit", user.CUIT),
                new SqlParameter("@Puntos", user.Puntos),
                new SqlParameter("@Ocupacion", user.Ocupacion),
                new SqlParameter("@Descripcion", user.Descripcion),
                new SqlParameter("@UrlFoto", user.URLFoto),
                new SqlParameter("@IdGenero", user.IdGenero),
                new SqlParameter("@IdLocalidad", user.IdLocalidad),
                new SqlParameter("@FechaCreacion", user.FechaCreacion),
                new SqlParameter("@FechaNacimiento", user.FechaNacimiento),
                new SqlParameter("@CantPrestamosExitosos", user.CantidadPrestamosExitosos)
             };
            return BD.ExecuteNonQuery("Usuarios_Update", parametros.ToArray());
        }
        public static int insertar(Usuario user)
        {
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter("@Nombre", user.Nombre),
                new SqlParameter("@Apellido", user.Apellido),
                new SqlParameter("@Username", user.UserName),
                new SqlParameter("@password", user.Password),
                new SqlParameter("@Direccion", user.Direccion),
                new SqlParameter("@Telefono", user.Telefono),
                new SqlParameter("@Dni", user.Dni),
                new SqlParameter("@Mail", user.Mail),
                new SqlParameter("@CBU", user.CBU),
                new SqlParameter("@CBUAlias", user.CBUAlias),
                new SqlParameter("@Cuit", user.CUIT),
                new SqlParameter("@Puntos", user.Puntos),
                new SqlParameter("@Ocupacion", user.Ocupacion),
                new SqlParameter("@Descripcion", user.Descripcion),
                new SqlParameter("@UrlFoto", user.URLFoto),
                new SqlParameter("@IdGenero", user.IdGenero),
                new SqlParameter("@IdLocalidad", user.IdLocalidad),
                new SqlParameter("@FechaCreacion", user.FechaCreacion),
                new SqlParameter("@FechaNacimiento", user.FechaNacimiento),
                new SqlParameter("@CantPrestamosExitosos", user.CantidadPrestamosExitosos)

             };
            return Convert.ToInt32(BD.ExecuteScalar("InsertarUsuario_scalar", parametros.ToArray()));
        }
        public static int insertarValido(Usuario user, out string errores)
        {
            int usuarioNuevo = 0;
            if(esvalido(user, out errores))
            {
                usuarioNuevo = insertar(user);
            }
            return usuarioNuevo;
        }

        public static int CambiarContraseña(int id, string nuevaContra)
        {
            SqlParameter[] parametros = new SqlParameter[2]
            {
                new SqlParameter("@idUsuario", id),
                new SqlParameter("@password", nuevaContra)

             };
            return BD.ExecuteNonQuery("Usuario_cambiarContraseña", parametros);
        }

        #endregion
        
    }
}