using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLoangrounds.Models
{
    public class Cuota
    {
        public int? IdDetalle { get; set; }
        public int? NroCuota { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int? IdEstadoCouta { get; set; }
        public double? Monto { get; set; }

    }
}