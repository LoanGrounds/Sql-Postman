using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLoangrounds.Models
{
    public class BusquedaFiltradaDTO
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public double? Monto { get; set; }
        public int? CantidadCuotas { get; set; }
        public double? InteresXCuota { get; set; }
        public int? DiasEntreCuotas { get; set; }
        public int? DiasTolerancia { get; set; }
    }
}