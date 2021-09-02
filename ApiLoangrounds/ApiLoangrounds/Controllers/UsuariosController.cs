using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiLoangrounds.Logica;
using ApiLoangrounds.Models;

namespace ApiLoangrounds.Controllers
{
    public class UsuariosController : ApiController
    {
        [Route("Usuarios/login")]
        [HttpPost]
        public IHttpActionResult login(Usuario aux)
        {
            Usuario user = UsuariosLogica.login(aux.Mail, aux.Password);
            if (user.Id>0)
            {
                return Ok(user);
            }
            return BadRequest("No encontre al Usuario");


        }

        #region POR GET

        [Route("Usuarios/ver")]
        [HttpGet]
        public IHttpActionResult TraerTodos()
        {
            List<Usuario> lista = UsuariosLogica.traerTodos();
            if(lista != null)
            {
                return Ok(lista);
            }
            return Ok("no se pudo leer de la base de datos");


        }


        [Route("Usuarios/traerPorid/{id}")]
        [HttpGet]
        public IHttpActionResult TraerUsuario(int id)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
              
                return Ok(UsuariosLogica.traerPorId(id));
            }
            return Unauthorized();
               
        }

        [Route("Usuarios/traerporUserName/{userName}")]
        [HttpGet]
        public IHttpActionResult TraerUsuario(string userName)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                return Ok(UsuariosLogica.traerPorUserName(userName));
            }
            return Unauthorized();
        }


        [Route("Usuarios/traerPorPuntos/{puntos}")]
        [HttpGet]
        public IHttpActionResult TraerPorPuntos(int puntos)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                return Ok(UsuariosLogica.traerPorPuntos(puntos));
            }
            return Unauthorized();
        }

        [Route("Usuarios/traerPorLocalidad/{idLocalidad}")]
        [HttpGet]
        public IHttpActionResult TraerPorLocalidad(int idLocalidad)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                return Ok(UsuariosLogica.traerPorLocalidad(idLocalidad));
            }
            return Unauthorized();
        }

        [Route("Usuarios/traerPorGenero/{idGenero}")]
        [HttpGet]
        public IHttpActionResult TraerPorGenero(int idGenero)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                return Ok(UsuariosLogica.traerPorGenero(idGenero));
            }
            return Unauthorized();
        }


        #endregion
        #region NonQuery

        [Route("Usuarios/signup")]
        [HttpPost]

        public IHttpActionResult insertar(Usuario user)
        {
                string errores = "";
                ResponseDTO response = new ResponseDTO();
                response.Id = UsuariosLogica.insertarValido(user, out errores);
                if (response.Id > 0)
                {
                    response.mensaje = "se creo el usuario con éxito";
                    return Ok(response);
                }            
                response.mensaje = errores;               
               return Ok(response);             
        }


        [Route("Usuarios/borrar")]
        [HttpDelete]
        public IHttpActionResult borrar([FromBody]int id)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                ResponseDTO response = new ResponseDTO();
                response.Id = UsuariosLogica.eliminar(id);
                if (response.Id > 0)
                {
                    response.mensaje = "Se borró de la base con éxito, el numero son la cantidad de registros afectados";
                    return Ok(response);
                }               
                response.mensaje = "ups algo salió mal";
                return Ok(response);

            }
            return Unauthorized();
        }
        [Route("Usuarios/update")]
        [HttpPut]
        public IHttpActionResult update(Usuario user)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                ResponseDTO response = new ResponseDTO();
                response.Id = UsuariosLogica.Cambiar(user);
                if (response.Id > 0)
                {
                    response.mensaje = "Se cambió el usuario con exito, el numero son la cantidad de columnas afectadas";
                    return Ok(response);
                }             
                response.mensaje = "algo salio mal, por favor vuelva a intentarlo";
                return Ok(response);
            }
            return Unauthorized();
        }


        [Route("Usuarios/update/password")]
        [HttpPut]
        public IHttpActionResult updatePassword([FromBody]string pass)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                int id = UsuariosLogica.obtenerIdPorApiKey(header);
                ResponseDTO response = new ResponseDTO();
                response.Id = UsuariosLogica.CambiarContraseña(id, pass);
                if (response.Id > 0)
                {
                    response.mensaje = "Se cambió la contraseña con exito";
                    return Ok(response);
                }             
                response.mensaje = "algo salio mal, por favor vuelva a intentarlo";
                return Ok(response);
            }
            return Unauthorized();
        }

        #endregion
    }
}
