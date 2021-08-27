using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLoangrounds.Models
{
    public class EstadoDePrestamo
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Comentarios { get; set; }

    }
}