using ApiLoangrounds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using ApiLoangrounds.Logica;

/*
 GET -> PARA SELECT
 POST -> PARA INSERT
 PUT -> PARA UPDATE
 DELETE -> PARA DELETE
 */

namespace ApiLoangrounds.Controllers
{
    public class VariosController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [Route("Varios/generos/ver")]
        [HttpGet]
        public IHttpActionResult mostrarGeneros()
        {
            return Ok(VariosLogica.traerGeneros());
        }

        [Route("Varios/provincias/ver")]
        [HttpGet]
        public IHttpActionResult MostrarProvincias()
        {
            return Ok(VariosLogica.obtenerTodasLasProvincias());
        }

        [Route("Varios/Localidades/ver/{id}")]
        [HttpGet]
        public IHttpActionResult MostrarLocalidadesDeUnDepto(int id)
        {
            return  Ok(VariosLogica.obtenerLocalidadesDeUnDepto(id));
        }

       
        [Route("Varios/FAQS/ver")]
        [HttpGet]
        public IHttpActionResult TraerFaqs()
        {
            return Ok(VariosLogica.obtenerFaqs());
        }

        [Route("Varios/EstadosDeUnprestamo/ver")]
        [HttpGet]
        public IHttpActionResult TraerEstados()
        {
            return Ok(VariosLogica.traerEstadosPosibles());
        }
    }
}
