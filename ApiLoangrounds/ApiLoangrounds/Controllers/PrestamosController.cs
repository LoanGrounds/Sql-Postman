using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApiLoangrounds.Logica;
using ApiLoangrounds.Models;
namespace ApiLoangrounds.Controllers
{
    public class PrestamosController : ApiController
    {
        #region POR GET
        [Route("Prestamos/ver")]
        [HttpGet]
        public IHttpActionResult ObtenerTodos()
        {
            List<Prestamo> ListaParaDevolver = new List<Prestamo>(); ;
            ListaParaDevolver = PrestamosLogica.traerTodos();
            if (ListaParaDevolver != null)
            {
                return Ok(ListaParaDevolver);
            }

            return Ok("No pude traer la lista de la base de datos");
        }
        [Route("Detalles/ver")]
        [HttpGet]
        public IHttpActionResult ObtenerTodosDetalles()
        {
            List<DetallePrestamo> ListaParaDevolver = new List<DetallePrestamo>(); ;
            ListaParaDevolver = DetallesLogica.traerTodos();
            if (ListaParaDevolver != null)
            {
                return Ok(ListaParaDevolver);
            }

            return Ok("No pude traer la lista de la base de datos");
        }

        [Route("Detalles/obtenerPorId/{id}")]
        [HttpGet]
        public IHttpActionResult ObtenerDetallePorId(int id)
        {
            DetallePrestamo detalle = new DetallePrestamo();
            detalle = DetallesLogica.obtenerPorId(id);
            if (detalle != null)
            {
                return Ok(detalle);
            }

            return Ok("No pude traer la lista de la base de datos");
        }



        [Route("Prestamos/recomendados/{montomax}")]
        [HttpGet]
        public IHttpActionResult ObtenerRecomendados(int montomax)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header)){
                int Id = UsuariosLogica.obtenerIdPorApiKey(header);
                return Ok(PrestamosLogica.traerPorMonto(montomax, Id));
            }

           return Unauthorized();
        }


        [Route("Prestamos/misPrestamosPrestados")]
        [HttpGet]
        public IHttpActionResult ObtenerDeUsuarioPrestamista()
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                int id = UsuariosLogica.obtenerIdPorApiKey(header);
                return Ok (PrestamosLogica.traerPrestamosDeUnPrestamista(id));
            }
            return Unauthorized();
        }

        [Route("Prestamos/misPrestamosPedidos")]
        [HttpGet]
        public IHttpActionResult ObtenerDeUsuarioPrestador()
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                int id = UsuariosLogica.obtenerIdPorApiKey(header);
                return Ok (PrestamosLogica.traerPrestamosDelQuePide(id));
            }
            return Unauthorized();
        }

        [Route("Prestamos/BusquedaFiltrada")]
        [HttpGet]
        public IHttpActionResult busquedaFiltrada([FromBody] FiltroPrestamo filtro)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                int IdUsuario = UsuariosLogica.obtenerIdPorApiKey(header);
                return Ok (PrestamosLogica.BusquedaFiltrada(filtro.montoMax, filtro.maxInteres, filtro.minDiasT,
                    filtro.minDiasCutoas, filtro.minCantCuotas, IdUsuario));               
            }
            return Unauthorized();

        }
        #endregion

        #region NonQuery
        [HttpPost]
        [Route("Prestamos/nuevo")]

        public IHttpActionResult insertar(Prestamo p)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                ResponseDTO response = new ResponseDTO();
                string errores = "";
                response.Id = PrestamosLogica.insertarValido(p, out errores);
                if (response.Id > 0)
                {
                    response.mensaje = "se creo el prestamo con exito";
                    return Ok(response);
                }
                response.mensaje = "ups, algo salio mal" + errores;
                return Ok(response);
            }

            return Unauthorized();

            // SE DEBERIA CREAR PRIMERO EL DETALLE ANTES DE PODER CREAR UN PRESTAMO
        }

        [HttpPost]
        [Route("Prestamos/nuevoDetalle")]
        
        public IHttpActionResult insertarDetalle(DetallePrestamo detalle)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                ResponseDTO response = new ResponseDTO();
                string errores = "";
                response.Id = DetallesLogica.insertarValido(detalle, out errores);
                if (response.Id > 0)
                {
                    response.mensaje = "se creo el detalle con exito";
                    return Ok(response);
                }
                response.mensaje = "ups, algo salio mal" + errores;
                return Ok(response);
            }
            return Unauthorized();
            // SE DEBERIA CREAR PRIMERO EL DETALLE ANTES DE PODER CREAR UN PRESTAMO
        }

        [Route("Prestamos/borrar")]
        [HttpDelete]
        public IHttpActionResult borrar([FromBody]int id)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                ResponseDTO response = new ResponseDTO();
                response.Id = PrestamosLogica.eliminar(id);
                if (response.Id > 0)
                {
                    response.mensaje = "se borró el prestamo con éxito";
                    return Ok(response);
                }
                response.mensaje = "ups, algo salió mal";
                return Ok(response);
            }
            return Unauthorized();
        }


        [Route("Prestamos/update")]
        [HttpPut]
        public IHttpActionResult actualizar([FromBody] Prestamo p)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                ResponseDTO response = new ResponseDTO();
                
                    response.Id = PrestamosLogica.cambiar(p);
                    if (response.Id > 0)
                    {
                        response.mensaje = "Se cambió el prestamo con exito, el numero son la cantidad de columnas afectadas";
                        return Ok(response);
                    }
                response.mensaje = "algo salio mal, por favor vuelva a intentarlo";
                return Ok(response);
            }
            return Unauthorized();
        }

        [Route("Detalles/update")]
        [HttpPut]
        public IHttpActionResult actualizarDetalle([FromBody]DetallePrestamo d)
        {
            string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
            if (UsuariosLogica.VerificarApiKey(header))
            {
                ResponseDTO response = new ResponseDTO();
             
                    response.Id = DetallesLogica.Cambiar(d);
                    if (response.Id > 0)
                    {
                        response.mensaje = "Se cambió el detalle con exito, el numero son la cantidad de columnas afectadas";
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
  


