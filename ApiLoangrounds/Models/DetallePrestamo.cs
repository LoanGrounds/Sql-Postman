using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLoangrounds.Models
{
    public class DetallePrestamo
    {
        public int Id { get; set; }
        public double Monto { get; set; }
        public int CantidadCuotas { get; set; }
        public double InteresXCuota { get; set; }
        public int DiasEntreCuotas { get; set; }
        public int DiasTolerancia { get; set; }
        public DateTime? FechaDeAcuerdo { get; set; }


        //FOREIGN KEYS:
        public int IdEstadoDePrestamo { get; set; }
    }
}