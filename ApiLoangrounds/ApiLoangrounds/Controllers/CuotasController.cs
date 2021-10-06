using ApiLoangrounds.Logica;
using ApiLoangrounds.Models;
using System.Linq;
using System.Web.Http;

namespace ApiLoangrounds.Controllers
{
    public class CuotasController : ApiController
    {
        #region GET
        [Route("Cuotas/ver/{id}")]
        [HttpGet]
        public IHttpActionResult getCoutas(int id)
        {
            try
            {
                string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
                if (UsuariosLogica.VerificarApiKey(header))
                {
                    return Ok(CuotasLogica.obtenerCoutasPorPrestamo(id));
                }
            }
            catch { return Unauthorized(); }
            return Unauthorized();
        }

        [Route("Cuotas/ver/{id}/{nro}")]
        [HttpGet]
        public IHttpActionResult getCoutas(int id, int nro)
        {
            try
            {
                string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
                if (UsuariosLogica.VerificarApiKey(header))
                {
                    return Ok(CuotasLogica.obtenerPorId(id, nro));
                }
            }
            catch { return Unauthorized(); }
            return Unauthorized();
        }
        #endregion
        #region NonQuery

        [Route("Cuotas/crear/{IdDetalle}")]
        [HttpPost]
        public IHttpActionResult insertCoutas(int IdDetalle)
        {
            try
            {
                string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
                if (UsuariosLogica.VerificarApiKey(header))
                {
                    DetallePrestamo detalle = DetallesLogica.obtenerPorId(IdDetalle);
                    if (CuotasLogica.insertarCuotasDeUnPrestamo(detalle)) return Ok();
                    return BadRequest();
                }
            }
            catch { return Unauthorized(); }
            return Unauthorized();
        }


        [Route("Cuotas/update")]
        [HttpPost]
        public IHttpActionResult updateCoutas(Cuota c)
        {
            try
            {
                string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
                if (UsuariosLogica.VerificarApiKey(header)) { 
                    return Ok(CuotasLogica.Cambiar(c));
                }
            }
            catch { return Unauthorized(); }
            return Unauthorized();
        }

        [Route("Cuotas/borrar/{id}")]
        [HttpPost]
        public IHttpActionResult deleteCoutas(int id)
        {
            try
            {
                string header = Request.Headers.GetValues("ApiKey").FirstOrDefault();
                if (UsuariosLogica.VerificarApiKey(header))
                {
                    return Ok(CuotasLogica.eliminar(id));
                }
            }
            catch { return Unauthorized(); }
            return Unauthorized();
        }
        #endregion
    }
}