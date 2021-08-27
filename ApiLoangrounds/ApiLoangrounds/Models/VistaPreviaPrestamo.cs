using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLoangrounds.Models
{
    public class VistaPreviaPrestamo
    {
        public int? IdDetallePrestamo  { get; set; }
        public string prestamista { get; set; }
        public double? Monto { get; set; }
        public string estado { get; set; }   
    }
}